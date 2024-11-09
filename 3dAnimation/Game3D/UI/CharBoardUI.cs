using System;
using Game3D.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game3D.UI;

public class CharBoardUI : UIEntity
{
    private Vector2 _origin => Sprite.Bounds.Size.ToVector2() / 2f;
    private Vector2 positionOffset => Vector2.UnitY * 85f;
    private Vector2 _position => new Vector2(Game1.ScreenW / 2.0f, _origin.Y) + positionOffset;
    private float _transparency = 1.0f;
    private const float TRANSPARENCY_DEFAULT_VALUE = 1.0f;
    private const float FADEOUT_SPEED = 10.0f;

    public override void Start()
    {
        Sprite = Scene.Content.Load<Texture2D>(Path.UI_CHAR_TEXTURE_PATH);
    }

    public override void Update(float deltaTime)
    {
        if(GameStates.CurrentState == GameStates.State.PLAYING)
        {
            _transparency = TRANSPARENCY_DEFAULT_VALUE;
            return;
        }

        if(GameStates.CurrentState == GameStates.State.END_LEVEL)
        {
            _transparency = Math.Max(_transparency - deltaTime * FADEOUT_SPEED, 0.0f);
            return;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprite, _position, null, Color.White * _transparency, 0.0f, _origin, 1.0f, SpriteEffects.None, 1.0f);
    }
}
