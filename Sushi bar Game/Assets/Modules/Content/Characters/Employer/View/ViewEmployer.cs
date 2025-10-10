using Cysharp.Threading.Tasks;
using Modules.Content.Characters.Base.Code;
using Modules.Content.FoodCollection;
using Modules.Content.Map_Points;
using Modules.Content.UI.Circle_Loading.Code;
using UnityEngine;

namespace Modules.Content.Characters.Employer.View
{
    public class ViewEmployer : BaseView
    {
        [SerializeField] private LoadingCircle _loadingCircle;

        private float _immitationTime;
        private string ANIMATOR_PARAM_IS_CHOP_FOOD = "IsChopFood";
        
        public void SetImmitationTime(float newTime)
        {
            _immitationTime = newTime;
        }
        
        public async UniTask GoToPoint(PointMono pointMono, bool withImmitation = false, bool withFood = false)
        {
            if (withFood)
            {
                PlayAnimationWalkWithFood();
            }
            
            else
            {
                PlayAnimationWalk();
            }

            await MoveAgentTo(pointMono.Position, pointMono.Rotation);
            
            PlayAnimationIdle();
            
            if (withImmitation)
            {
                await _loadingCircle.RunImmitation(_immitationTime);
            }
        }
        
        public void PlayAnimationCook(FoodType foodType)
        {
            switch (foodType)
            {
                case FoodType.Sushi:
                    PlayAnimationChopChopFood();
                    break;
                
                // другие анимации под другую еду
            }
        }

        public void PlayAnimationWalkWithFood()
        {
            PlayAnimationWalk();

            _animator.SetBool(ANIMATOR_PARAM_IS_CHOP_FOOD, false);
        }

        public void PlayAnimationChopChopFood()
        {
            PlayAnimationIdle();

            _animator.SetBool(ANIMATOR_PARAM_IS_CHOP_FOOD, true);
        }
    }
}