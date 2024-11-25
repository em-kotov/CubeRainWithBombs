using System;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private Color _defaultColor;
    private bool _hasEnteredPlatform = false;

    public float LifeTime { get; private set; }

    public event Action<Cube> EnteredPlatform;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform)
                && _hasEnteredPlatform == false)
        {
            SetColor(GraphicUtils.GetRandomColor());
            _hasEnteredPlatform = true;
            EnteredPlatform?.Invoke(this);
        }
    }

    public void Reset()
    {
        SetColor(_defaultColor);
        _hasEnteredPlatform = false;
    }

    public void SetLifeTime(float lifeTime)
    {
        if (lifeTime > 0f)
            LifeTime = lifeTime;
    }

    private void SetColor(Color color)
    {
        _renderer.material.color = color;
    }
}
