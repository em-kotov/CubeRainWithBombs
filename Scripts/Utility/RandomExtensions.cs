using UnityEngine;

public static class RandomExtensions
{
    public static Vector3 GetRandomPositionXZ(Vector3 position, float radius)
    {
        Vector3 randomPositionXZ = Random.insideUnitSphere * radius;
        return new Vector3(randomPositionXZ.x, position.y, randomPositionXZ.z);
    }

    public static float GetRandomNumber(float min, float max)
    {
        return Random.Range(min, max);
    }
}
