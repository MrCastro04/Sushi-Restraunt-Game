using Cysharp.Threading.Tasks;
using Modules.Content.Characters.Base.Code;
using UnityEngine;

namespace Modules.Content.Characters.Customer.View
{
    public class ViewCustomer : BaseView
    {
        // переписать метод проигрывания анимаций GoToPoint();

        public async UniTask GoToPoint(Vector3 position, Quaternion rotation)
        {
            PlayAnimationWalk();

            await MoveAgentTo(position, rotation);

            PlayAnimationIdle();
        }
    }
}