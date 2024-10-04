using Microsoft.Xna.Framework.Graphics;

namespace Game3D.Entites;

public class HouseEntity : Entity
{
    public override void Start()
    {
        Model = Scene.Content.Load<Model>(Path.MODEL_HOUSE_PATH);
        Texture = Scene.Content.Load<Texture2D>(Path.BASIC_TEXTURE_PATH);
        Effect = Scene.Content.Load<Effect>(Path.BASIC_SHADERS_PATH);
    }
}
