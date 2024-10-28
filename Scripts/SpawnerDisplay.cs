using System;
using TMPro;
using UnityEngine;

public abstract class SpawnerDisplay<T> : MonoBehaviour where T : MonoBehaviour
{
    protected Spawner<T> Spawner;
    protected TextMeshProUGUI MetricsText;

    private Type _type;

    protected virtual void Awake()
    {
        _type = typeof(T);
    }

    private void Update()
    {
        DisplayMetrics();
    }

    public void DisplayMetrics()
    {
        MetricsText.text = $"{_type.Name}\n" +
                            $"Total: {Spawner.TotalQuantity}\n" +
                           $"Active: {Spawner.ActiveQuantity}\n" +
                           $"Created: {Spawner.CreatedQuantity}";
    }
}
