using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Modules.Core.Serializeable_Collections.Map_Points;
using Modules.Features;
using Modules.Features.Map_Points;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Modules.Core.Services
{
    public class ServiceMapPoint
    {
        private readonly Dictionary<string, PointMono> _mapPoints;

        public ServiceMapPoint(CollectionMapPoints collectionMapPoints)
        {
            _mapPoints = collectionMapPoints.BasePoints;
        }

        public void RegisterPointWithID(string pointID)
        {
            if (_mapPoints.ContainsKey(pointID))
            {
                _mapPoints[pointID].SetNotEmpty();
            }
            else
            {
                Debug.Log("Такой точки не существует");
            }
        }

        public void UnRegisterPointWithID(string pointID)
        {
            if (_mapPoints.ContainsKey(pointID))
            {
                _mapPoints[pointID].SetEmpty();
            }
            else
            {
                Debug.Log("Такой точки не существует");
            }
        }

        public PointMono GetAnyFreePointWithType(PointType pointType)
        {
            var freePoints = _mapPoints.Where(
                    x =>
                        x.Value.IsEmpty &
                        x.Value.PointType == pointType)
                .ToArray();

            if (freePoints.Any() == false)
            {
                Debug.Log("Свободных мест нет");
                return null;
            }

            var randomFreePoint = freePoints[Random.Range(0, freePoints.Length)].Value;

            return randomFreePoint;
        }

        public PointMono GetFreePointByID(string id)
        {
            return _mapPoints.FirstOrDefault(x => x.Key == id).Value;
        }

        public PointMono GetNeighboringPointForEmployer(string pointID)
        {
            if (pointID.Length < 2)
                return null;

            var customerPointIDNumber = pointID.Substring(1);

            var employerPointID = $"S{customerPointIDNumber}";

            var point = _mapPoints.FirstOrDefault(
                x =>
                    x.Key == employerPointID &
                    x.Value.PointType == PointType.Sell &
                    x.Value.IsEmpty).Value;

            if (point == null)
                return null;

            point.SetNotEmpty();

            return point;
        }
    }
}