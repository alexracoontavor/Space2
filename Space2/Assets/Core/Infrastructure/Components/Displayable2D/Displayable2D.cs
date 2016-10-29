using UnityEngine;
using UniRx;

namespace Assets.Infrastructure.Components.Displayable2D
{ 
	public class Displayable2D : MonoBehaviour, I2DDisplayable
	{
		private IObservable<Unit> _showCompleteStream;
		private IObservable<Unit> _hideCompleteStream;
		private IObservable<Unit> _enableCompleteStream;
		private IObservable<Unit> _disableCompleteStream;

		private Subject<Unit> _showSubject = new Subject<Unit>();
		private Subject<Unit> _hideSubject = new Subject<Unit>();
		private Subject<Unit> _enableSubject = new Subject<Unit>();
		private Subject<Unit> _disableSubject= new Subject<Unit>();

		void Awake()
		{
			_showCompleteStream = _showSubject.Select(s=>s);
			_hideCompleteStream = _hideSubject.Select(s=>s);
			_enableCompleteStream = _enableSubject.Select(s=>s);
			_disableCompleteStream = _disableSubject.Select(s=>s);
		}

		public void Show(bool isOn)
		{
			gameObject.SetActive(isOn);

			if (isOn)
				_showSubject.OnNext(Unit.Default);
			else
				_hideSubject.OnNext(Unit.Default);
		}

		public void Enable(bool isOn)
		{
			if (isOn)
				_enableSubject.OnNext(Unit.Default);
			else
				_disableSubject.OnNext(Unit.Default);
		}

		public GameObject GameObject {get { return gameObject; }}
		public IObservable<Unit> ShowComplete { get { return _showCompleteStream; } }
		public IObservable<Unit> HideComplete { get { return _hideCompleteStream; } }
		public IObservable<Unit> EnableComplete { get { return _enableCompleteStream; } }
		public IObservable<Unit> DisableComplete { get { return _disableCompleteStream; } }
	}
}
