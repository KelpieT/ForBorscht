public class AttackEnemyState : IState
{
    private Enemy enemy;
    private Player player;
    private WeaponOwner weaponOwner;

    public AttackEnemyState(Enemy enemy, Player player, WeaponOwner weaponOwner)
    {
        this.player = player;
        this.enemy = enemy;
        this.weaponOwner = weaponOwner;
    }

    public void StartState()
    {
    }

    public void UpdateState()
    {
        if (enemy.EnemyDetector.InAttackDistance() == false)
        {
            enemy.ChangeAttackState(Enemy.EnemyCommand.Idle);
        }
        else
        {
            weaponOwner?.TryAttack(player);
        }

    }

    public void EndState()
    {
    }
}
