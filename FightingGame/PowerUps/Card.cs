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
        public CardRarity Rarity;
        public Action PowerUp;
        public Icon Icon;
        public Card(Texture2D texture, CardRarity rarity, Color color, Action powerUp) : base(texture, new Vector2(0, 0), new Vector2(texture.Width * 0.5f, texture.Height * 0.5f), color)
        {
            PowerUp = powerUp;
            Rarity = rarity;
        }
    }
}
