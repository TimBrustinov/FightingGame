using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class Button : DrawableObjectBase
    {
        public float Scale;
        private Vector2 origin;
        public Button(Texture2D texture, Vector2 position, Vector2 dimentions, Color color, float scale) : base(texture, position, dimentions, color)
        {
            Scale = scale;
            origin = new Vector2(dimentions.X / 2, dimentions.Y / 2);
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, Position - Vector2.One, new Rectangle(0, 0, (int)Dimentions.X + 2, (int)Dimentions.Y + 2), Color.White, 0, origin, Scale, SpriteEffects.None, 0);
            Globals.SpriteBatch.Draw(Texture, Position, new Rectangle(0, 0, (int)Dimentions.X, (int)Dimentions.Y), Color, 0, origin, Scale, SpriteEffects.None, 0);
        }
    }
}
