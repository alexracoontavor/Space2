using System;
using Assets.Infrastructure.Architecture.Modulux.State;
using Assets.Infrastructure.Architecture.Redux;
using Assets.Infrastructure.Modules.HGS.HGS_BJ_Module.API;

namespace Assets.Infrastructure.Modules.HGS.HGS_BJ_Module.State
{
    public class HGS_BJ_State : BaseState
    {
        public bool isReadyToStart;
        public HGS_Bet_State betState;
        public HGS_BJ_API.CardData[] playerCards;
        public HGS_BJ_API.CardData[] dealerVisibleCards;

        public HGS_BJ_State(BaseState[] modules = null, Func<BaseState, IAction, BaseState>[] reducers = null) : base(modules, reducers)
        {
        }
    }

    public class HGS_Bet_State
    {
        public bool isWaitingForConfirmation;
        public bool isBetConfirmed;
    }
}