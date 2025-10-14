using System;
using Modules.Core.Factories;
using UnityEngine;
using Zenject;

namespace Modules.Core.Managers
{
    public class ManagerEmployer : IInitializable, IDisposable
    {
        private readonly FactoryEmployer _factoryEmployer;
        private readonly Vector3 _spawnPosition;

        public ManagerEmployer(FactoryEmployer factoryEmployer, Vector3 spawnPosition)
        {
            _spawnPosition = spawnPosition;

            _factoryEmployer = factoryEmployer;
        }

        public void Initialize()
        {
            SpawnEmployer();

            EventsItem.OnGetMoreEmployers += SpawnEmployer;
        }

        public void Dispose()
        {
            EventsItem.OnGetMoreEmployers -= SpawnEmployer;
        }

        private void SpawnEmployer()
        {
            _factoryEmployer.CreateItemIn(_spawnPosition, Quaternion.identity);
        }
    }
}