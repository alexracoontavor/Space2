using System;
using System.Linq;
using Assets.Scripts.Space2Module.Redux.State;
using UnityEngine;

namespace Assets.Scripts.Space2Module.Controllers.ObjectsPopulation
{
    public static class ObjectDataHelpers
    {
        public static bool CompareV3ToData(Vector3 v3, Vector3Data v3d)
        {
            return Math.Abs(Math.Round(v3d.x) - Math.Round(v3.x)) < .01
                   && Math.Abs(Math.Round(v3d.y) - Math.Round(v3.y)) < .01
                   && Math.Abs(Math.Round(v3d.z) - Math.Round(v3.z)) < .01;
        }

        public static bool CompareObjectsDatas(ObjectData[] d0, ObjectData[] d1)
        {
            return
                (d0 == null && d1 == null)
                || (
                    d0 != null
                    && d1 != null
                    &&CompareNonNullObjectsDatas(d0, d1)
                    );
        }

        private static bool CompareNonNullObjectsDatas(ObjectData[] d0, ObjectData[] d1)
        {
            if (d0.Length != d1.Length)
                return false;

            var pairs = d0
                .Select(do1 => new[]
                {
                    do1,
                    d1.FirstOrDefault(do2 => do2.Id == do1.Id)
                }).ToArray();

            return pairs.All(objectDatasPair => CompareObjectDatas(objectDatasPair[0], objectDatasPair[1]));
        }

        public static bool CompareObjectDatas(ObjectData d0, ObjectData d1)
        {
            return
                (d0 == null && d1 == null)
                || (
                    d0 != null
                    && d1 != null
                    && d0.Id == d1.Id
                    && d0.ObjectType == d1.ObjectType
                    && CompareTransformDatas(d0.Transform, d1.Transform)
                    && CompareRigidbodyDatas(d0.Rigidbody, d1.Rigidbody)
                    );
        }

        public static bool CompareTransformDatas(TransformData d0, TransformData d1)
        {
            return 
                (d0 == null && d1 == null)
                || (
                    d0 != null
                    && d1 != null
                    && CompareVector3Datas(d0.Position, d1.Position)
                    && CompareVector3Datas(d0.EulerAngles, d1.EulerAngles)
                    );
        }

        public static bool CompareRigidbodyDatas(RigidbodyData d0, RigidbodyData d1)
        {
            var floatTolerance = 0.001f;

            return
                (d0 == null && d1 == null)
                || (
                    d0 != null
                    && d1 != null
                    && Math.Abs(d0.angularDrag - d1.angularDrag) < floatTolerance
                    && Math.Abs(d0.drag - d1.drag) < floatTolerance
                    && Math.Abs(d0.mass - d1.mass) < floatTolerance
                    && CompareVector3Datas(d0.angularVelocity, d1.angularVelocity)
                    && CompareVector3Datas(d0.centerOfMass, d1.centerOfMass)
                    && CompareVector3Datas(d0.velocity, d1.velocity)
                    );
        }

        public static bool CompareVector3Datas(Vector3Data d0, Vector3Data d1)
        {
            var floatTolerance = 0.001f;

            return
                (d0 == null && d1 == null)
                || (
                    d0 != null
                    && d1 != null
                    && Math.Abs(d0.x - d1.x) < floatTolerance
                    && Math.Abs(d0.y - d1.y) < floatTolerance
                    && Math.Abs(d0.z - d1.z) < floatTolerance
                    );

        }

        public static Space2State GenerateSpace2State(int numObjects, float floatTestVal, int timelineDepth)
        {

            return new Space2State
            {
                Timeline = GenerateTimeline(numObjects, floatTestVal, timelineDepth),
                DataSaveRequest = new DataSaveRequest { FileName = "noSuchFile", Progress = 1f}
            };
        }

        public static ObjectsTimeline GenerateTimeline(int numObjects, float floatTestVal, int timelineDepth)
        {
            var objects = GenerateObjectDatas(numObjects, floatTestVal);

            var timeline = Enumerable
                .Range(0, timelineDepth)
                .Select(i => objects.Concat(GenerateObjectDatas(i + 1, floatTestVal)).ToArray())
                .ToArray();

            var currentIndex = Math.Max(0, timelineDepth - 2);

            return new ObjectsTimeline
            {
                CurrentIndex = currentIndex,
                Timeline = timeline,
                CurrentObjects = timeline[currentIndex]
            };
        }

        public static ObjectData[] GenerateObjectDatas(int numObjects, float floatTestVal)
        {
            return Enumerable
                .Range(0, numObjects)
                .Select(i => GenerateObjectData(i.ToString(), floatTestVal))
                .ToArray();
        }

        public static ObjectData GenerateObjectData(string testId, float testFloatVal, string objectType = "Default")
        {
            return new ObjectData()
            {
                Id = testId,
                ObjectType = objectType,
                Rigidbody = new RigidbodyData()
                {
                    angularVelocity = new Vector3Data()
                    {
                        x = testFloatVal,
                        y = testFloatVal,
                        z = testFloatVal
                    },
                    velocity = new Vector3Data()
                    {
                        x = testFloatVal,
                        y = testFloatVal,
                        z = testFloatVal
                    },
                    centerOfMass = new Vector3Data()
                    {
                        x = testFloatVal,
                        y = testFloatVal,
                        z = testFloatVal
                    },
                    angularDrag = testFloatVal,
                    drag = testFloatVal,
                    mass = testFloatVal
                },
                Transform = new TransformData()
                {
                    Position = new Vector3Data()
                    {
                        x = testFloatVal,
                        y = testFloatVal,
                        z = testFloatVal
                    },
                    EulerAngles = new Vector3Data()
                    {
                        x = testFloatVal,
                        y = testFloatVal,
                        z = testFloatVal
                    }
                }
            };
        }
    }
}