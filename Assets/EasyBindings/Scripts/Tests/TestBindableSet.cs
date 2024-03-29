// -----------------------------------------------------------------------
// <copyright file="TestBindableSet.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings.Tests
{
    using AillieoUtils.EasyBindings.Collections;
    using NUnit.Framework;

    [Category("BindableSet")]
    public class TestBindableSet
    {
        [Test]
        public void TestAdd()
        {
            int invoke = 0;
            Binder binder = new Binder();
            BindableSet<int> set = new BindableSet<int>();
            binder.BindSetEvent(set, e =>
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
            binder.BindSetEvent(set, e =>
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
            binder.BindSetEvent(set, e =>
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
