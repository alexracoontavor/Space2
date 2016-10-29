using System;
using Assets.Infrastructure.Architecture.Modulux.State;
using Assets.Infrastructure.Architecture.Redux;

namespace Assets.Infrastructure.Modules.HGS.HGS_Login_Module.State
{
    public class HGS_Login_State : BaseState
    {
        public bool IsLoggingIn;
        public bool IsLoggedIn;

        public HGS_Login_State(BaseState[] modules = null, Func<BaseState, IAction, BaseState>[] reducers = null) : base(modules, reducers)
        {
        }
    }
}