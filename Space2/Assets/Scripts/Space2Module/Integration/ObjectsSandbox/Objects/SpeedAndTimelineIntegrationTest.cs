using UnityEngine;

namespace Assets.Scripts.Space2Module.Integration.ObjectsSandbox.Objects
{
    public class SpeedAndTimelineIntegrationTest : MonoBehaviour
    {
        void Awake()
        {
            Setup.Setup.Reset();
        }

        void Start()
        {
            /*
            TODO -
                Add/Remove objects if mismatch between existence of data/object
                Where went inertia?
                Add explosion on hit
                    physical force
                    visuals
                Add detection of goal-to-target collision
                    Add game end detection
                    Add game end screen + restart
            */

            /*
            TODO -
                DONE why is first frame moving one forward every time? 
                    DONE - is first frame same? 
                    DONE - timestep unit tests!
                DONE -  why is last frame incorrect?
                DONE - why is force staying?
            */
        }
    }
}