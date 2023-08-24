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
        public float Scale = 0.5f;
        private Vector2 origin;
        public Card(Texture2D texture, CardRarity rarity, Color color, Action powerUp, Icon icon) : base(texture, new Vector2(0, 0), new Vector2(texture.Width * 0.5f, texture.Height * 0.5f), color)
        {
            PowerUp = powerUp;
            Rarity = rarity;
            Icon = icon;
            origin = new Vector2(texture.Width / 2, texture.Height / 2); 
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(ContentManager.Instance.Pixel, Position - new Vector2(2.5f, 2.5f), new Rectangle(0, 0, Texture.Width + 10, Texture.Height + 10), Color.White, 0, origin, Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Texture, Position, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0, origin, Scale, SpriteEffects.None, 0);
        }
    }
}
