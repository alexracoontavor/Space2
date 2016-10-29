using System;
using System.Collections.Generic;
using Assets.Infrastructure.Architecture;
using Assets.Infrastructure.Architecture.Logger;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.Architecture.Server.WebSockets;
using Assets.Infrastructure.CoreTools.Instanced.JsonFormatter;
using UniRx;

namespace Assets.Infrastructure.Modules.HGS.HGS_Module.BaseClasses
{
    public class HGSReceiver<T> where T : ModuleBase
    {
        private Dictionary<string, Action<string>> _dataToActionsMap = new Dictionary<string, Action<string>>()
        {
        };

        public HGSReceiver(Dictionary<string, Action<string>> dataToActionsMap)
        {
            _dataToActionsMap = dataToActionsMap;
            Initialize();
        }

        public HGSReceiver()
        {
            Initialize();
        }

        private void Initialize()
        {
            ModuluxRoot.IoC.Get<IWsComm>().Receive()
                .Subscribe(ProcessString);
        }

        private void ProcessString(string s)
        {
            var response = ModuluxRoot.IoC.Get<IJsonFormatter>().FromString<WsResponse>(s);

            ModuluxRoot.IoC.Get<ILogger>().Log("Receiving string from WS: " + s);

            if (_dataToActionsMap.ContainsKey(response.type))
            {
                _dataToActionsMap[response.type](s);
            }
        }
    }
}