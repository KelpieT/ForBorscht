using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class WanderEnemyState : IState
{
    private const float POSITION_OFFSET_CHECK = 0.1f;
    private NavMeshAgent navMeshAgent;
    private Enemy enemy;
    private Vector3 selectedPosition;
    private float minRange = 1f;
    private float maxRange = 10f;
    private float minWaitTime = 0.2f;
    private float maxWaitTime = 2f;
    private float currentWaitTime;
    private bool waitTaskSet;
    private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    public WanderEnemyState(Enemy enemy, NavMeshAgent navMeshAgent)
    {
        this.enemy = enemy;
        this.navMeshAgent = navMeshAgent;
    }

    public void StartState()
    {
        cancellationTokenSource = new CancellationTokenSource();
        SetRandomDestination();
    }

    private async void SetRandomDestination()
    {
        try
        {
            currentWaitTime = Random.Range(minWaitTime, maxWaitTime);
            waitTaskSet = true;
            await UniTask.Delay(currentWaitTime.ToMilliseconds(), cancellationToken: cancellationTokenSource.Token);
            if (cancellationTokenSource.Token.IsCancellationRequested)
            {
                return;
            }
            float distance = Random.Range(minRange, maxRange);
            Vector3 pos = navMeshAgent.transform.position + Tools.RandomVectorXZ().normalized * distance;
            NavMeshHit hit;
            NavMesh.SamplePosition(pos, out hit, distance, NavMesh.AllAreas);
            selectedPosition = hit.position;
            navMeshAgent.SetDestination(selectedPosition);
            waitTaskSet = false;
        }
        catch
        {
            waitTaskSet = false;
        }
    }

    public void UpdateState()
    {
        if (enemy.EnemyDetector.InDetectDistance())
        {
            enemy.ChangeMoveState(Enemy.EnemyCommand.Move);
            return;
        }
        if ((waitTaskSet == false) && (navMeshAgent.transform.position - selectedPosition).magnitude < POSITION_OFFSET_CHECK)
        {
            SetRandomDestination();
        }

    }

    public void EndState()
    {
        if (navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.ResetPath();
        }
        cancellationTokenSource.Cancel();
    }
}
