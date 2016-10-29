using System;
using Assets.Infrastructure.Architecture.Redux;

namespace Assets.Infrastructure.Architecture.Modulux.State
{
    /// <summary>
    /// Base class for all Modulux root states
    /// </summary>
    [Serializable]
    public class BaseState
    {
        public readonly BaseState[] Modules;
        public readonly Func<BaseState, IAction, BaseState>[] Reducers;

        /// <summary>
        /// Base state constructor
        /// </summary>
        /// <param name="modules">Array of sub-module states</param>
        /// <param name="reducers">Array of sub-module reducers. Must match modules array</param>
        public BaseState(
            BaseState[] modules,
            Func<BaseState, IAction, BaseState>[] reducers)
        {
            Modules = modules;
            Reducers = reducers;
        }
    }
}