using Faces;
using UnityEngine;
using Zenject;

namespace Emojis
{
    public class SmileDetectionMediator : MonoBehaviour
    {
        [SerializeField] private SmileDetectionView view;

        [Inject] private SmileDetectionController smileDetectionController;
        [Inject] private FaceRecognitionController faceRecognitionController;

        private void OnEnable()
        {
            faceRecognitionController.OnImageRecognized.AddListener(HandleImageRecognized);
        }

        private void OnDisable()
        {
            faceRecognitionController.OnImageRecognized.RemoveListener(HandleImageRecognized);
        }

        private void HandleImageRecognized(string imageJson)
        {
            var smile = smileDetectionController.DetectSmile(imageJson);
            view.UpdateSmile(smile);
        }
    }
}