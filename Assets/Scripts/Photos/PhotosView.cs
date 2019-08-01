using System.Collections.Generic;
using Common;
using UnityEngine;
using Zenject;

namespace Photos
{
    public class PhotosView : MonoBehaviour
    {
        [SerializeField] private RectTransform content;
        [SerializeField] private PhotoView template;

        [Inject] private DynamicContent dynamicContent;

        private readonly IList<PhotoView> photoViews;

        public PhotosView()
        {
            photoViews = new List<PhotoView>();
        }

        public void Refresh(IList<PhotoData> photos)
        {
            dynamicContent.Init(content, template, photoViews, photos);
        }
    }
}