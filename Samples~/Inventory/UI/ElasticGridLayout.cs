using UnityEngine;
using UnityEngine.UI;

namespace Sample.Inventory
{
    [AddComponentMenu("Layout/Elastic Grid")]
    public class ElasticGridLayout: LayoutGroup
    {
        #region Variables
        [Header("Position")]
        [Tooltip("Based on the total width of the component")]
        [SerializeField] AnimationCurve xPosition;
        [Tooltip("Based on the total height of the component")]
        [SerializeField] AnimationCurve yPosition;
        [Header("Size")]
        [Tooltip("Based on the X component of baseSize")]
        [SerializeField] AnimationCurve width;
        [Tooltip("Based on the Y component of baseSize")]
        [SerializeField] AnimationCurve height;
        [Tooltip("Reference value for the size of child components")]
        [SerializeField] Vector2 baseSize;
        #endregion
        #region LayoutGroup Implementation
        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            for(int index = 0; index < rectChildren.Count; index++) {
                var item = rectChildren[index];
                var position = GetPositionFor(index);
                var size = GetSizeFor(index);

                PlaceObject(item, position, size);
            }
        }

        public override void CalculateLayoutInputVertical() {}

        public override void SetLayoutHorizontal() {}

        public override void SetLayoutVertical() {}
        #endregion
        #region Methods
        public Vector2 GetPositionFor(int index)
        {
            var offset = new Vector2(padding.left, padding.top);
            var layoutWidth = rectTransform.rect.width;
            var layoutHeight = rectTransform.rect.height;

            return new Vector2(
                xPosition.Evaluate(index) * layoutWidth,
                yPosition.Evaluate(index) * layoutHeight
            ) + offset;
        }

        public Vector2 GetSizeFor(int index)
        {
            return new Vector2(
                width.Evaluate(index) * baseSize.x,
                height.Evaluate(index) * baseSize.y
            );
        }

        public void PlaceObject(RectTransform transform, Vector2 position, Vector2 size)
        {
            SetChildAlongAxis(transform, 0, position.x, size.x);
            SetChildAlongAxis(transform, 1, position.y, size.y);
        }
        #endregion
    }
}