using UnityEngine;

public class MissileAttack : AttackSystem
{
    [SerializeField] private GameObject missilePref;
    protected override void Awake()
    {
        targetInfo = GetComponent<TargetInfo>();
    }

    public override void Attack(Transform target)
    {
        Instantiate(missilePref, attackingPart.position, attackingPart.rotation, transform);
        
        if (target.TryGetComponent<HealthSystem>(out var healthSystem))
        {
            healthSystem.TakeDamage(damage);
        }
    }
}
