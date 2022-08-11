using UnityEngine;
using UnityEditor;
using MartonioJunior.Trinkets.Collectables;

namespace MartonioJunior.Trinkets.Editor {
  [CustomEditor(typeof(CollectableWallet))]
  public class CollectableWalletEditor : UnityEditor.Editor {
#region Variables
    CollectableWallet wallet;
#endregion
#region Editor Implementation
    private void OnEnable() { wallet = target as CollectableWallet; }

    public override void OnInspectorGUI() {
      base.OnInspectorGUI();

      var categoryManager = wallet as IResourceManager<ICollectableCategory>;
      var categoryList = categoryManager.Search(null);

      var headerStyle = Style.BasedOn(EditorStyles.whiteLargeLabel)
                            .BG(Theme.H1.BGColor)
                            .TextColor(Theme.H1.TextColor);
      GUILayout.Label("Contents", headerStyle);
      if (categoryList.Length + categoryManager.AmountOf(null) <= 0) {
        GUILayout.Label("Wallet is Empty");
        return;
      } else
        foreach (var category in categoryList) {
          DisplayCategoryList(category);
        }
      DisplayCategoryList(null);
    }
#endregion
#region Methods
    public void DisplayCategoryList(ICollectableCategory category) {
      var collectableList = wallet.Search((item) => item.Category == category);
      if (collectableList.Length <= 0) return;

          using (Present.Vertical()) {
            DisplayCategoryHeader(category);
            foreach (var collectable in collectableList) {
              DisplayCollectable(collectable);
            }
          }
    }

    public void DisplayCategoryHeader(ICollectableCategory category) {
      const int WidgetHeight = 32;
      const int LabelHeight = WidgetHeight / 2;

      var texture = AssetPreview.GetAssetPreview(category?.Image);
      var categoryStyle = Style.BasedOn(EditorStyles.whiteBoldLabel)
                              .BG(Theme.H2.BGColor)
                              .TextColor(Theme.H2.TextColor);

      using (
          Present.Horizontal(categoryStyle, GUILayout.Height(WidgetHeight))) {
        GUILayout.Label(texture, GUILayout.MaxWidth(WidgetHeight),
                        GUILayout.MaxHeight(WidgetHeight));
        using (Present.Vertical()) {
          Present.FlexibleLabel((category as IResourceCategory)?.Name ??
                                    "Other Collectables",
                                GUILayout.Height(LabelHeight));
        }
        GUILayout.FlexibleSpace();
      }
    }

    public void DisplayCollectable(ICollectable collectable) {
      const int WidgetHeight = 24;
      const int LabelHeight = WidgetHeight / 2;

      var texture = AssetPreview.GetAssetPreview(collectable.Image);

      using (Present.Horizontal(GUILayout.Height(WidgetHeight))) {
        GUILayout.Label(texture, GUILayout.MaxWidth(WidgetHeight),
                        GUILayout.MaxHeight(WidgetHeight));
        using (Present.Vertical()) {
          Present.FlexibleLabel(collectable.Name,
                                GUILayout.Height(LabelHeight));
        }
        GUILayout.FlexibleSpace();
      }
    }
#endregion
  }
}
