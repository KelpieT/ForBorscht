using UnityEngine;

public class RangeWeapon : WeaponBase
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float speedProjectile;

    protected override void Attack()
    {
        Projectile projectile = Instantiate<Projectile>(projectilePrefab, transform.position, transform.rotation);
        projectile.Init(speedProjectile, damage);
    }
}
