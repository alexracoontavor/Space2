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

        void Awake()
        {
            Integration.ObjectsSandbox.Setup.Setup.Reset();
        }
    }
}
