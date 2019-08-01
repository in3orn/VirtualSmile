using UnityEngine;

namespace Cameras
{
    [CreateAssetMenu(menuName = "Krk/Cameras/Camera")]
    public class CameraConfig : ScriptableObject
    {
        public int defaultCamera;
    }
}