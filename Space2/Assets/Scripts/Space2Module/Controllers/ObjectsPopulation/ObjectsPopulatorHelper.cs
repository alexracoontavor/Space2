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

        public static Vector3 FromData(Vector3Data data)
        {
            return new Vector3(data.x, data.y, data.z);
        }

        public static Rigidbody ApplyPhysicsFromData(Rigidbody rigidbody, PhysicsChangeRequest data)
        {
            if (data == null)
                return rigidbody;

            if (data.AngularVelocityChange != null)
                rigidbody.AddTorque(FromData(data.AngularVelocityChange));

            if (!float.IsNaN(data.ThrustChange))
                rigidbody.AddForce(rigidbody.transform.up * data.ThrustChange);

            return rigidbody;
        }

        public static Rigidbody FromData(Rigidbody rigidbody, ObjectData objectData)
        {
            rigidbody.angularVelocity = FromData(objectData.Rigidbody.angularVelocity);
            rigidbody.centerOfMass = FromData(objectData.Rigidbody.centerOfMass);
            rigidbody.velocity = FromData(objectData.Rigidbody.velocity);
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