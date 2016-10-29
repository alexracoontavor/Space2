using Assets.Scripts.Space2Module.Redux.State;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Space2Module.ObjectsSandbox.UI.ControlsPanel.Views
{
    public class DefaultControlsView : MonoBehaviour, IControlsPanel
    {
        public ControlsPanelItem ThrustItem;
        public ControlsPanelItem PitchItem;
        public ControlsPanelItem YawItem;
        public ControlsPanelItem RollItem;
        
        public void Initialize()
        {
            var pitchChangedStream = PitchItem.ChangesStream.Select(f => new Vector3Data { y = f });
            var yawChangedStream = YawItem.ChangesStream.Select(f => new Vector3Data { x = f });
            var rollChangedStream = RollItem.ChangesStream.Select(f => new Vector3Data { z = f });

            ChangeAngularVelocityRequestStream = Observable.Merge(new[]
            {
                pitchChangedStream, yawChangedStream, rollChangedStream
            });

            ChangeThrustRequestStream = ThrustItem.ChangesStream;
        }

        public void SetThrust(float value)
        {
            ThrustItem.SetValue(value);
        }

        public void SetAngularVelocity(Vector3Data vector)
        {
            PitchItem.SetValue(vector.y);
            YawItem.SetValue(vector.x);
            RollItem.SetValue(vector.z);
        }

        public void SetAngularVelocityChanges(Vector3Data vector)
        {
            PitchItem.SetValue2(vector.y);
            YawItem.SetValue2(vector.x);
            RollItem.SetValue2(vector.z);
        }

        public IObservable<Vector3Data> ChangeAngularVelocityRequestStream { get; private set; }
        public IObservable<float> ChangeThrustRequestStream { get; private set; }

    }
}
