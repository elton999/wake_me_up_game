using System;

namespace Game3D.Entities;

public class GameStates
{
    public static Action<State> OnSwitchStateEvent;

    public enum State
    {
        NONE,
        PLAYING,
        END_LEVEL,
        GO_TO_NEXT_LEVEL,
    }

    public static State CurrentState = State.PLAYING;

    public static void SwitchState(State state)
    {
        if(state == CurrentState) return;
        CurrentState = state;
        OnSwitchStateEvent?.Invoke(state);
    }

}
