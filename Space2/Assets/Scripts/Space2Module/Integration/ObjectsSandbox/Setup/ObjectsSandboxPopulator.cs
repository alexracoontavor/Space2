using System.Linq;
using Assets.Infrastructure.CoreTools.Extensions;
using Assets.Scripts.Space2Module.Controllers.ObjectsPopulation;
using UnityEngine;

namespace Assets.Scripts.Space2Module.Integration.ObjectsSandbox.Setup
{
    public class ObjectsSandboxPopulator : MonoBehaviour
    {
        public GameObject PhysicalObjectPrefab;
        public int NumObjects = 5;
        public float SpreadRadius = 10f;
        public float MaxForceToApply = 10f;

        public void Start()
        {
            AddObjects();
        }

        public void AddObjects()
        {
            Enumerable.Range(0, NumObjects)
                .ForEach(i =>
                {
                    var o = Instantiate(PhysicalObjectPrefab);
                    o.transform.position = o.transform.position + (Random.insideUnitSphere*SpreadRadius);
                    o.GetComponent<PopulatableObject>().Rigidbody.AddForce(Random.insideUnitSphere * Random.Range(0, MaxForceToApply));
                });
        }
    }
}