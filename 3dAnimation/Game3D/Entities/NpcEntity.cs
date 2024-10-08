using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game3D.Entities;

public class NpcEntity : Entity3d
{
    public override void Start()
    {
        Model = Scene.Content.Load<Model>(Path.MODEL_NPC_PATH);
        Texture = Scene.Content.Load<Texture2D>(Path.SOLID_COLOR_TEXTURE_PATH);
        Effect = Scene.Content.Load<Effect>(Path.BASIC_SHADERS_PATH);
        ModelColor = Color.Red;

        Position = new Vector3(1.0f, 3.0f, 0);
    }
}
