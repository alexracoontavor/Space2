using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.CoreTools.Instanced.FileSystemAccessor;
using Assets.Scripts.Space2Module.Redux.Actions;
using Assets.Scripts.Space2Module.Redux.State;
using NUnit.Framework;

namespace Assets.Editor.Tests.Space2Module.Redux.Actions
{
    public class ActionsTests
    {
        [TestFixture]
        public class GetObjectsDataTest : ActionsTestBase
        {
            [Test]
            public void ActionIsDispatchedWithObjects()
            {
                ActionsCreator.UpdateObjects(new[] { new ObjectData() });
                Assert.AreEqual(Actions.Count, 1, "Was expecting 1 action but got " + Actions.Count);

                var a = Actions[0] as ObjectsDataUpdatedAction;
                Assert.IsNotNull(a, "Action is not an ObjectsDataUpdatedAction!");

                Assert.AreEqual(a.Objects.Length, 1, string.Format("Action had {0} objects instead of {1}!", a.Objects.Length, 1));
            }
        }

        [TestFixture]
        public class StepInTimeTest : ActionsTestBase
        {
            [Test]
            public void ActionIsDispatchedWithObjects()
            {
                var steps = 10;
                ActionsCreator.StepInTime(steps);
                Assert.AreEqual(Actions.Count, 1, "Was expecting 1 action but got " + Actions.Count);

                var a = Actions[0] as StepInTimeRequestAction;
                Assert.IsNotNull(a, "Action is not an ObjectsDataUpdatedAction!");

                Assert.AreEqual(a.Steps, steps, string.Format("Action had {0} objects instead of {1}!", a.Steps, steps));
            }
        }
    }
}