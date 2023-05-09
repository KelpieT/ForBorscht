using UnityEngine;

[CreateAssetMenu(menuName = "GameData/UnitSettings")]
public class UnitSettings : ScriptableObject
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float damage;
    [SerializeField] private float timeBeetwenAttack;

    public float MaxHealth { get => maxHealth; }
    public float Damage { get => damage; }
    public float TimeBeetwenAttack { get => timeBeetwenAttack; }
}