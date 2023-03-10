using UnityEngine;
using UnityEditor;
using MartonioJunior.Trinkets.Collectables;
using System.Linq;

namespace MartonioJunior.Trinkets.Editor
{
    [CustomEditor(typeof(CollectableWallet))]
    public class CollectableWalletEditor: UnityEditor.Editor
    {
        #region Variables
        CollectableWallet wallet;
        #endregion
        #region Editor Implementation
        private void OnEnable()
        {
            wallet = target as CollectableWallet;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var categoryQuery = wallet.GroupBy(item => {
                return (item.Resource as ICollectable)?.Category;
            });

            var headerStyle = Style.BasedOn(EditorStyles.whiteLargeLabel)
                .BG(Theme.H1.BGColor).TextColor(Theme.H1.TextColor);
            GUILayout.Label("Contents", headerStyle);
            if (wallet.IsEmpty) {
                GUILayout.Label("Wallet is Empty");
                return;
            } else foreach(var collectableGroup in categoryQuery) {
                Display(collectableGroup);
            }
            // Display(null);
        }
        #endregion
        #region Methods
        public void Display(IGrouping<ICollectableCategory, IResourceData> group)
        {
            if (group == null) return;

            var category = group.Key;
            using (Present.Vertical()) {
                DisplayCategoryHeader(category);
                foreach(var collectable in group) {
                    DisplayCollectable(collectable);
                }
            }
        }

        public void DisplayCategoryHeader(ICollectableCategory category)
        {
            const int WidgetHeight = 32;
            const int LabelHeight = WidgetHeight/2;

            var texture = AssetPreview.GetAssetPreview(category?.Image);
            var categoryStyle = Style.BasedOn(EditorStyles.whiteBoldLabel)
                .BG(Theme.H2.BGColor).TextColor(Theme.H2.TextColor);

            using (Present.Horizontal(categoryStyle, GUILayout.Height(WidgetHeight))) {
                GUILayout.Label(texture, GUILayout.MaxWidth(WidgetHeight), GUILayout.MaxHeight(WidgetHeight));
                using (Present.Vertical()) {
                    Present.FlexibleLabel(category?.Name ?? "Other Collectables", GUILayout.Height(LabelHeight));
                }
                GUILayout.FlexibleSpace();
            }
        }

        public void DisplayCollectable(IResourceData data)
        {
            if (!(data.Resource is ICollectable collectable)) return;

            const int WidgetHeight = 24;
            const int LabelHeight = WidgetHeight/2;

            var texture = AssetPreview.GetAssetPreview(collectable.Image);

            using (Present.Horizontal(GUILayout.Height(WidgetHeight))) {
                GUILayout.Label(texture, GUILayout.MaxWidth(WidgetHeight), GUILayout.MaxHeight(WidgetHeight));
                using (Present.Vertical()) {
                    Present.FlexibleLabel(collectable.Name, GUILayout.Height(LabelHeight));
                }
                GUILayout.FlexibleSpace();
            }
        }
        #endregion
    }
}