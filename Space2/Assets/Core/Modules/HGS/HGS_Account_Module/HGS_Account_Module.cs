using System;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.Architecture.Modulux.State;
using Assets.Infrastructure.Modules.HGS.HGS_Account_Module.API;
using Assets.Infrastructure.Modules.HGS.HGS_Account_Module.Reducers;
using Assets.Infrastructure.Modules.HGS.HGS_Account_Module.State;

namespace Assets.Infrastructure.Modules.HGS.HGS_Account_Module
{
    public class HGS_Account_Module : ModuleBase
    {
        private HGS_Account_Receiver _receiver;

        public override BaseState GetInitialState(int[] path, params Type[] subModules)
        {
            if (_receiver == null)
                _receiver = new HGS_Account_Receiver();

            _path = path;
            _rootReducer = HGS_Account_RootReducer.Reduce;
            
            return new HGS_Account_State();
        }
    }
}