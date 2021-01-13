using System;
using DynamicDispatchDemo;
using NUnit.Framework;

namespace DynamicKeywordDemo.Tests
{
    public class CastingTests
    {
        private SomeClass _someClass;

        [SetUp]
        public void SetUp()
        {
            _someClass = new SomeClass(new SpecificType());
        }

        [Test]
        public void CastingModelToDynamicSucceeds()
        {
            Assert.DoesNotThrow(
                () => Dependency.MethodThatNeedsToKnowT((dynamic)_someClass.Model, typeof(SpecificType)),
                "When the dynamic keyword is used, the runtime directed to use reflection at runtime to determine the specific type of Model from the variable metadata passed to the signature.");
        }

        [Test]
        public void NotCastingModelToDynamicFails()
        {
            var exception = Assert.Throws<InvalidOperationException>(
                () => Dependency.MethodThatNeedsToKnowT(_someClass.Model, typeof(SpecificType)),
                "If we don't cast the argument to (dynamic), the runtime does not use reflection. Below is an exception caused by the methods inability to infer the type of T model.");

            Assert.AreEqual(
                "The type of T wasn't specific enough. It should be SpecificType, but was DynamicDispatchDemo.ISomeInterface.",
                exception.Message);
        }
    }
}