using System;
using System.Drawing;
using System.Linq;
using Modules.Features;
using Modules.Features.Characters.Customer;
using UnityEngine;
using Zenject;

namespace Modules.Core
{
    public class ServiceMapPoint : IInitializable, IDisposable
    {
        private readonly PointMono[] _mapPoints;

        public ServiceMapPoint(PointMono[] mapPoints)
        {
            _mapPoints = mapPoints;
        }

        public void Initialize()
        {
            EventsCustomer.OnGetBuyPoint += MarkTargetPointAsNonEmpty;
        }

        public void Dispose()
        {
            EventsCustomer.OnGetBuyPoint += MarkTargetPointAsNonEmpty;
        }

        public PointMono GetFreePointByType(PointType pointType)
        {
            return _mapPoints.FirstOrDefault(x => x.IsEmpty & x.PointType == pointType);
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

            return _mapPoints.FirstOrDefault(x => x.ID == targetID && x.PointType == PointType.Sell);
        }

        private void MarkTargetPointAsNonEmpty(string pointID, Customer customer)
        {
            var point = _mapPoints.FirstOrDefault(x => x.ID == pointID);

            if (point == null)
            {
                Debug.Log($"|{this}| not foundTargetPoint");
                return;
            }

            point.SetNotEmpty(customer);
        }
    }
}