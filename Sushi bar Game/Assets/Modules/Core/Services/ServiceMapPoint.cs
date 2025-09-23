using System.Drawing;
using System.Linq;
using Modules.Features;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Modules.Core.Services
{
    public class ServiceMapPoint
    {
        private readonly PointMono[] _mapPoints;
        
        public ServiceMapPoint(PointMono[] mapPoints)
        {
            _mapPoints = mapPoints;
        }

        public void RegisterPointWithID(string pointID)
        {
            var poinWithID = _mapPoints.FirstOrDefault(x => x.ID == pointID);

            if (poinWithID == null)
            {
                Debug.Log("Точку с таким ID несуществует");
                return;
            }
            
            poinWithID.SetNotEmpty();
        }

        public void UnRegisterPointWithID(string pointID)
        {
            var point = _mapPoints.FirstOrDefault(x => x.ID == pointID);

            if (point == null)
            {
                Debug.Log("Точку с таким ID несуществует");
                return;
            }

            point.SetEmpty();
        }

        public PointMono GetAnyFreePointWithType(PointType pointType)
        {
            var freePoints = _mapPoints.Where(x => x.IsEmpty & x.PointType == pointType).ToArray();

            if (freePoints.Any() == false)
            {
                Debug.Log("Свободных мест нет");
                return null;
            }

            var randomFreePoint = freePoints[Random.Range(0, freePoints.Length)];

            randomFreePoint.SetNotEmpty();

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

            var point = _mapPoints.FirstOrDefault(x =>
                x.ID == targetID & x.PointType == PointType.Sell & x.IsEmpty);

            if (point == null)
                return null;

            point.SetNotEmpty();

            return point;
        }
    }
}