using System.Collections.Generic;
using System.Linq;

public class StateMachine<TState, TComand>
{
    private TState currentState;
    private List<Transition<TState, TComand>> transitions = new List<Transition<TState, TComand>>();
    private Dictionary<TState, IState> states = new Dictionary<TState, IState>();

    public StateMachine(Dictionary<TState, IState> states, List<Transition<TState, TComand>> transitions)
    {
        this.states = states;
        this.transitions = transitions;
    }

    public TState CurrentState { get => currentState; }

    private TState GetNextState(TComand command)
    {
        var v = transitions.FirstOrDefault(x => command.Equals(x.command) && currentState.Equals(x.currentState));
        if (v == null)
        {
            return default;
        }
        return v.nextState;
    }

    private IState GetState(TState state)
    {
        return states[state];
    }

    public IState GetNextStateByCommand(TComand command)
    {
        TState stateEnum = GetNextState(command);
        IState nextState = GetState(stateEnum);
        currentState = stateEnum;
        return nextState;
    }

}

public class Transition<TState1, TComand1>
{
    public TState1 currentState;
    public TState1 nextState;
    public TComand1 command;

    public Transition(TState1 currentState, TState1 nextState, TComand1 command)
    {
        this.currentState = currentState;
        this.nextState = nextState;
        this.command = command;
    }
}
