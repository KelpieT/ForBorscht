using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class Player : Unit<Player.PlayerMoveStateEnum, Player.PlayerAttackStateEnum, Player.PlayerCommand>
{
    public enum PlayerMoveStateEnum
    {
        Idle,
        Move,
        Dead
    }

    public enum PlayerAttackStateEnum
    {
        Idle,
        Attack,
    }

    public enum PlayerCommand
    {
        Idle,
        Move,
        Attack,
        Dead,
    }
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;
    [SerializeField] private WeaponOwner weaponOwner;
    private Joystick joystick;
    private Level level;
    private Inventory inventory = new Inventory();

    public Inventory Inventory { get => inventory; }

    [Inject]
    public void Construct(Joystick joystick)
    {
        this.joystick = joystick;

    }

    public void SetLevel(Level level)
    {
        this.level = level;
    }

    public override void Init()
    {
        base.Init();
        weaponOwner.Init(unitSettings.Damage, unitSettings.TimeBeetwenAttack);
        ChangeMoveState(PlayerCommand.Idle);
        ChangeAttackState(PlayerCommand.Idle);
    }

    private void Update()
    {
        currentMoveState?.UpdateState();
        currentAttackState?.UpdateState();
    }

    public override void Dead()
    {
        base.Dead();
        ChangeMoveState(PlayerCommand.Dead);
        ChangeAttackState(PlayerCommand.Dead);
    }

    public void Revive()
    {
        inventory.RemoveAll();
        IsDead = false;
        Health = MaxHealth;
        ChangeMoveState(PlayerCommand.Idle);
        ChangeAttackState(PlayerCommand.Idle);
    }

    public void PickUp(InventoryItem inventoryItem)
    {
        inventory.Add(inventoryItem);
    }

    protected override void InitMoveStateMachine()
    {
        List<Transition<PlayerMoveStateEnum, PlayerCommand>> transitions = new List<Transition<PlayerMoveStateEnum, PlayerCommand>>()
        {
            new Transition<PlayerMoveStateEnum, PlayerCommand>(PlayerMoveStateEnum.Idle,PlayerMoveStateEnum.Idle,PlayerCommand.Idle),
            new Transition<PlayerMoveStateEnum, PlayerCommand>(PlayerMoveStateEnum.Move,PlayerMoveStateEnum.Idle,PlayerCommand.Idle),
            new Transition<PlayerMoveStateEnum, PlayerCommand>(PlayerMoveStateEnum.Dead,PlayerMoveStateEnum.Idle,PlayerCommand.Idle),

            new Transition<PlayerMoveStateEnum, PlayerCommand>(PlayerMoveStateEnum.Idle,PlayerMoveStateEnum.Move,PlayerCommand.Move),

            new Transition<PlayerMoveStateEnum, PlayerCommand>(PlayerMoveStateEnum.Idle,PlayerMoveStateEnum.Dead,PlayerCommand.Dead),
            new Transition<PlayerMoveStateEnum, PlayerCommand>(PlayerMoveStateEnum.Move,PlayerMoveStateEnum.Dead,PlayerCommand.Dead),

        };

        Dictionary<PlayerMoveStateEnum, IState> states = new Dictionary<PlayerMoveStateEnum, IState>();
        states.Add(PlayerMoveStateEnum.Idle, new IdlePlayerState(this, animator, joystick));
        states.Add(PlayerMoveStateEnum.Move, new MovePlayerState(this, animator, joystick, characterController));
        states.Add(PlayerMoveStateEnum.Dead, new DeadPlayerState(animator));

        moveLayerStateMachine = new StateMachine<PlayerMoveStateEnum, PlayerCommand>(states, transitions);
    }

    protected override void InitAttackStateMachine()
    {
        List<Transition<PlayerAttackStateEnum, PlayerCommand>> transitions = new List<Transition<PlayerAttackStateEnum, PlayerCommand>>()
        {
            new Transition<PlayerAttackStateEnum, PlayerCommand>(PlayerAttackStateEnum.Attack,PlayerAttackStateEnum.Idle,PlayerCommand.Idle),
            new Transition<PlayerAttackStateEnum, PlayerCommand>(PlayerAttackStateEnum.Idle,PlayerAttackStateEnum.Attack,PlayerCommand.Attack),
            new Transition<PlayerAttackStateEnum, PlayerCommand>(PlayerAttackStateEnum.Attack,PlayerAttackStateEnum.Idle,PlayerCommand.Dead),


        };

        Dictionary<PlayerAttackStateEnum, IState> states = new Dictionary<PlayerAttackStateEnum, IState>();
        states.Add(PlayerAttackStateEnum.Idle, new IdleAttackPlayerState(animator));
        states.Add(PlayerAttackStateEnum.Attack, new AttackPlayerState(this, animator, weaponOwner, level));

        attackLayerStatemachine = new StateMachine<PlayerAttackStateEnum, PlayerCommand>(states, transitions);
    }

}
