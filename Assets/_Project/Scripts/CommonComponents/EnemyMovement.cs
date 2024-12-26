using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public Transform Destination;
    [SerializeField] private float speed;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        SetupAndConfigure();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Destination.tag))
        {
            GameManager.Instance.PlayerHealth--;
            if (TryGetComponent<HealthSystem>(out var health))
            {
                health.Die();
            }
        }
    }

    public IEnumerator RemindTarget()
    {
        while (Agent != null)
        {
            Agent?.SetDestination(Destination.position);
            yield return new WaitForSeconds(1);
        }
    }

    public void SetupAndConfigure()
    {
        Destination = GameManager.Instance.PlayerBase;
        Agent.speed = speed;
        StartCoroutine(RemindTarget());
    }
}
