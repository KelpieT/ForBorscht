using UnityEngine;
using UnityEngine.AI;

public class MoveToTargetEnemyState : IState
{
    private Enemy enemy;
    private NavMeshAgent navMeshAgent;
    private Player player;

    public MoveToTargetEnemyState(Enemy enemy, NavMeshAgent navMeshAgent, Player player)
    {
        this.enemy = enemy;
        this.navMeshAgent = navMeshAgent;
        this.player = player;
    }

    public void StartState()
    {
    }

    public void UpdateState()
    {
        if (enemy.EnemyDetector.InDetectDistance())
        {
            Vector3 dif = player.transform.position - enemy.transform.position;
            navMeshAgent.SetDestination(player.transform.position - dif.normalized);
        }
        else
        {
            enemy.ChangeMoveState(Enemy.EnemyCommand.Wander);
        }
    }

    public void EndState()
    {
        if (navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.ResetPath();
        }
    }
}
