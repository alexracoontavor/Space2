using System;
using System.Collections.Generic;
using Assets.Infrastructure.Architecture;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.CoreTools.Instanced.JsonFormatter;
using Assets.Infrastructure.Modules.HGS.HGS_Account_Module.Actions;
using Assets.Infrastructure.Modules.HGS.HGS_Module.BaseClasses;

namespace Assets.Infrastructure.Modules.HGS.HGS_Account_Module.API
{
    public class HGS_Account_Receiver : HGSReceiver<HGS_Account_Module>
    {
        private static readonly Dictionary<string, Action<string>> _dataToActionsMap = new Dictionary<string, Action<string>>()
        {
            {
            "com.akamon.suite.api.protocol.server.MoneyUpdate", s =>
            {
                HGS_Account_ActionsCreator.ProcessServerResponse(
                    ModuluxRoot.IoC.Get<IJsonFormatter>()
                        .FromString<HGS_Account_API.MoneyUpdateResponse>(s));
            }
             }
        };

        public HGS_Account_Receiver() : base(_dataToActionsMap)
        {

        }
    }
}