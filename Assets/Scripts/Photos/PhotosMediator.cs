using UnityEngine;
using Zenject;

namespace Photos
{
    public class PhotosMediator : MonoBehaviour
    {
        [SerializeField] private PhotosView photosView;

        [Inject] private PhotosController photosController;

        private void Awake()
        {
            photosController.Load();
        }

        private void Start()
        {
            photosView.Refresh(photosController.Photos);
        }

        private void OnEnable()
        {
            photosController.OnUpdated.AddListener(HandlePhotosUpdated);
        }

        private void OnDisable()
        {
            photosController.OnUpdated.RemoveListener(HandlePhotosUpdated);
        }

        private void HandlePhotosUpdated()
        {
            photosView.Refresh(photosController.Photos);
        }

        private void OnDestroy()
        {
            photosController.Save();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus) photosController.Save();
        }
    }
}