using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.Architecture.Redux;
using Assets.Scripts.Space2Module.Redux.State;

namespace Assets.Scripts.Space2Module.Redux.Actions
{
    public static class ActionsCreator
    {
        public static void UpdateObjects(ObjectData[] objects)
        {
            ModuluxRoot.Store.Dispatch(new ObjectsDataUpdatedAction() {Objects = objects});
        }

        public static void StepInTime(int numSteps)
        {
            ModuluxRoot.Store.Dispatch(new StepInTimeRequestAction() { Steps = numSteps });
        }

        public static void StepUpdateComplete()
        {
            ModuluxRoot.Store.Dispatch(new StepUpdateCompleteAction());
        }
    }

    public class StepUpdateCompleteAction : IAction
    {
    }

    public class ObjectsDataUpdatedAction : IAction
    {
        public ObjectData[] Objects;
    }

    public class StepInTimeRequestAction : IAction
    {
        public int Steps;
    }
}