using System;
using Game3D.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game3D.UI;

public enum ScoreType
{
    Perfect,
    Good,
    Ok,
    Wrong
}

public class WordsFeedbacksUI : UIEntity, IOnGotScore
{
    private Texture2D _perfectSprite;
    private Texture2D _goodSprite;
    private Texture2D _okSprite;
    private Texture2D _wrongSprite;

    private Vector2 _center => new Vector2(Game1.ScreenW / 2.0f, Game1.ScreenH / 2.0f);
    private Vector2 _origin => new Vector2(Sprite.Bounds.Width / 2.0f, Sprite.Bounds.Height / 2.0f);
    private float _size = 0.0f;
    private float _transparency = 1.0f;
    private float _totalTime = 0.0f;
    private const float SPEED = 5.0f;
    private const float TIME_TO_LEAVE = 0.5f;
    private const float MAX_SPRITE_SIZE = 1.0f;

    private IRegisterScore _registerScore;

    public WordsFeedbacksUI(IRegisterScore registerScore)
    {
        _registerScore = registerScore;
    }

    public override void Start()
    {
        _registerScore.OnRegisterScore += OnGotScore;
        _perfectSprite = Scene.Content.Load<Texture2D>(Path.PERFECT_TEXTURE_PATH);
        _goodSprite = Scene.Content.Load<Texture2D>(Path.GOOD_TEXTURE_PATH);
        _okSprite = Scene.Content.Load<Texture2D>(Path.OK_TEXTURE_PATH);
        _wrongSprite = Scene.Content.Load<Texture2D>(Path.WRONG_TEXTURE_PATH);
    }

    public override void OnDestroy()
    {
        _registerScore.OnRegisterScore -= OnGotScore;
    }

    public override void Update(float deltaTime)
    {
        _size += SPEED * deltaTime;
        if (_size <= MAX_SPRITE_SIZE) return;

        _size = MAX_SPRITE_SIZE;

        _totalTime += deltaTime;
        if (_totalTime <= TIME_TO_LEAVE) return;

        _transparency -= SPEED * deltaTime;
        _transparency = MathF.Max(0.0f, _transparency);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (Sprite == null) return;
        spriteBatch.Draw(Sprite, _center, null, Color.White * _transparency, 0.0f, _origin, _size, SpriteEffects.None, 1.0f);
    }

    public void OnGotScore(ScoreType scoreType)
    {
        switch (scoreType)
        {
            case ScoreType.Perfect:
                Sprite = _perfectSprite;
                break;
            case ScoreType.Good:
                Sprite = _goodSprite;
                break;
            case ScoreType.Ok:
                Sprite = _okSprite;
                break;
            default:
                Sprite = _wrongSprite;
                break;
        }

        _size = 0.0f;
        _transparency = 1.0f;
        _totalTime = 0.0f;
    }
}
