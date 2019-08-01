using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Photos
{
    public class PhotosController
    {
        private readonly PhotosConfig config;
        private readonly List<Texture2D> storedPhotos;
        private readonly List<Texture2D> capturedPhotos;

        private string SavePath => Application.persistentDataPath;

        public PhotosController(PhotosConfig config)
        {
            this.config = config;

            storedPhotos = new List<Texture2D>();
            capturedPhotos = new List<Texture2D>();
        }

        public void Load()
        {
            if (capturedPhotos.Count > 0) Save();

            storedPhotos.Clear();

            var info = new DirectoryInfo(SavePath);
            var files = info.GetFiles("*.png");
            foreach (var file in files)
            {
                var texture = LoadPng(file.FullName);
                storedPhotos.Add(texture);
            }

            Debug.Log($"Photos loaded: {storedPhotos.Count}");
        }

        private Texture2D LoadPng(string filePath)
        {
            if (File.Exists(filePath))
            {
                var fileData = File.ReadAllBytes(filePath);
                var result = new Texture2D(1, 1);
                result.LoadImage(fileData);
                return result;
            }

            return null;
        }

        public void Save()
        {
            var count = capturedPhotos.Count;
            for (var i = 0; i < count; i++)
            {
                var photo = capturedPhotos[i];
                SavePng(photo, storedPhotos.Count + i + 1);
            }

            storedPhotos.AddRange(capturedPhotos);
            capturedPhotos.Clear();

            Debug.Log($"Photos saved: {count} / {storedPhotos.Count}");
        }

        private void SavePng(Texture2D texture, int fileIndex)
        {
            var bytes = texture.EncodeToPNG();
            File.WriteAllBytes($"{SavePath}/{config.saveName}-{fileIndex}.png", bytes);
        }

        public void Add(Texture2D photo)
        {
            capturedPhotos.Add(photo);
        }
    }
}