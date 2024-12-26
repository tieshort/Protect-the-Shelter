using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Missile : MonoBehaviour
{
    public MissileBlueprint blueprint;
    private CapsuleCollider _collider;
    // [HideInInspector] public TargetInfo targetInfo;
    private float lifetime;
    private float speed;
    [HideInInspector] public float damage;

    private void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
        _collider.isTrigger = true;
    }
    private void OnEnable()
    {
        SetupAndConfigure();
    }

    private void FixedUpdate()
    {
        float _speed = speed * Time.fixedDeltaTime;
        transform.Translate(_speed * Vector3.forward);
        _collider.height = Time.deltaTime * speed;
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    //     if (targetInfo.HasTag(other.tag))
    //     {
            transform.position = other.transform.position;
            // healthSystem.TakeDamage(damage);
            Destroy(gameObject);
            return;
    //     }
    }

    // private IEnumerator SlowEnemy(Enemy enemy)
    // {
    //     if (enemy != null)
    //     {
    //         enemy.agent.speed -= blueprint.slow;
    //         yield return new WaitForSeconds(blueprint.slowDuration);
    //     }
    //     if (enemy != null)
    //     {
    //         enemy.agent.speed += blueprint.slow;
    //     }
    // }

    private void SetupAndConfigure()
    {
        lifetime = blueprint.lifetime;
        speed = blueprint.speed;
        // _collider.height = speed * 2;
        // _collider.radius = 5f;
        // Destroy(gameObject, lifetime);
    }
}
