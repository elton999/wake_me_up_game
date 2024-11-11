using Game3D.Entities;
using Microsoft.Xna.Framework.Graphics;

namespace Game3D.UI;

public class ScoreTextUI : UIEntity
{
    public override void Start()
    {

    }

    public override void Update(float deltaTime)
    {
        if(GameStates.CurrentState != GameStates.State.END_LEVEL) return;
        
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if(GameStates.CurrentState != GameStates.State.END_LEVEL) return;

    }
}
