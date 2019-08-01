using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace Photos
{
    public class PhotosController
    {
        public UnityEvent OnUpdated { get; } = new UnityEvent();

        private readonly PhotosConfig config;
        private readonly List<PhotoData> photos;

        private bool upToDate;

        private string SavePath => Application.persistentDataPath;
        public IList<PhotoData> Photos => photos;

        public PhotosController(PhotosConfig config)
        {
            this.config = config;

            photos = new List<PhotoData>();
        }

        public void Load()
        {
            if (upToDate) return;

            photos.Clear();

            var info = new DirectoryInfo(SavePath);
            var files = info.GetFiles("*.png");
            foreach (var file in files)
            {
                var photo = LoadPhoto(file.FullName);
                photos.Add(photo);
            }

            upToDate = true;

            Debug.Log($"Photos loaded: {photos.Count}");
        }

        private PhotoData LoadPhoto(string filePath)
        {
            return new PhotoData
            {
                texture = LoadPng(filePath),
                persistent = true
            };
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
            if (upToDate) return;

            var count = 0;
            for (var i = 0; i < photos.Count; i++)
            {
                var photo = photos[i];
                if (!photo.persistent)
                {
                    SavePhoto(photo, i + 1);
                    count++;
                }
            }

            PlayerPrefs.Save();
            upToDate = true;

            Debug.Log($"Photos saved: {count} / {photos.Count}");
        }

        private void SavePhoto(PhotoData photo, int fileIndex)
        {
            var name = $"{config.saveName}-{fileIndex}.png";

            SavePng(photo.texture, name);

            photo.persistent = true;
        }

        private void SavePng(Texture2D texture, string name)
        {
            var bytes = texture.EncodeToPNG();
            File.WriteAllBytes($"{SavePath}/{name}", bytes);
        }

        public void Add(PhotoData photo)
        {
            upToDate = false;

            photos.Add(photo);
            OnUpdated.Invoke();
        }
    }
}