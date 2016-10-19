using Assets.Infrastructure.Architecture.Modulux;
using Assets.Scripts.Space2Module.Redux.Actions;
using Assets.Scripts.Space2Module.Redux.State;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Space2Module.Controllers.Speed
{
    public class SpeedHudController : MonoBehaviour
    {
        private ISpeedHUD _viewImpl;

        void Start()
        {
            _viewImpl = GetComponentInChildren<ISpeedHUD>();

            var state = ModuluxRoot
                .GetStateStream<Space2State>()
                .Where(s => s.Timeline != null);

            state
                .DistinctUntilChanged(s => s.Timeline)
                .Subscribe(s =>
                {
                    Time.timeScale = s.Timeline.GameSpeed;
                    _viewImpl.Populate(s.Timeline.GameSpeed);
                }).AddTo(this);

            _viewImpl.NewSpeedSelectedStream.Subscribe(ActionsCreator.SpeedChangeRequest);
        }
    }
}