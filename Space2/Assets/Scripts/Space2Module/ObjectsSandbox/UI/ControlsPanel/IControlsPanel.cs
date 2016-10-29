using Assets.Scripts.Space2Module.Redux.State;
using UniRx;

namespace Assets.Scripts.Space2Module.ObjectsSandbox.UI.ControlsPanel
{
    public interface IControlsPanel
    {
        void Initialize();
        void SetThrust(float value);
        void SetAngularVelocity(Vector3Data vector);
        void SetAngularVelocityChanges(Vector3Data vector);

        IObservable<Vector3Data> ChangeAngularVelocityRequestStream { get; }
        IObservable<float> ChangeThrustRequestStream { get; }
    }
}
