using System;
using Assets.Infrastructure.Modules.HGS.HGS_Module.BaseClasses;

namespace Assets.Infrastructure.Modules.HGS.HGS_Account_Module.API
{
    public class HGS_Account_API
    {
        [Serializable]
        public class MoneyUpdateResponse : WsResponse
        {
            public MoneyUpdate data;
        }

        [Serializable]
        public class MoneyUpdate
        {
            public string type;         // currency type, for now CHIP, COIN
            public long amount;         // amount
        }
    }
}