using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private Bomb _bombPrefab;

    private int _poolCapacity = 40;
    private int _poolMaxSize = 50;

    protected override void Awake()
    {
        Prefab = _bombPrefab;
        PoolCapacity = _poolCapacity;
        PoolMaxSize = _poolMaxSize;
        base.Awake();
    }

    protected override void OnGet(Bomb bomb)
    {
        base.OnGet(bomb);
        bomb.gameObject.SetActive(true);
        bomb.HasExploded += Despawn;
    }

    protected override void OnRelease(Bomb bomb)
    {
        base.OnRelease(bomb);
        bomb.Reset();
        SetPosition(bomb, transform.position);
        bomb.HasExploded -= Despawn;
        bomb.gameObject.SetActive(false);
    }

    public void SpawnBomb(Vector3 position, float duration)
    {
        Bomb bomb = Spawn();
        SetPosition(bomb, position);
        bomb.StartDecreaseAlphaCoroutine(duration);
    }
}
