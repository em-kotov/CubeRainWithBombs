using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 15;
    [SerializeField] private int _poolMaxSize = 20;

    protected ObjectPool<T> Pool;

    public int TotalQuantity { get; private set; }
    public int CreatedQuantity { get; private set; }
    public int ActiveQuantity { get; private set; }

    private void Awake()
    {
        Pool = new ObjectPool<T>(
            createFunc: () => OnCreate(),
            actionOnGet: item => OnGet(item),
            actionOnRelease: item => OnRelease(item),
            actionOnDestroy: item => Destroy(item),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);

        TotalQuantity = 0;
        CreatedQuantity = 0;
        ActiveQuantity = 0;
    }

    protected virtual void OnGet(T item)
    {
        TotalQuantity++;
        ActiveQuantity++;
        item.gameObject.SetActive(true);
    }

    protected virtual void OnRelease(T item)
    {
        ActiveQuantity--;
    }

    protected void SetItemPosition(T item, Vector3 position)
    {
        item.transform.position = position;
    }

    private T OnCreate()
    {
        CreatedQuantity++;
        return Instantiate(_prefab);
    }
}