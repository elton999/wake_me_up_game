using System;
using Game3D.Entities;
using Game3D.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game3D.UI;

public class ScoreTextUI : UIEntity, IScore
{
    private const int MAX_NUMBERS = 9;
    private const float NUMBER_WIDTH_SIZE = 22;
    private const float DELAY_TO_SHOW_SCORE = 2.0f;
    private ISpritesCoord _numberSprites;
    private IScore _scoreData;
    
    public ISpritesCoord NumberSprites => _numberSprites;
    public IScore ScoreData => _scoreData;

    public ITimerPercent TextAnimationTimer;

    public int Score { get; set; }

    public Vector2 Position = new Vector2(100, 100);

    public ScoreTextUI(ISpritesCoord numberSprites, IScore scoreData)
    {
        _numberSprites = numberSprites;
        _scoreData = scoreData;
    }

    public override void Start()
    {
        Sprite = Scene.Content.Load<Texture2D>(Path.NUMBERS_PATH);
    }

    public override void Update(float deltaTime)
    {
        if(GameStates.CurrentState == GameStates.State.END_LEVEL)
        {
            TextAnimationTimer ??= new TimerPercent(DELAY_TO_SHOW_SCORE);
            TextAnimationTimer.Update(deltaTime);

            Score = (int)Math.Clamp(TextAnimationTimer.PercentTotal * ScoreData.Score, 0, ScoreData.Score);
            return;
        }

        if(GameStates.CurrentState == GameStates.State.PLAYING)
        {
            Score = 0;
            TextAnimationTimer = null;
            return;
        }
        
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if(GameStates.CurrentState != GameStates.State.END_LEVEL) return;
        for(int numbIndex = 0; numbIndex < MAX_NUMBERS; numbIndex++)
        {
            Vector2 numPosition = GetPositionByIndex(numbIndex);
            string scoreFormatted = Utils.ToScoreFormat(Score, MAX_NUMBERS);
            int spriteIndex = (int)char.GetNumericValue(scoreFormatted[numbIndex]);
            Rectangle source = NumberSprites.SpritesCoord[spriteIndex];
            
            spriteBatch.Draw(Sprite, numPosition, source, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
        }
    }

    private Vector2 GetPositionByIndex(int numbIndex)
    {
        Vector2 numPosition = new Vector2(NUMBER_WIDTH_SIZE * numbIndex, numbIndex);
        return numPosition + Position;
    }
}
