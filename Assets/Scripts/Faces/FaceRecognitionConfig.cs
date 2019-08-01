using UnityEngine;

namespace Faces
{
    [CreateAssetMenu(menuName = "Krk/Faces/Face Recognition")]
    public class FaceRecognitionConfig : ScriptableObject
    {
        public string subscriptionKey;
        public string requestUrl;

        public bool returnFaceId;
        public bool returnFaceLandmarks;
        public string returnFaceAttributes;

        public float recognitionInterval;
    }
}