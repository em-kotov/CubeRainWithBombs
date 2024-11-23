using System;
using TMPro;
using UnityEngine;

public abstract class SpawnerDisplay<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _metricsText;
    [SerializeField] private Spawner<T> _spawner;

    private Type _type;

    private void Awake()
    {
        _type = typeof(T);
    }

    private void Update()
    {
        DisplayMetrics();
    }

    private void DisplayMetrics()
    {
        _metricsText.text = $"{_type.Name}\n" +
                            $"Total: {_spawner.TotalQuantity}\n" +
                           $"Active: {_spawner.ActiveQuantity}\n" +
                           $"Created: {_spawner.CreatedQuantity}";
    }
}
