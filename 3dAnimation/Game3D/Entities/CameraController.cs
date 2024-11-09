using System;
using Microsoft.Xna.Framework;

namespace Game3D.Entities;

public class CameraController : UIEntity
{
    public float Speed = 10.0f;
    public Vector3 CameraDefaultPosition = new Vector3(14.5f, 6.8f, 7.0f);

    public override void Start() => StartLevel();

    public override void Update(float deltaTime)
    {
        if(GameStates.CurrentState == GameStates.State.PLAYING)
        {
            StartLevel();
        }

        if(GameStates.CurrentState == GameStates.State.END_LEVEL)
        {
            var cameraPosition = Scene.Camera.Position;
            float xValue = Math.Clamp(cameraPosition.X - Speed * deltaTime , 0.0f, CameraDefaultPosition.X);
            
            Scene.Camera.SetPosition(new Vector3(xValue, cameraPosition.Y, cameraPosition.Z));
        }
    }

    public void StartLevel()
    {
        Scene.Camera.SetPosition(CameraDefaultPosition);
    }

}
