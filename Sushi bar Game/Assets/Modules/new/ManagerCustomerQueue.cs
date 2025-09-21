using System;
using Cysharp.Threading.Tasks;
using Modules.Core.Factories;
using Modules.Features.Characters.Customer;
using Modules.@new;
using UnityEngine;
using Zenject;

public class ManagerCustomerQueue : IInitializable, IDisposable
{
    private readonly PoolCustomer _poolCustomer;
    private readonly Vector3 _spawnPosition;

    public ManagerCustomerQueue(
        FactoryCustomer factoryCustomer
        , Vector3 spawnPosition)
    {
        _spawnPosition = spawnPosition;
        _poolCustomer = new(factoryCustomer, _spawnPosition,5);

        SpawnFirstCustomer();
    }

    public void Initialize()
    {
        EventsCustomer.OnGetFood += SpawnCustomer;
        EventsCustomer.OnLeft += _poolCustomer.Return;
    }

    public void Dispose()
    {
        EventsCustomer.OnGetFood -= SpawnCustomer;
        EventsCustomer.OnLeft -= _poolCustomer.Return;
    }

    private void SpawnCustomer(Customer customer)
    {
        _poolCustomer.GetIn(_spawnPosition, Quaternion.identity);
    }

    private void SpawnFirstCustomer()
    {
        _poolCustomer.GetIn(_spawnPosition, Quaternion.identity);
    }
}