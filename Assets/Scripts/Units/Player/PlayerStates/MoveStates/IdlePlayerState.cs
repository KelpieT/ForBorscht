using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePlayerState : IState
{
    private readonly Joystick joystick;
    private readonly Player player;
    private readonly Animator animator;

    public IdlePlayerState(Player player, Animator animator, Joystick joystick)
    {
        this.joystick = joystick;
        this.player = player;
        this.animator = animator;
    }

    public void StartState()
    {
        joystick.OnEnableJoystick += Move;
        animator.SetTrigger(ConstParams.ANIM_TRIGGER_IDLE);
    }

    private void Move()
    {
        player.ChangeMoveState(Player.PlayerCommand.Move);
    }

    public void UpdateState()
    {
    }
    public void EndState()
    {
        joystick.OnEnableJoystick -= Move;
    }
}
