using UnityEngine;

public static class LayerMaskConverter
{
    public static LayerMask GetLayerMask(params int[] layerIndexes)
    {
        LayerMask mask = 0;

        foreach (int index in layerIndexes)
        {
            if (index >= 0 || index <= 31)
                mask |= 1 << index;
        }

        return mask;
    }
}
