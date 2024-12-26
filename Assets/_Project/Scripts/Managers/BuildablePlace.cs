using UnityEngine;

[System.Serializable]
public class BuildablePlace
{
    public static bool buildMode = false;
    public bool available = true;
    public Transform transform;

    public BuildablePlace(Transform _transform)
    {
        transform = _transform;
    }
}
