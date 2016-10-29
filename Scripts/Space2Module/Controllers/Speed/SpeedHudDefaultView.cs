using System;
using System.Linq;
using Assets.Infrastructure.CoreTools.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Space2Module.Controllers.Speed
{
    public class SpeedHudDefaultView : MonoBehaviour, ISpeedHUD
    {
        public Button[] Buttons;
        public float[] Values;

        public Text Text;
        public IObservable<float> NewSpeedSelectedStream { get { return _combiButtonsStream; } }

        private IObservable<float> _combiButtonsStream;

        void Awake()
        {
            _combiButtonsStream = Enumerable
                .Range(0, Buttons.Length)
                .Select(i =>
                    Buttons[i]
                        .OnClickAsObservable()
                        .Select(_ => Values[i]))
                .Merge();
        }

        public void Populate(float speed)
        {
            Text.text = speed.ToString("0.00");

            Enumerable
                .Range(0, Buttons.Length)
                .ForEach(
                    i =>
                        Buttons[i].GetComponentInChildren<Image>().color =
                            Math.Abs(speed - Values[i]) < 0.001f ? Color.green : Color.white);
        }
    }
}