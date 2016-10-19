using System.Linq;
using Assets.Scripts.Space2Module.Controllers.ObjectsPopulation;
using Assets.Scripts.Space2Module.Redux.State;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.Tests.Space2Module.Controllers.ObjectsPopulation
{
    [TestFixture]
    public class ObjectPopulatorTests
    {
        [Test]
        public void RegisterAddsOneObjectTest()
        {
            var populator = new ObjectsPopulator();

            populator.Register(new GameObject().AddComponent<PopulatableObject>());
            populator.Register(new GameObject().AddComponent<PopulatableObject>());
            Assert.AreEqual(populator.GetObjectsData().Length, 2);
        }

        [Test]
        public void NoDoubleRegisterTest()
        {
            var populator = new ObjectsPopulator();
            var o = new GameObject().AddComponent<PopulatableObject>();
            populator.Register(o);

            var o1 = new GameObject().AddComponent<PopulatableObject>();
            o1.Id = o.Id;
            populator.Register(o1);
            Assert.AreEqual(populator.GetObjectsData().Length, 1);
        }

        [Test]
        public void PopulateFromDataByIdTest()
        {
            var populator = new ObjectsPopulator();

            var o0 = new GameObject().AddComponent<PopulatableObject>();
            o0.Rigidbody = o0.gameObject.AddComponent<Rigidbody>();
            var o1 = new GameObject().AddComponent<PopulatableObject>();
            o1.Rigidbody = o1.gameObject.AddComponent<Rigidbody>();

            var t0 = new Vector3Data() {x = 10f, y = 10f, z = 10f};
            var t1 = new Vector3Data() {x = 30f, y = 30f, z = 30f};

            populator.Register(o0);
            populator.Register(o1);

            var objectsData = populator.GetObjectsData();
            objectsData[0].Transform.Position = t0;
            objectsData[1].Transform.Position = t1;

            populator.PopulateObjectsFromData(objectsData);

            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(o0.transform.position, objectsData[0].Transform.Position),
                string.Format("Transform position of object with id {0} is not same as saved!", o0.Id));
            Assert.IsTrue(ObjectDataHelpers.CompareV3ToData(o1.transform.position, objectsData[1].Transform.Position),
                string.Format("Transform position of object with id {0} is not same as saved!", o1.Id));
        }

        [Test]
        public void ReturnExtraDataWhenPopulatingFromDataTest()
        {
            var testId = "testId";
            var testFloatVal = 20f;

            var populator = new ObjectsPopulator();

            var o0 = new GameObject().AddComponent<PopulatableObject>();
            o0.Rigidbody = o0.gameObject.AddComponent<Rigidbody>();
            var o1 = new GameObject().AddComponent<PopulatableObject>();
            o1.Rigidbody = o1.gameObject.AddComponent<Rigidbody>();

            populator.Register(o0);
            populator.Register(o1);

            var ol = populator.GetObjectsData().ToList();
            var extraObjects = populator.PopulateObjectsFromData(ol.ToArray());
            Assert.IsTrue(extraObjects.DatasWithNoPopulatables.Length == 0,
                "Populator returned unexpected qualtity of orphan objects");

            ol.Add(ObjectDataHelpers.GenerateObjectData(testId, testFloatVal));
            extraObjects = populator.PopulateObjectsFromData(ol.ToArray());

            Assert.IsTrue(extraObjects.DatasWithNoPopulatables.Length == 1,
                "Populator returned unexpected qualtity of orphan objects");
        }

        [Test]
        public void ReturnExtraPopulatablesWhenPopulatingFromDataTest()
        {
            var testId = "testId";
            var testFloatVal = 20f;

            var populator = new ObjectsPopulator();


            var o0 = new GameObject().AddComponent<PopulatableObject>();
            o0.Rigidbody = o0.gameObject.AddComponent<Rigidbody>();
            var o1 = new GameObject().AddComponent<PopulatableObject>();
            o1.Rigidbody = o1.gameObject.AddComponent<Rigidbody>();
            var o2 = new GameObject().AddComponent<PopulatableObject>();
            o2.Rigidbody = o2.gameObject.AddComponent<Rigidbody>();


            populator.Register(o0);
            populator.Register(o1);
            populator.Register(o2);

            var extraObjects = populator.PopulateObjectsFromData(populator.GetObjectsData());
            Assert.IsTrue(extraObjects.PopulatablesWithNoData.Length == 0,
                "Populator returned unexpected qualtity of orphan objects");

            extraObjects = populator.PopulateObjectsFromData(populator.GetObjectsData().Take(2).ToArray());

            Assert.IsTrue(extraObjects.PopulatablesWithNoData.Length == 1,
                "Populator returned unexpected qualtity of orphan objects");
        }

        [Test]
        public void DataIsUpdatedFomObjectsTest()
        {
            var populator = new ObjectsPopulator();

            var o0 = new GameObject().AddComponent<PopulatableObject>();
            o0.Rigidbody = o0.gameObject.AddComponent<Rigidbody>();
            var o1 = new GameObject().AddComponent<PopulatableObject>();
            o1.Rigidbody = o1.gameObject.AddComponent<Rigidbody>();

            var t0 = new Vector3() { x = 10f, y = 10f, z = 10f };
            var t1 = new Vector3() { x = 10f, y = 10f, z = 10f };

            populator.Register(o0);
            populator.Register(o1);

            var objectsData = populator.GetObjectsData();

            o0.transform.position = t0;
            o0.Rigidbody.velocity = t1;

            var dataFromObjects = populator.GetObjectsData();

            Assert.IsFalse(ObjectDataHelpers.CompareVector3Datas(dataFromObjects[0].Transform.Position, objectsData[0].Transform.Position), "Transform data was not updated from object");
            Assert.IsTrue(ObjectDataHelpers.CompareVector3Datas(dataFromObjects[1].Transform.Position, objectsData[1].Transform.Position), "Transform data was updated but object wan't?!");
            Assert.IsFalse(ObjectDataHelpers.CompareVector3Datas(dataFromObjects[0].Rigidbody.velocity, objectsData[0].Rigidbody.velocity), "Rigidbody data was not updated from object");
            Assert.IsTrue(ObjectDataHelpers.CompareVector3Datas(dataFromObjects[1].Rigidbody.velocity, objectsData[1].Rigidbody.velocity), "Rigidbody data was updated but object wan't?!");
        }
    }
}