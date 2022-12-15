using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MartonioJunior.Trinkets.Editor
{
    [CustomEditor(typeof(WalletDetectorComponent))]
    public class WalletDetectorComponentEditor : Editor
    {
        #region Editor Implementation
        public override void Bind(VisualElement root)
        {
            DefaultBind(root);
        }
        #endregion
    }
}