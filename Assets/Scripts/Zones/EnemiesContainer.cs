using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

public class EnemiesContainer : MonoBehaviour
{
    [SerializeField] private int minInclusive = 5;
    [SerializeField] private int maxExclusive = 10;
    [SerializeField] private Transform minPosition;
    [SerializeField] private Transform maxPosition;
    private Func<Enemy> enemyFactory;
    private List<Enemy> enemies = new List<Enemy>();
    private bool playerInZone;

    [Inject]
    public void Construct(Func<Enemy> enemyFactory)
    {
        this.enemyFactory = enemyFactory;
    }
    public void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Enemy enemy = enemyFactory?.Invoke();
        enemy.Init();
        enemy.EnemyDetector.Wary = playerInZone;
        enemy.transform.position = RandomPosition();
        enemies.Add(enemy);
        enemy.OnDead += ClearEnemiesList;
        enemy.OnDead += SpawnAfterDeath;
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(
            minPosition.position.x, maxPosition.position.x),
            0,
            Random.Range(minPosition.position.z, maxPosition.position.z));
    }

    private void ClearEnemiesList()
    {
        enemies = enemies.Where(x => x != null).ToList();
    }

    private async void SpawnAfterDeath()
    {
        await UniTask.Delay(Random.Range(minInclusive.ToMilliseconds(), maxExclusive.ToMilliseconds()));
        Spawn();
    }

    public Enemy GetNearestEnemy(Vector3 position)
    {
        var ordered = enemies
            .Where(x => x != null)
            .Where(x => x.IsDead == false)
            .OrderBy(x => (x.transform.position - position).magnitude);
        return ordered.FirstOrDefault();
    }

    public void PlayerInZone(bool inZone)
    {
        playerInZone = inZone;
        ClearEnemiesList();
        enemies.ForEach(x => x.EnemyDetector.Wary = inZone);
    }
}
