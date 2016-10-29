using System;
using System.Collections.Generic;
using Assets.Infrastructure.Architecture;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.CoreTools.Instanced.JsonFormatter;
using Assets.Infrastructure.Modules.HGS.HGS_Login_Module.Actions;
using Assets.Infrastructure.Modules.HGS.HGS_Module.BaseClasses;

namespace Assets.Infrastructure.Modules.HGS.HGS_Login_Module.API
{
    public class HGS_Login_Receiver : HGSReceiver<HGS_Login_Module>
    {
        private static readonly Dictionary<string, Action<string>> _dataToActionsMap = new Dictionary<string, Action<string>>()
        {
            {
                "com.akamon.jreactive.sessions.TakeSessionResponse", s =>
                {
                    HGS_Login_ActionsCreator.ProcessServerResponse(
                        ModuluxRoot.IoC.Get<IJsonFormatter>()
                            .FromString<HGS_Login_API.SocketOpenedResponse>(s));
                }
            },
            {
                "com.akamon.gameapi.login.LoginResponse", s =>
                {
                    HGS_Login_ActionsCreator.ProcessServerResponse(
                        ModuluxRoot.IoC.Get<IJsonFormatter>()
                            .FromString<HGS_Login_API.LoginSuccessResponse>(s));
                }
            }
        };

        public HGS_Login_Receiver() : base(_dataToActionsMap)
        {
            
        }
    }
}