using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Modules.Content.Characters.Base.Code
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(BaseView))]
    public abstract class BaseController : MonoBehaviour
    {
        [SerializeField] protected BaseView _baseView;
        
        protected CancellationToken _cancellationToken => this.GetCancellationTokenOnDestroy();
        
        public virtual void Init()
        {
            _baseView.Init(_cancellationToken);
        }
    }
}