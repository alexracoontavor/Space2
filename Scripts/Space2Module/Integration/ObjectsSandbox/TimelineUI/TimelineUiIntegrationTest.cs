using System.Linq;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.CoreTools.Extensions;
using Assets.Scripts.Space2Module.Controllers.ObjectsPopulation;
using Assets.Scripts.Space2Module.Controllers.ObjectsSandbox.UI;
using Assets.Scripts.Space2Module.Controllers.ObjectsSandbox.UI.Controllers;
using Assets.Scripts.Space2Module.Redux.Actions;
using Assets.Scripts.Space2Module.Redux.State;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Space2Module.Integration.ObjectsSandbox.TimelineUI
{
    public class TimelineUiIntegrationTest : MonoBehaviour
    {
        public TestTimelineUiView View;
        public TimelineUiObjectsSandboxController Controller;

        void Awake()
        {
            Setup.Setup.Reset();
        }

        void Start ()
        {
            /*
                DONE - see that view is updated on population
                DONE - see that input translates into update of objects
            */

            var numUpdates = 5;

            View
                .TimelineStream
                .Take(numUpdates)
                .Subscribe(s =>
                {
                    if (s.Timeline.Length == numUpdates)
                    {
                        ModuluxRoot.GetStateStream<Space2State>()
                            .Subscribe(s2 =>
                            {
                                if (s2.Timeline.IsWaitingToUpdateObjects)
                                    IntegrationTest.Pass();
                            }).AddTo(this);

                        View.SimulateUserStepInput(-100);
                    }
                })
                .AddTo(this);

            Enumerable.Range(0, numUpdates).ForEach(s =>
            {
                ActionsCreator.UpdateObjects(ObjectDataHelpers.GenerateObjectDatas(s+1, s + 1));
            });
        }
    }
}
