using System.Collections.Generic;

namespace Game3D;

public enum DirectionKey
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public struct KeySequenceData
{
    public float Time;
    public DirectionKey Key;
}

public struct KeysSequence
{
    public List<KeySequenceData> Keys;
}
