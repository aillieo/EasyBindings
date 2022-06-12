using NUnit.Framework;
using System;
using UnityEngine.Events;

namespace AillieoUtils.EasyBindings.Tests
{
    [Category("Binder")]
    public class TestBinder
    {
        public class UnityEventInt : UnityEvent<int> { }

        private Event easyEvent0 = new Event();
        private Event<int> easyEvent1 = new Event<int>();
        private UnityEvent unityEvent0 = new UnityEvent();
        private UnityEventInt unityEvent1 = new UnityEventInt();
        private event Action csEvent0;
        private event Action<int> csEvent1;

        [Test]
        public void TestEasyEvent0()
        {
            Binder binder = new Binder();
            int counter = 0;

            binder.Bind(easyEvent0, () => counter++);

            easyEvent0.Invoke();
            Assert.AreEqual(counter, 1);

            binder.Dispose();

            easyEvent0.Invoke();
            Assert.AreEqual(counter, 1);
            Assert.AreEqual(easyEvent0.ListenerCount, 0);
        }

        [Test]
        public void TestEasyEvent1()
        {
            Binder binder = new Binder();
            int counter0 = 0;
            int counter1 = 0;

            binder.Bind(easyEvent0, () => counter0++);

            easyEvent0.Invoke();
            Assert.AreEqual(counter0, 1);

            binder.Bind(easyEvent0, () => counter1++);
            binder.Bind(easyEvent1, n => counter0 += n);

            easyEvent1.Invoke(5);
            Assert.AreEqual(counter0, 6);
            Assert.AreEqual(counter1, 0);

            easyEvent0.Invoke();
            Assert.AreEqual(counter0, 7);
            Assert.AreEqual(counter1, 1);

            binder.Dispose();

            easyEvent0.Invoke();
            easyEvent1.Invoke(5);
            Assert.AreEqual(counter0, 7);
            Assert.AreEqual(counter1, 1);
            Assert.AreEqual(easyEvent0.ListenerCount, 0);
            Assert.AreEqual(easyEvent1.ListenerCount, 0);
        }

        [Test]
        public void TestUnityEvent0()
        {
            Binder binder = new Binder();
            int counter = 0;

            binder.Bind(unityEvent0, () => counter++);

            unityEvent0.Invoke();
            Assert.AreEqual(counter, 1);

            binder.Dispose();

            unityEvent0.Invoke();
            Assert.AreEqual(counter, 1);
        }

        [Test]
        public void TestUnityEvent1()
        {
            Binder binder = new Binder();
            int counter0 = 0;
            int counter1 = 0;

            binder.Bind(unityEvent0, () => counter0++);

            unityEvent0.Invoke();
            Assert.AreEqual(counter0, 1);

            binder.Bind(unityEvent0, () => counter1++);
            binder.Bind(unityEvent1, n => counter0 += n);

            unityEvent1.Invoke(5);
            Assert.AreEqual(counter0, 6);
            Assert.AreEqual(counter1, 0);

            unityEvent0.Invoke();
            Assert.AreEqual(counter0, 7);
            Assert.AreEqual(counter1, 1);

            binder.Dispose();

            unityEvent0.Invoke();
            unityEvent1.Invoke(5);
            Assert.AreEqual(counter0, 7);
            Assert.AreEqual(counter1, 1);
        }

        [Test]
        public void TestCSEvent0()
        {
            Binder binder = new Binder();
            int counter = 0;

            Action action = () => counter++;
            csEvent0 += action;
            binder.RegisterCustomCleanupAction(() => csEvent0 -= action);

            csEvent0?.Invoke();
            Assert.AreEqual(counter, 1);

            binder.Dispose();

            csEvent0?.Invoke();
            Assert.AreEqual(counter, 1);
        }

        [Test]
        public void TestCSEvent1()
        {
            Binder binder = new Binder();
            int counter0 = 0;
            int counter1 = 0;

            Action action0 = () => counter0++;
            csEvent0 += action0;
            binder.RegisterCustomCleanupAction(() => csEvent0 -= action0);

            csEvent0?.Invoke();
            Assert.AreEqual(counter0, 1);

            Action action1 = () => counter1++;
            csEvent0 += action1;
            binder.RegisterCustomCleanupAction(() => csEvent0 -= action1);
            Action<int> action2 = n => counter0 += n;
            csEvent1 += action2;
            binder.RegisterCustomCleanupAction(() => csEvent1 -= action2);

            csEvent1?.Invoke(5);
            Assert.AreEqual(counter0, 6);
            Assert.AreEqual(counter1, 0);

            csEvent0?.Invoke();
            Assert.AreEqual(counter0, 7);
            Assert.AreEqual(counter1, 1);

            binder.Dispose();

            csEvent0?.Invoke();
            csEvent1?.Invoke(5);
            Assert.AreEqual(counter0, 7);
            Assert.AreEqual(counter1, 1);
        }
    }
}
