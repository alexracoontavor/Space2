using System;
using Assets.Infrastructure.Architecture.Modulux.State;
using Assets.Infrastructure.Architecture.Redux;

/// <summary>
/// Modular Redux architecture
/// </summary>
namespace Assets.Infrastructure.Architecture.Modulux
{
    /// <summary>
    /// Interface of a module
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Path to this module's state in Modulux hierarchy. Set and used by ModuluxRoot
        /// </summary>
        int[] Path { get; }
        /// <summary>
        /// Reference to this module's root reducer function
        /// </summary>
        Func<BaseState, IAction, BaseState> RootReducer { get; }
        /// <summary>
        /// Initialization function. Use this to initialize the module
        /// </summary>
        /// <param name="path">Path to module in parent state heirarchy</param>
        /// <param name="subModules">Submodules to initialize</param>
        /// <returns></returns>
        BaseState GetInitialState(int[] path, params Type[] subModules);
    }
}