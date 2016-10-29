using System;
using Assets.Infrastructure.Architecture.Modulux.State;
using Assets.Infrastructure.Architecture.Redux;

namespace Assets.Infrastructure.Modules.HGS.HGS_Account_Module.State
{
    public class HGS_Account_State : BaseState
    {
        public Currency[] Currencies;

        public HGS_Account_State(Currency[] currencies = null, BaseState[] modules = null, Func<BaseState, IAction, BaseState>[] reducers = null) : base(modules, reducers)
        {
            this.Currencies = currencies;
        }
    }

    [Serializable]
    public class Currency
    {
        public string Type;
        public long Amount;
    }
}