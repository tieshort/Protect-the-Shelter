using UnityEngine;

[RequireComponent(typeof(TargetInfo))]
public class AttackSystem : MonoBehaviour
{
    [SerializeField] protected Transform attackingPart;
    [SerializeField] private GameObject attackEffectPref;
    [SerializeField] protected float damage = 5;
    [SerializeField] private float attackRate = 1;
    protected TargetInfo targetInfo;
    private float attackCountdown;
    private ParticleSystem attackParticles;

    protected virtual void Awake()
    {
        _ = Instantiate(attackEffectPref, attackingPart.position, attackingPart.rotation, attackingPart).TryGetComponent(out attackParticles);
        targetInfo = GetComponent<TargetInfo>();
    }

    private void OnEnable()
    {
        Setup();
    }

    private void Update()
    {
        if (targetInfo.currentTarget != null && attackCountdown <= 0f)
        {
            Attack(targetInfo.currentTarget);
            Reload();
        }

        if (attackCountdown > 0f) { attackCountdown -= Time.deltaTime; }
    }

    public virtual void Attack(Transform target)
    {
        if (target.TryGetComponent<HealthSystem>(out var health))
        {
            health.TakeDamage(damage);
            attackParticles?.Play();
        }
    }

    public void Reload()
    {
        attackCountdown = 1f / attackRate;
    }

    private void Setup()
    {
        attackCountdown = 0;
    }
}
