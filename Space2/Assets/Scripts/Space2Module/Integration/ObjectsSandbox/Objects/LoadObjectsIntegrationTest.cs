using System;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Scripts.Space2Module.Controllers.ObjectsPopulation;
using Assets.Scripts.Space2Module.Redux.Actions;
using Assets.Scripts.Space2Module.Redux.State;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Space2Module.Integration.ObjectsSandbox.Objects
{
    public class LoadObjectsIntegrationTest : MonoBehaviour
    {
        void Awake()
        {
            Setup.Setup.Reset();
        }

        void Start ()
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

                Step forward in time
                    re-generate data from objects, compare results
            */

            var controller = FindObjectOfType<ObjectsController>();
            controller.Reset();
            
            var stateStream = ModuluxRoot.GetStateStream<Space2State>();

            Observable
                .Timer(TimeSpan.FromMilliseconds(1000))
                .CombineLatest(stateStream, (i, stream)=>stream)
                .First()
                .Subscribe(s =>
                {
                    ActionsCreator.StepInTime(int.MinValue);
                })
                .AddTo(this);

            var objectsUpdatedStream = stateStream
                .Where(s => s.Timeline != null)
                .Distinct(s => s.Timeline.IsWaitingToUpdateObjects)
                .Where(s => s.Timeline.IsWaitingToUpdateObjects);

            objectsUpdatedStream
                .Subscribe(s =>
                {
                    var newObjects = controller.ObjectsPopulator.GetObjectsData();

                    if (!ObjectDataHelpers.CompareObjectsDatas(newObjects, s.Timeline.Timeline[0]))
                        IntegrationTest.Fail();

                    ActionsCreator.StepInTime(int.MaxValue);
                })
                .AddTo(this);

            stateStream
                .SkipUntil(objectsUpdatedStream)
                .Skip(1)
                .Subscribe(s =>
                {
                    var newObjects = controller.ObjectsPopulator.GetObjectsData();

                    if (!ObjectDataHelpers.CompareObjectsDatas(newObjects, s.Timeline.Timeline[s.Timeline.Timeline.Length+1]))
                        IntegrationTest.Fail();
                    else
                    {
                        IntegrationTest.Pass();
                    }
                }).AddTo(this);
        }
    }
}
