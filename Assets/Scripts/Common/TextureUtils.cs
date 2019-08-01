using UnityEngine;

namespace Common
{
    public static class TextureUtils
    {
        public static Texture2D Rotate(this Texture2D texture, bool clockwise)
        {
            var original = texture.GetPixels32();
            var rotated = new Color32[original.Length];
            var w = texture.width;
            var h = texture.height;

            for (var j = 0; j < h; ++j)
            {
                for (var i = 0; i < w; ++i)
                {
                    var iRotated = (i + 1) * h - j - 1;
                    var iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
                    rotated[iRotated] = original[iOriginal];
                }
            }

            var rotatedTexture = new Texture2D(h, w);
            rotatedTexture.SetPixels32(rotated);
            rotatedTexture.Apply();

            return rotatedTexture;
        }

        public static Texture2D RreflectHorizontally(this Texture2D texture)
        {
            var original = texture.GetPixels32();
            var reflected = new Color32[original.Length];
            var w = texture.width;
            var h = texture.height;

            for (var j = 0; j < h; ++j)
            {
                for (var i = 0; i < w; ++i)
                {
                    var iReflected = h - j * w - w + i;
                    var iOriginal = j * w + i;
                    reflected[iReflected] = original[iOriginal];
                }
            }

            var reflectedTexture = new Texture2D(h, w);
            reflectedTexture.SetPixels32(reflected);
            reflectedTexture.Apply();

            return reflectedTexture;
        }
    }
}