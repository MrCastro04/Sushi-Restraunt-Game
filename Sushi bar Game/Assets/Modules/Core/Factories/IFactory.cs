using UnityEngine;

namespace Modules.Core.Factories
{
    public interface IFactory<T>
    {
        public T CreateItemIn(Vector3 createPosition, Quaternion createRotation, Transform parent = null);
    }
}