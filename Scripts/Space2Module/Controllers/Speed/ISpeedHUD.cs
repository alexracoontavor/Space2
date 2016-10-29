using UniRx;

namespace Assets.Scripts.Space2Module.Controllers.Speed
{
    public interface ISpeedHUD
    {
        IObservable<float> NewSpeedSelectedStream { get; }
        void Populate(float speed);
    }
}
