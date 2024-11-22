using UnityEngine;

public class GraphicUtils : MonoBehaviour
{
    public Material GetMaterialWithAlpha(float alpha, Material material)
    {
        Color color = material.color;
        color.a = alpha;
        material.color = color;
        return material;
    }

    public Color GetRandomColor()
    {
        float minHue = 0f;
        float maxHue = 1f;
        float saturation = 1f;
        float brightness = 0.85f;

        return Random.ColorHSV(minHue, maxHue, saturation, saturation, brightness, brightness);
    }
}
