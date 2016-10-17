using Assets.Infrastructure.Architecture.Modulux;
using NUnit.Framework;

namespace Assets.Editor.Tests.Space2Module.Integration.ObjectsSandbox.Setup
{
    public class SetupTests
    {
        [TestFixture]
        public class ReloadTests
        {
            [Test]
            public void ReloadGeneratesNewState()
            {
                ModuluxRoot.Store = null;
                Scripts.Space2Module.Integration.ObjectsSandbox.Setup.Setup.Reset();
                Assert.IsNotNull(ModuluxRoot.Store);
            }
        }
    }
}