using System;
using Game3D.UI;

namespace Game3D.Interfaces;

public interface IRegisterScore
{
    event Action<ScoreType> OnRegisterScore;

    void RegisterScore(DirectionKey direction, ScoreType score);
    
}
