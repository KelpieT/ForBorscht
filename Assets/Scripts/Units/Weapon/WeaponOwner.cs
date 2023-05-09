using System;
using UnityEngine;

public class WeaponOwner : MonoBehaviour
{

    public event Action OnEmptyCage;
    public event Action OnAttackSucces;
    [SerializeField] private WeaponBase currentWeapon;

    public WeaponBase CurrentWeapon { get => currentWeapon; }

    public void Init(float damage, float timeBeetwenAttack)
    {
        currentWeapon?.Init(damage, timeBeetwenAttack);
    }

    public void TryAttack(IDamageble target)
    {
        if (currentWeapon == null)
        {
            Debug.Log("weapon null");
            return;
        }
        WeaponBase.AttackStatus status = currentWeapon.TryAttack(target);
        switch (status)
        {
            case WeaponBase.AttackStatus.EmptyCage:
                OnEmptyCage?.Invoke();
                break;
            case WeaponBase.AttackStatus.Success:
                OnAttackSucces?.Invoke();
                break;

        }

    }

}
