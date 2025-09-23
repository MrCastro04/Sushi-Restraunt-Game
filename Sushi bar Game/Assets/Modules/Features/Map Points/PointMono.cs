using Modules.Core.Services;
using Modules.Features.Characters.Base.Code;
using UnityEngine;
using Zenject;

namespace Modules.Features.Map_Points
{
    public class PointMono : MonoBehaviour
    {
       private string _id;
        [SerializeField] private PointType _pointType;
        [SerializeField] private Color _gizmosColor = Color.blue;
        [SerializeField] private float _gizmosSize = 0.5f;

        [Inject] private ServiceMapPoint _serviceMapPoint;
        
        private bool _isEmpty = true;
        private BaseEntity _baseEntity;
        
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        public PointType PointType => _pointType;
        public bool IsEmpty => _isEmpty;
        public string ID => _id;
 
        public void Init(string id)
        {
            _id = id;
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
        GatheringFood
    }
}
