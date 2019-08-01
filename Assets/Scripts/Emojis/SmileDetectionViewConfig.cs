using UnityEngine;

namespace Emojis
{
    [CreateAssetMenu(menuName = "Krk/Emojis/Smile Detection View")]
    public class SmileDetectionViewConfig : ScriptableObject
    {
        public EmojiData[] emojis;

        public EmojiData GetEmoji(SmileType type)
        {
            foreach (var emoji in emojis)
            {
                if (emoji.type == type)
                    return emoji;
            }

            return null;
        }
    }
}