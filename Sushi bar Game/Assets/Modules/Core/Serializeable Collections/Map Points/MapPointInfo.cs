using System;
using Modules.Features.Map_Points;

namespace Modules.Core.Serializeable_Collections.Map_Points
{
    [Serializable]
    public struct MapPointInfo
    {
        public PointMono PointMono;
        public PointType PointType;

        public void Init(string pointID)
        {
            PointMono.Init(pointID,PointType);
        }
    }
}