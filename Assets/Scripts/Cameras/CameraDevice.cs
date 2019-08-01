using Common;
using Photos;
using UnityEngine;
using UnityEngine.UI;

namespace Cameras
{
    public class CameraDevice : MonoBehaviour
    {
        [SerializeField] private RawImage previewImage;
        [SerializeField] private CameraConfig config;

        private RectTransform previewTransform;
        private CameraEntry[] cameras;
        private CameraEntry currentCamera;
        private int currentIndex;

        public bool Initialized { get; private set; }
        public int NumOfCameras => cameras.Length;
        public bool Playing => Initialized && currentCamera.texture.isPlaying;
        public float CurrentAngle => currentCamera.texture.videoRotationAngle;

        public float AspectRatio =>
            (float) currentCamera.texture.requestedWidth / currentCamera.texture.requestedHeight;

        public void Init()
        {
            InitPreview();
            InitCameras();

            Initialized = cameras.Length > 0;
            if (Initialized) InitCurrentCamera();
        }

        private void InitPreview()
        {
            previewTransform = previewImage.GetComponent<RectTransform>();
        }

        private void InitCameras()
        {
            var devices = WebCamTexture.devices;
            cameras = new CameraEntry[devices.Length];

            for (var i = 0; i < devices.Length; i++)
            {
                cameras[i] = Create(devices[i]);
            }
        }

        private void InitCurrentCamera()
        {
            currentIndex = Mathf.Clamp(config.defaultCamera, 0, cameras.Length);
            currentCamera = cameras[currentIndex];
            currentCamera.texture.Play();

            UpdatePreview();
        }

        private CameraEntry Create(WebCamDevice device)
        {
            var result = new CameraEntry
            {
                device = device,
                texture = CreateTexture(device)
            };

            return result;
        }

        private WebCamTexture CreateTexture(WebCamDevice device)
        {
            var resolution = GetPreferredResolution(device);

            return new WebCamTexture(device.name)
            {
                requestedWidth = resolution.width,
                requestedHeight = resolution.height,
                requestedFPS = config.cameraFps
            };
        }

        private Resolution GetPreferredResolution(WebCamDevice device)
        {
            for (var i = device.availableResolutions.Length - 1; i >= 0; i--)
            {
                var resolution = device.availableResolutions[i];
                if (resolution.width < config.maxCameraSize && resolution.height < config.maxCameraSize)
                    return resolution;
            }

            return device.availableResolutions[0];
        }

        public void SwitchCamera()
        {
            if (cameras.Length <= 1) return;

            if (currentCamera.texture.isPlaying)
                currentCamera.texture.Stop();

            currentIndex++;
            currentIndex %= cameras.Length;

            currentCamera = cameras[currentIndex];
            currentCamera.texture.Play();

            UpdatePreview();
        }

        public PhotoData TakePhoto()
        {
            Texture2D texture = new Texture2D(currentCamera.texture.width, currentCamera.texture.height);
            texture.SetPixels(currentCamera.texture.GetPixels());
            texture.Apply();

            return new PhotoData
            {
                texture = texture,
                cameraAngle = CurrentAngle
            };
        }

        private void LateUpdate()
        {
            if (Playing)
                previewImage.texture = currentCamera.texture;
        }

        private void UpdatePreview()
        {
            previewTransform.localRotation = Quaternion.Euler(0f, 0f, -CurrentAngle);
            previewTransform.ApplyRatio(AspectRatio);
            Debug.Log("Aspect ratio: " + AspectRatio);
        }
    }
}