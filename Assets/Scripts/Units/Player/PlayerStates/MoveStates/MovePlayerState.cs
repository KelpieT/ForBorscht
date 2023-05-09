using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerState : IState
{
    private readonly CharacterController controller;
    private readonly Joystick joystick;
    private readonly Animator animator;
    private readonly Player player;
    private float moveSpeed = 10f;

    public MovePlayerState(Player player, Animator animator, Joystick joystick, CharacterController controller)
    {
        this.animator = animator;
        this.controller = controller;
        this.joystick = joystick;
        this.player = player;
    }

    public void StartState()
    {
        animator.SetTrigger(ConstParams.ANIM_TRIGGER_RUN);
        joystick.OnDisableJoystick += StopMoveing;
    }

    private void StopMoveing()
    {
        player.ChangeMoveState(Player.PlayerCommand.Idle);
    }

    public void UpdateState()
    {
        Vector3 input = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        controller.Move(input * moveSpeed * Time.deltaTime + Physics.gravity * Time.deltaTime);
        if (joystick.InDeadZone())
        {
            Quaternion rotation = Quaternion.LookRotation(input);
            controller.transform.rotation = rotation;
        }
        animator.SetFloat(ConstParams.ANIM_PARAM_RUN_SPEED, input.magnitude);

    }

    public void EndState()
    {
        joystick.OnDisableJoystick -= StopMoveing;
    }

}
