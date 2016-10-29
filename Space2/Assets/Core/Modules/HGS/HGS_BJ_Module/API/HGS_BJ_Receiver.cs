using System;
using System.Collections.Generic;
using Assets.Infrastructure.Architecture;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.CoreTools.Instanced.JsonFormatter;
using Assets.Infrastructure.Modules.HGS.HGS_BJ_Module.Actions;
using Assets.Infrastructure.Modules.HGS.HGS_Module.BaseClasses;

namespace Assets.Infrastructure.Modules.HGS.HGS_BJ_Module.API
{
    public class HGS_BJ_Receiver : HGSReceiver <HGS_BJ_Module>
    {
        private static readonly Dictionary<string, Action<string>> _dataToActionsMap = new Dictionary
            <string, Action<string>>()
        {
            {
                "com.akamon.blackjack.protocol.server.InitGameResponse", s =>
                {
                    HGS_BJ_ActionsCreator
                        .ProcessServerResponse(
                            ModuluxRoot.IoC
                                .Get<IJsonFormatter>()
                                .FromString
                                <
                                    HGS_BJ_API.InitGameBJResponse
                                    >(s));
                }
            },
            {
                "com.akamon.blackjack.protocol.server.DealCardsToPlayer", s =>
                {
                    HGS_BJ_ActionsCreator
                        .ProcessServerResponse(
                            ModuluxRoot.IoC
                                .Get<IJsonFormatter>()
                                .FromString
                                <
                                    HGS_BJ_API.DealCardsToPlayerIncoming
                                    >(s));
                }
            },
            {
                "com.akamon.blackjack.protocol.server.DealCardsToCroupier", s =>
                {
                    HGS_BJ_ActionsCreator
                        .ProcessServerResponse(
                            ModuluxRoot.IoC
                                .Get<IJsonFormatter>()
                                .FromString
                                <
                                    HGS_BJ_API.DealCardsToCroupierIncoming
                                    >(s));
                }
            },
            {
                "com.akamon.blackjack.protocol.server.BetResponse", s =>
                {
                    HGS_BJ_ActionsCreator
                        .ProcessServerResponse(
                            ModuluxRoot.IoC
                                .Get<IJsonFormatter>()
                                .FromString
                                <
                                    HGS_BJ_API.BetReplyIncoming
                                    >(s));
                }
            },
            {
                "com.akamon.blackjack.protocol.server.FinishGameServer", s =>
                {
                    HGS_BJ_ActionsCreator
                        .ProcessServerResponse(
                            ModuluxRoot.IoC
                                .Get<IJsonFormatter>()
                                .FromString
                                <
                                    HGS_BJ_API.FinishGameServer
                                    >(s));
                }
            },
            {
                "com.akamon.blackjack.protocol.server.DealCroupierHiddenCard", s =>
                {
                    ModuluxRoot.Logger.Log("Received DealCroupierHiddenCard: " + s);
                    //{"tid":"5412a34f-a9dd-4215-b870-81b250864dae","@timestamp":"2016-08-11T15:32:01,305","from":"BlackjackService","type":"com.akamon.blackjack.protocol.server.DealCroupierHiddenCard","data":{}}
                }
            },
            {
                "com.akamon.blackjack.protocol.server.CroupierViewHiddenCard", s =>
                {
                    ModuluxRoot.Logger.Log("Received CroupierViewHiddenCard: " + s);
                }
            },
            {
                "com.akamon.blackjack.protocol.server.UncoverHiddenCroupierCard", s =>
                {
                    ModuluxRoot.Logger.Log("Received UncoverHiddenCroupierCard: " + s);
                }
            },
            {
                "com.akamon.blackjack.protocol.server.FinishCroupierHand", s =>
                {
                    ModuluxRoot.Logger.Log("Received FinishCroupierHand: " + s);
                }
            },
            {
                "com.akamon.blackjack.protocol.server.SetHandResult", s =>
                {
                    ModuluxRoot.Logger.Log("Received SetHandResult: " + s);
                }
            }
        };

        public HGS_BJ_Receiver() : base(_dataToActionsMap)
        {

        }
    }
}