using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Infrastructure.Architecture.Modulux.State;
using Assets.Infrastructure.Architecture.Redux;
using Assets.Scripts.Space2Module.Redux.Actions;
using Assets.Scripts.Space2Module.Redux.State;
using UnityEngine;

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

        private static PhysicsChangeRequest UpdatePhysicsChangeRequest(PhysicsChangeRequest to, PhysicsChangeRequest from)
        {
            var answer = new PhysicsChangeRequest
            {
                AngularVelocityChange = to.AngularVelocityChange ?? new Vector3Data(),
                ThrustChange = float.IsNaN(to.ThrustChange) ? 0f : to.ThrustChange
            };

            if (!float.IsNaN(from.ThrustChange))
                answer.ThrustChange += from.ThrustChange;

            if (@from.AngularVelocityChange == null) return answer;

            answer.AngularVelocityChange.x += @from.AngularVelocityChange.x;
            answer.AngularVelocityChange.y += @from.AngularVelocityChange.y;
            answer.AngularVelocityChange.z += @from.AngularVelocityChange.z;

            return answer;
        }

        public static ObjectsTimeline ReduceTimeline(Space2State prevState, IAction action)
        {
            var physicsChangeRequestAction = action as PhysicsChangeRequestAction;

            if (physicsChangeRequestAction != null)
            {
                if (prevState.Timeline == null || prevState.Timeline.CurrentObjects == null)
                    return prevState.Timeline;

                var player = prevState.Timeline.CurrentObjects.FirstOrDefault(o => o.Id == physicsChangeRequestAction.Id);

                if (player != null)
                {
                    player.PendingPhysicsChange = player.PendingPhysicsChange == null? physicsChangeRequestAction.Request : UpdatePhysicsChangeRequest(player.PendingPhysicsChange, physicsChangeRequestAction.Request);

                    return new ObjectsTimeline
                    {
                        Timeline = prevState.Timeline.Timeline,
                        CurrentIndex = prevState.Timeline.CurrentIndex,
                        GameSpeed = prevState.Timeline.GameSpeed,
                        IsWaitingToUpdateObjects = prevState.Timeline.IsWaitingToUpdateObjects,
                        CurrentObjects = prevState.Timeline.CurrentObjects.Where(o=>o.Id!=player.Id).Concat(new []{player}).ToArray()
                    };
                }

                return prevState.Timeline;
            }

            var dataUpdatedAction = action as ObjectsDataUpdatedAction;

            if (dataUpdatedAction != null)
            {
                if (prevState.Timeline == null || prevState.Timeline.Timeline == null)
                {
                    return new ObjectsTimeline() { Timeline = new [] { dataUpdatedAction.Objects } };
                }

                var tl = prevState.Timeline.Timeline == null ? new List<ObjectData[]>() : prevState.Timeline.Timeline.ToList();
                tl.Add(dataUpdatedAction.Objects);

                return new ObjectsTimeline()
                {
                    Timeline = tl.ToArray(),
                    CurrentIndex = prevState.Timeline.Timeline.Length-1,
                    CurrentObjects = dataUpdatedAction.Objects,
                    GameSpeed = prevState.Timeline.GameSpeed
                };
            }

            var stepAction = action as StepInTimeRequestAction;

            if (stepAction != null && prevState.Timeline != null && prevState.Timeline.Timeline.Length > 0)
            {
                var steps = prevState.Timeline.CurrentIndex + stepAction.Steps;

                var index = stepAction.Steps > 0 ? 
                    Math.Min(prevState.Timeline.Timeline.Length - 1, steps) : 
                    Math.Max(0, steps);

                var currentObjects = prevState.Timeline.Timeline[index];

                return new ObjectsTimeline
                {
                    CurrentIndex = index,
                    Timeline = prevState.Timeline.Timeline,
                    CurrentObjects = currentObjects,
                    IsWaitingToUpdateObjects = true,
                    GameSpeed = 0
                };
            }

            var stepCompleteAction = action as StepUpdateCompleteAction;

            if (stepCompleteAction != null && prevState.Timeline != null)
            {
                return new ObjectsTimeline
                {
                    CurrentIndex = prevState.Timeline.CurrentIndex,
                    Timeline = prevState.Timeline.Timeline,
                    CurrentObjects = prevState.Timeline.CurrentObjects,
                    IsWaitingToUpdateObjects = false,
                    GameSpeed = prevState.Timeline.GameSpeed
                };
            }

            var speedChangeAction = action as SpeedChangeRequestAction;

            if (speedChangeAction != null)
            {
                var tolerance = 0.01f;
                var isUnpausing = prevState.Timeline.GameSpeed < tolerance && speedChangeAction.Speed > tolerance;
                var isPausing = prevState.Timeline.GameSpeed > tolerance && speedChangeAction.Speed < tolerance;

                var t = isUnpausing
                    ? prevState.Timeline.Timeline.Take(prevState.Timeline.CurrentIndex).ToArray()
                    : prevState.Timeline.Timeline;
                
                return new ObjectsTimeline
                {
                    CurrentIndex = prevState.Timeline.CurrentIndex,
                    Timeline = t,
                    CurrentObjects = prevState.Timeline.CurrentObjects,
                    IsWaitingToUpdateObjects = isUnpausing,
                    GameSpeed = Mathf.Min(1f, Mathf.Max(0, speedChangeAction.Speed))
                };
            }

            return prevState.Timeline;
        }
    }
}