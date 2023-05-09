using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public enum AttackStatus
    {
        Success,
        EmptyCage,
        Reload
    }

    private float lastAttackTime;
    private float timeBeetwenAttack;
    protected float damage;
    protected IDamageble target;

    public void Init(float damage, float timeBeetwenAttack)
    {
        this.damage = damage;
        this.timeBeetwenAttack = timeBeetwenAttack;
    }

    public AttackStatus TryAttack(IDamageble target)
    {
        this.target = target;
        bool readyToAttack = Time.time - lastAttackTime >= timeBeetwenAttack;
        if (readyToAttack)
        {
            Attack();
            ResetAttackTime();
            return AttackStatus.Success;
        }
        else
        {
            return AttackStatus.Reload;
        }
    }

    protected abstract void Attack();

    private void ResetAttackTime()
    {
        lastAttackTime = Time.time;
    }
}
