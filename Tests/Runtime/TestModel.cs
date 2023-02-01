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
        #region Abstract
        public abstract void CreateTestContext();
        public abstract void DestroyTestContext();
        #endregion
        #region Methods
        [SetUp]
        public void Setup()
        {
            CreateTestContext();
        }

        [TearDown]
        public void TearDown()
        {
            DestroyTestContext();
            Mock.Clear();
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