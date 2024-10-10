using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game3D.UI;

public class CharBoardUI : UIEntity
{
    private Vector2 _origin => Sprite.Bounds.Size.ToVector2() / 2f;
    private Vector2 positionOffset => Vector2.UnitY * 85f;
    private Vector2 _position => new Vector2(Game1.ScreenW / 2.0f, _origin.Y) + positionOffset;

    public override void Start()
    {
        Sprite = Scene.Content.Load<Texture2D>(Path.UI_CHAR_TEXTURE_PATH);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprite, _position, null, Color.White, 0.0f, _origin, 1.0f, SpriteEffects.None, 1.0f);
    }
}
