using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyDetector
{
    [SerializeField] private Transform currentTransform;
    [SerializeField] private float detectDistance;
    [SerializeField] private float attackDistance;
    private Player target;

    public float DetectDistance { get => detectDistance; }
    public bool Wary { get; set; }

    public void SetTarget(Player target)
    {
        this.target = target;
    }

    public bool InDetectDistance()
    {
        return InDistance(detectDistance);
    }

    public bool InAttackDistance()
    {
        return InDistance(attackDistance);
    }

    private bool InDistance(float distance)
    {

        if (target == null)
        {
            return false;
        }
        if (target.IsDead == true)
        {
            return false;
        }
        if (Wary == false)
        {
            return false;
        }
        return ((currentTransform.position - target.transform.position).magnitude < distance);
    }
}
