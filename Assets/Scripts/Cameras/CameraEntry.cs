using System;
using UnityEngine;

namespace Cameras
{
    [Serializable]
    public class CameraEntry
    {
        public WebCamDevice device;
        public WebCamTexture texture;
    }
}