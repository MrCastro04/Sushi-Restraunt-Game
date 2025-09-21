using UnityEngine;

namespace Modules
{
    public interface IFactory<T>
    {
        public T CreateItemIn(Vector3 createPosition, Quaternion createRotation, Transform parent = null);
    }
}