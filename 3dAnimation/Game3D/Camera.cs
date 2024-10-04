using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        Position = new Vector3(20.0f, 12.0f, 10.0f);
        Target = Vector3.One;

        View = Matrix.CreateLookAt(Position, Target, Up);
        Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, gpu.Viewport.AspectRatio, 0.01f, FAR_PLANE);
        ViewProjection = View * Projection;
    }

    public void MoveCamera(Vector3 move)
    {
        Position += move;
        View = Matrix.CreateLookAt(Position, Target, Up);
        ViewProjection = View * Projection;
    }

    public void UpdateTarget(Vector3 new_target)
    {
        Target = new_target;
        View = Matrix.CreateLookAt(Position, Target, Position);
        ViewProjection = View * Projection;
    }
}