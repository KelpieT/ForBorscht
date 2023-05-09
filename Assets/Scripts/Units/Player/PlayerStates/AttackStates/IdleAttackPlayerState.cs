using UnityEngine;

public class IdleAttackPlayerState : IState
{
    private readonly Animator animator;

    public IdleAttackPlayerState(Animator animator)
    {
        this.animator = animator;
    }

    public void EndState()
    {
    }

    public void StartState()
    {
        animator.SetTrigger(ConstParams.ANIM_TRIGGER_RIFFLE_IDLE);
    }

    public void UpdateState()
    {
    }
}
