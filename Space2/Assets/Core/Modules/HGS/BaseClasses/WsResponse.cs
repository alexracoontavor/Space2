using System;

namespace Assets.Infrastructure.Modules.HGS.HGS_Module.BaseClasses
{
    [Serializable]
    public class WsResponse : IResponseData
    {
        public string @timestamp;
        public string from;
        public string type;
    }
}