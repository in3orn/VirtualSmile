using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Photos
{
    public class PhotoView : MonoBehaviour, IDynamicItem<PhotoData>
    {
        [SerializeField] private RawImage image;

        private RectTransform imageTransform;

        private void InitTransform()
        {
            if (imageTransform == null)
                imageTransform = image.GetComponent<RectTransform>();
        }

        public void Init(PhotoData data)
        {
            InitTransform();

            image.texture = data.texture;
            imageTransform.ApplyRatio(data.AspectRatio);
        }
    }
}