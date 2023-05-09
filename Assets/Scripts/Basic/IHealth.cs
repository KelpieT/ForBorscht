public interface IHealth : IDamageble
{
    event System.Action OnDead;
    event System.Action OnHealthChanged;

    float Health { get; }
    float MaxHealth { get; }
    bool IsDead { get; }
    void Dead();
}