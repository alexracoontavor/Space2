using UniRx;
using UnityEngine;

namespace Assets.Infrastructure.Components.Displayable2D
{
    public interface I2DDisplayable
    {
        void Show(bool isOn);
        void Enable(bool isOn);
        GameObject GameObject { get; }

        IObservable<Unit> ShowComplete { get; }
        IObservable<Unit> HideComplete { get; }
        IObservable<Unit> EnableComplete { get; }
        IObservable<Unit> DisableComplete { get; }
    }
}