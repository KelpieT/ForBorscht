using UnityEngine;

public class AttackPlayerState : IState
{
    private readonly Player player;
    private readonly Animator animator;
    private readonly WeaponOwner weaponOwner;
    private readonly Level level;

    public AttackPlayerState(Player player, Animator animator, WeaponOwner weaponOwner, Level level)
    {
        this.player = player;
        this.animator = animator;
        this.weaponOwner = weaponOwner;
        this.level = level;

    }

    public void StartState()
    {
        animator.SetTrigger(ConstParams.ANIM_TRIGGER_RIFFLE_WALK);

    }

    public void UpdateState()
    {
        Enemy enemy = level.GetNearestEnemy(player.transform.position);
        if (enemy != null)
        {
            Vector3 dir = enemy.transform.position - player.transform.position;
            player.transform.rotation = Quaternion.LookRotation(dir);
            weaponOwner?.TryAttack(enemy);
        }
    }

    public void EndState()
    {
    }

}
