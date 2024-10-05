using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game3D;
public class Camera
{
    public const float FAR_PLANE = 5000;

    public Vector3 Position, Target;
    public Matrix View, Projection, ViewProjection;
    public Vector3 Up;
    public GraphicsDevice GPU;

    public Camera(GraphicsDevice gpu, Vector3 UpDirection)
    {
        GPU = gpu;
        Up = UpDirection;
        Position = new Vector3(10.0f, 5.0f, 5.0f);
        Target = Vector3.One;

        View = Matrix.CreateLookAt(Position, Target, Up);
        Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, gpu.Viewport.AspectRatio, 0.01f, FAR_PLANE);
        ViewProjection = View * Projection;
    }

    public void MoveCamera(Vector3 move)
    {
        Position += move;
        UpdateCamera();
    }

    public void SetPosition(Vector3 position)
    {
        Position = position;
        UpdateCamera();
    }

    public void UpdateTarget(Vector3 new_target)
    {
        Target = new_target;
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        View = Matrix.CreateLookAt(Position, Target, Up);
        ViewProjection = View * Projection;
    }


    public void DebugUpdate(float deltaTime)
    {
        float speed = 10.0f;
        if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                MoveCamera(Vector3.UnitX * speed * deltaTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                MoveCamera(-Vector3.UnitX * speed * deltaTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                MoveCamera(Vector3.UnitY * speed * deltaTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                MoveCamera(-Vector3.UnitY * speed * deltaTime);
        }
    }
}