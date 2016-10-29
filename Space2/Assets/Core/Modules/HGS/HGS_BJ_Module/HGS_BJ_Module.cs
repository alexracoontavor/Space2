using System;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.Architecture.Modulux.State;
using Assets.Infrastructure.Modules.HGS.HGS_BJ_Module.API;
using Assets.Infrastructure.Modules.HGS.HGS_BJ_Module.Reducers;
using Assets.Infrastructure.Modules.HGS.HGS_BJ_Module.State;

namespace Assets.Infrastructure.Modules.HGS.HGS_BJ_Module
{
    public class HGS_BJ_Module : ModuleBase
    {
        private static HGS_BJ_Receiver _receiver;

        public override BaseState GetInitialState(int[] path, params Type[] subModules)
        {
            if (_receiver == null)
                _receiver = new HGS_BJ_Receiver();

            _path = path;
            _rootReducer = HGS_BJ_RootReducer.Reduce;

            return new HGS_BJ_State();
        }
    }
}