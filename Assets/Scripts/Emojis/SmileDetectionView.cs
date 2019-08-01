using TMPro;
using UnityEngine;

namespace Emojis
{
    public class SmileDetectionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI smileLabel;
        [SerializeField] private SmileDetectionViewConfig config;

        public void UpdateSmile(SmileType type)
        {
            smileLabel.text = config.GetEmoji(type).emoji;
        }
    }
}