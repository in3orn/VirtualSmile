using UnityEngine;
using UnityEngine.UI;

namespace Cameras
{
    public class CameraDevice : MonoBehaviour
    {
        [SerializeField] private RawImage previewImage;
        [SerializeField] private CameraConfig config;

        private CameraEntry[] cameras;
        private CameraEntry currentCamera;
        private int currentIndex;

        public bool Initialized { get; private set; }
        public int NumOfCameras => cameras.Length;
        public bool Playing => Initialized && currentCamera.texture.isPlaying;
        public float CurrentAngle => currentCamera.texture.videoRotationAngle;

        public void Init()
        {
            InitCameras();

            Initialized = cameras.Length > 0;
            if (Initialized) InitCurrentCamera();
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

            UpdateAngle();
            //TODO update aspect ratio
        }

        private CameraEntry Create(WebCamDevice device)
        {
            return new CameraEntry
            {
                device = device,
                texture = new WebCamTexture(device.name)
            }; //TODO init target fps + resolution
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

            UpdateAngle();
            //TODO update aspect ratio
        }

        public Texture2D TakePhoto()
        {
            Texture2D photo = new Texture2D(currentCamera.texture.width, currentCamera.texture.height);
            photo.SetPixels(currentCamera.texture.GetPixels());
            photo.Apply();

            return photo;
        }

        private void LateUpdate()
        {
            if (Playing)
                previewImage.texture = currentCamera.texture;
        }

        private void UpdateAngle()
        {
            previewImage.transform.localRotation = Quaternion.Euler(0f, 0f, -CurrentAngle);
        }
    }
}