using System.Linq;
using Assets.Editor.Tests.Space2Module.Controllers.ObjectsPopulation;
using Assets.Editor.Tests.Space2Module.Redux.Actions;
using Assets.Scripts.Space2Module.Controllers.ObjectsPopulation;
using Assets.Scripts.Space2Module.Redux.Actions;
using Assets.Scripts.Space2Module.Redux.Reducers;
using Assets.Scripts.Space2Module.Redux.State;
using NUnit.Framework;

namespace Assets.Editor.Tests.Space2Module.Redux.Reducers
{
    public class ReducersTests
    {
        [TestFixture]
        public class ReduceTimelineTest
        {
            [Test]
            public void ReduceOtherAction()
            {
                var numObjects = 3;
                var floatTestVal = 3f;
                var timelineDepth = 3;

                var prevState = ObjectDataHelpers.GenerateSpace2State(numObjects, floatTestVal, timelineDepth);
                var action = new OtherAction();

                var obj = Stage2StateReducer.ReduceTimeline(prevState, action);

                Assert.AreEqual(obj, prevState.Timeline);
            }

            [Test]
            public void ReduceObjectsDataUpdatedAction()
            {
                var numObjects = 3;
                var floatTestVal = 3f;
                var timelineDepth = 3;

                var prevState = ObjectDataHelpers.GenerateSpace2State(numObjects, floatTestVal, timelineDepth);
                var action = new ObjectsDataUpdatedAction { Objects = ObjectDataHelpers.GenerateObjectDatas(numObjects + 1, floatTestVal * 2f) };

                var tl = prevState.Timeline.Timeline.ToList();
                tl.Add(action.Objects);

                var obj = Stage2StateReducer.ReduceTimeline(prevState, action);

                Assert.AreEqual(obj.Timeline, tl.ToArray());
            }

            [Test]
            public void UpReduceStepInTimeRequestAction()
            {
                var numSteps = 100;

                var numObjects = 3;
                var floatTestVal = 3f;
                var timelineDepth = 3;

                var prevState = ObjectDataHelpers.GenerateSpace2State(numObjects, floatTestVal, timelineDepth);
                var action = new StepInTimeRequestAction { Steps = numSteps };

                var obj = Stage2StateReducer.ReduceTimeline(prevState, action);

                Assert.AreEqual(obj.CurrentIndex, timelineDepth - 1);
                Assert.AreEqual(obj.CurrentObjects, prevState.Timeline.Timeline[obj.CurrentIndex]);
            }

            [Test]
            public void DownReduceStepInTimeRequestAction()
            {
                var numSteps = -100;

                var numObjects = 3;
                var floatTestVal = 3f;
                var timelineDepth = 3;

                var prevState = ObjectDataHelpers.GenerateSpace2State(numObjects, floatTestVal, timelineDepth);
                var action = new StepInTimeRequestAction { Steps = numSteps };

                var obj = Stage2StateReducer.ReduceTimeline(prevState, action);

                Assert.AreEqual(obj.CurrentIndex, 0);
                Assert.AreEqual(obj.CurrentObjects, prevState.Timeline.Timeline[obj.CurrentIndex]);
            }
        }
    }
}