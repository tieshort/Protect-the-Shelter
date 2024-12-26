using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent<Transform> OnEnemyKilled = new();
    public static UnityEvent OnTurretBuilt = new();

    public static void SendEnemyKilled(Transform enemyTransform)
    {
        OnEnemyKilled.Invoke(enemyTransform);
    }

    public static void SendTurretBuilt()
    {
        OnTurretBuilt.Invoke();
    }
}