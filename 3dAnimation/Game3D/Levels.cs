using System.Collections.Generic;

namespace Game3D;

public class Levels
{
    public static List<KeySequenceData> GetLevel1()
    {
        return new List<KeySequenceData>()
        {
            new KeySequenceData() { Time = 9.0f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 10.0f, KeyDirection = DirectionKey.RIGHT},
            new KeySequenceData() { Time = 11.0f, KeyDirection = DirectionKey.DOWN},
            new KeySequenceData() { Time = 12.0f, KeyDirection = DirectionKey.LEFT},
            new KeySequenceData() { Time = 13.0f, KeyDirection = DirectionKey.RIGHT},
            new KeySequenceData() { Time = 13.5f, KeyDirection = DirectionKey.RIGHT},
            new KeySequenceData() { Time = 15.0f, KeyDirection = DirectionKey.DOWN},
            new KeySequenceData() { Time = 17.0f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 18.0f, KeyDirection = DirectionKey.RIGHT},
            new KeySequenceData() { Time = 19.0f, KeyDirection = DirectionKey.DOWN},
            new KeySequenceData() { Time = 20.0f, KeyDirection = DirectionKey.LEFT},
            new KeySequenceData() { Time = 21.0f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 21.5f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 22.0f, KeyDirection = DirectionKey.RIGHT},
            new KeySequenceData() { Time = 22.5f, KeyDirection = DirectionKey.RIGHT},
            new KeySequenceData() { Time = 23.0f, KeyDirection = DirectionKey.DOWN},
            new KeySequenceData() { Time = 23.5f, KeyDirection = DirectionKey.DOWN},
            new KeySequenceData() { Time = 24.0f, KeyDirection = DirectionKey.LEFT},
            new KeySequenceData() { Time = 24.5f, KeyDirection = DirectionKey.LEFT},
            new KeySequenceData() { Time = 26.0f, KeyDirection = DirectionKey.UP},
        };
    }
}
