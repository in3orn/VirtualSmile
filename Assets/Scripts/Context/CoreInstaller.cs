using System;
using Photos;
using UnityEngine;
using Zenject;

namespace Context
{
    public class CoreInstaller : MonoInstaller<CoreInstaller>
    {
        [SerializeField] private PhotosConfig photosConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(photosConfig).IfNotBound();

            Container.Bind<PhotosController>().AsSingle();
        }
    }
}