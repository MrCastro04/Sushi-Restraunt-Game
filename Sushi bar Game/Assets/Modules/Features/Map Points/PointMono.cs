using System;
using Modules.Core;
using Modules.Features.Characters.Base.Code;
using UnityEngine;
using Zenject;

namespace Modules.Features
{
    public class PointMono : MonoBehaviour
    {
        [SerializeField] private string _id;
        [SerializeField] private PointType _pointType;
        [SerializeField] private Color _gizmosColor = Color.blue;
        [SerializeField] private float _gizmosSize = 0.5f;

        [Inject] private ServiceMapPoint _serviceMapPoint;
        
        private bool _isEmpty = true;
        private BaseEntity _baseEntity;

        public BaseEntity BaseEntity => _baseEntity;
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        public PointType PointType => _pointType;
        public bool IsEmpty => _isEmpty;
        public string ID => _id;

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawSphere(transform.position, _gizmosSize);
        }
        
        public void SetNotEmpty(BaseEntity baseEntity)
        {
            _isEmpty = false;

            _baseEntity = baseEntity;
        }

        public void SetEmpty()
        {
            _isEmpty = true;

            _baseEntity = null;
        }
    }

    public enum PointType
    {
        Buy,
        Sell,
        GatheringFood
    }
}
