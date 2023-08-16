using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class Card : DrawableObjectBase
    {
        public Action PowerUp;
        public Card(Texture2D texture, Vector2 position, Vector2 dimentions, Color color, Action powerUp) : base(texture, position, dimentions, color)
        {
            PowerUp = powerUp;
        }
    }
}
