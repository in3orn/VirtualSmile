using UnityEngine;
using UnityEngine.UI;

namespace Cameras
{
    public class CameraMediator : MonoBehaviour
    {
        [SerializeField] private Button switchCameraButton;
        [SerializeField] private Button takePhotoButton;
        [SerializeField] private CameraDevice cameraDevice;
        [SerializeField] private RectTransform cameraPreview;
        [SerializeField] private GameObject error;

        private void Start()
        {
            cameraDevice.Init();

            error.SetActive(!cameraDevice.Initialized);
            takePhotoButton.interactable = cameraDevice.Initialized;
            switchCameraButton.interactable = cameraDevice.NumOfCameras > 1;
        }

        private void OnEnable()
        {
            takePhotoButton.onClick.AddListener(HandleTakePhotoButtonClicked);
            switchCameraButton.onClick.AddListener(HandleSwitchCameraButtonClicked);
        }

        private void OnDisable()
        {
            takePhotoButton.onClick.RemoveListener(HandleTakePhotoButtonClicked);
            switchCameraButton.onClick.RemoveListener(HandleSwitchCameraButtonClicked);
        }

        private void HandleTakePhotoButtonClicked()
        {
            var photo = cameraDevice.TakePhoto();
        }

        private void HandleSwitchCameraButtonClicked()
        {
            cameraDevice.SwitchCamera();
        }
    }
}