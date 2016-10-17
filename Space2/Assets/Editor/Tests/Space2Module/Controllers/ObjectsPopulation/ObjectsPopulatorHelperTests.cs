using System;
using Assets.Scripts.Space2Module.Controllers.ObjectsPopulation;
using Assets.Scripts.Space2Module.Redux.State;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.Tests.Space2Module.Controllers.ObjectsPopulation
{
    [TestFixture]
    public class ObjectsPopulatorHelperTests
    {
        #region params

        internal static float testFloatVal = 20f;
        internal static string testId = "TestId";

        internal static ObjectData od = ObjectDataHelpers.GenerateObjectData(testId, testFloatVal);

        #endregion

        [Test]
        public void PopulateTransformFromSavedTest()
        {
            var transform = new GameObject().transform;
            var tRotX = transform.rotation.x;

            ObjectsPopulatorHelper.FromData(transform, od);

            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(transform.position, od.Transform.Position), "Transform position is not same as saved!");
            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(transform.eulerAngles, od.Transform.EulerAngles), "Transform eulerAngles are not same as saved!");

            Assert.AreNotEqual(tRotX, transform.rotation.x, string.Format("Expected rotation to change, but it remained {0}!", tRotX));
        }

        [Test]
        public void PopulateRigidbodyFromSavedTest()
        {
            var rigidbody = new GameObject().AddComponent<Rigidbody>();
            ObjectsPopulatorHelper.FromData(rigidbody, od);

            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(rigidbody.angularVelocity, od.Rigidbody.angularVelocity),
                "angularVelocity is not same as saved!");
            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(rigidbody.centerOfMass, od.Rigidbody.centerOfMass),
                "centerOfMass is not same as saved!");
            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(rigidbody.velocity, od.Rigidbody.velocity),
                "velocity is not same as saved!");
            Assert.AreEqual(rigidbody.mass, od.Rigidbody.mass,
                "mass is not same as saved!");
            Assert.AreEqual(rigidbody.drag, od.Rigidbody.drag,
                "drag is not same as saved!");
            Assert.AreEqual(rigidbody.angularDrag, od.Rigidbody.angularDrag,
                "angularDrag is not same as saved!");
        }

        [Test]
        public void PopulateDataFromRigidbodyTest()
        {
            var rigidbody = new GameObject().AddComponent<Rigidbody>();
            rigidbody.angularDrag = rigidbody.drag = rigidbody.mass = testFloatVal;
            rigidbody.angularVelocity = new Vector3(testFloatVal, testFloatVal, testFloatVal);
            rigidbody.velocity = new Vector3(testFloatVal, testFloatVal, testFloatVal);
            rigidbody.centerOfMass = new Vector3(testFloatVal, testFloatVal, testFloatVal);

            var data = ObjectsPopulatorHelper.RigidbodyToData(rigidbody);

            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(rigidbody.angularVelocity, data.angularVelocity),
                "angularVelocity is not same as saved!");
            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(rigidbody.centerOfMass, data.centerOfMass),
                "centerOfMass is not same as saved!");
            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(rigidbody.velocity, data.velocity),
                "velocity is not same as saved!");
            Assert.AreEqual(rigidbody.mass, data.mass,
                "mass is not same as saved!");
            Assert.AreEqual(rigidbody.drag, data.drag,
                "drag is not same as saved!");
            Assert.AreEqual(rigidbody.angularDrag, data.angularDrag,
                "angularDrag is not same as saved!");
        }

        [Test]
        public void PopulateDataFromTransformTest()
        {
            var transform = new GameObject().transform;
            transform.rotation = new Quaternion(testFloatVal, testFloatVal, testFloatVal, testFloatVal);
            transform.position = new Vector3(testFloatVal, testFloatVal, testFloatVal);

            var data = ObjectsPopulatorHelper.TransformToData(transform);

            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(transform.position, data.Position), "Transform position is not same as saved!");
            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(transform.eulerAngles, data.EulerAngles), "Transform eulerAngles are not same as saved!");
        }

        [Test]
        public void PopulateDataFromPopulatableObjectTest()
        {
            var pop = new GameObject().AddComponent<PopulatableObject>();
            pop.Id = testId;
            var transform = pop.transform;
            transform.rotation = new Quaternion(testFloatVal, testFloatVal, testFloatVal, testFloatVal);
            transform.position = new Vector3(testFloatVal, testFloatVal, testFloatVal);

            var rigidbody = new GameObject().AddComponent<Rigidbody>();
            rigidbody.angularDrag = rigidbody.drag = rigidbody.mass = testFloatVal;
            rigidbody.angularVelocity = new Vector3(testFloatVal, testFloatVal, testFloatVal);
            rigidbody.velocity = new Vector3(testFloatVal, testFloatVal, testFloatVal);
            rigidbody.centerOfMass = new Vector3(testFloatVal, testFloatVal, testFloatVal);

            pop.Rigidbody = rigidbody;

            var data = ObjectsPopulatorHelper.PopulatableObjectToData(pop);
            Assert.AreEqual(data.Id, testId, string.Format("Got id {0} rather than expected {1}!", data.Id, testId));
            var transformData = data.Transform;
            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(transform.position, transformData.Position), "Transform position is not same as saved!");
            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(transform.eulerAngles, transformData.EulerAngles), "Transform eulerAngles are not same as saved!");

            var rigidbodyData = data.Rigidbody;

            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(rigidbody.angularVelocity, rigidbodyData.angularVelocity),
                "angularVelocity is not same as saved!");
            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(rigidbody.centerOfMass, rigidbodyData.centerOfMass),
                "centerOfMass is not same as saved!");
            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(rigidbody.velocity, rigidbodyData.velocity),
                "velocity is not same as saved!");
            Assert.AreEqual(rigidbody.mass, rigidbodyData.mass,
                "mass is not same as saved!");
            Assert.AreEqual(rigidbody.drag, rigidbodyData.drag,
                "drag is not same as saved!");
            Assert.AreEqual(rigidbody.angularDrag, rigidbodyData.angularDrag,
                "angularDrag is not same as saved!");
        }

        [Test]
        public void PopulatableObjectFromDataTest()
        {
            var pop = new GameObject().AddComponent<PopulatableObject>();
            var transform = pop.transform;
            var rigidbody = new GameObject().AddComponent<Rigidbody>();
            pop.Rigidbody = rigidbody;

            pop = ObjectsPopulatorHelper.DataToPopulatableObject(pop, od);

            Assert.AreEqual(od.Id, pop.Id, string.Format("Got id {0} rather than expected {1}!", od.Id, pop.Id));
            var transformData = od.Transform;
            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(transform.position, transformData.Position), "Transform position is not same as saved!");
            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(transform.eulerAngles, transformData.EulerAngles), "Transform eulerAngles are not same as saved!");

            var rigidbodyData = od.Rigidbody;

            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(rigidbody.angularVelocity, rigidbodyData.angularVelocity),
                "angularVelocity is not same as saved!");
            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(rigidbody.centerOfMass, rigidbodyData.centerOfMass),
                "centerOfMass is not same as saved!");
            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(rigidbody.velocity, rigidbodyData.velocity),
                "velocity is not same as saved!");
            Assert.AreEqual(rigidbody.mass, rigidbodyData.mass,
                "mass is not same as saved!");
            Assert.AreEqual(rigidbody.drag, rigidbodyData.drag,
                "drag is not same as saved!");
            Assert.AreEqual(rigidbody.angularDrag, rigidbodyData.angularDrag,
                "angularDrag is not same as saved!");
        }
    }
}