using System;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.Architecture.Redux;
using Assets.Infrastructure.CoreTools.Dux.UiDux.Redux.State;

namespace Assets.Infrastructure.CoreTools.Dux.UiDux.Redux.Actions
{
    public class ActionsCreator
    {
        public static void ShowScreen(Type screenType)
        {
            ModuluxRoot.Store.Dispatch(new ScreenChangeRequestAction() { ToScreenType = screenType });
        }

        public static void ShowScreen(UiScreenState screenStateInstance)
        {
            ModuluxRoot.Store.Dispatch(new ScreenChangeRequestAction() { ToScreenType = screenStateInstance.GetType(), ScreenStateInstance = screenStateInstance});
        }

        public static void ShowPrevScreen()
        {
            ModuluxRoot.Store.Dispatch(new RestorePreviousScreenAction());
        }
    }

    public class RestorePreviousScreenAction : IAction
    {
    }

    public class ScreenChangeRequestAction : IAction
    {
        public Type ToScreenType;
        public UiScreenState ScreenStateInstance;
    }
}