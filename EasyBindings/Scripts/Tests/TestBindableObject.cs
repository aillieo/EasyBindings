using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace AillieoUtils.EasyBindings.Tests
{
    public class TestClass : BindableObject
    {
        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                SetStructValue(ref age, value);
            }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                SetClassValue(ref address, value);
            }
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

            IEventHandle handle = testClass.onPropertyChanged.AddListener(propName =>
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

            binder.Bind(testClass, propName =>
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

            binder.Bind(testClass, "Age", () =>
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
