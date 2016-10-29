using System;
using Assets.Infrastructure.Architecture.Modulux.State;

namespace Assets.Scripts.Space2Module.Redux.State
{
    [Serializable]
    public class Space2State : BaseState
    {
        public DataSaveRequest DataSaveRequest;
        public ObjectsTimeline Timeline;

        public Space2State() : base(null, null)
        {
        }
    }

    [Serializable]
    public class DataSaveRequest
    {
        public float Progress;
        public string FileName;
    }

    [Serializable]
    public class ObjectsTimeline
    {
        public float GameSpeed = 1f;
        public bool IsWaitingToUpdateObjects = false;
        public int CurrentIndex;
        public ObjectData[] CurrentObjects;
        public ObjectData[][] Timeline;
    }

    [Serializable]
    public class ObjectData
    {
        public string Id;
        public string ObjectType;
        public TransformData Transform;
        public RigidbodyData Rigidbody;
    }

    [Serializable]
    public class RigidbodyData
    {
        public Vector3Data angularVelocity;
        public Vector3Data centerOfMass;
        public Vector3Data velocity;
        public float mass;
        public float drag;
        public float angularDrag;
    }

    [Serializable]
    public class TransformData
    {
        public Vector3Data Position;
        public Vector3Data EulerAngles;
    }

    [Serializable]
    public class Vector3Data
    {
        public float x;
        public float y;
        public float z;
    }

    [Serializable]
    public class QuaternionData
    {
        public float x;
        public float y;
        public float z;
        public float w;
    }
}