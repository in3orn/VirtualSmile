using System;
using UnityEngine;

namespace Photos
{
    [Serializable]
    public class PhotoData
    {
        public Texture2D texture;
        public bool persistent;
        public float rotation;

        public float AspectRatio => (float) texture.width / texture.height;
    }
}