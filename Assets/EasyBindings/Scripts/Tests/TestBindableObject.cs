// -----------------------------------------------------------------------
// <copyright file="TestBindableObject.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings.Tests
{
    using NUnit.Framework;

    public class TestClass : BindableObject
    {
        [NotifyOnValueChanged]
        public int Age
        {
            get;
            set;
        }

        [NotifyOnValueChanged]
        public string Address
        {
            get;
            set;
        }
    }

    [Category("BindableObject")]
    public class TestBindableObject
    {
        [Test]
        public void TestValueChange()
        {
            TestClass testClass = new TestClass();
            int counter = 0;

            EventHandle handle = testClass.onPropertyChanged.AddListener(propName =>
            {
                counter++;
                Assert.AreEqual(propName, "Age");
            });

            testClass.Age = 1;
            Assert.AreEqual(counter, 1);

            handle.Unlisten();
        }

        [Test]
        public void TestBinder0()
        {
            TestClass testClass = new TestClass();
            Binder binder = new Binder();
            int counter = 0;

            binder.BindObjectChange(testClass, propName =>
            {
                counter++;
                Assert.AreEqual(propName, "Age");
            });

            testClass.Age = 1;
            Assert.AreEqual(counter, 1);

            binder.Dispose();
        }

        [Test]
        public void TestBinder1()
        {
            TestClass testClass = new TestClass();
            Binder binder = new Binder();
            int counter = 0;

            binder.BindObjectChange(testClass, "Age", () =>
            {
                counter++;
            });

            testClass.Age = 1;
            Assert.AreEqual(counter, 1);

            testClass.Address = "SomeValue";
            Assert.AreEqual(counter, 1);

            binder.Dispose();
        }
    }
}
