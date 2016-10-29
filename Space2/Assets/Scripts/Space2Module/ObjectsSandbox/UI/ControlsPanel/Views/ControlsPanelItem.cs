using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Space2Module.ObjectsSandbox.UI.ControlsPanel.Views
{
    public class ControlsPanelItem : MonoBehaviour
    {
        public Text OutputText;
        public Button UpButton;
        public Button DownButton;
        public float StepValue;
        public IObservable<float> ChangesStream;

        private float _value1 = float.NaN;
        private float _value2 = float.NaN;
        
        private void Awake()
        {
            UpButton.GetComponentInChildren<Text>().text = "+" + StepValue.ToString("0.00");
            DownButton.GetComponentInChildren<Text>().text = "-" + StepValue.ToString("0.00");

            ChangesStream =
                UpButton.OnClickAsObservable().Select(_ => 1f)
                    .Merge(DownButton.OnClickAsObservable().Select(_ => -1f))
                    .Select(f => f*StepValue);
        }

        public void SetValue(float value)
        {
            _value1 = value;
            DisplayValues();
        }

        public void SetValue2(float value)
        {
            _value2 = value;
            DisplayValues();
        }

        private void DisplayValues()
        {
            var text = "";

            if (!float.IsNaN(_value1))
            {
                text = _value1.ToString("0.00");

                if (!float.IsNaN(_value2))
                {
                    text = text + " | " + _value2.ToString("0.00");
                }
            }

            OutputText.text = text;
        }
    }
}
