using System;
using Modules.Content.FoodCollection;
using Modules.Content.Map_Points;
using UnityEngine;

namespace Modules.Core.Serializeable_Collections.Map_Points
{
    [Serializable]
    public struct PointMonoInfo
    {
        public PointMono PointMono;
        public PointType PointType;
        public FoodType FoodType;

        public PointMonoInfo(PointMono pointMono, PointType pointType, FoodType foodType)
        {
            PointMono = pointMono;

            PointType = pointType;

            FoodType = foodType;
        }

        public void Init(string pointID)
        {
            PointMono.Init(pointID, PointType, FoodType);
        }
    }
}