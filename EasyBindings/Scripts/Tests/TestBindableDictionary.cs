using AillieoUtils.EasyBindings.Collections;
using NUnit.Framework;
using System;

namespace AillieoUtils.EasyBindings.Tests
{

    [Category("BindableDictionary")]
    public class TestBindableDictionary
    {
        [Test]
        public void TestAdd()
        {
            int invoke = 0;
            Binder binder = new Binder();
            BindableDictionary<int, int> dict = new BindableDictionary<int, int>();
            binder.BindDictionaryEvent(dict, e =>
            {
                Assert.AreEqual(e.type, EventType.Add);
                Assert.AreEqual(e.key, 1);
                Assert.AreEqual(dict.Count, 1);
                Assert.AreEqual(dict[1], 1);
                invoke++;
            });

            dict.Add(1, 1);
            Assert.AreEqual(invoke, 1);

            binder.Dispose();

            binder.BindDictionaryEvent(dict, e =>
            {
                Assert.AreEqual(e.type, EventType.Add);
                Assert.AreEqual(e.key, 2);
                Assert.AreEqual(dict.Count, 2);
                Assert.AreEqual(dict[2], 2);
                invoke++;
            });

            dict[2] = 2;
            Assert.AreEqual(invoke, 2);
        }

        [Test]
        public void TestRemove()
        {
            int invoke = 0;
            Binder binder = new Binder();
            BindableDictionary<int, int> dict = new BindableDictionary<int, int>() { { 1, 1 }, { 2, 2 } };
            binder.BindDictionaryEvent(dict, e =>
            {
                Assert.AreEqual(e.type, EventType.Remove);
                Assert.AreEqual(e.key, 1);
                Assert.AreEqual(dict.Count, 1);
                Assert.False(dict.ContainsKey(1));
                Assert.True(dict.ContainsKey(2));
                invoke++;
            });

            dict.Remove(3);
            Assert.AreEqual(invoke, 0);
            dict.Remove(1);
            Assert.AreEqual(invoke, 1);
        }

        [Test]
        public void TestUpdate()
        {
            int invoke = 0;
            Binder binder = new Binder();
            BindableDictionary<int, int> dict = new BindableDictionary<int, int>() { { 1, 1 }, { 2, 2 } };
            binder.BindDictionaryEvent(dict, e =>
            {
                Assert.AreEqual(e.type, EventType.Update);
                if (e.key == 1)
                {
                    Assert.AreEqual(dict[1], -1);
                }
                else if (e.key == 2)
                {
                    Assert.AreEqual(dict[2], -2);
                }

                invoke++;
            });

            dict[1] = -1;
            Assert.AreEqual(invoke, 1);

            dict[2] = -2;
            Assert.AreEqual(invoke, 2);
        }

        [Test]
        public void TestClear()
        {
            int invoke = 0;
            Binder binder = new Binder();
            BindableDictionary<int, int> dict = new BindableDictionary<int, int>() { { 1, 1 }, { 2, 2 } };
            binder.BindDictionaryEvent(dict, e =>
            {
                Assert.AreEqual(e.type, EventType.Clear);
                invoke++;
            });

            dict.Clear();
            Assert.AreEqual(invoke, 1);
            dict.Clear();
            Assert.AreEqual(invoke, 1);
        }
    }
}
