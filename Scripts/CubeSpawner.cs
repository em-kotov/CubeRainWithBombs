using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private BombSpawner _bombSpawner;

    private int _poolCapacity = 40;
    private int _poolMaxSize = 50;
    private float _repeatRate = 2f;

    protected override void Awake()
    {
        Prefab = _cubePrefab;
        PoolCapacity = _poolCapacity;
        PoolMaxSize = _poolMaxSize;
        base.Awake();
    }

    private void Start()
    {
        StartCoroutine(SpawnCubesCoroutine(_repeatRate));
    }

    protected override void OnGet(Cube cube)
    {
        base.OnGet(cube);
        cube.gameObject.SetActive(true);
        cube.EnteredPlatform += StartDelayedDeactivationCoroutine;
        cube.SetLifeTime(GetRandomLifeTime());
        SetPosition(cube, GetRandomSpawnPositionXZ());
    }

    protected override void OnRelease(Cube cube)
    {
        base.OnRelease(cube);
        ActivateBomb(cube.transform.position, cube.LifeTime);
        cube.Reset();
        SetPosition(cube, transform.position);
        cube.EnteredPlatform -= StartDelayedDeactivationCoroutine;
        cube.gameObject.SetActive(false);
    }

    private void StartDelayedDeactivationCoroutine(Cube cube)
    {
        StartCoroutine(DelayedDeactivationCoroutine(cube));
    }

    private void ActivateBomb(Vector3 position, float duration)
    {
        _bombSpawner.SpawnBomb(position, duration);
    }

    private Vector3 GetRandomSpawnPositionXZ()
    {
        float spawnRadius = 5.5f;
        Vector3 randomPositionXZ = Random.insideUnitSphere * spawnRadius;
        return new Vector3(randomPositionXZ.x, transform.position.y, randomPositionXZ.z);
    }

    private float GetRandomLifeTime()
    {
        float minLifeTime = 2f;
        float maxLifeTime = 5f;

        return Random.Range(minLifeTime, maxLifeTime);
    }

    private IEnumerator DelayedDeactivationCoroutine(Cube cube)
    {
        yield return new WaitForSeconds(cube.LifeTime);
        OnRelease(cube);
    }

    private IEnumerator SpawnCubesCoroutine(float seconds)
    {
        var wait = new WaitForSeconds(seconds);

        while (enabled)
        {
            Spawn();
            yield return wait;
        }
    }
}
