using System;
using Game3D.UI;
using Game3D.Interfaces;

namespace Game3D;

public class ScoreHandler : IOnGotScore, IScore
{
    public struct ScoreValue : IScore
    {
        public int Score {get; set;}
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

    public int Score {get; set;}

    private IRegisterScore _registerScore;

    public ScoreHandler(IRegisterScore registerScore)
    {
        _registerScore = registerScore;
        _registerScore.OnRegisterScore += OnGotScore;
        SequenceTimeLineUI.OnStartNewLevelEvent += StartLevel;
    }

    public void StartLevel() => Score = 0;

    public int GetScoreValue(ScoreType scoreType)
    {
        for(int scoreIndex = 0; scoreIndex < ScoreTable.Length; scoreIndex++)
            if(ScoreTable[scoreIndex].Type == scoreType)
                return ScoreTable[scoreIndex].Score;

        return 0;
    }

    public void OnGotScore(ScoreType scoreType)
    {
        Score += GetScoreValue(scoreType);
        Console.WriteLine($"Score: {Score}");
    }
}
