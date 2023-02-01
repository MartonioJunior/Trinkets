using UnityEngine;

namespace Tests
{
    public static partial class Mock
    {
        #region Methods
        public static GameObject GameObject(string name)
        {
            var gameObject = new GameObject(name+"-Mock");
            Register(gameObject);
            return gameObject;
        }

        public static Sprite Sprite()
        {
            var sprite = UnityEngine.Sprite.Create(Texture2D.grayTexture, new Rect(), Vector2.zero);
            Register(sprite);
            return sprite;
        }

        public static T ScriptableObject<T>() where T: ScriptableObject
        {
            var reference = UnityEngine.ScriptableObject.CreateInstance<T>();
            Register(reference);
            return reference;
        }

        public static void ScriptableObject<T>(out T value) where T: ScriptableObject
        {
            value = ScriptableObject<T>();
        }
        #endregion
    }
}