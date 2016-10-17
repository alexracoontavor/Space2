using Assets.Infrastructure.Architecture.Modulux;
using Assets.Scripts.Space2Module.Redux.State;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Space2Module.Integration.ObjectsSandbox.Objects
{
    public class SaveObjectsIntegrationTest : MonoBehaviour
    {
        void Awake()
        {
            //DONE initialize system (Setup.Reset())

            Setup.Setup.Reset();
        }

        void Start ()
        {
            /*
            DONE
            DONE    initialize objects from default state (objects initializer)
            DONE        prefab w/PopulatableObject
            DONE        populator object that adds instances to scene
            DONE            random distribution
            DONE            random force applied to objects
            DONE

            DONE    initialize update every tick (objects controller)

            DONE    see that after X ticks, saved data matches expected
            DONE        add reducer for adding data snapshots to state
            */

            var saveTicks = 5;
            Space2State state = null;

            ModuluxRoot.GetStateStream<Space2State>()
                .Take(saveTicks)
                .Subscribe(
                    s =>
                    {
                        state = s; 
                    },
                    () =>
                    {
                        if (state.Timeline.CurrentObjects.Length > 0 && state.Timeline.Timeline.Length > 1)
                        {
                            IntegrationTest.Pass();
                        }
                        else
                        {
                            IntegrationTest.Fail();
                        }
                    }).AddTo(this);

        }
    }
}
