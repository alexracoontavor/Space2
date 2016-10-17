using Assets.Scripts.Space2Module.Redux.State;
using UnityEngine;

namespace Assets.Scripts.Space2Module.Controllers.ObjectsPopulation
{
    public static class ObjectsPopulatorHelper
    {
        public static Transform FromData(Transform transform, ObjectData objectData)
        {
            transform.position = new Vector3(objectData.Transform.Position.x, objectData.Transform.Position.y, objectData.Transform.Position.z);
            transform.eulerAngles = new Vector3(objectData.Transform.EulerAngles.x, objectData.Transform.EulerAngles.y, objectData.Transform.EulerAngles.z);
            return transform;
        }

        public static Rigidbody FromData(Rigidbody rigidbody, ObjectData objectData)
        {
            rigidbody.angularVelocity = new Vector3(objectData.Rigidbody.angularVelocity.x, objectData.Rigidbody.angularVelocity.y, objectData.Rigidbody.angularVelocity.z);
            rigidbody.centerOfMass = new Vector3(objectData.Rigidbody.centerOfMass.x, objectData.Rigidbody.centerOfMass.y, objectData.Rigidbody.centerOfMass.z);
            rigidbody.velocity = new Vector3(objectData.Rigidbody.velocity.x, objectData.Rigidbody.velocity.y, objectData.Rigidbody.velocity.z);
            rigidbody.drag = objectData.Rigidbody.drag;
            rigidbody.angularDrag = objectData.Rigidbody.angularDrag;
            rigidbody.mass = objectData.Rigidbody.mass;
            return rigidbody;
        }

        public static Vector3Data Vector3ToData(Vector3 v)
        {
            return new Vector3Data()
            {
                x = v.x,
                y = v.y,
                z = v.z
            };
        }

        public static TransformData TransformToData(Transform transform)
        {
            return new TransformData()
            {
                EulerAngles = new Vector3Data()
                {
                    x = transform.eulerAngles.x,
                    y = transform.eulerAngles.y,
                    z = transform.eulerAngles.z
                },
                Position = new Vector3Data()
                {
                    x = transform.position.x,
                    y = transform.position.y,
                    z = transform.position.z
                }
            };
        }

        public static RigidbodyData RigidbodyToData(Rigidbody rigidbody)
        {
            return new RigidbodyData()
            {
                angularVelocity = Vector3ToData(rigidbody.angularVelocity),
                centerOfMass = Vector3ToData(rigidbody.centerOfMass),
                velocity = Vector3ToData(rigidbody.velocity),
                drag = rigidbody.drag,
                angularDrag = rigidbody.angularDrag,
                mass = rigidbody.mass
            };
        }

        public static ObjectData PopulatableObjectToData(PopulatableObject obj)
        {
            return new ObjectData
            {
                Id = obj.Id,
                Rigidbody = obj.Rigidbody == null ? null : RigidbodyToData(obj.Rigidbody),
                Transform = TransformToData(obj.transform)
            };
        }

        public static PopulatableObject DataToPopulatableObject(PopulatableObject obj, ObjectData objectData)
        {
            obj.Id = objectData.Id;
            FromData(obj.Rigidbody, objectData);
            FromData(obj.transform, objectData);
            return obj;
        }
    }
}