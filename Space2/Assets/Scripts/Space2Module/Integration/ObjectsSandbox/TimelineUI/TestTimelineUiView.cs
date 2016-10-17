using Assets.Scripts.Space2Module.Controllers.ObjectsSandbox.UI;
using Assets.Scripts.Space2Module.Redux.State;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Space2Module.Integration.ObjectsSandbox.TimelineUI
{
    public class TestTimelineUiView : MonoBehaviour, ITimelineUiObjectsSandbox
    {
        public IObservable<int> ChangeIndexRequestStream { get { return _changeIndexRequestSubject.AsObservable(); } }
        public IObservable<ObjectsTimeline> TimelineStream { get { return _timelineSubject.AsObservable(); } }
        private readonly Subject<int> _changeIndexRequestSubject = new Subject<int>();
        private readonly Subject<ObjectsTimeline> _timelineSubject = new Subject<ObjectsTimeline>();

        public void Populate(ObjectsTimeline timeline)
        {
            _timelineSubject.OnNext(timeline);
        }

        public void SimulateUserStepInput(int steps)
        {
            _changeIndexRequestSubject.OnNext(steps);
        }
    }
}
