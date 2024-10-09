using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit.Input;

namespace Game3D.UI;

public class SequenceTimeLineUI : UIEntity
{
    private KeysSequence _keysSequence = new KeysSequence();
    private float _totalTime = 0f;

    private float _perfectTime = 0.03f;
    private float _goodTime = 0.07f;
    private float _okTime = 0.15f;

    private float _offsetKeyPressing = 0.1f;
    private float _size = 1.0f;

    private Texture2D _spriteUp;
    private Texture2D _spriteDown;
    private Texture2D _spriteRight;
    private Texture2D _spriteLeft;

    public override void Start()
    {
        _spriteUp = Scene.Content.Load<Texture2D>(Path.UP_TEXTURE_PATH);
        _spriteDown = Scene.Content.Load<Texture2D>(Path.DOWN_TEXTURE_PATH);
        _spriteRight = Scene.Content.Load<Texture2D>(Path.RIGHT_TEXTURE_PATH);
        _spriteLeft = Scene.Content.Load<Texture2D>(Path.LEFT_TEXTURE_PATH);
        SetUp();
    }

    private void SetUp()
    {
        _keysSequence.Keys = new System.Collections.Generic.List<KeySequenceData>()
        {
            new KeySequenceData() { Time = 2.0f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 2.10f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 3.30f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 4.0f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 5.0f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 6.0f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 6.30f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 6.60f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 7.0f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 7.20f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 7.40f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 8.60f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 9.0f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 9.20f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 9.40f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 9.60f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 9.90f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 10.0f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 10.10f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 11.30f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 12.0f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 13.0f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 14.0f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 14.30f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 14.60f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 16.0f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 16.20f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 16.40f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 17.60f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 17.0f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 17.20f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 17.40f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 17.60f, Key = DirectionKey.UP},
            new KeySequenceData() { Time = 17.90f, Key = DirectionKey.UP},
        };
    }

    public override void Update(float deltaTime)
    {
        if (KeyBoardHandler.KeyPressed("reset"))
        {
            _totalTime = 0.0f;
            SetUp();
        }

        _totalTime += deltaTime;
        float timer = _totalTime + _offsetKeyPressing;


        _size = MathF.Max(1.0f, _size - 10.0f * deltaTime);

        if (!KeyBoardHandler.KeyPressed("up")) return;

        foreach (var key in _keysSequence.Keys)
        {
            if (key.Checked) continue;
            if (key.GetTimer(_totalTime) < 0.0f) continue;
            if (key.GetTimer(timer) <= _perfectTime)
            {
                _size += 1.0f;
                key.Checked = true;
                Console.WriteLine("Perfect!");
                Console.WriteLine(key.GetTimer(_totalTime));
                return;
            }

            if (key.GetTimer(timer) <= _goodTime)
            {
                _size += 1.0f;
                key.Checked = true;
                Console.WriteLine("Good!");
                Console.WriteLine(key.GetTimer(_totalTime));
                return;
            }

            if (key.GetTimer(timer) <= _okTime)
            {
                _size += 1.0f;
                key.Checked = true;
                Console.WriteLine("Ok!");
                Console.WriteLine(key.GetTimer(_totalTime));
                return;
            }
        }

        Console.WriteLine("wrong!");
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        DrawSprite(spriteBatch, _spriteUp, new Vector2(95.0f, 130.0f), Color.Black, _size);

        foreach (var key in _keysSequence.Keys)
        {
            var color = key.Checked ? Color.Gray : Color.White;
            DrawSprite(spriteBatch, _spriteUp, key.GetPosition(_totalTime, new Vector2(95.0f, 130.0f)), color);
        }
    }

    public void DrawSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Color color, float size = 1.0f)
    {
        spriteBatch.Draw(texture, position, null, color, 0.0f, texture.Bounds.Size.ToVector2() / 2f, size, SpriteEffects.None, 1.0f);
    }
}
