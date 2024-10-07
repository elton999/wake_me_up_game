
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game3D.UI;

public class SequenceTimeLineUI : UIEntity
{
    private KeysSequence _keysSequence = new KeysSequence();
    private float _totalTime = 0f;

    public override void Start()
    {
        Sprite = Scene.Content.Load<Texture2D>(Path.SOLID_COLOR_TEXTURE_PATH);
        _keysSequence.Keys = new System.Collections.Generic.List<KeySequenceData>()
        {
            new KeySequenceData() { Time = 2.0f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 2.10f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 3.30f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 4.00f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 5.00f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 6.00f, Key = DirectionKey.UP},
        };
    }

    public override void Update(float deltaTime)
    {
        _totalTime += deltaTime;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        foreach (var key in _keysSequence.Keys)
        {
            var position = new Vector2(400.0f, 400.0f);
            position += Vector2.UnitX * (key.Time - _totalTime) * 500.0f;

            spriteBatch.Draw(Sprite, position, Color.White);
        }
    }
}
