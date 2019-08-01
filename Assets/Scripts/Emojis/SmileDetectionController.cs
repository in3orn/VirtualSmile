using System.Globalization;
using com.adjust.sdk;
using UnityEngine.Events;

namespace Emojis
{
    public enum SmileType
    {
        Neutral = 0,
        Happy,
        VeryHappy
    }

    public class SmileEvent : UnityEvent<SmileType>
    {
    }

    public class SmileDetectionController
    {
        private readonly SmileDetectionConfig config;

        public SmileDetectionController(SmileDetectionConfig config)
        {
            this.config = config;
        }

        public SmileType DetectSmile(string json)
        {
            var faces = JSON.Parse(json);
            if (faces.Count <= 0)
                return SmileType.Neutral;

            var face = faces[0];
            if (face.Count <= 0)
                return SmileType.Neutral;

            var attributes = face["faceAttributes"];
            if (attributes.Count <= 0)
                return SmileType.Neutral;

            var smile = attributes["smile"];
            if (string.IsNullOrEmpty(smile))
                return SmileType.Neutral;

            var smileValue = float.Parse(smile, CultureInfo.InvariantCulture);

            if (smileValue > config.minVeryHappyValue)
                return SmileType.VeryHappy;
            if (smileValue > config.minHappyValue)
                return SmileType.Happy;

            return SmileType.Neutral;
        }
    }
}