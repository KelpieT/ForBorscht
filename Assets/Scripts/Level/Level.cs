using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private EnemiesContainer enemiesContainer;
    [SerializeField] private EnemyZone enemyZone;
    [SerializeField] private int delaySpawnEnemiesStart = 3;
    [SerializeField] private int delaySpawnEnemies = 5;
    [SerializeField] private int startCountEnemies = 5;
    [SerializeField] private int spawnCountEnemiesPerWave = 2;
    [SerializeField] private int countSpawnWaves = 10;
    private Player player;
    private GameLifetimeScope gameLifetimeScope;

    [Inject]
    public void Construct(Player player, GameLifetimeScope gameLifetimeScope, Func<Enemy> enemyFactory)
    {
        this.player = player;
        this.player.transform.SetPositionAndRotation(playerSpawnPoint.position, playerSpawnPoint.rotation);
        this.player.SetLevel(this);
        this.gameLifetimeScope = gameLifetimeScope;
    }

    public async void Init()
    {
        enemyZone.OnPlayerInZone += enemiesContainer.PlayerInZone;
        await UniTask.Delay(delaySpawnEnemiesStart.ToMilliseconds());
        enemiesContainer.SpawnEnemies(startCountEnemies);
        for (int i = 0; i < countSpawnWaves; i++)
        {
            await UniTask.Delay(delaySpawnEnemies.ToMilliseconds());
            enemiesContainer.SpawnEnemies(spawnCountEnemiesPerWave);
        }
    }

    public Enemy GetNearestEnemy(Vector3 position)
    {
        return enemiesContainer.GetNearestEnemy(position);
    }

    private void OnDestroy()
    {
        if (enemiesContainer != null)
        {
            enemyZone.OnPlayerInZone -= enemiesContainer.PlayerInZone;
        }
    }

    public void RevivePlayer()
    {
        player.Revive();
        player.transform.SetPositionAndRotation(playerSpawnPoint.position, playerSpawnPoint.rotation);
    }
}
