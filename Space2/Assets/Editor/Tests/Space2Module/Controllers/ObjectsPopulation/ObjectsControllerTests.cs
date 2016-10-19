using System.Diagnostics;
using System.Linq;
using Assets.Editor.Tests.Space2Module.Redux.Actions;
using Assets.Infrastructure.CoreTools.Extensions;
using Assets.Scripts.Space2Module.Controllers.ObjectsPopulation;
using NUnit.Framework;
using UniRx;
using UnityEngine;

namespace Assets.Editor.Tests.Space2Module.Controllers.ObjectsPopulation
{
    [TestFixture]
    public class ObjectsControllerTests : ActionsTestBase
    {
        [Test]
        public void IsTickerTickingTest()
        {
            Scheduler.DefaultSchedulers.SetDotNetCompatible();

            var tickDuration = 1;
            var numTicks = 5;

            var ts = ObjectsController.TickedStream(tickDuration);
            var xs = ts
                .Take(numTicks)
                .Timestamp()
                .ToArray()
                .Wait();

            Assert.AreNotEqual(xs.Length, numTicks - 1, "Ticks # same as numTicks-1");
            Assert.AreEqual(xs.Length, numTicks, "Ticks are result of ticks not same length!");
        }

        [Test]
        public void IsEmptyObjectsControllerCallingActionsCreator()
        {
            var subj = new Subject<long>();
            var dispose = ObjectsController.SubscribeToGenerateObjectUpdates(subj, new ObjectsPopulator());
            var numTicks = 3;
            Enumerable.Range(0, numTicks).ForEach(i => subj.OnNext(i));

            Assert.AreEqual(Actions.Count, 0, string.Format("Got {0} actions for {1} ticks!", Actions.Count, 0));

            dispose.Dispose();
        }

        [Test]
        public void IsNonEmptyObjectsControllerCallingActionsCreator()
        {
            var subj = new Subject<long>();
            var populator = new ObjectsPopulator();
            populator.Register(new GameObject().AddComponent<PopulatableObject>());

            var dispose = ObjectsController.SubscribeToGenerateObjectUpdates(subj, populator);
            var numTicks = 3;
            Enumerable.Range(0, numTicks).ForEach(i => subj.OnNext(i));

            Assert.AreEqual(Actions.Count, numTicks, string.Format("Got {0} actions for {1} ticks!", Actions.Count, numTicks));

            dispose.Dispose();
        }


    }
}