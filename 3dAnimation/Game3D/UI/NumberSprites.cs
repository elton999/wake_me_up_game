using Game3D.Interfaces;
using Microsoft.Xna.Framework;

namespace Game3D.UI;

public class NumberSprites : ISpritesCoord
{
    private Rectangle[] _spritesCoord = new Rectangle[]
    {
        new Rectangle(0,0,21,28),
        new Rectangle(25,0,14,28),
        new Rectangle(41,0,22,28),
        new Rectangle(63,0,21,28),
        new Rectangle(84,0,22,28),
        new Rectangle(107,0,22,28),
        new Rectangle(130,0,21,28),
        new Rectangle(151,0,21,28),
        new Rectangle(172,0,22,28),
        new Rectangle(194,0,21,28),
    };

    public Rectangle[] SpritesCoord => _spritesCoord;
}
