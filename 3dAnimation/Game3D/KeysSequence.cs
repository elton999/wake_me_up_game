using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Game3D;

public enum DirectionKey
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class KeySequenceData
{
    public float Time;
    public DirectionKey Key;
    public bool Checked;

    public Vector2 GetPosition(float currentTimer)
    {
        var position = new Vector2(Game1.ScreenW / 2.0f, 400.0f);
        position += Vector2.UnitX * GetTimer(currentTimer) * 200.0f;
        return position;
    }

    public float GetTimer(float currentTimer)
    {
        return Time - currentTimer;
    }
}

public struct KeysSequence
{
    public List<KeySequenceData> Keys;
}
