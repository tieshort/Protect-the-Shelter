using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetInfo), typeof(SphereCollider))]
public class Aiming : MonoBehaviour
{
    [SerializeField] private Transform trackingPart;
    [SerializeField] private Transform partToRotate;
    [SerializeField] private float range = 10;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private float lockAngle = 15f;
    public AimingType aimingType = AimingType.ClosestToSomePoint;
    [SerializeField] private Transform pointToProtect;
    private Func<bool> SelectionFunc;
    private TargetInfo targetInfo;
    private SphereCollider _collider;
    private List<Transform> targets = new();
    private Transform currentTarget;

    private void Start()
    {
        targetInfo = GetComponent<TargetInfo>();
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
        _collider.radius = range;
    }

    private void Awake()
    {
        SetSelectionFunc(aimingType);
    }

    private void OnEnable()
    {
        Setup();
    }

    private void Update()
    {
        if (SelectionFunc())
        {
            FocusOnTarget();
            if (LockedOnTarget())
            {
                targetInfo.currentTarget = currentTarget;
            }
            else
            {
                targetInfo.currentTarget = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetInfo.HasTag(other.tag))
        {
            targets.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (targetInfo.HasTag(other.tag))
        {
            targets.Remove(other.transform);
            if (other.transform == targetInfo.currentTarget)
            {
                targetInfo.currentTarget = null;
            }
        }
    }

    public void FocusOnTarget()
    {
        Vector3 direction = currentTarget.position - trackingPart.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
    }

    public bool LockedOnTarget()
    {
        return Vector3.Angle(currentTarget.position - transform.position, partToRotate.forward) <= lockAngle;
    }

    public void SetSelectionFunc(AimingType type)
    {
        switch(type)
        {
            case AimingType.Closest:
                SelectionFunc = TrySelectClosestTarget;
                break;
            case AimingType.ClosestToSomePoint:
                SelectionFunc = TrySelectClosestToPointTarget;
                break;
            case AimingType.FirstSeen:
                SelectionFunc = TrySelectFirstSeenTarget;
                break;
        }
    }

    private void Setup()
    {
        if (pointToProtect == null)
        {
            // pointToProtect = GameManager.Instance.PlayerBase;
            pointToProtect = GameObject.FindGameObjectWithTag("PlayerBase").transform;
        }
        targets.Clear();
    }

    private bool TrySelectClosestTarget()
    {
        targets.RemoveAll(item => item == null);
        if (targets.Count > 0)
        {
            float closest = float.PositiveInfinity;
            foreach (var target in targets)
            {
                if (target == null)
                {
                    continue;
                }
                float distance = Vector3.Distance(transform.position, target.position);
                if (distance < closest)
                {
                    closest = distance;
                    currentTarget = target;
                }
            }
            return true;
        }
        return false;
    }

    private bool TrySelectClosestToPointTarget()
    {
        targets.RemoveAll(item => item == null);
        if (targets.Count > 0)
        {
            float closest = float.PositiveInfinity;
            foreach (var target in targets)
            {
                if (target == null)
                {
                    continue;
                }
                float distance = Vector3.Distance(pointToProtect.position, target.position);
                if (distance < closest)
                {
                    closest = distance;
                    currentTarget = target;
                }
            }
            return true;
        }
        return false;
    }

    private bool TrySelectFirstSeenTarget()
    {
        targets.RemoveAll(item => item == null);
        if (targets.Count > 0)
        {
            currentTarget = targets.Find(item => item != null);
        }
        return false;
    }
}

public enum AimingType
{
    Closest,
    ClosestToSomePoint,
    FirstSeen
}
