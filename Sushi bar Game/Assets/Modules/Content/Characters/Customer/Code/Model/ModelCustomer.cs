using Modules.Content.FoodCollection;
using Modules.Content.Map_Points;
using UnityEngine;

namespace Modules.Content.Characters.Customer.Model
{
    public class ModelCustomer
    {
        private readonly FoodType _desiredFoodType;

        private bool _hasFoodStatus = false;
        private Vector3 _startPosition;
        private Quaternion _startRotation;

        public bool HasFoodStatus => _hasFoodStatus;
        public Vector3 StartPosition => _startPosition;
        public Quaternion StartRotation => _startRotation;
        public FoodType DesiredFoodType => _desiredFoodType;
        public PointType PointType => PointType.Buy;

        public ModelCustomer(FoodType foodType)
        {
            _desiredFoodType = foodType;
        }

        public void SetFoodStatus(bool newStatus) => _hasFoodStatus = newStatus;
        public void SetPositionAndRotation(Vector3 newPosition, Quaternion newRotation)
        {
            _startPosition = newPosition;
            _startRotation = newRotation;
        }
    }
}