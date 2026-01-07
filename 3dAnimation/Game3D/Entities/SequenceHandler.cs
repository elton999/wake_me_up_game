using System;
using Game3D.Interfaces;
using Game3D.UI;
using UmbrellaToolsKit.Input;

namespace Game3D.Entities;

public class SequenceHandler : UIEntity, ISetup, IRegisterScore, ISequence, ITotalTimer
{
    public const float PERFECT_TIME = 0.03f;
    public const float GOOD_TIME = 0.07f;
    public const float OK_TIME = 0.15f;
    public const float OFFSET_KEY_PRESSING = 0.1f;

    private ITimer _timerCallback = new Timer();
    private float _totalTime = 0f;

    private KeysSequence _keysSequence = new KeysSequence();

    public event Action<ScoreType> OnRegisterScore;
    public event Action<DirectionKey> OnPressCorrectDirection;

    public KeysSequence KeysSequence => _keysSequence;

    public float TotalTimer => _totalTime;

    public void Setup()
    {
        _keysSequence.Keys = Levels.GetCurrentLevel();
        float lastTime = 0.0f;
        foreach (var key in _keysSequence.Keys)
            if (key.Time > lastTime)
                lastTime = key.Time;

        float delay = 1.0f;

        _timerCallback = new Timer(lastTime + delay, delegate
        {
            GameStates.SwitchState(GameStates.State.END_LEVEL);
        });
    }

    public override void Start()
    {
        Setup();
    }

    public override void Update(float deltaTime)
    {
        if (KeyBoardHandler.KeyPressed("reset"))
        {
            _totalTime = 0.0f;
            Setup();
        }

        if (GameStates.CurrentState != GameStates.State.PLAYING) return;

        _totalTime += deltaTime;
        _timerCallback.Update(deltaTime);

        if (KeyBoardHandler.KeyPressed("up"))
            OnPressButton(DirectionKey.UP);
        if (KeyBoardHandler.KeyPressed("down"))
            OnPressButton(DirectionKey.DOWN);
        if (KeyBoardHandler.KeyPressed("left"))
            OnPressButton(DirectionKey.LEFT);
        if (KeyBoardHandler.KeyPressed("right"))
            OnPressButton(DirectionKey.RIGHT);
    }

    public void OnPressButton(DirectionKey direction)
    {
        float timer = _totalTime + OFFSET_KEY_PRESSING;

        foreach (var key in _keysSequence.Keys)
        {
            if (key.KeyDirection != direction) continue;
            if (key.Checked) continue;
            if (key.GetTimer(_totalTime) < 0.0f) continue;

            if (key.GetTimer(timer) <= PERFECT_TIME)
            {
                key.Checked = true;
                RegisterScore(direction, ScoreType.Perfect);
                return;
            }

            if (key.GetTimer(timer) <= GOOD_TIME)
            {
                key.Checked = true;
                RegisterScore(direction, ScoreType.Good);
                return;
            }

            if (key.GetTimer(timer) <= OK_TIME)
            {
                key.Checked = true;
                RegisterScore(direction, ScoreType.Ok);
                return;

            }
        }

        OnRegisterScore?.Invoke(ScoreType.Wrong);
    }

    public void RegisterScore(DirectionKey direction, ScoreType score)
    {
        OnRegisterScore?.Invoke(score);
        OnPressCorrectDirection?.Invoke(direction);
    }
}
