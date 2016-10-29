using System;
using Assets.Infrastructure.Architecture.Modulux.State;
using Assets.Infrastructure.Architecture.Redux;
using Assets.Infrastructure.CoreTools.Dux.UiDux.Redux.Actions;
using Assets.Infrastructure.CoreTools.Dux.UiDux.Redux.State;
using Assets.Infrastructure.CoreTools.Extensions;

namespace Assets.Infrastructure.CoreTools.Dux.UiDux.Redux.Reducers
{
    public class UiRootReducer
    {
        public static BaseState Reduce(BaseState prevState, IAction action)
        {
            if (action is ScreenChangeRequestAction)
            {
                return ReduceScreenChangeRequest(prevState as UiState, action as ScreenChangeRequestAction);
            }
            if (action is RestorePreviousScreenAction)
            {
                return ReduceScreenRestoreRequest(prevState as UiState, action as RestorePreviousScreenAction);
            }

            return prevState;
        }

        private static BaseState ReduceScreenRestoreRequest(UiState prevState, RestorePreviousScreenAction screenRestoreRequestAction)
        {
            return new UiState() {CurrentState = prevState.PrevState, PrevState = prevState.CurrentState};
        }

        private static BaseState ReduceScreenChangeRequest(UiState prevState, ScreenChangeRequestAction screenChangeRequestAction)
        {
            if (prevState != null 
                && prevState.CurrentState != null 
                && prevState.CurrentState.LegalTransitions != null 
                && !prevState.CurrentState.LegalTransitions.Contains(screenChangeRequestAction.ToScreenType)
               )
                return prevState;

            return new UiState()
            {
                CurrentState = screenChangeRequestAction.ScreenStateInstance ?? (UiScreenState)Activator.CreateInstance(screenChangeRequestAction.ToScreenType),
                PrevState = prevState != null?prevState.CurrentState:null
            };
        }
    }
}