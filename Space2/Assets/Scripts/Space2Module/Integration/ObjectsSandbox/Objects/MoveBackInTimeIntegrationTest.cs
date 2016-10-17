using System;
using System.Linq;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Scripts.Space2Module.Controllers.ObjectsPopulation;
using Assets.Scripts.Space2Module.Redux.Actions;
using Assets.Scripts.Space2Module.Redux.State;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Space2Module.Integration.ObjectsSandbox.Objects
{
    public class MoveBackInTimeIntegrationTest : MonoBehaviour
    {

        void Awake()
        {
            Setup.Setup.Reset();
        }

        void Start()
        {
            /*
                DONE Generate data for objects
                  
                Push update through system
                    DONE Action - step in time request
                    DONE Reducers - process actions
                        DONE step in time request
                            DONE handle back and forth
                            DONE make sure to clamp values that are too high or too low
                    DONE State - request status
                    DONE    current timeline index
                    DONE    max timeline index
                    DONE Controller
                        DONE listen to state change
                        DONE populate objects
                        DONE trim extra objects and datas

                 DONE Step back in time
                    DONE re-generate data from objects, compare results

                DONE Step forward in time
                    DONE re-generate data from objects, compare results
            */

            var controller = FindObjectOfType<ObjectsController>();
            controller.Reset();

            var stateStream = ModuluxRoot.GetStateStream<Space2State>();

            Observable
                .Timer(TimeSpan.FromMilliseconds(1000))
                .CombineLatest(stateStream, (i, stream) => stream)
                .First()
                .Subscribe(s =>
                {
                    ActionsCreator.StepInTime(s.Timeline.Timeline.Length + 1);
                })
                .AddTo(this);

            //something about this stream building fucks Unity up and it goes FUBAR while throwing UNetWeaver error: Exception :System.ArgumentException: An element with the same key already exists in the dictionary.
            //hence all the crap with isReadyForStepForward
            var objectsUpdatedStream = stateStream
                .Where(s => s.Timeline != null)
                .Distinct(s => s.Timeline.IsWaitingToUpdateObjects)
                .Where(s => s.Timeline.IsWaitingToUpdateObjects);

            var isReadyForStepForward = false;

            objectsUpdatedStream
                .First()
                .Subscribe(state=>
                {
                    var newObjects = controller.ObjectsPopulator.GetObjectsData();

                    if (!ObjectDataHelpers.CompareObjectsDatas(newObjects, state.Timeline.Timeline[0]))
                        IntegrationTest.Fail();

                    isReadyForStepForward = true;

                    ActionsCreator.StepInTime(-(state.Timeline.Timeline.Length + 1));
                });

            stateStream
                .Subscribe(state =>
                {
                    if (isReadyForStepForward)
                    {
                        var newObjects = controller.ObjectsPopulator.GetObjectsData();

                        if (Enumerable.Range(0, state.Timeline.Timeline.Length - 1).All(i=> ObjectDataHelpers.CompareObjectsDatas(state.Timeline.Timeline[0], state.Timeline.Timeline[i])))
                        {
                            IntegrationTest.Fail("All the timeline is the same!");
                        }
                        else if (ObjectDataHelpers.CompareObjectsDatas(newObjects,state.Timeline.Timeline[state.Timeline.Timeline.Length - 1]))
                        {
                            IntegrationTest.Fail("Freshly collected objects data is not the same as retrieved from save!");
                        }
                        else
                        {
                            IntegrationTest.Pass();
                        }
                    }
                });
        }
    }
}
