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

    public Material GetMaterialTransparent(Material material)
    {
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
        return material;
    }

    public Material GetMaterialOpaque(Material material)
    {
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
        return material;
    }

    public Color GetRandomColor()
    {
        float minHue = 0f;
        float maxHue = 1f;
        float saturation = 0.8f;
        float brightness = 0.95f;

        return Random.ColorHSV(minHue, maxHue, saturation, saturation, brightness, brightness);
    }
}
