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
            photosView.Hide();
            photosView.Refresh(photosController.Photos);
        }

        private void OnEnable()
        {
            photosView.showButton.onClick.AddListener(HandleShowButtonClicked);
            photosView.hideButton.onClick.AddListener(HandleHideButtonClicked);
            photosController.OnUpdated.AddListener(HandlePhotosUpdated);
        }

        private void OnDisable()
        {
            photosView.showButton.onClick.RemoveListener(HandleShowButtonClicked);
            photosView.hideButton.onClick.RemoveListener(HandleHideButtonClicked);
            photosController.OnUpdated.RemoveListener(HandlePhotosUpdated);
        }

        private void HandleShowButtonClicked()
        {
            photosView.Show();
        }

        private void HandleHideButtonClicked()
        {
            photosView.Hide();
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