using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Infrastructure.Architecture.Modulux.State;
using Assets.Infrastructure.Architecture.Redux;
using Assets.Scripts.Space2Module.Redux.Actions;
using Assets.Scripts.Space2Module.Redux.State;

namespace Assets.Scripts.Space2Module.Redux.Reducers
{
    public static class Stage2StateReducer
    {
        public static BaseState Reduce(BaseState prevState, IAction action)
        {
            var ps = prevState as Space2State;
            var timeline = ReduceTimeline(ps, action);
            
            return new Space2State()
            {
                Timeline = timeline
            };
        }

        public static ObjectsTimeline ReduceTimeline(Space2State prevState, IAction action)
        {
            var dataUpdatedAction = action as ObjectsDataUpdatedAction;

            if (dataUpdatedAction != null)
            {
                if (prevState.Timeline == null || prevState.Timeline.Timeline == null)
                {
                    return new ObjectsTimeline() { Timeline = new [] { dataUpdatedAction.Objects } };
                }

                var tl = prevState.Timeline.Timeline == null ? new List<ObjectData[]>() : prevState.Timeline.Timeline.ToList();
                tl.Add(dataUpdatedAction.Objects);
                return new ObjectsTimeline() {Timeline = tl.ToArray(), CurrentIndex = prevState.Timeline.Timeline.Length-1, CurrentObjects = dataUpdatedAction.Objects};
            }

            var stepAction = action as StepInTimeRequestAction;

            if (stepAction != null && prevState.Timeline != null && prevState.Timeline.Timeline.Length > 0)
            {
                int index;

                if (stepAction.Steps > 0)
                {
                    index = Math.Min(prevState.Timeline.Timeline.Length - 1, prevState.Timeline.CurrentIndex + stepAction.Steps);
                }
                else
                {
                    index = Math.Max(0, prevState.Timeline.CurrentIndex + stepAction.Steps);
                }

                var currentObjects = prevState.Timeline.Timeline[index];

                return new ObjectsTimeline
                {
                    CurrentIndex = index,
                    Timeline = prevState.Timeline.Timeline,
                    CurrentObjects = currentObjects,
                    IsWaitingToUpdateObjects = true
                };
            }

            return prevState.Timeline;
        }
    }
}