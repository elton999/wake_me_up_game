using System;
using Game3D.UI;
using Microsoft.Xna.Framework;

namespace Game3D.Entities;

public class CameraController : UIEntity
{
    public enum CameraAnimation
    {
        Default,
        EndLevel,
    }

    public CameraAnimation CurrentAnimation = CameraAnimation.Default;

    public float Speed = 10.0f;
    public Vector3 CameraDefaultPosition = new Vector3(14.5f, 6.8f, 7.0f);

    public override void Start()
    {
        SequenceTimeLineUI.OnFinishEvent += EndLevel;
        StartLevel();
    }

    public override void Update(float deltaTime)
    {
        if(CurrentAnimation == CameraAnimation.Default)
        {

        }

        if(CurrentAnimation == CameraAnimation.EndLevel)
        {
            var cameraPosition = Scene.Camera.Position;
            float xValue = Math.Clamp(cameraPosition.X - Speed * deltaTime , 0.0f, CameraDefaultPosition.X);
            
            Scene.Camera.SetPosition(new Vector3(xValue, cameraPosition.Y, cameraPosition.Z));
        }
    }


    public void EndLevel()
    {
        CurrentAnimation = CameraAnimation.EndLevel;
    }

    public void StartLevel()
    {
        CurrentAnimation = CameraAnimation.Default;
        Scene.Camera.SetPosition(CameraDefaultPosition);
    }

}
