using System;

namespace Game3D.Interfaces;

public interface ISequence : ITotalTimer
{
    event Action<DirectionKey> OnPressCorrectDirection;

    KeysSequence KeysSequence {get;}
}
