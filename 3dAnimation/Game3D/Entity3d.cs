using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game3D;

public class Entity3d : Entity
{
    public Model Model;
    public Texture2D Texture;
    public Effect Effect;

    public Matrix World;
    public Color ModelColor = Color.White;

    public void UpdateAnimation(float deltaTime) { }

    public void UpdateTransforms()
    {
        World = Matrix.CreateTranslation(Position);
    }

    public virtual void Draw(Matrix view, Matrix projection)
    {
        Scene.Camera.GPU.BlendState = BlendState.Opaque;
        Scene.Camera.GPU.DepthStencilState = DepthStencilState.Default;
        Scene.Camera.GPU.SamplerStates[0] = SamplerState.LinearWrap;
        Scene.Camera.GPU.RasterizerState = RasterizerState.CullNone;

        foreach (ModelMesh mesh in Model.Meshes)
        {
            foreach (ModelMeshPart meshPart in mesh.MeshParts)
            {
                meshPart.Effect = Effect;

                Effect.Parameters["World"].SetValue(World);
                Effect.Parameters["View"].SetValue(view);
                Effect.Parameters["Projection"].SetValue(projection);

                Effect.Parameters["lightPosition"].SetValue(Vector3.One * 50);
                Effect.Parameters["lightColor"].SetValue(Color.White.ToVector4());
                Effect.Parameters["modelColor"].SetValue(ModelColor.ToVector4());
                Effect.Parameters["lightIntensity"].SetValue(1.0f);
                Effect.Parameters["SpriteTexture"].SetValue(Texture);
            }
            mesh.Draw();
        }
    }

}
