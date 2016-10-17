using System;
using System.Linq;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Scripts.Space2Module.Controllers.ObjectsPopulation;
using Assets.Scripts.Space2Module.Redux.State;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Space2Module.Controllers.ObjectsSandbox.Setup
{
    public class Setup : MonoBehaviour
    {
        public ObjectsController ObjectsController;
        public int RunForMs = 5000;
        public CanvasGroup HudCg;

        void Awake()
        {
            Integration.ObjectsSandbox.Setup.Setup.Reset();
            Time.timeScale = 0.3f;
            HudCg.interactable = false;
        }

        void Start()
        {
            Observable.Timer(TimeSpan.FromMilliseconds(RunForMs)).Subscribe(_ =>
            {
                ObjectsController.IsUpdating = false;
                Time.timeScale = 0f;
                HudCg.interactable = true;
            }).AddTo(this);
        }
    }
}
