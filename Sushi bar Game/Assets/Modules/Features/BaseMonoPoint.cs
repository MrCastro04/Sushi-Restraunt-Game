using UnityEngine;

namespace Modules.Features
{
    public abstract class BaseMonoPoint : MonoBehaviour
    {
        [SerializeField] private Color _gizmosColor;
        [SerializeField] private float _gizmosSize;
    
        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawSphere(transform.position, _gizmosSize);
        } 
    }
}
