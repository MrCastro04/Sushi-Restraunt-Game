using System;
using System.Drawing;
using System.Linq;
using Modules.Features;
using Modules.Features.Characters.Customer;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Modules.Core
{
    public class ServiceMapPoint
    {
        private readonly PointMono[] _mapPoints;

        public ServiceMapPoint(PointMono[] mapPoints)
        {
            _mapPoints = mapPoints;
        }

        public PointMono RegisterAndGetAnyFreePointWithType(PointType pointType)
        {
            var freePoints = _mapPoints.Where(x => x.IsEmpty & x.PointType == pointType).ToArray();

            if (freePoints.Any() == false)
            {
                Debug.Log("Свободных мест нет");
                return null;
            }

            var randomFreePoint = freePoints[Random.Range(0, freePoints.Length)];

            MarkTargetPointAsNonEmpty(randomFreePoint.ID);

            return randomFreePoint;
        }
        

        public PointMono GetFreePointByID(string id)
        {
            return _mapPoints.FirstOrDefault(x => x.ID == id);
        }

        public PointMono GetNeighboringPointForEmployer(string pointID)
        {
            if (pointID.Length < 2)
                return null;

            var numberID = pointID.Substring(1);

            var targetID = $"S{numberID}";

            return _mapPoints.FirstOrDefault(x => x.ID == targetID & x.PointType == PointType.Sell);
        }

        private void MarkTargetPointAsNonEmpty(string pointID)
        {
            var point = _mapPoints.FirstOrDefault(x => x.ID == pointID);
            
            if (point == null)
            {
                Debug.Log($"|{this}| not foundTargetPoint");
                return;
            }

            point.SetNotEmpty();
        }
    }
}