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
        _keysSequence.Keys = Levels.GetLevel1();
    }

    public override void Update(float deltaTime)
    {
        if (KeyBoardHandler.KeyPressed("reset"))
        {
            _totalTime = 0.0f;
            SetUp();
        }

        _totalTime += deltaTime;

        UpdateSizeAnimation(DirectionKey.UP, deltaTime);
        UpdateSizeAnimation(DirectionKey.DOWN, deltaTime);
        UpdateSizeAnimation(DirectionKey.LEFT, deltaTime);
        UpdateSizeAnimation(DirectionKey.RIGHT, deltaTime);

        if (KeyBoardHandler.KeyPressed("up"))
            OnPressButton(DirectionKey.UP);
        if (KeyBoardHandler.KeyPressed("down"))
            OnPressButton(DirectionKey.DOWN);
        if (KeyBoardHandler.KeyPressed("left"))
            OnPressButton(DirectionKey.LEFT);
        if (KeyBoardHandler.KeyPressed("right"))
            OnPressButton(DirectionKey.RIGHT);

    }

    public void OnPressButton(DirectionKey direction)
    {
        float timer = _totalTime + _offsetKeyPressing;

        foreach (var key in _keysSequence.Keys)
        {
            if (key.KeyDirection != direction) continue;
            if (key.Checked) continue;
            if (key.GetTimer(_totalTime) < 0.0f) continue;

            if (key.GetTimer(timer) <= _perfectTime)
            {
                _keySettings[direction].Size += 1.0f;
                key.Checked = true;
                Console.WriteLine("Perfect!");
                Console.WriteLine(key.GetTimer(_totalTime));
                return;
            }

            if (key.GetTimer(timer) <= _goodTime)
            {
                _keySettings[direction].Size += 1.0f;
                key.Checked = true;
                Console.WriteLine("Good!");
                Console.WriteLine(key.GetTimer(_totalTime));
                return;
            }

            if (key.GetTimer(timer) <= _okTime)
            {
                _keySettings[direction].Size += 1.0f;
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

    private void UpdateSizeAnimation(DirectionKey directionKey, float deltaTime)
    {
        _keySettings[directionKey].Size = MathF.Max(1.0f, _keySettings[directionKey].Size - 10.0f * deltaTime);
    }
}
