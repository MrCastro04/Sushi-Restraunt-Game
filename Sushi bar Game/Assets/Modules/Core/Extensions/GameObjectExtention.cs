using UnityEngine;

namespace Modules.@new
{
    public static class GameObjectExtention
    {
        public static void SetTransformValues(this GameObject go, Vector3 pos, Quaternion rot, Transform parent = null)
        {
            go.transform.position = pos;
            go.transform.rotation = rot;
            go.transform.parent = parent;
        }
    }
}