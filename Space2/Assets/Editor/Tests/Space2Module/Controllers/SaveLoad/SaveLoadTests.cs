using System;
using Assets.Infrastructure.CoreTools.Instanced.FileSystemAccessor;
using Assets.Scripts.Space2Module.Controllers.ObjectsPopulation;
using Assets.Scripts.Space2Module.Controllers.SaveLoad;
using Assets.Scripts.Space2Module.Redux.State;
using NUnit.Framework;
using UniRx;
using UnityEngine;

namespace Assets.Editor.Tests.Space2Module.Controllers.SaveLoad
{
    public class SaveLoadTests
    {
        [TestFixture]
        public class SaveTests
        {
            #region params
            internal static float testFloatVal = 20f;
            internal static string filename = "ActionsTestsObjectsAreSavedTest";

            internal static ObjectData od = new ObjectData()
            {
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
            #endregion

            [Test]
            public void ObjectsAreSavedAndLoaded()
            {
                SerializedDataImporter.DeleteSaved(filename);

                var numObjects = 3;
                var floatTestVal = 3f;
                var timelineDepth = 3;

                var subject = new Subject<Space2State>();
                var controller = SaveLoadController.InitSaveStream(subject.AsObservable());
                subject.OnNext(new Space2State());
                var s = ObjectDataHelpers.GenerateSpace2State(numObjects, floatTestVal, timelineDepth);
                s.DataSaveRequest.FileName = filename;
                subject.OnNext(s);

                var loadedData = SerializedDataImporter.LoadData<Space2State>(filename);

                Assert.IsNotNull(loadedData, "Could not load data!");
                Assert.AreEqual(loadedData.Timeline.Timeline.Length, timelineDepth, string.Format("Data contained {0} rather than {1} timeline items!", loadedData.Timeline.Timeline.Length, timelineDepth));

                controller.Dispose();

                SerializedDataImporter.DeleteSaved(filename);
            }
        }
    }
}