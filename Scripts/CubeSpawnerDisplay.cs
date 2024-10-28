using UnityEngine;
using TMPro;

public class CubeSpawnerDisplay : SpawnerDisplay<Cube>
{
    [SerializeField] private Spawner<Cube> _spawner;
    [SerializeField] private TextMeshProUGUI _metricsText;

    protected override void Awake()
    {
        Spawner = _spawner;
        MetricsText = _metricsText;
        base.Awake();
    }
}
