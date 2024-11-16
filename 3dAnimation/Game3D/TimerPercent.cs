using System;
using Game3D.Interfaces;

namespace Game3D;

public class TimerPercent : Timer, ITimerPercent
{
    public float PercentTotal => TotalTimer / TimeTarget;

    public TimerPercent(float timeTarget = float.MaxValue, Action callback = null) : base(timeTarget, callback)
    {
    }
}
