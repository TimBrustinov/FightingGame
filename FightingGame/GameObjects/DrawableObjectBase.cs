using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace FightingGame
{
    public abstract class DrawableObjectBase
    {
       

        public Texture2D Texture { get; set; }
        public virtual Vector2 Position { get; set; }
        public Vector2 Dimentions { get; set; }
        public Rectangle HitBox { get; set; }
        public Color Color { get; set; }

        public DrawableObjectBase(Texture2D texture, Vector2 position, Vector2 dimentions, Color color)
        {
            Texture = texture;
            Position = position;
            Dimentions = dimentions;
            Color = color;
            HitBox = new Rectangle((int)Position.X, (int)Position.Y, (int)Dimentions.X, (int)Dimentions.Y);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, (int)Dimentions.X, (int)Dimentions.Y), Color);
        }
        public virtual void DrawFrame(SpriteBatch spriteBatch, Rectangle sourceRectangle)
        {
            spriteBatch.Draw(Texture, Position, sourceRectangle, Color);
        }
        //public virtual Rectangle HitBox
        //{
        //    get
        //    {
        //        return new Rectangle((int)Position.X, (int)Position.Y, (int)Dimentions.X, (int)Dimentions.Y);
        //    }
        //}

        public ClickResult GetMouseAction(MouseState ms)
        {
            if (HitBox.Contains(ms.Position))
            {
                if (ms.LeftButton == ButtonState.Pressed)
                {
                    return ClickResult.LeftClicked;
                }
                else if (ms.RightButton == ButtonState.Pressed)
                {
                    return ClickResult.RightClicked;
                }
                return ClickResult.Hovering;
            }
            return ClickResult.Nothing;
        }
    }
}
