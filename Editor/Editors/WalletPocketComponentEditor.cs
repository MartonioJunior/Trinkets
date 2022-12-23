using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MartonioJunior.Trinkets.Editor
{
    [CustomEditor(typeof(WalletPocketComponent))]
    public class WalletPocketComponentEditor : Editor
    {
        #region Editor Implementation
        public override void Bind(VisualElement root)
        {
            DefaultBind(root);
        }
        #endregion
    }
}