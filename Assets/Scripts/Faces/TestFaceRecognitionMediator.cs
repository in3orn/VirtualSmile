using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Faces
{
    public class TestFaceRecognitionMediator : MonoBehaviour
    {
        [SerializeField] private Texture2D testFace;
        [SerializeField] private Button faceRecognitionButton;
        
        [Inject] private FaceRecognitionController faceRecognitionController;

        private void OnEnable()
        {
            faceRecognitionButton.onClick.AddListener(HandleFaceRecognitionButtonClicked);
        }
        
        private void OnDisable()
        {
            faceRecognitionButton.onClick.RemoveListener(HandleFaceRecognitionButtonClicked);
        }

        private void HandleFaceRecognitionButtonClicked()
        {
            faceRecognitionController.Recognize(testFace);
        }
    }
}