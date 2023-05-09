using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    protected override void Attack()
    {
        Debug.Log(this);
        target.TakeDamage(damage);
    }
}
