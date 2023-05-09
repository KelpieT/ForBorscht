public class IdleEnemyState : IState
{
    private Enemy enemy;

    public IdleEnemyState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void StartState()
    {
    }

    public void UpdateState()
    {
        if (enemy.EnemyDetector.InDetectDistance())
        {
            enemy.ChangeMoveState(Enemy.EnemyCommand.Move);
            return;
        }
    }

    public void EndState()
    {
    }

}
