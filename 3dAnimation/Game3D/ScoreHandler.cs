using System;
using Game3D.UI;

namespace Game3D;

public class ScoreHandler
{
    public struct ScoreValue
    {
        public int Score;
        public ScoreType Type;

        public static bool operator ==(ScoreValue a, ScoreValue b)
        {
            return a.Type == b.Type;
        }

        public static bool operator !=(ScoreValue a, ScoreValue b)
        {
            return !(a == b);
        }
    }

    public ScoreValue[] ScoreTable = new ScoreValue[]
    {
        new ScoreValue { Score = 180, Type = ScoreType.Perfect},
        new ScoreValue { Score = 150, Type = ScoreType.Good},
        new ScoreValue { Score = 100, Type = ScoreType.Ok},
        new ScoreValue { Score = -50, Type = ScoreType.Wrong},
    };

    public int Score;

    public int GetScoreValue(ScoreType scoreType)
    {
        int score = 0;
        int  index = Array.IndexOf(ScoreTable, new ScoreValue() {Type = scoreType});

        if(index != -1) score = ScoreTable[index].Score;

        return score;
    }

    public void AddScore(ScoreType scoreType)
    {
        Score += GetScoreValue(scoreType);
    }
}
