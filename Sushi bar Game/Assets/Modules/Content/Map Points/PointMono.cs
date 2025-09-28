using UnityEngine;

namespace Modules.Features.Map_Points
{
    public class PointMono : MonoBehaviour
    {
        [SerializeField] private Color _gizmosColor = Color.blue;
        [SerializeField] private float _gizmosSize = 0.5f;
        
        private PointType _pointType;
        private string _id;
        private bool _isEmpty = true;

        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        public PointType PointType => _pointType;
        public bool IsEmpty => _isEmpty;
        public string ID => _id;
 
        public void Init(string id, PointType pointerType)
        {
            _id = id;
            _pointType = pointerType;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawSphere(transform.position, _gizmosSize);
        }
        
        public void SetNotEmpty() => _isEmpty = false;
        public void SetEmpty() => _isEmpty = true;
    }

    public enum PointType
    {
        Buy,
        Sell,
        GatheringFood,
        CustomerSpawnPoint
    }
}
