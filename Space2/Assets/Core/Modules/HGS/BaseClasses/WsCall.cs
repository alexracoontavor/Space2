using System;

namespace Assets.Infrastructure.Modules.HGS.HGS_Module.BaseClasses
{
    [Serializable]
    public class WsCall
    {
        public WsCall(string type, string to, BaseMessageData data)
        {
            this.type = type;
            this.to = to;
            this.data = data;
        }

        public string type; //specific command id that service will process
        public string to = ""; //service name
        public BaseMessageData data;
    }
}