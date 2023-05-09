using UnityEngine;

public class DeadPlayerState : IState
{
    private Animator animator;

    public DeadPlayerState(Animator animator)
    {
        this.animator = animator;
    }

    public void EndState()
    {
    }

    public void StartState()
    {
        Debug.Log(ConstParams.ANIM_TRIGGER_DEAD);
        animator.SetTrigger(ConstParams.ANIM_TRIGGER_DEAD);
    }

    public void UpdateState()
    {
    }

}
