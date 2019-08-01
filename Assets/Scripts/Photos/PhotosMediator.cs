using UnityEngine;
using Zenject;

namespace Photos
{
    public class PhotosMediator : MonoBehaviour
    {
        [Inject] private PhotosController photosController;

        private void OnEnable()
        {
            photosController.Load();
        }

        private void OnDisable()
        {
            photosController.Save();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
                photosController.Load();
            else
                photosController.Save();
        }
    }
}