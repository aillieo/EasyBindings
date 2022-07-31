using AillieoUtils.EasyBindings.Collections;
using NUnit.Framework;
using System;

namespace AillieoUtils.EasyBindings.Tests
{

    [Category("BindableSet")]
    public class TestBindableSet
    {
        [Test]
        public void TestAdd()
        {
            int invoke = 0;
            Binder binder = new Binder();
            BindableSet<int> set = new BindableSet<int>();
            binder.BindSet(set, e =>
            {
                Assert.AreEqual(e.type, EventType.Add);
                Assert.AreEqual(set.Count, 1);
                invoke++;
            });

            set.Add(0);
            Assert.AreEqual(invoke, 1);

            set.Add(0);
            Assert.AreEqual(invoke, 1);
        }

        [Test]
        public void TestRemove()
        {
            int invoke = 0;
            Binder binder = new Binder();
            BindableSet<int> set = new BindableSet<int>() { 1, 2, 3 };
            binder.BindSet(set, e =>
            {
                Assert.AreEqual(e.type, EventType.Remove);
                Assert.AreEqual(set.Count, 2);
                invoke++;
            });

            set.Remove(4);
            Assert.AreEqual(invoke, 0);

            set.Remove(3);
            Assert.AreEqual(invoke, 1);

            set.Remove(3);
            Assert.AreEqual(invoke, 1);
        }

        [Test]
        public void TestClear()
        {
            int invoke = 0;
            Binder binder = new Binder();
            BindableSet<int> set = new BindableSet<int>() { 1, 2, 3 };
            binder.BindSet(set, e =>
            {
                Assert.AreEqual(e.type, EventType.Clear);
                invoke++;
            });

            set.Clear();
            Assert.AreEqual(invoke, 1);

            set.Clear();
            Assert.AreEqual(invoke, 1);
        }
    }
}
