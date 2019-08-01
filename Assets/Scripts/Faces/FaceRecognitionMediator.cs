using System.Collections;
using Cameras;
using UnityEngine;
using Zenject;

namespace Faces
{
    public class FaceRecognitionMediator : MonoBehaviour
    {
        [SerializeField] private CameraDevice cameraDevice;

        [Inject] private FaceRecognitionController faceRecognitionController;

        private WaitForSeconds interval;

        private void Awake()
        {
            interval = new WaitForSeconds(faceRecognitionController.Config.recognitionInterval);
        }

        private void OnEnable()
        {
            Debug.Log("Start recognition routine.");
            StartCoroutine(RecognitionRoutine());
        }

        private IEnumerator RecognitionRoutine()
        {
            while (enabled)
            {
                yield return interval;
                if (cameraDevice.Playing)
                {
                    faceRecognitionController.Recognize(cameraDevice.GetTexture());
                }
            }
        }
    }
}