// -----------------------------------------------------------------------
// <copyright file="TestBinder.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings.Tests
{
    using System;
    using NUnit.Framework;
    using UnityEngine.Events;

    [Category("Binder")]
    public class TestBinder
    {
        public class UnityEventInt : UnityEvent<int>
        {
        }

        private EasyDelegate easyEvent0 = new EasyDelegate();
        private EasyDelegate<int> easyEvent1 = new EasyDelegate<int>();
        private UnityEvent unityEvent0 = new UnityEvent();
        private UnityEventInt unityEvent1 = new UnityEventInt();

        private event Action csEvent0;

        private event Action<int> csEvent1;

        [Test]
        public void TestEasyEvent0()
        {
            Binder binder = new Binder();
            int counter = 0;

            binder.BindListenable(this.easyEvent0, () => counter++);

            this.easyEvent0.Invoke();
            Assert.AreEqual(counter, 1);

            binder.Dispose();

            this.easyEvent0.Invoke();
            Assert.AreEqual(counter, 1);
            Assert.AreEqual(this.easyEvent0.ListenerCount, 0);
        }

        [Test]
        public void TestEasyEvent1()
        {
            Binder binder = new Binder();
            int counter0 = 0;
            int counter1 = 0;

            binder.BindListenable(this.easyEvent0, () => counter0++);

            this.easyEvent0.Invoke();
            Assert.AreEqual(counter0, 1);

            binder.BindListenable(this.easyEvent0, () => counter1++);
            binder.BindListenable(this.easyEvent1, n => counter0 += n);

            this.easyEvent1.Invoke(5);
            Assert.AreEqual(counter0, 6);
            Assert.AreEqual(counter1, 0);

            this.easyEvent0.Invoke();
            Assert.AreEqual(counter0, 7);
            Assert.AreEqual(counter1, 1);

            binder.Dispose();

            this.easyEvent0.Invoke();
            this.easyEvent1.Invoke(5);
            Assert.AreEqual(counter0, 7);
            Assert.AreEqual(counter1, 1);
            Assert.AreEqual(this.easyEvent0.ListenerCount, 0);
            Assert.AreEqual(this.easyEvent1.ListenerCount, 0);
        }

        [Test]
        public void TestUnityEvent0()
        {
            Binder binder = new Binder();
            int counter = 0;

            binder.BindUnityEvent(this.unityEvent0, () => counter++);

            this.unityEvent0.Invoke();
            Assert.AreEqual(counter, 1);

            binder.Dispose();

            this.unityEvent0.Invoke();
            Assert.AreEqual(counter, 1);
        }

        [Test]
        public void TestUnityEvent1()
        {
            Binder binder = new Binder();
            int counter0 = 0;
            int counter1 = 0;

            binder.BindUnityEvent(this.unityEvent0, () => counter0++);

            this.unityEvent0.Invoke();
            Assert.AreEqual(counter0, 1);

            binder.BindUnityEvent(this.unityEvent0, () => counter1++);
            binder.BindUnityEvent(this.unityEvent1, n => counter0 += n);

            this.unityEvent1.Invoke(5);
            Assert.AreEqual(counter0, 6);
            Assert.AreEqual(counter1, 0);

            this.unityEvent0.Invoke();
            Assert.AreEqual(counter0, 7);
            Assert.AreEqual(counter1, 1);

            binder.Dispose();

            this.unityEvent0.Invoke();
            this.unityEvent1.Invoke(5);
            Assert.AreEqual(counter0, 7);
            Assert.AreEqual(counter1, 1);
        }

        [Test]
        public void TestCSEvent0()
        {
            Binder binder = new Binder();
            int counter = 0;

            Action action = () => counter++;
            this.csEvent0 += action;
            binder.RegisterCustomCleanupAction(() => this.csEvent0 -= action);

            this.csEvent0?.Invoke();
            Assert.AreEqual(counter, 1);

            binder.Dispose();

            this.csEvent0?.Invoke();
            Assert.AreEqual(counter, 1);
        }

        [Test]
        public void TestCSEvent1()
        {
            Binder binder = new Binder();
            int counter0 = 0;
            int counter1 = 0;

            Action action0 = () => counter0++;
            this.csEvent0 += action0;
            binder.RegisterCustomCleanupAction(() => this.csEvent0 -= action0);

            this.csEvent0?.Invoke();
            Assert.AreEqual(counter0, 1);

            Action action1 = () => counter1++;
            this.csEvent0 += action1;
            binder.RegisterCustomCleanupAction(() => this.csEvent0 -= action1);
            Action<int> action2 = n => counter0 += n;
            this.csEvent1 += action2;
            binder.RegisterCustomCleanupAction(() => this.csEvent1 -= action2);

            this.csEvent1?.Invoke(5);
            Assert.AreEqual(counter0, 6);
            Assert.AreEqual(counter1, 0);

            this.csEvent0?.Invoke();
            Assert.AreEqual(counter0, 7);
            Assert.AreEqual(counter1, 1);

            binder.Dispose();

            this.csEvent0?.Invoke();
            this.csEvent1?.Invoke(5);
            Assert.AreEqual(counter0, 7);
            Assert.AreEqual(counter1, 1);
        }
    }
}
