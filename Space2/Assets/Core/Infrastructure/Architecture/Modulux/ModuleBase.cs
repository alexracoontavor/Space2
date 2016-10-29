using System;
using Assets.Infrastructure.Architecture.IDI.StrangeIoC;
using Assets.Infrastructure.Architecture.Logger;
using Assets.Infrastructure.Architecture.Logger.SimpleLog;
using Assets.Infrastructure.Architecture.Modulux.State;
using Assets.Infrastructure.Architecture.Redux;
using Assets.Infrastructure.Architecture.Server.WebSockets;
using Assets.Infrastructure.CoreTools.Instanced.FileSystemAccessor;
using Assets.Infrastructure.CoreTools.Instanced.JsonFormatter;

namespace Assets.Infrastructure.Architecture.Modulux
{
    /// <summary>
    /// Base class for modules
    /// </summary>
	public abstract class ModuleBase : IModule, IDI.IDI
    {
		public IDI.IDI IoC;
        public ILogger Logger;

        protected int[] _path;
        public int[] Path { get { return _path; } }

        protected Func<BaseState, IAction, BaseState> _rootReducer;
        public Func<BaseState, IAction, BaseState> RootReducer { get { return _rootReducer; }}

        public abstract BaseState GetInitialState(int[] path, params Type[] subModules);

        /// <summary>
        /// Initializes the IoC and Logger for this Module
        /// </summary>
        protected ModuleBase()
        {
            IoC = new StrangeIoCWrapper();
            IoC.Bind<ILogger, SimpleLogger>();
            Logger = IoC.Get<ILogger>();
        }

        public virtual void InitInjections()
        {
            #region tools
            IoC.Bind<IFileSystemAccessor, FileAccessWrapper>();
            IoC.Bind<IJsonFormatter, JsonFormatter>();
            #endregion
        }

        public void Bind<TFrom, TTo>(bool isSingleton = true, string filter = null)
        {
            IoC.Bind(isSingleton, filter);
        }

        public void Bind(object @from, object to, string filter = null)
        {
            IoC.Bind(from, to, filter);
        }

        public T Get<T>(string filter = null)
        {
            return IoC.Get<T>(filter);
        }
    }
}