using System;

namespace Game3D;

public class Timer
{
    private float _currentTime;
    private float _timeTarget;
    private Action _callback;
    private bool _finished = false;

    public float CurrentTime => _currentTime;
    public float TimeTarget => _timeTarget;

    public Timer(float timeTarget = float.MaxValue, Action callback = null)
    {
        _timeTarget = timeTarget;
        _callback = callback;
    }

    public void Update(float deltaTime)
    {
        if(_finished) return;

        _currentTime += deltaTime;
        if(_currentTime > _timeTarget) _callback?.Invoke();
    }
}
