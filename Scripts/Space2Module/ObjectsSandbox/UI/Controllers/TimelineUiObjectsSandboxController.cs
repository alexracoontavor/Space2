using Assets.Infrastructure.Architecture.Modulux;
using Assets.Scripts.Space2Module.Redux.Actions;
using Assets.Scripts.Space2Module.Redux.State;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Space2Module.Controllers.ObjectsSandbox.UI.Controllers
{
    public class TimelineUiObjectsSandboxController : MonoBehaviour
    {
        private ITimelineUiObjectsSandbox _viewImpl;

        void Start()
        {
            _viewImpl = GetComponentInChildren<ITimelineUiObjectsSandbox>();

            var state = ModuluxRoot
                .GetStateStream<Space2State>()
                .Where(s => s.Timeline != null);

            state
                .DistinctUntilChanged(s => s.Timeline)
                .Subscribe(s =>
                {
                    _viewImpl.Populate(s.Timeline);
                }).AddTo(this);

            _viewImpl.ChangeIndexRequestStream.Subscribe(ActionsCreator.StepInTime);
        }
    }
}