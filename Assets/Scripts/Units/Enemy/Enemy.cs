using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

public sealed class Enemy : Unit<Enemy.EnemyMoveStateEnum, Enemy.EnemyAttackStateEnum, Enemy.EnemyCommand>
{
    public enum EnemyMoveStateEnum
    {
        Idle,
        Move,
        Wander,
        Dead
    }

    public enum EnemyAttackStateEnum
    {
        Idle,
        Attack,
    }

    public enum EnemyCommand
    {
        Idle,
        Move,
        Wander,
        Attack,
        Dead,
    }

    private Player player;
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private EnemyDetector enemyDetector;
    [SerializeField] private WeaponOwner weaponOwner;
    [SerializeField] private DropChance dropChance;
    private PickupbleObjectFactory pickupFactory;

    public EnemyDetector EnemyDetector { get => enemyDetector; }

    [Inject]
    public void Construct(Player player, PickupbleObjectFactory pickupFactory)
    {
        this.player = player;
        this.pickupFactory = pickupFactory;
    }

    public override void Init()
    {
        base.Init();
        if (player != null)
        {
            enemyDetector.SetTarget(player);
        }
        weaponOwner.Init(unitSettings.Damage, unitSettings.TimeBeetwenAttack);
        weaponOwner.OnAttackSucces += AttackAnimation;
        ChangeAttackState(EnemyCommand.Idle);
    }
    
    private async void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 1);
        Debug.Log("Hello-o-o");
        await UniTask.Delay(1000);
        ChangeMoveState(EnemyCommand.Wander);

    }

    private void Update()
    {
        currentMoveState?.UpdateState();
        currentAttackState?.UpdateState();
        animator.SetFloat(ConstParams.ANIM_PARAM_RUN_SPEED, navMeshAgent.velocity.magnitude);
    }

    public override void Dead()
    {
        base.Dead();
        ChangeMoveState(EnemyCommand.Dead);
        ChangeAttackState(EnemyCommand.Dead);
    }

    protected override void InitMoveStateMachine()
    {
        List<Transition<EnemyMoveStateEnum, EnemyCommand>> transitions = new List<Transition<EnemyMoveStateEnum, EnemyCommand>>()
        {
            new Transition<EnemyMoveStateEnum, EnemyCommand>(EnemyMoveStateEnum.Idle,EnemyMoveStateEnum.Idle,EnemyCommand.Idle),
            new Transition<EnemyMoveStateEnum, EnemyCommand>(EnemyMoveStateEnum.Move,EnemyMoveStateEnum.Idle,EnemyCommand.Idle),
            new Transition<EnemyMoveStateEnum, EnemyCommand>(EnemyMoveStateEnum.Dead,EnemyMoveStateEnum.Idle,EnemyCommand.Idle),

            new Transition<EnemyMoveStateEnum, EnemyCommand>(EnemyMoveStateEnum.Idle,EnemyMoveStateEnum.Move,EnemyCommand.Move),

            new Transition<EnemyMoveStateEnum, EnemyCommand>(EnemyMoveStateEnum.Idle,EnemyMoveStateEnum.Dead,EnemyCommand.Dead),
            new Transition<EnemyMoveStateEnum, EnemyCommand>(EnemyMoveStateEnum.Move,EnemyMoveStateEnum.Dead,EnemyCommand.Dead),
            new Transition<EnemyMoveStateEnum, EnemyCommand>(EnemyMoveStateEnum.Wander,EnemyMoveStateEnum.Dead,EnemyCommand.Dead),

            new Transition<EnemyMoveStateEnum, EnemyCommand>(EnemyMoveStateEnum.Idle,EnemyMoveStateEnum.Wander,EnemyCommand.Wander),
            new Transition<EnemyMoveStateEnum, EnemyCommand>(EnemyMoveStateEnum.Move,EnemyMoveStateEnum.Wander,EnemyCommand.Wander),

        };

        Dictionary<EnemyMoveStateEnum, IState> states = new Dictionary<EnemyMoveStateEnum, IState>();
        states.Add(EnemyMoveStateEnum.Idle, new IdleEnemyState(this));
        states.Add(EnemyMoveStateEnum.Move, new MoveToTargetEnemyState(this, navMeshAgent, player));
        states.Add(EnemyMoveStateEnum.Dead, new DeadEnemyState(this, animator, pickupFactory, dropChance));
        states.Add(EnemyMoveStateEnum.Wander, new WanderEnemyState(this, navMeshAgent));

        moveLayerStateMachine = new StateMachine<EnemyMoveStateEnum, EnemyCommand>(states, transitions);
    }

    protected override void InitAttackStateMachine()
    {
        List<Transition<EnemyAttackStateEnum, EnemyCommand>> transitions = new List<Transition<EnemyAttackStateEnum, EnemyCommand>>()
        {
            new Transition<EnemyAttackStateEnum, EnemyCommand>(EnemyAttackStateEnum.Attack,EnemyAttackStateEnum.Idle,EnemyCommand.Idle),
            new Transition<EnemyAttackStateEnum, EnemyCommand>(EnemyAttackStateEnum.Idle,EnemyAttackStateEnum.Attack,EnemyCommand.Attack),
            new Transition<EnemyAttackStateEnum, EnemyCommand>(EnemyAttackStateEnum.Attack,EnemyAttackStateEnum.Idle,EnemyCommand.Dead),


        };

        Dictionary<EnemyAttackStateEnum, IState> states = new Dictionary<EnemyAttackStateEnum, IState>();
        states.Add(EnemyAttackStateEnum.Idle, new IdleAttackEnemyState(this, player));
        states.Add(EnemyAttackStateEnum.Attack, new AttackEnemyState(this, player, weaponOwner));

        attackLayerStatemachine = new StateMachine<EnemyAttackStateEnum, EnemyCommand>(states, transitions);
    }

    private void AttackAnimation()
    {
        animator.SetTrigger(ConstParams.ANIM_TRIGGER_ATTACK);
    }
}
