using System;
using Assets.Infrastructure.Architecture.Modulux.State;
using Assets.Infrastructure.Architecture.Redux;

namespace Assets.Infrastructure.CoreTools.Dux.UiDux.Redux.State
{
    [Serializable]
    public class UiState : BaseState
    {
        public UiScreenState CurrentState;
        public UiScreenState PrevState;

        public UiState(BaseState[] modules = null, Func<BaseState, IAction, BaseState>[] reducers = null) : base(modules, reducers)
        {
        }
    }

    [Serializable]
    public abstract class UiScreenState
    {
        public Type[] LegalTransitions = null;
    }
}