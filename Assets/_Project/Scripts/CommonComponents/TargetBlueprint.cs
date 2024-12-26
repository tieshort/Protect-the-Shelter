using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UnitBlueprint", menuName = "ScriptableObject/Unit Config")]
public class TargetBlueprint : ScriptableObject
{
    public GameObject hitEffect;
    public GameObject burnEffect;

    // public bool HasHealth = true;
    public GameObject UnitInfoUI;
    [Min(0)] public float hp = 100;
    [Min(0)] public float def = 0;

    // public bool CanMove = true;
    [Min(0)] public float speed = 1;

    // public bool CanBurn = true;
    [Min(0)] public int maxBurnStacks = 5;
    [Min(0)] public int burnProcsPerStack = 2;
    [Min(0)] public float burnDuration = 2;

    // public bool CanFreeze = true;
    [Min(0)] public int maxSlowStacks = 5;
    [Min(0)] public float slowDuration = 2;

    // cost
    [Min(0)] public int cost = 5;
}

// [CustomEditor(typeof(TargetBlueprint))]
// public class TargetBlueprintInspector : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         TargetBlueprint blueprint = (TargetBlueprint)target;

//         blueprint.HasHealth = EditorGUILayout.Toggle("Has health", blueprint.HasHealth);
//         if (blueprint.HasHealth)
//         {
//             blueprint.hp = EditorGUILayout.FloatField("HP", blueprint.hp);
//             blueprint.def = EditorGUILayout.FloatField("DEF", blueprint.def);
//         }

//         blueprint.CanBurn = EditorGUILayout.Toggle("Can burn", blueprint.CanBurn);
//         if (blueprint.CanBurn)
//         {
//             blueprint.maxBurnStacks = EditorGUILayout.IntField("Max burn stacks", blueprint.maxBurnStacks);
//             blueprint.burnProcsPerStack = EditorGUILayout.IntField("Burn procs per stack", blueprint.burnProcsPerStack);
//             blueprint.burnDuration = EditorGUILayout.FloatField("Burn duration", blueprint.burnDuration);
//         }

//         blueprint.CanFreeze = EditorGUILayout.Toggle("Can freeze", blueprint.CanFreeze);
//         if (blueprint.CanFreeze)
//         {
//             blueprint.maxSlowStacks = EditorGUILayout.IntField("Max slow stacks", blueprint.maxSlowStacks);
//             blueprint.slowDuration = EditorGUILayout.FloatField("Slow duration", blueprint.slowDuration);
//         }
//     }
// }