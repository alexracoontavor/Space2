using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Infrastructure.Architecture.IDI.StrangeIoC;
using Assets.Infrastructure.Architecture.Logger.SimpleLog;
using Assets.Infrastructure.Architecture.Modulux.State;
using Assets.Infrastructure.Architecture.Redux;
using Assets.Infrastructure.Architecture.Redux.Middlewares;
using Assets.Infrastructure.Architecture.Server.WebSockets;
using Assets.Infrastructure.CoreTools.Extensions;
using Assets.Infrastructure.CoreTools.Instanced.JsonFormatter;
using UniRx;
using UnityEngine;
using ILogger = Assets.Infrastructure.Architecture.Logger.ILogger;

namespace Assets.Infrastructure.Architecture.Modulux
{
    /// <summary>
    /// Interface object for Modulux
    /// </summary>
    public static class ModuluxRoot
    {
        public static IDI.IDI IoC = new StrangeIoCWrapper();
        /// <summary>
        /// The system's Store
        /// </summary>
        public static Store<BaseState> Store;
        /// <summary>
        /// System-wide logger
        /// </summary>
        public static ILogger Logger = new SimpleLogger();
        /// <summary>
        /// Saved reference to all modules loaded by Modulux
        /// </summary>
        private static readonly List<ModuleBase> _modules = new List<ModuleBase>();
        /// <summary>
        /// Paths to all loaded modules
        /// </summary>
        private static readonly Dictionary<Type, int[]> _paths = new Dictionary<Type, int[]>();

        private static void Initialize()
        {
            IoC.Bind<IWsComm, WsComm>();
            IoC.Bind<IJsonFormatter, JsonFormatter>();
            IoC.Bind<ILogger, SimpleLogger>();
        }

        /// <summary>
        /// Initiales Modulux with the given modules and connector map
        /// </summary>
        /// <param name="modulesClasses">Types of modules that should be loaded</param>
        /// <param name="connectorMap">Map for creating connections between modules.
        /// The keys are types of actions (types that implement the IAction interface)
        /// The values are the functions to call when the key action passes through the system
        /// See ActionsConnectorMiddleware for usage</param>
        public static void Initialize(Type[] modulesClasses, Dictionary<Type, Action<IAction>> connectorMap)
        {
            Initialize();

            _modules.Clear();
            _paths.Clear();

            var sm = RegisterModules(new int[] { }, modulesClasses);

            Store = new Store<BaseState>(Reduce, new BaseState(sm.Modules, sm.Reducers), new LoggerMiddleware(Logger).Middleware,
                new ActionsConnectorMiddleware(connectorMap).Middleware);
        }

        /// <summary>
        /// Finds and returns a reference to Module instance
        /// </summary>
        /// <typeparam name="T">Type of module to find</typeparam>
        /// <returns>Module reference if found, null if not</returns>
        public static ModuleBase GetModule<T>()
        {
            return _modules.FirstOrDefault(s => s is T);
        }
        /// <summary>
        /// Retreives a stream that emits when state with given type updates
        /// </summary>
        /// <typeparam name="T">Type of state to listen to</typeparam>
        /// <returns>Observable stream of given state's updates</returns>
        public static IObservable<T> GetStateStream<T>() where T : BaseState
        {
            int[] pathToState = GetPathTo(typeof(T));

            return Store
                .ObserveOnMainThread()
                .Select(s => ModuleHelper.ExtractState<T>(s, pathToState) as T)
                .Where(s => s != null);
        }

        /// <summary>
        /// Retrieves an instance of a state with the given type
        /// </summary>
        /// <typeparam name="T">state type</typeparam>
        /// <returns>found state or null if not found</returns>
        public static T GetState<T>() where T : BaseState
        {
            return Store
                .GetState()
                .Modules
                .Select(s => ModuleHelper.ExtractState<T>(s, GetPathTo(typeof(T))) as T)
                .Where(s => s != null) as T;
        }

        //PRIVATE
        /// <summary>
        /// Adds state type to paths dictionary
        /// </summary>
        /// <param name="t">type to register</param>
        /// <param name="path">path to this state</param>
        private static void RegisterPath(Type t, int[] path)
        {
            _paths.Add(t, path);
        }
        /// <summary>
        /// Root Modulux reducer - calles the reducers of its submodules
        /// </summary>
        /// <param name="prevState">BaseState</param>
        /// <param name="action">IAction</param>
        /// <returns>BaseState</returns>
        private static BaseState Reduce(BaseState prevState, IAction action)
        {
            var modules = new BaseState[prevState.Modules.Length];

            for (var i = 0; i < modules.Length; i++)
            {
                modules[i] = prevState.Reducers[i](prevState.Modules[i], action);
            }

            return new BaseState(modules, prevState.Reducers);
        }
        /// <summary>
        /// Generic reducer for generating new BaseState.Modules
        /// </summary>
        /// <param name="prevState">BaseState</param>
        /// <param name="action">IAction</param>
        /// <returns>BaseState</returns>
        private static BaseState[] ReduceModules(BaseState prevState, IAction action)
        {
            if (prevState.Modules == null)
                return prevState.Modules;

            var modules = new BaseState[prevState.Modules.Length];

            for (var i = 0; i < modules.Length; i++)
            {
                modules[i] = prevState.Reducers[i](prevState.Modules[i], action);
            }

            return modules;
        }
        /// <summary>
        /// Gets path to state by type
        /// </summary>
        /// <param name="t">state type</param>
        /// <returns>path array</returns>
        private static int[] GetPathTo(Type t)
        {
            return (int[])_paths.GetValueOrNull(t);
        }
        /// <summary>
        /// Modles registration in the system. Adds to path
        /// </summary>
        /// <param name="path">root path for these modules</param>
        /// <param name="modules">module types to register</param>
        /// <returns>Modules and Reducers array, ready to initialize a BaseState with</returns>
        private static AddModulesReply RegisterModules(int[] path, Type[] modules)
        {
            var replies = new AddModuleReply[modules.Length];
            var i = 0;

            foreach (var module in modules)
            {
                replies[i] = AddModuleToPath(path, module);
                i++;
            }

            return new AddModulesReply() { Modules = replies.Select(r => r.Module).ToArray(), Reducers = replies.Select(r => r.Reducer).ToArray() };
        }
        /// <summary>
        /// Registration of a single module
        /// </summary>
        /// <param name="path">root path</param>
        /// <param name="module">module path</param>
        /// <returns>Module and Reducer for BaseState of given type</returns>
        private static AddModuleReply AddModuleToPath(int[] path, Type module)
        {
            var m = (ModuleBase)Activator.CreateInstance(module);
            _modules.Add(m);
            m.InitInjections();

            var state = m.GetInitialState(path.Concat(new[] { _paths.Count }).ToArray());
            RegisterPath(state.GetType(), m.Path);
            return new AddModuleReply() { Module = state, Reducer = m.RootReducer };
        }
        /// <summary>
        /// wrapper object for RegisterModules reply
        /// </summary>
        private struct AddModulesReply
        {
            public BaseState[] Modules;
            public Func<BaseState, IAction, BaseState>[] Reducers;
        }
        /// <summary>
        /// wrapper object for AddModuleToPath reply
        /// </summary>
        private struct AddModuleReply
        {
            public BaseState Module;
            public Func<BaseState, IAction, BaseState> Reducer;
        }
    }
}