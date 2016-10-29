using Assets.Infrastructure.Architecture;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.Architecture.Redux;
using Assets.Infrastructure.Modules.HGS.HGS_Account_Module.API;
using Assets.Infrastructure.Modules.HGS.HGS_Module.BaseClasses;

namespace Assets.Infrastructure.Modules.HGS.HGS_Account_Module.Actions
{
    public class HGS_Account_ActionsCreator
    {
        public static void ProcessServerResponse(IResponseData response)
        {
            ModuluxRoot.Logger.Log("HGS_Account_ActionsCreator processing server response " + response);
            if (response is HGS_Account_API.MoneyUpdateResponse)
            {
                ModuluxRoot.Logger.Log("Dispatching Money update");

                var r = response as HGS_Account_API.MoneyUpdateResponse;
                ModuluxRoot.Store.Dispatch(new MoneyUpdateAction(r.data.type, r.data.amount));
            }
        }
    }

    public class MoneyUpdateAction : IAction
    {
        public string Type;
        public long Amount;

        public MoneyUpdateAction(string type, long amount)
        {
            this.Type = type;
            this.Amount = amount;
        }
    }
}