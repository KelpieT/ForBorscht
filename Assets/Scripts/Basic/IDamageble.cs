public interface IDamageble
{
    event System.Action OnTakeDamage;
    void TakeDamage(float damage);
}