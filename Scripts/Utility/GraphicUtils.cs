using UnityEngine;

public static class GraphicUtils
{
    public static Material SetAlpha(float alpha, Material material)
    {
        Color color = material.color;
        color.a = alpha;
        material.color = color;
        return material;
    }

    public static Color GetRandomColor()
    {
        float minHue = 0f;
        float maxHue = 1f;
        float saturation = 1f;
        float brightness = 0.85f;

        return Random.ColorHSV(minHue, maxHue, saturation, saturation, brightness, brightness);
    }
}
