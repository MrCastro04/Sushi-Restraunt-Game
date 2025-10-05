using System;
using Modules.Content.FoodCollection;
using Modules.Content.Map_Points;

namespace Modules.Core.Serializeable_Collections.Map_Points
{
    [Serializable]
    public struct PointMonoInfo
    {
        public PointMono PointMono;
        public PointType PointType;
        public FoodType FoodType;
        
        public void Init(string pointID)
        {
            PointMono.Init(pointID,PointType,FoodType);
        }
    }
}