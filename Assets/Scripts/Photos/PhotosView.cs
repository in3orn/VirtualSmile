using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Photos
{
    public class PhotosView : MonoBehaviour
    {
        public Button showButton;
        public Button hideButton;

        [SerializeField] private RectTransform content;
        [SerializeField] private PhotoView template;
        [SerializeField] private GameObject panel;

        [Inject] private DynamicContent dynamicContent;

        private readonly IList<PhotoView> photoViews;

        public PhotosView()
        {
            photoViews = new List<PhotoView>();
        }

        public void Show()
        {
            panel.SetActive(true);
        }

        public void Hide()
        {
            panel.SetActive(false);
        }

        public void Refresh(IList<PhotoData> photos)
        {
            dynamicContent.Init(content, template, photoViews, photos);
        }
    }
}