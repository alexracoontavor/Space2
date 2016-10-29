using System;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.Architecture.Modulux.State;
using Assets.Infrastructure.Modules.HGS.HGS_Login_Module.API;
using Assets.Infrastructure.Modules.HGS.HGS_Login_Module.Reducers;
using Assets.Infrastructure.Modules.HGS.HGS_Login_Module.State;

namespace Assets.Infrastructure.Modules.HGS.HGS_Login_Module
{
    public class HGS_Login_Module : ModuleBase
    {
        private static HGS_Login_Receiver _receiver;

        public override BaseState GetInitialState(int[] path, params Type[] subModules)
        {
            if (_receiver == null)
                _receiver = new HGS_Login_Receiver();

            _path = path;
            _rootReducer = HGS_Login_Reducer.Reduce;

            return new HGS_Login_State();
        }
    }
}