using System;
using NUnit.Framework;
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
        #region Abstract Methods
        [SetUp] public abstract void CreateTestContext();
        [TearDown] public abstract void DestroyTestContext();
        #endregion
    }

    public abstract class TestModel<T>: TestModel
    {
        #region Variables
        protected T modelReference;
        #endregion
    }
}