using System;
using Common;
using Emojis;
using Faces;
using Photos;
using UnityEngine;
using Zenject;

namespace Context
{
    public class CoreInstaller : MonoInstaller<CoreInstaller>
    {
        [SerializeField] private FaceRecognitionConfig faceRecognitionConfig;
        [SerializeField] private SmileDetectionConfig smileDetectionConfig;
        [SerializeField] private PhotosConfig photosConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(faceRecognitionConfig).IfNotBound();
            Container.BindInstance(smileDetectionConfig).IfNotBound();
            Container.BindInstance(photosConfig).IfNotBound();

            Container.Bind<FaceRecognitionController>().AsSingle();
            Container.Bind<SmileDetectionController>().AsSingle();
            Container.Bind<PhotosController>().AsSingle();
            Container.Bind<DynamicContent>().AsSingle();
        }
    }
}