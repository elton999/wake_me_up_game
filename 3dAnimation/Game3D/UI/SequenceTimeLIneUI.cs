using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit.Input;

namespace Game3D.UI;

public class ArrowsDirection
{
    public Texture2D Sprite;
    public Vector2 Position;
    public float Size;
}

public class SequenceTimeLineUI : UIEntity
{
    private KeysSequence _keysSequence = new KeysSequence();
    private float _totalTime = 0f;
    private Dictionary<DirectionKey, ArrowsDirection> _keySettings;

    private float _perfectTime = 0.03f;
    private float _goodTime = 0.07f;
    private float _okTime = 0.15f;

    private float _offsetKeyPressing = 0.1f;
    private float _size = 1.0f;

    public override void Start()
    {
        Texture2D _spriteUp = Scene.Content.Load<Texture2D>(Path.UP_TEXTURE_PATH);
        Texture2D _spriteDown = Scene.Content.Load<Texture2D>(Path.DOWN_TEXTURE_PATH);
        Texture2D _spriteRight = Scene.Content.Load<Texture2D>(Path.RIGHT_TEXTURE_PATH);
        Texture2D _spriteLeft = Scene.Content.Load<Texture2D>(Path.LEFT_TEXTURE_PATH);

        _keySettings = new Dictionary<DirectionKey, ArrowsDirection>
        {
            { DirectionKey.UP, new ArrowsDirection() { Sprite = _spriteUp, Position = new Vector2(95.0f, 130.0f), Size = 1.0f } },
            { DirectionKey.RIGHT, new ArrowsDirection() { Sprite = _spriteRight, Position = new Vector2(95.0f, 170.0f), Size = 1.0f } },
            { DirectionKey.DOWN, new ArrowsDirection() { Sprite = _spriteDown, Position = new Vector2(95.0f, 210.0f), Size = 1.0f } },
            { DirectionKey.LEFT, new ArrowsDirection() { Sprite = _spriteLeft, Position = new Vector2(95.0f, 250.0f), Size = 1.0f } }
        };

        SetUp();
    }

    private void SetUp()
    {
        _keysSequence.Keys = new List<KeySequenceData>()
        {
            new KeySequenceData() { Time = 2.0f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 2.10f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 3.30f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 4.0f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 5.0f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 6.0f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 6.30f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 6.60f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 7.0f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 7.20f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 7.40f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 8.60f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 9.0f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 9.20f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 9.40f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 9.60f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 9.90f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 10.0f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 10.10f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 11.30f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 12.0f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 13.0f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 14.0f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 14.30f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 14.60f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 16.0f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 16.20f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 16.40f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 17.60f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 17.0f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 17.20f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 17.40f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 17.60f, KeyDirection = DirectionKey.UP},
            new KeySequenceData() { Time = 17.90f, KeyDirection = DirectionKey.UP},
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
        DrawSprite(spriteBatch, _keySettings[DirectionKey.UP].Sprite, _keySettings[DirectionKey.UP].Position, Color.Black, _keySettings[DirectionKey.UP].Size);
        DrawSprite(spriteBatch, _keySettings[DirectionKey.RIGHT].Sprite, _keySettings[DirectionKey.RIGHT].Position, Color.Black, _keySettings[DirectionKey.RIGHT].Size);
        DrawSprite(spriteBatch, _keySettings[DirectionKey.DOWN].Sprite, _keySettings[DirectionKey.DOWN].Position, Color.Black, _keySettings[DirectionKey.DOWN].Size);
        DrawSprite(spriteBatch, _keySettings[DirectionKey.LEFT].Sprite, _keySettings[DirectionKey.LEFT].Position, Color.Black, _keySettings[DirectionKey.LEFT].Size);

        foreach (var key in _keysSequence.Keys)
        {
            var color = key.Checked ? Color.Gray : Color.White;
            var position = key.GetPosition(_totalTime, _keySettings[key.KeyDirection].Position);
            DrawSprite(spriteBatch, _keySettings[key.KeyDirection].Sprite, position, color);
        }
    }

    public void DrawSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Color color, float size = 1.0f)
    {
        spriteBatch.Draw(texture, position, null, color, 0.0f, texture.Bounds.Size.ToVector2() / 2f, size, SpriteEffects.None, 1.0f);
    }
}
