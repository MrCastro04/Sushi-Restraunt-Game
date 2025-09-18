using UnityEngine;

namespace Modules.Features
{
    public class PointMono : MonoBehaviour
    {
        [SerializeField] private PointType _pointType;
        [SerializeField] private Color _gizmosColor = Color.blue;
        [SerializeField] private float _gizmosSize = 0.5f;

        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        public PointType PointType => _pointType;

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawSphere(transform.position, _gizmosSize);
        } 
    }

    public enum PointType
    {
        Buy,
        Sell,
        GatheringFood
    }
}
