using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Photos
{
    public class PhotoView : MonoBehaviour, IDynamicItem<PhotoData>
    {
        [SerializeField] private RawImage image;

        public void Init(PhotoData data)
        {
            image.texture = data.texture;
        }
    }
}