using System;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.Architecture.Modulux.State;
using Assets.Scripts.Space2Module.Redux.Reducers;
using Assets.Scripts.Space2Module.Redux.State;

namespace Assets.Scripts.Space2Module
{
    public class Space2Module : ModuleBase
    {
        public override BaseState GetInitialState(int[] path, params Type[] subModules)
        {
            _path = path;
            _rootReducer = Stage2StateReducer.Reduce;
            return new Space2State();
        }
    }
}