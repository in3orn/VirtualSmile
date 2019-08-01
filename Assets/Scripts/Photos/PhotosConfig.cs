using UnityEngine;

namespace Photos
{
    [CreateAssetMenu(menuName = "Krk/Photos/Photos")]
    public class PhotosConfig : ScriptableObject
    {
        public string savePath;
        public string saveName;
    }
}