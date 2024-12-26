using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionDamage;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] ParticleSystem explosionEffect;
    private Missile missile;

    private void Awake()
    {
        explosionEffect = Instantiate(explosionEffect, GameManager.Instance.EffectsCollector);
    }
    private void OnEnable()
    {
        TryGetComponent<Missile>(out missile);
        if (missile != null)
        {
            explosionRadius = missile.blueprint.explosionRadius;
            explosionDamage = missile.blueprint.damage;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HealthSystem>(out var _))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        explosionEffect.transform.position = transform.position;
        explosionEffect.Play();
        Destroy(explosionEffect.gameObject, 1);
        Explode();
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, layerMask);

        foreach(var col in colliders)
        {
            if (col.TryGetComponent<HealthSystem>(out var target))
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);

                float damage = explosionDamage * (explosionRadius - distance) / (explosionRadius + 0.01f);

                target.TakeDamage(damage);
            }
            else if (col.TryGetComponent<Explosion>(out var explosive))
            {
                Destroy(explosive.gameObject);
            }
        }
    }
}
