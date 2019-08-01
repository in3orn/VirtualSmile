using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Common;
using UnityEngine;
using UnityEngine.Events;

namespace Faces
{
    public class StringEvent : UnityEvent<string>
    {
    }

    public class FaceRecognitionController
    {
        public StringEvent OnImageRecognized { get; } = new StringEvent();

        private readonly FaceRecognitionConfig config;

        private bool inProcess;

        public FaceRecognitionConfig Config => config;

        public FaceRecognitionController(FaceRecognitionConfig config)
        {
            this.config = config;
        }

        public void Recognize(Texture2D texture)
        {
            if (inProcess) return;

            try
            {
                inProcess = true;
                SendRequest(texture);
            }
            catch (Exception ex)
            {
                inProcess = false;
                Debug.LogError($"Cannot process image: {texture.name}: {ex}");
            }
        }

        private async void SendRequest(Texture2D texture)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", config.subscriptionKey);

            var requestParameters = $"returnFaceId={config.returnFaceId}" +
                                    $"&returnFaceLandmarks={config.returnFaceLandmarks}" +
                                    $"&returnFaceAttributes={config.returnFaceAttributes}";


            var uri = $"{config.requestUrl}?{requestParameters}";
            var byteData = texture.EncodeToPNG();

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                
                var response = await client.PostAsync(uri, content);
                var contentString = await response.Content.ReadAsStringAsync();
                Debug.Log($"Recognize:{Environment.NewLine}{JsonUtils.Format(contentString)}");
                inProcess = false;

                OnImageRecognized.Invoke(contentString);
            }
        }
    }
}