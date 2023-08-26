using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FightingGame
{
    public class Drop
    {
        public Icon Icon;
        public Vector2 Position;
        public Rectangle Hitbox;
        public Rarity Rarity;
        private float elapsedTime = 0.0f;
        private float oscillationSpeed = 2f; 
        private float oscillationAmplitude = 3f; 

        public Drop(Rarity rarity, Icon icon)
        {
            Rarity = rarity;
            Icon = icon;
        }
        public void Activate(Vector2 position)
        {
            Position = position;
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Icon.Dimensions.X, (int)Icon.Dimensions.Y);
        }
         
        public void Draw()
        {
            elapsedTime += (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            float yOffset = oscillationAmplitude * (float)Math.Sin(elapsedTime * oscillationSpeed);
            Vector2 positionWithOscillation = new Vector2(Position.X, Position.Y + yOffset);
            Globals.SpriteBatch.Draw(Icon.Texture, positionWithOscillation, Icon.SourceRectangle, Color.White, 0, Vector2.Zero, Icon.Scale, SpriteEffects.None, 0);
            Globals.SpriteBatch.Draw(ContentManager.Instance.Shadow, new Rectangle(Hitbox.X, Hitbox.Y + Hitbox.Height + 3, Hitbox.Width, 10), new Color(255, 255, 255, 100));
        }

        public Drop Clone()
        {
            return new Drop(Rarity, Icon);
        }
    }
}
