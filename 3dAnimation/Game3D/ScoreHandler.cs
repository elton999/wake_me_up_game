using Game3D.UI;

namespace Game3D;

public class ScoreHandler
{
    public struct ScoreValue
    {
        public int Score;
        public ScoreType Type;
    }

    public ScoreValue[] ScoreTable = new ScoreValue[]
    {
        new ScoreValue { Score = 180, Type = ScoreType.Perfect},
        new ScoreValue { Score = 150, Type = ScoreType.Good},
        new ScoreValue { Score = 100, Type = ScoreType.Ok},
        new ScoreValue { Score = -50, Type = ScoreType.Wrong},
    };
}
