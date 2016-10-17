using Assets.Scripts.Space2Module.Controllers.ObjectsPopulation;
using Assets.Scripts.Space2Module.Redux.State;
using NUnit.Framework;

namespace Assets.Editor.Tests.Space2Module.Controllers.ObjectsPopulation
{
    [TestFixture]
    public class ObjectDataHelperTests
    {
        [Test]
        public void CompareObjectDatasTest()
        {
            var d0 = ObjectDataHelpers.GenerateObjectData("0", 1f);
            var d01 = ObjectDataHelpers.GenerateObjectData("0", 1f);
            var d1 = ObjectDataHelpers.GenerateObjectData("1", 1f);
            var d2 = ObjectDataHelpers.GenerateObjectData("0", 2f);
            var d3 = ObjectDataHelpers.GenerateObjectData("0", 1f);
            d3.Transform = null;

            Assert.IsTrue(ObjectDataHelpers.CompareObjectDatas(null, null));
            Assert.IsTrue(ObjectDataHelpers.CompareObjectDatas(d0, d01));
            Assert.IsFalse(ObjectDataHelpers.CompareObjectDatas(d0, null));
            Assert.IsFalse(ObjectDataHelpers.CompareObjectDatas(d0, d1));
            Assert.IsFalse(ObjectDataHelpers.CompareObjectDatas(d0, d2));
            Assert.IsFalse(ObjectDataHelpers.CompareObjectDatas(d0, d3));
        }

        [Test]
        public void CompareObjectsDatasTest()
        {
            var d0 = ObjectDataHelpers.GenerateObjectDatas(3, 1f);
            var d01 = ObjectDataHelpers.GenerateObjectDatas(3, 1f);
            var d2 = ObjectDataHelpers.GenerateObjectDatas(2, 1f);
            var d3 = ObjectDataHelpers.GenerateObjectDatas(3, 2f);

            Assert.IsTrue(ObjectDataHelpers.CompareObjectsDatas(null, null));
            Assert.IsTrue(ObjectDataHelpers.CompareObjectsDatas(d0, d01));
            Assert.IsFalse(ObjectDataHelpers.CompareObjectsDatas(d0, null));
            Assert.IsFalse(ObjectDataHelpers.CompareObjectsDatas(d0, d2));
            Assert.IsFalse(ObjectDataHelpers.CompareObjectsDatas(d0, d3));
        }
    }
}