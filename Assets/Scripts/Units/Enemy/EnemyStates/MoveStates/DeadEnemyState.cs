using UnityEngine;

public class DeadEnemyState : IState
{
    private const float TimeDestroyAfterDeath = 1f;
    private Animator animator;
    private Enemy enemy;
    private PickupbleObjectFactory pickupFactory;
    private DropChance dropChance;
    private float timer;
    private float destroyTime;

    public DeadEnemyState(Enemy enemy, Animator animator, PickupbleObjectFactory pickupFactory, DropChance dropChance)
    {
        this.enemy = enemy;
        this.animator = animator;
        this.dropChance = dropChance;
        this.pickupFactory = pickupFactory;
    }

    public void StartState()
    {
        destroyTime = Time.time + TimeDestroyAfterDeath;
        animator.SetTrigger(ConstParams.ANIM_TRIGGER_DEAD);
    }

    public void UpdateState()
    {
        timer += Time.deltaTime;
        if (Time.time + timer > destroyTime)
        {
            ItemID itemID = dropChance.GetItemID();
            Debug.Log(itemID);
            PickupbleObject pickupbleObject = pickupFactory.Create(itemID);
            if (pickupbleObject != null)
            {
                pickupbleObject.transform.position = enemy.transform.position;
            }
            GameObject.Destroy(enemy.gameObject);
        }
    }

    public void EndState()
    {
    }
}
