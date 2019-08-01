using UnityEngine;

namespace Emojis
{
    [CreateAssetMenu(menuName = "Krk/Emojis/Smile Detection")]
    public class SmileDetectionConfig : ScriptableObject
    {
        public float minHappyValue;
        public float minVeryHappyValue;
    }
}