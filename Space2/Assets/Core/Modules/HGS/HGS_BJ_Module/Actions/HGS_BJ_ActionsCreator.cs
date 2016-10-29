using Assets.Infrastructure.Architecture;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.Architecture.Redux;
using Assets.Infrastructure.Architecture.Server.WebSockets;
using Assets.Infrastructure.CoreTools.Instanced.JsonFormatter;
using Assets.Infrastructure.Modules.HGS.HGS_BJ_Module.API;
using Assets.Infrastructure.Modules.HGS.HGS_Module.BaseClasses;

namespace Assets.Infrastructure.Modules.HGS.HGS_BJ_Module.Actions
{
    public class HGS_BJ_ActionsCreator
    {
        public static void ProcessServerResponse(IResponseData response)
        {
            ModuluxRoot.Logger.Log("HGS_BJ_ActionsCreator processing server response " + response);
            if (response is HGS_BJ_API.InitGameBJResponse)
            {
                ModuluxRoot.Store.Dispatch(new BjGameInitialized());
            }
            else if (response is HGS_BJ_API.DealCardsToPlayerIncoming)
            {
                var r = response as HGS_BJ_API.DealCardsToPlayerIncoming;
                ModuluxRoot.Store.Dispatch(new PlayerCardsDealt() { dealtCards = r.data.dealtCards });
            }
            else if (response is HGS_BJ_API.DealCardsToCroupierIncoming)
            {
                var r = response as HGS_BJ_API.DealCardsToCroupierIncoming;
                ModuluxRoot.Store.Dispatch(new DealerCardsDealt() { dealtCards = r.data.dealtCards });
            }
            else if (response is HGS_BJ_API.BetReplyIncoming)
            {
                var r = response as HGS_BJ_API.BetReplyIncoming;
                ModuluxRoot.Store.Dispatch(new BjBetStateUpdate() { isWaitingForBet = false, isBetConfirmed = r.data.betResponse == "ACCEPTED" });
            }
            else if (response is HGS_BJ_API.FinishGameServer)
            {
                var r = response as HGS_BJ_API.FinishGameServer;
                ModuluxRoot.Store.Dispatch(new BjGameFinished());
            }
        }

        public static void InitGameBJ()
        {
            var call = new HGS_BJ_API.InitGameBJ();
            ModuluxRoot.IoC.Get<IWsComm>().Send(ModuluxRoot.IoC.Get<IJsonFormatter>().ToJsonString(call));
        }

        public static void StartMatchBJ(int bet)
        {
            ModuluxRoot.Store.Dispatch(new BjBetStateUpdate() { isWaitingForBet = true, isBetConfirmed = false});

            var call = new HGS_BJ_API.StartMatchBJ(new HGS_BJ_API.StartMatchBjData() { betAmount = bet });
            var callString = ModuluxRoot.IoC.Get<IJsonFormatter>().ToJsonString(call);
            ModuluxRoot.IoC.Get<IWsComm>().Send(callString);
        }

        public static void HitAction()
        {
            var call = new HGS_BJ_API.PlayerHitAction();
            var callString = ModuluxRoot.IoC.Get<IJsonFormatter>().ToJsonString(call);
            ModuluxRoot.IoC.Get<IWsComm>().Send(callString);
        }

        public static void StandAction()
        {
            var call = new HGS_BJ_API.PlayerStandAction();
            var callString = ModuluxRoot.IoC.Get<IJsonFormatter>().ToJsonString(call);
            ModuluxRoot.IoC.Get<IWsComm>().Send(callString);
        }

        public static void SurrenderAction()
        {
            var call = new HGS_BJ_API.PlayerSurrenderAction();
            var callString = ModuluxRoot.IoC.Get<IJsonFormatter>().ToJsonString(call);
            ModuluxRoot.IoC.Get<IWsComm>().Send(callString);
        }
    }

    public class BjGameInitialized : IAction { }
    public class BjGameFinished : IAction { }

    public class PlayerCardsDealt : IAction
    {
        public HGS_BJ_API.CardData[] dealtCards;
    }

    public class DealerCardsDealt : IAction
    {
        public HGS_BJ_API.CardData[] dealtCards;
    }

    public class BjBetStateUpdate : IAction
    {
        public bool isWaitingForBet;
        public bool isBetConfirmed;
    }
}