using Assets.Infrastructure.Architecture.Modulux.State;
using Assets.Infrastructure.Architecture.Redux;
using Assets.Infrastructure.Modules.HGS.HGS_Login_Module.Actions;
using Assets.Infrastructure.Modules.HGS.HGS_Login_Module.State;

namespace Assets.Infrastructure.Modules.HGS.HGS_Login_Module.Reducers
{
    public class HGS_Login_Reducer
    {
        public static BaseState Reduce(BaseState prevState, IAction action)
        {
            if (action is LogoutAction)
            {
                return new HGS_Login_State() { IsLoggedIn = false, IsLoggingIn = false };
            }
            if (action is LoginStartAction)
            {
                return new HGS_Login_State() { IsLoggedIn = false, IsLoggingIn = true };
            }
            if (action is LoginSucceedAction)
            {
                return new HGS_Login_State() { IsLoggedIn = true, IsLoggingIn = false };
            }
            return prevState;
        }
    }
}