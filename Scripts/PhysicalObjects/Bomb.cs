using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private GraphicUtils _graphicUtils;

    private Renderer _renderer;
    private float _alphaDefault = 1f;
    private float _alphaTransparent = 0f;

    public Action<Bomb> HasExploded;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Reset()
    {
        _renderer.material = _graphicUtils.GetMaterialWithAlpha(_alphaDefault, _renderer.material);
    }

    public void StartDecreaseAlphaCoroutine(float duration)
    {
        StartCoroutine(SmoothlyDecreaseAlphaCoroutine(duration));
    }

    private void Explode()
    {
        float explosionForce = 1000f;
        float explosionRadius = 6f;

        List<Rigidbody> objectsToExplode = GetObjectsInRadius(explosionRadius);

        foreach (Rigidbody objectToExplode in objectsToExplode)
            objectToExplode.AddExplosionForce(explosionForce, transform.position, explosionRadius);

        HasExploded?.Invoke(this);
    }

    private IEnumerator SmoothlyDecreaseAlphaCoroutine(float targetTime)
    {
        float currentTime = 0f;

        while (currentTime < targetTime)
        {
            float alpha = Mathf.Lerp(_alphaDefault, _alphaTransparent, currentTime / targetTime);
            _renderer.material = _graphicUtils.GetMaterialWithAlpha(alpha, _renderer.material);
            currentTime += Time.deltaTime;
            yield return null;
        }

        _renderer.material = _graphicUtils.GetMaterialWithAlpha(_alphaTransparent, _renderer.material);
        Explode();
    }

    private List<Rigidbody> GetObjectsInRadius(float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        List<Rigidbody> objectsInRadius = new List<Rigidbody>();

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
                objectsInRadius.Add(rigidbody);
        }

        return objectsInRadius;
    }
}
