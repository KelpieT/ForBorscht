using System;
using UnityEngine;

public abstract class Unit<TMoveEnum, TAttackEnum, TComand> : MonoBehaviour, IHealth
{
    public event Action OnDead;
    public event Action OnTakeDamage;
    public event Action OnHealthChanged;

    [SerializeField] protected UnitSettings unitSettings;
    protected IState currentMoveState;
    protected IState currentAttackState;
    protected StateMachine<TMoveEnum, TComand> moveLayerStateMachine;
    protected StateMachine<TAttackEnum, TComand> attackLayerStatemachine;
    private float health;

    public float Health
    {
        get => health; protected set
        {
            health = value;
            OnHealthChanged?.Invoke();
        }
    }
    public float MaxHealth { get; private set; }
    public bool IsDead { get; protected set; }

    public virtual void Init()
    {
        MaxHealth = unitSettings.MaxHealth;
        Health = MaxHealth;
        IsDead = false;
        InitStateMachines();
    }

    public virtual void Dead()
    {
        IsDead = true;
        OnDead?.Invoke();
    }

    public virtual void TakeDamage(float damage)
    {
        if (IsDead)
        {
            return;
        }
        Health -= damage;
        OnTakeDamage?.Invoke();
        Debug.Log(damage);
        if (Health <= 0)
        {
            Dead();
            return;
        }
    }

    protected void InitStateMachines()
    {
        InitMoveStateMachine();
        InitAttackStateMachine();
    }

    public void ChangeMoveState(TComand command)
    {
        currentMoveState?.EndState();
        currentMoveState = moveLayerStateMachine?.GetNextStateByCommand(command);
        currentMoveState?.StartState();
    }

    public void ChangeAttackState(TComand command)
    {
        currentAttackState?.EndState();
        currentAttackState = attackLayerStatemachine.GetNextStateByCommand(command);
        currentAttackState?.StartState();
    }

    protected abstract void InitMoveStateMachine();
    protected abstract void InitAttackStateMachine();

    private void OnDestroy()
    {
        currentMoveState?.EndState();
        currentAttackState?.EndState();
    }

}
