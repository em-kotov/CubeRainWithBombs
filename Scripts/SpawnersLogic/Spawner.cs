using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    protected T Prefab;
    protected ObjectPool<T> Pool;
    protected int PoolCapacity, PoolMaxSize;

    public int TotalQuantity { get; protected set; }
    public int CreatedQuantity { get; protected set; }
    public int ActiveQuantity { get; protected set; }

    protected virtual void Awake()
    {
        Pool = new ObjectPool<T>(
            createFunc: () => OnCreate(),
            actionOnGet: item => OnGet(item),
            actionOnRelease: item => OnRelease(item),
            actionOnDestroy: item => Destroy(item),
            collectionCheck: true,
            defaultCapacity: PoolCapacity,
            maxSize: PoolMaxSize);

        TotalQuantity = 0;
        CreatedQuantity = 0;
        ActiveQuantity = 0;
    }

    protected virtual void OnGet(T item)
    {
        ActiveQuantity++;
    }

    protected virtual void OnRelease(T item)
    {
        ActiveQuantity--;
    }

    public T OnCreate()
    {
        CreatedQuantity++;
        return Instantiate(Prefab);
    }

    public T Spawn()
    {
        TotalQuantity++;
        return Pool.Get();
    }

    public void Despawn(T item)
    {
        Pool.Release(item);
    }

    protected void SetPosition(T item, Vector3 position)
    {
        item.transform.position = position;
    }
}