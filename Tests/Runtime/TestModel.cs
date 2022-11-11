using System;
using NSubstitute;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public abstract class TestModel
    {
        #region Constants
        protected const string NotImplemented = "TEST NOT IMPLEMENTED";
        protected const string IncompleteImplementation = "INCOMPLETE TEST";
        #endregion
        #region Variables
        protected Mock Mock;
        #endregion
        #region Abstract
        public abstract void CreateTestContext();
        public abstract void DestroyTestContext();
        #endregion
        #region Methods
        [SetUp]
        public void Setup()
        {
            Mock = new Mock();
            CreateTestContext();
        }

        [TearDown]
        public void TearDown()
        {
            DestroyTestContext();
            Mock.Dispose();
        }

        public T Value<T>(T value, out T output)
        {
            output = value;
            return value;
        }

        public T ValueSubstitute<T>(out T output) where T: class
        {
            output = Substitute.For<T>();
            return output;
        }
        #endregion
    }

    public abstract class TestModel<T>: TestModel
    {
        #region Variables
        protected T modelReference;
        #endregion
    }
}