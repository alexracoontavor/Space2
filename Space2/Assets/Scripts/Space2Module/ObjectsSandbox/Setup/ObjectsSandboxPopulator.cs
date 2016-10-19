using System.Linq;
using Assets.Infrastructure.CoreTools.Extensions;
using Assets.Scripts.Space2Module.Controllers.ObjectsPopulation;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Space2Module.Integration.ObjectsSandbox.Setup
{
    public class ObjectsSandboxPopulator : MonoBehaviour
    {
        public GameObject PhysicalObjectPrefab;
        public int NumObjects = 5;
        public float SpreadRadius = 10f;
        public float MaxForceToApply = 10f;
        public bool IsAddingInitialForce = false;

        public void Start()
        {
            Observable.TimerFrame(2).Subscribe(_=>AddObjects());
        }

        public void AddObjects()
        {
            Enumerable.Range(0, NumObjects)
                .ForEach(i =>
                {
                    var o = Instantiate(PhysicalObjectPrefab);
                    o.transform.position = o.transform.position + (Random.insideUnitSphere*SpreadRadius);
                    if (IsAddingInitialForce) o.GetComponent<PopulatableObject>().Rigidbody.AddForce(Random.insideUnitSphere * Random.Range(0, MaxForceToApply));
                });
        }
    }
}