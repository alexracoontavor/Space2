using System;
using Assets.Scripts.Space2Module.Redux.State;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/*

        DONE - playtest scene
            Initialize with objects & random force
                enable UI & timeline controls after a time
            Simple UI with current scene and max scene as text
            Move up/down timeline with arrows || arrow buttons
                update scene every movement
*/

namespace Assets.Scripts.Space2Module.Controllers.ObjectsSandbox.UI.Views
{
    public class TimelineWalkerView : MonoBehaviour, ITimelineUiObjectsSandbox
    {
        public Text TimelineText;
        public Button StepDown;
        public Button StepUp;
        public Slider Slider;

        public IObservable<int> ChangeIndexRequestStream { get { return _indexOutStream; }}
        private IObservable<int> _indexOutStream;

        private int _currentIndex;
        private int _maxIndex;

        private void Awake()
        {
            var sliderStream = Slider.OnValueChangedAsObservable()
                .Select(v => (int)v)
                .Where(v => v != _currentIndex)
                .Select(v => v - _currentIndex);

            _indexOutStream = sliderStream.Merge(StepDown
                .OnClickAsObservable()
                .Select(_ => -1)
                .Merge(StepUp
                    .OnClickAsObservable()
                    .Select(_ => 1)));
        }

        public void Populate(ObjectsTimeline timeline)
        {
            _maxIndex = timeline.Timeline.Length;
            _currentIndex = timeline.CurrentIndex;

            Slider.maxValue = _maxIndex;
            Slider.value = _currentIndex;


            TimelineText.text = string.Format("{0}/{1}", _currentIndex, _maxIndex);
        }
    }
}
