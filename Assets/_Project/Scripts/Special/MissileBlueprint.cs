using UnityEngine;

[CreateAssetMenu(fileName = "MissileConfig", menuName = "ScriptableObject/Missile Config")]
public class MissileBlueprint : ScriptableObject
{
    public GameObject flyingEffect;
    public GameObject hitEffect;
    [Min(0)] public float lifetime = 2f;
    [Min(0)] public float speed = 150f;
    [Min(0)] public float damage = 5f;
    [Min(0)] public float slow = 0.001f;
    [Min(0)] public float slowDuration = 0.1f;
    [Min(0)] public float explosionRadius = 0f;
}
