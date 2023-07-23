using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FightingGame
{
    public class CharacterUIManager
    {
        private Character character;
        private Rectangle UIBackground;
        private Vector2 UIPosition;
        private float lerpSpeed = 0.2f;

        public CharacterUIManager(Character character)
        {
            this.character = character;
        }

        public void Update(GraphicsDevice graphicsDevice)
        {
            // Get the screen width and height from the viewport
            UIPosition = new Vector2(graphicsDevice.Viewport.X - 75, graphicsDevice.Viewport.Y + graphicsDevice.Viewport.Height / 2 - 40);
            // Use LERP to move the UI towards the target position
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentManager.Instance.Pixel, UIPosition, new Rectangle(0, 0, 125, 40), Color.DarkGray);
        }
    }
}
