using System.Collections.Generic;
using System.Linq;
using Modules.Content.FoodCollection;
using Modules.Content.Map_Points;
using Modules.Core.Serializeable_Collections.Map_Points;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Modules.Core.Services
{
    public class ServiceMapPoint
    {
        private readonly Dictionary<string, PointMonoInfo> _mapPointsInfo;

        public ServiceMapPoint(CollectionPointsMono collectionPointsMono)
        {
            _mapPointsInfo = collectionPointsMono.MapPoints;
        }

        public void AddNewPoint(PointMono pointMono, PointType pointType, FoodType foodType)
        {
            var targetMapPointsInfo = _mapPointsInfo
                .Where(pair => pair.Value.PointType == pointType).ToList();

            if (targetMapPointsInfo.Any() == false)
            {
                PointMonoInfo newPointMonoInfo = new(pointMono, pointType, foodType);

                string idLetter = "";
                
                switch (pointType)
                {
                    case PointType.Buy:
                        idLetter = "B";
                        break;
                    
                    case PointType.Sell:
                        idLetter = "S";
                        break;
                    
                    case PointType.GatheringFood:
                        idLetter = "G";
                        break;
                    
                    case PointType.CustomerSpawnPoint:
                        idLetter = "CS";
                        break;
                }
                
                _mapPointsInfo.Add($"{idLetter}" + 1, newPointMonoInfo);
                return;
            }

            var lastPair = targetMapPointsInfo.OrderBy(pair => pair.Key).Last();

            var lastPairIDNumberString = lastPair.Key.Substring(1); 

            if (int.TryParse(lastPairIDNumberString, out int lastNumber))
            {
                int newIDNumber = lastNumber + 1; 
                
                string newPointIDPrefix = lastPair.Key.Substring(0,1);
                
                string newPointID = $"{newPointIDPrefix}{newIDNumber}";

                PointMonoInfo newPointMonoInfo = new(pointMono, pointType, foodType);
        
                _mapPointsInfo.Add(newPointID, newPointMonoInfo);
            }
        }

        public void SetNonEmptyPointWithID(string pointID)
        {
            if (_mapPointsInfo.ContainsKey(pointID))
            {
                _mapPointsInfo[pointID].PointMono.SetNotEmpty();
            }
            else
            {
                Debug.Log("Такой точки не существует");
            }
        }

        public void SetEmptyPointWithID(string pointID)
        {
            if (_mapPointsInfo.ContainsKey(pointID))
            {
                _mapPointsInfo[pointID].PointMono.SetEmpty();
            }
            else
            {
                Debug.Log("Такой точки не существует");
            }
        }

        public PointMono GetAnyFreePointWithType(PointType pointType)
        {
            var freePoints = _mapPointsInfo.Where(
                    x =>
                        x.Value.PointMono.IsEmpty &
                        x.Value.PointType == pointType)
                .ToArray();

            if (freePoints.Any() == false)
            {
                Debug.Log("Свободных мест нет");
                return null;
            }

            var randomFreePoint = freePoints[Random.Range(0, freePoints.Length)].Value.PointMono;

            return randomFreePoint;
        }

        public PointMono GetFreePointByID(string id)
        {
            return _mapPointsInfo.FirstOrDefault(x => x.Key == id).Value.PointMono;
        }

        public PointMono GetNeighboringPointForEmployer(string pointID)
        {
            if (pointID.Length < 2)
                return null;

            var customerPointIDNumber = pointID.Substring(1);

            var employerPointID = $"S{customerPointIDNumber}";

            var point = _mapPointsInfo.FirstOrDefault(
                x =>
                    x.Key == employerPointID &
                    x.Value.PointType == PointType.Sell &
                    x.Value.PointMono.IsEmpty).Value.PointMono;

            if (point == null)
                return null;

            point.SetNotEmpty();

            return point;
        }
    }
}