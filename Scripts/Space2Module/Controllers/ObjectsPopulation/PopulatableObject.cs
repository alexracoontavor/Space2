using System;
using UnityEngine;

namespace Assets.Scripts.Space2Module.Controllers.ObjectsPopulation
{
    public class PopulatableObject : MonoBehaviour
    {
        public string Id;
        public string ObjectType;
        public Rigidbody Rigidbody;

        public void Start()
        {
            var oc = FindObjectOfType<ObjectsController>();

            if (oc == null)
                throw new Exception("PopulatableObject could not find ObjectsController on stage!");

            oc.ObjectsPopulator.Register(this);
        }
    }
}