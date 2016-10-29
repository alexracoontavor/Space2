using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.Architecture.Modulux.State;
using Assets.Infrastructure.Architecture.Redux;
using Assets.Infrastructure.Modules.HGS.HGS_BJ_Module.Actions;
using Assets.Infrastructure.Modules.HGS.HGS_BJ_Module.API;
using Assets.Infrastructure.Modules.HGS.HGS_BJ_Module.State;

namespace Assets.Infrastructure.Modules.HGS.HGS_BJ_Module.Reducers
{
    public class HGS_BJ_RootReducer
    {
        public static BaseState Reduce(BaseState prevState, IAction action)
        {
            var s = prevState as HGS_BJ_State;

            if (s == null)
                return prevState;

            return new HGS_BJ_State
            {
                isReadyToStart = ReduceIsReadyToStart(s, action),
                playerCards = ReducePlayerCards(s, action),
                dealerVisibleCards = ReducerDealerCards(s, action),
                betState = ReduceBetState(s, action)
            };
        }

        private static HGS_Bet_State ReduceBetState(HGS_BJ_State prevState, IAction action)
        {
            var a = action as BjBetStateUpdate;
            return a == null ? prevState.betState : new HGS_Bet_State() {isBetConfirmed = a.isBetConfirmed, isWaitingForConfirmation = a.isWaitingForBet};
        }

        private static HGS_BJ_API.CardData[] ReducerDealerCards(HGS_BJ_State prevState, IAction action)
        {
            var a = action as DealerCardsDealt;
            return a == null ? prevState.dealerVisibleCards : a.dealtCards;
        }

        private static HGS_BJ_API.CardData[] ReducePlayerCards(HGS_BJ_State prevState, IAction action)
        {
            var a = action as PlayerCardsDealt;
            return a == null ? prevState.playerCards : a.dealtCards;
        }

        private static bool ReduceIsReadyToStart(HGS_BJ_State prevState, IAction action)
        {
            if (action is BjGameInitialized)
                return true;

            if (action is BjGameFinished)
                return false;

            return prevState.isReadyToStart;
        }
    }
}