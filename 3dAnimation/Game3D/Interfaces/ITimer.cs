namespace Game3D.Interfaces;

public interface ITimer : ITotalTimer, IUpdate
{
    float TimeTarget {get;}
}
