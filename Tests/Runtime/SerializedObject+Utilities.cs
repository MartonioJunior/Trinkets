using UnityEditor;

namespace Tests
{
    public static partial class SerializedObjectExtensions
    {
        public static SerializedObject PrepareForChanges(this UnityEngine.Object self)
        {
            return new SerializedObject(self);
        }

        public static SerializedObject Set(this SerializedObject self, string fieldName, object value)
        {
            self.FindProperty(fieldName).managedReferenceValue = value;
            return self;
        }

        public static void Commit(this SerializedObject self)
        {
            self.ApplyModifiedProperties();
        }
    }
}