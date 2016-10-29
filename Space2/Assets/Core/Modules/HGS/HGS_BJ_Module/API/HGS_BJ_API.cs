using System;
using Assets.Infrastructure.Modules.HGS.HGS_Module.BaseClasses;

namespace Assets.Infrastructure.Modules.HGS.HGS_BJ_Module.API
{
    public class HGS_BJ_API
    {
        //OUT
        [Serializable]
        public class InitGameBJ
        {
            public InitGameBJ(string to = "BlackjackService", string type = "com.akamon.blackjack.protocol.client.InitGameRequest")
            {
                this.to = to;
                this.type = type;
            }
            public string type;
            public string to;
            public EmptyData data = new EmptyData();
        }

        [Serializable]
        public class PlayerHitAction
        {
            public PlayerHitAction(string to = "BlackjackService", string type = "com.akamon.blackjack.protocol.client.PlayerHit")
            {
                this.to = to;
                this.type = type;
            }
            public string type;
            public string to;
            public EmptyData data = new EmptyData();
        }

        [Serializable]
        public class PlayerStandAction
        {
            public PlayerStandAction(string to = "BlackjackService", string type = "com.akamon.blackjack.protocol.client.PlayerStand")
            {
                this.to = to;
                this.type = type;
            }
            public string type;
            public string to;
            public EmptyData data = new EmptyData();
        }

        [Serializable]
        public class PlayerSurrenderAction
        {
            public PlayerSurrenderAction(string to = "BlackjackService", string type = "com.akamon.blackjack.protocol.client.PlayerSurrender")
            {
                this.to = to;
                this.type = type;
            }
            public string type;
            public string to;
            public EmptyData data = new EmptyData();
        }

        [Serializable]
        public class StartMatchBJ
        {
            public StartMatchBJ(StartMatchBjData data, string to = "BlackjackService", string type = "com.akamon.blackjack.protocol.client.StartMatch")
            {
                this.to = to;
                this.type = type;
                this.data = data;
            }

            public string type;
            public string to;
            public StartMatchBjData data;
        }

        [Serializable]
        public class StartMatchBjData
        {
            public int betAmount;
        }

        //IN
        [Serializable]
        public class InitGameBJResponse : WsResponse
        {
            public BJInitDataL01 data;
        }

        [Serializable]
        public class DealCardsToPlayerIncoming : WsResponse
        {
            public PlayerCardsData data;
        }

        [Serializable]
        public class DealCardsToCroupierIncoming : WsResponse
        {
            public PlayerCardsData data;
        }

        [Serializable]
        public class BetReplyIncoming : WsResponse
        {
            public BetReplyData data;
        }

        [Serializable]
        public class FinishGameServer : WsResponse
        {
        }

        [Serializable]
        public class BetReplyData : IResponseData
        {
            public string betResponse;
        }

        [Serializable]
        public class BJInitDataL01 : IResponseData
        {
            public BJInitDataL02 gameConfig;
        }

        [Serializable]
        public class BJInitDataL02 : IResponseData
        {
            public string gameName;
            public int[] availableBets;
            public int dedefaultBet;
        }

        [Serializable]
        public class PlayerCardsData : IResponseData
        {
            public int handIndex;
            public int[] handValue;
            public string handState;
            public CardData[] dealtCards;
        }

        [Serializable]
        public class CardData
        {
            public string value;
            public string suit;
        }
    }
}