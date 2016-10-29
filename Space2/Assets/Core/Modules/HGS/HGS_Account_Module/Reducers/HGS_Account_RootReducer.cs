using Assets.Infrastructure.Architecture.Modulux.State;
using Assets.Infrastructure.Architecture.Redux;
using Assets.Infrastructure.Modules.HGS.HGS_Account_Module.Actions;
using Assets.Infrastructure.Modules.HGS.HGS_Account_Module.State;

namespace Assets.Infrastructure.Modules.HGS.HGS_Account_Module.Reducers
{
    public class HGS_Account_RootReducer
    {
        public static BaseState Reduce(BaseState prevState, IAction action)
        {
            var a = action as MoneyUpdateAction;

            return a == null ? prevState : new HGS_Account_State(new[] { new Currency() { Amount = a.Amount, Type = a.Type } });
        }
    }
}