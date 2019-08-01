using UnityEngine;

namespace Common
{
    public static class TransformUtils
    {
        public static void ApplyRatio(this RectTransform transform, float aspectRatio)
        {
            if (aspectRatio <= 1f)
                transform.localScale = new Vector3(aspectRatio, 1f, 1f);
            else
                transform.localScale = new Vector3(1f, 1f / aspectRatio, 1f);
        }
    }
}