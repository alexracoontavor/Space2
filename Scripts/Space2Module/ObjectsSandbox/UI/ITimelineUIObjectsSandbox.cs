using Assets.Scripts.Space2Module.Redux.State;
using UniRx;

namespace Assets.Scripts.Space2Module.Controllers.ObjectsSandbox.UI
{
    public interface ITimelineUiObjectsSandbox
    {
        IObservable<int> ChangeIndexRequestStream { get; }
        void Populate(ObjectsTimeline timeline);
    }
}