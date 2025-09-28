using System;
using UnityEngine;

namespace Modules.Features.UI.Billboard
{
    public class Billboard : MonoBehaviour
    {
        private Camera _cameraCmp;

        private void Awake()
        {
            _cameraCmp = Camera.main;
        }

        private void LateUpdate()
        {
            Vector3 directionOffset = transform.position + _cameraCmp.transform.forward;

            transform.LookAt(directionOffset);
        }
    }
}