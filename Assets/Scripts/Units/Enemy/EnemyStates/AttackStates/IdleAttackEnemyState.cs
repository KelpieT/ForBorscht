public class IdleAttackEnemyState : IState
{
    private Enemy enemy;
    private Player player;

    public IdleAttackEnemyState(Enemy enemy, Player player)
    {
        this.enemy = enemy;
        this.player = player;
    }

    public void StartState()
    {
    }

    public void UpdateState()
    {
        if (enemy.EnemyDetector.InAttackDistance() == true)
        {
            enemy.ChangeAttackState(Enemy.EnemyCommand.Attack);
        }
    }

    public void EndState()
    {
    }
}
