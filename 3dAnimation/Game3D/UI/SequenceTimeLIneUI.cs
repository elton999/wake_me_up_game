
using Microsoft.Xna.Framework.Graphics;

namespace Game3D.UI;

public class SequenceTimeLIneUI : UIEntity
{
    private KeysSequence _keysSequence = new KeysSequence();
    private float _totalTime = 0f;

    public override void Start()
    {

    }

    public override void Update(float deltaTime)
    {
        _totalTime += deltaTime;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {

    }
}
