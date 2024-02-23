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
        private int age;

        public int Age
        {
            get { return this.age; }
            set { this.SetStructValue(ref this.age, value); }
        }

        private string address;

        public string Address
        {
            get { return this.address; }
            set { this.SetClassValue(ref this.address, value); }
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

            EventHandle handle = testClass.onPropertyChangedDel.AddListener(propName =>
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
