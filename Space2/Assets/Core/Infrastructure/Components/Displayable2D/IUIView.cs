using UnityEngine;
using System;

namespace Assets.Infrastructure.Components.Displayable2D
{
    public interface IUIView
    {
        GameObject GameObject { get; }

        void Subscribe(Type type, IUIView pIView);
        void Show(Type type);
        void Hide();
        void HideAll();
    }
}