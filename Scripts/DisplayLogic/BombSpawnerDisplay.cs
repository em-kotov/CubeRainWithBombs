using UnityEngine;
using TMPro;

public class BombSpawnerDisplay : SpawnerDisplay<Bomb>
{
    [SerializeField] private Spawner<Bomb> _spawner;
    [SerializeField] private TextMeshProUGUI _metricsText;

    protected override void Awake()
    {
        Spawner = _spawner;
        MetricsText = _metricsText;
        base.Awake();
    }
}
