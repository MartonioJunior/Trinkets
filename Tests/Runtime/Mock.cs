using UnityEngine;
using System.Collections.Generic;
using NSubstitute;
using System.Collections;
using System;
using Object = UnityEngine.Object;

namespace Tests
{
    public partial class Mock: IDisposable
    {
        #region Variables
        protected List<Object> objectList;
        private bool disposedValue;
        #endregion
        #region Constructors
        public Mock()
        {
            objectList = new List<Object>();
        }
        #endregion
        #region Mock Types
        public Sprite Sprite {
            get {
                var sprite = Sprite.Create(Texture2D.grayTexture, new Rect(), Vector2.zero);
                objectList.Add(sprite);
                return sprite;
            }
        }

        public GameObject GameObject(string name)
        {
            var gameObject = new GameObject(name+"-Mock");
            objectList.Add(gameObject);
            return gameObject;
        }
        #endregion
        #region IDisposable Implementation
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    objectList.ForEach(Object.DestroyImmediate);
                    objectList.Clear();
                }

                
                objectList = null;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}