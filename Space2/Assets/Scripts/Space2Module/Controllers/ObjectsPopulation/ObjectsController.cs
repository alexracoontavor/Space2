using System;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.Assets.Infrastructure.CoreTools.Extensions;
using Assets.Scripts.Space2Module.Redux.Actions;
using Assets.Scripts.Space2Module.Redux.State;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Space2Module.Controllers.ObjectsPopulation
{
    public class ObjectsController : MonoBehaviour
    {
        private readonly ObjectsPopulator _populator = new ObjectsPopulator();
        private IDisposable _objectsFromDataSubscription;

        public int SampleRate = 10;
        public const int MinTickMs = 10;
        
        public ObjectsPopulator ObjectsPopulator { get { return _populator; } }

        //THIS IS A FUCKING UGLY HACK DUE TO TIME PAUSE NOT BEING IMPLEMENTED
        public bool IsUpdating = true;

        public void Start()
        {
            SubscribeToGenerateObjectUpdates(TickedStream(SampleRate).Where(s=>IsUpdating), _populator).AddTo(this);
            Reset();
        }

        public void Reset()
        {
            if (_objectsFromDataSubscription != null)
                _objectsFromDataSubscription.Dispose();

            _objectsFromDataSubscription = SubscribeToObjectsFromData(ModuluxRoot.GetStateStream<Space2State>(), _populator);
        }

        public static IDisposable SubscribeToObjectsFromData(IObservable<Space2State> stateStream, ObjectsPopulator populator)
        {
            return stateStream
                .Where(s=>s.Timeline!=null)
                .DistinctUntilChanged(s => new { s.Timeline })
                .DistinctUntilChanged(s=>s.Timeline.IsWaitingToUpdateObjects)
                .Where(s=>s.Timeline.IsWaitingToUpdateObjects)
                .Subscribe(s =>
                {
                    populator.PopulateObjectsFromData(s.Timeline.CurrentObjects);
                    ActionsCreator.StepUpdateComplete();
                });
        }

        public static IObservable<long> TickedStream(int sampleRate)
        {
            return Observable
                .Interval(TimeSpan.FromMilliseconds(MinTickMs))
                .SampleEvery(()=>sampleRate);
        }

        public static IDisposable SubscribeToGenerateObjectUpdates(IObservable<long> stream, ObjectsPopulator populator)
        {
            return stream
                .Subscribe(s =>
                {
                    var d = populator.GetObjectsData();
                    if (d.Length > 0)
                        ActionsCreator.UpdateObjects(d);
                });
        }
    }
}