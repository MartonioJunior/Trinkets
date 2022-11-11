using MartonioJunior.Trinkets.Currencies;
using UnityEditor;
using UnityEngine;

namespace MartonioJunior.Trinkets.Editor
{
    [CustomEditor(typeof(CurrencyWallet))]
    public class CurrencyWalletEditor: UnityEditor.Editor
    {
        #region Variables
        CurrencyWallet wallet;
        #endregion
        #region Editor Implementation
        private void OnEnable()
        {
            wallet = target as CurrencyWallet;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var currencyList = wallet.All();

            var headerStyle = Style.BasedOn(EditorStyles.whiteLargeLabel)
                .BG(Theme.H1.BGColor).TextColor(Theme.H1.TextColor);
            GUILayout.Label("Contents", headerStyle);

            if (currencyList.Count <= 0) {
                GUILayout.Label("Wallet is Empty");
            } else foreach(var currency in currencyList) {
                DisplayCurrency(currency.Resource as ICurrency, currency.Amount);
            }
        }
        #endregion
        #region Methods
        public void DisplayCurrency(ICurrency currency, int amount)
        {
            if (currency == null) return;

            const int WidgetHeight = 32;
            const int LabelHeight = WidgetHeight/2;
            const int RightSpacing = 20;

            var texture = AssetPreview.GetAssetPreview(currency.Image);

            using (Present.Horizontal(GUILayout.Height(WidgetHeight))) {
                GUILayout.Label(texture, GUILayout.MaxWidth(WidgetHeight), GUILayout.MaxHeight(WidgetHeight));
                using (Present.Vertical()) {
                    Present.FlexibleLabel(currency.Name, GUILayout.Height(LabelHeight));
                }
                GUILayout.FlexibleSpace();
                using (Present.Vertical()) {
                    Present.FlexibleLabel($"{amount}", GUILayout.Height(LabelHeight));
                }

                GUILayout.Space(RightSpacing);
            }
        }
        #endregion
    }
}