using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    public void SpawnBomb(Vector3 position, float duration)
    {
        Bomb bomb = Pool.Get();
        SetItemPosition(bomb, position);
        bomb.StartDecreaseAlpha(duration);
    }

    protected override void OnGet(Bomb bomb)
    {
        base.OnGet(bomb);
        bomb.HasExploded += Pool.Release;
    }

    protected override void OnRelease(Bomb bomb)
    {
        base.OnRelease(bomb);
        bomb.HasExploded -= Pool.Release;
        bomb.Reset();
        SetItemPosition(bomb, transform.position);
        bomb.gameObject.SetActive(false);
    }
}
