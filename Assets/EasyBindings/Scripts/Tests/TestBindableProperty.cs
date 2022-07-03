using NUnit.Framework;
using System;
using UnityEngine;

namespace AillieoUtils.EasyBindings.Tests
{
    [Category("BindableProperty")]
    public class TestBindableProperty
    {
        [Test]
        public void TestValueChange()
        {
            BindableProperty<int> intProperty = new BindableProperty<int>(0);
            int counter = 0;

            Assert.AreEqual(intProperty.CurrentValue, 0);

            IEventHandle handle = intProperty.onValueChanged.AddListener(change =>
            {
                counter++;
            });

            intProperty.Next(1);
            Assert.AreEqual(intProperty.CurrentValue, 1);
            Assert.AreEqual(counter, 1);

            intProperty.Next(1);
            Assert.AreEqual(intProperty.CurrentValue, 1);
            Assert.AreEqual(counter, 1);

            intProperty.Next(0);
            Assert.AreEqual(intProperty.CurrentValue, 0);
            Assert.AreEqual(counter, 2);

            handle.Unlisten();

            intProperty.Next(100);
            Assert.AreEqual(intProperty.CurrentValue, 100);
            Assert.AreEqual(counter, 2);
        }

        [Test]
        public void TestValueChangeArg()
        {
            BindableProperty<int> intProperty = new BindableProperty<int>(0);
            int oldValue = 0;
            int newValue = 0;

            Assert.AreEqual(intProperty.CurrentValue, oldValue);

            IEventHandle handle = intProperty.onValueChanged.AddListener(change =>
            {
                Assert.AreEqual(oldValue, change.oldValue);
                Assert.AreEqual(newValue, change.nextValue);
            });

            newValue = 1;
            intProperty.Next(1);

            oldValue = newValue;
            newValue = 2;
            intProperty.Next(2);

            handle.Unlisten();
        }

        [Test]
        public void TestEqualityComparer()
        {
            BindableProperty<string> stringProperty = new BindableProperty<string>(
                string.Empty,
                StringComparer.InvariantCultureIgnoreCase);
            int counter = 0;

            Assert.AreEqual(stringProperty.CurrentValue, string.Empty);

            IEventHandle handle = stringProperty.onValueChanged.AddListener(change => counter++);

            stringProperty.Next("SomeValue");
            Assert.AreEqual(counter, 1);
            Assert.AreEqual("SomeValue", stringProperty.CurrentValue);

            stringProperty.Next("someValue");
            Assert.AreEqual(counter, 1);
            Assert.AreEqual("SomeValue", stringProperty.CurrentValue);

            stringProperty.Next("some_value");
            Assert.AreEqual(counter, 2);
            Assert.AreEqual("some_value", stringProperty.CurrentValue);

            stringProperty.Next("someValue");
            Assert.AreEqual(counter, 3);
            Assert.AreEqual("someValue", stringProperty.CurrentValue);
        }

        [Test]
        public void TestBinder()
        {
            BindableProperty<int> intProperty = new BindableProperty<int>(0);
            Binder binder = new Binder();
            int counter = 0;

            Assert.AreEqual(intProperty.CurrentValue, 0);

            binder.BindPropertyChange(intProperty, change => counter++);

            intProperty.Next(1);
            Assert.AreEqual(intProperty.CurrentValue, 1);
            Assert.AreEqual(counter, 1);

            intProperty.Next(1);
            Assert.AreEqual(intProperty.CurrentValue, 1);
            Assert.AreEqual(counter, 1);

            intProperty.Next(0);
            Assert.AreEqual(intProperty.CurrentValue, 0);
            Assert.AreEqual(counter, 2);

            binder.Dispose();

            intProperty.Next(100);
            Assert.AreEqual(intProperty.CurrentValue, 100);
            Assert.AreEqual(counter, 2);
        }
    }
}
