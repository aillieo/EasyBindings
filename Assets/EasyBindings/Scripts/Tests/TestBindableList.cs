// -----------------------------------------------------------------------
// <copyright file="TestBindableList.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings.Tests
{
    using AillieoUtils.EasyBindings.Collections;
    using NUnit.Framework;

    [Category("BindableList")]
    public class TestBindableList
    {
        [Test]
        public void TestAdd()
        {
            int invoke = 0;
            Binder binder = new Binder();
            BindableList<int> list = new BindableList<int>();
            binder.BindListEvent(list, e =>
            {
                Assert.AreEqual(e.type, EventType.Add);
                Assert.AreEqual(e.index, 0);
                Assert.AreEqual(list.Count, 1);
                Assert.AreEqual(list[0], 100);
                invoke++;
            });

            list.Add(100);
            Assert.AreEqual(invoke, 1);
        }

        [Test]
        public void TestRemove()
        {
            int invoke = 0;
            Binder binder = new Binder();
            BindableList<int> list = new BindableList<int>() { 1, 2, 3 };
            binder.BindListEvent(list, e =>
            {
                Assert.AreEqual(e.type, EventType.Remove);
                Assert.AreEqual(e.index, 1);
                Assert.AreEqual(list.Count, 2);
                Assert.AreEqual(list[1], 3);
                invoke++;
            });

            list.RemoveAt(1);
            Assert.AreEqual(invoke, 1);
        }

        [Test]
        public void TestUpdate()
        {
            int invoke = 0;
            Binder binder = new Binder();
            BindableList<int> list = new BindableList<int>() { 1 };
            binder.BindListEvent(list, e =>
            {
                Assert.AreEqual(e.type, EventType.Update);
                Assert.AreEqual(e.index, 0);
                Assert.AreEqual(list.Count, 1);
                Assert.AreEqual(list[0], -1);
                invoke++;
            });

            list[0] = -1;
            Assert.AreEqual(invoke, 1);
        }

        [Test]
        public void TestClear()
        {
            int invoke = 0;
            Binder binder = new Binder();
            BindableList<int> list = new BindableList<int>() { 1, 2, 3 };
            binder.BindListEvent(list, e =>
            {
                Assert.AreEqual(e.type, EventType.Clear);
                Assert.AreEqual(list.Count, 0);
                invoke++;
            });

            list.Clear();
            Assert.AreEqual(invoke, 1);
        }
    }
}
