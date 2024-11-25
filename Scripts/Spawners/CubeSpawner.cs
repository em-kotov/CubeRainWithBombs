using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private float _spawnRadius = 2f;
    [SerializeField] private float _repeatRate = 0.8f;
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private void Start()
    {
        StartCoroutine(SpawnCubes(_repeatRate));
    }

    protected override void OnGet(Cube cube)
    {
        base.OnGet(cube);
        cube.EnteredPlatform += StartDelayedDeactivation;
        SetItemPosition(cube, RandomExtensions.GetRandomPositionXZ(transform.position, _spawnRadius));
        cube.SetLifeTime(RandomExtensions.GetRandomNumber(_minLifeTime, _maxLifeTime));
    }

    protected override void OnRelease(Cube cube)
    {
        Vector3 cubePosition = cube.transform.position;
        float cubeLifetime = cube.LifeTime;

        base.OnRelease(cube);
        cube.EnteredPlatform -= StartDelayedDeactivation;
        _bombSpawner.SpawnBomb(cubePosition, cubeLifetime);
        cube.Reset();
        SetItemPosition(cube, transform.position);
        cube.gameObject.SetActive(false);
    }

    private void StartDelayedDeactivation(Cube cube)
    {
        StartCoroutine(DelayedDeactivation(cube));
    }

    private IEnumerator DelayedDeactivation(Cube cube)
    {
        yield return new WaitForSeconds(cube.LifeTime);
        Pool.Release(cube);
    }

    private IEnumerator SpawnCubes(float seconds)
    {
        var wait = new WaitForSeconds(seconds);

        while (enabled)
        {
            Pool.Get();
            yield return wait;
        }
    }
}
