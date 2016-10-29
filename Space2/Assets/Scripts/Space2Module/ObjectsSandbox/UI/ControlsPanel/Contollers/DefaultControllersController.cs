using System.Linq;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Scripts.Space2Module.Controllers.ObjectsPopulation;
using Assets.Scripts.Space2Module.Redux.Actions;
using Assets.Scripts.Space2Module.Redux.State;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Space2Module.ObjectsSandbox.UI.ControlsPanel.Contollers
{
    public class DefaultControllersController : MonoBehaviour
    {
        void Start ()
        {
            var stateStream = ModuluxRoot.GetStateStream<Space2State>();
            
            var view = GetComponentInChildren<IControlsPanel>(true);
            view.Initialize();

            var playerStream = stateStream
                .Where(s => s.Timeline != null && s.Timeline.CurrentObjects != null)
                .Select(s => s.Timeline.CurrentObjects.FirstOrDefault(o => o.Id == "Player"))
                .Where(s=>s!=null);

            playerStream.Subscribe(s =>
            {
                view.SetAngularVelocity(s.Rigidbody.angularVelocity);
            }).AddTo(this);

            playerStream
                .Where(s => s.PendingPhysicsChange != null)
                .Subscribe(s =>
                {
                    view.SetThrust(s.PendingPhysicsChange.ThrustChange);
                    view.SetAngularVelocityChanges(s.PendingPhysicsChange.AngularVelocityChange);
                }).AddTo(this);

            view.ChangeAngularVelocityRequestStream.Subscribe(s =>
            {
                ActionsCreator.PhysicsChangeRequest("Player", new PhysicsChangeRequest { AngularVelocityChange = s });
            }).AddTo(this);

            view.ChangeThrustRequestStream.Subscribe(s =>
            {
                ActionsCreator.PhysicsChangeRequest("Player", new PhysicsChangeRequest { ThrustChange = s });
            }).AddTo(this);
        }
    }
}
