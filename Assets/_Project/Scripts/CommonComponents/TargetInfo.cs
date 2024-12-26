using System.Collections.Generic;
using UnityEngine;

public class TargetInfo : MonoBehaviour
{
    public LayerMask targetLayers;
    public List<string> targetTags = new();
    [HideInInspector] public Transform currentTarget;

    public bool HasTag(string tag)
    {
        return targetTags.Contains(tag);
    }
}
