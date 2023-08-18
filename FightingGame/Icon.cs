using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace FightingGame
{
    public struct Icon
    {
        public IconType Type;
        public Texture2D Texture;
        public Rectangle SourceRectangle;
        public Vector2 Scale;
        public Vector2 Dimensions;
        public Icon(IconType type, Texture2D texture, float scale)
        {
            Type = type;
            Texture = texture;
            Scale = new Vector2(scale, scale);
            SourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Dimensions = new Vector2(SourceRectangle.Width * scale, SourceRectangle.Height * scale);
        }
        public Icon(IconType type, Texture2D texture, Rectangle sourceRectangle, float scale)
        {
            Type = type;
            Texture = texture;
            Scale = new Vector2(scale, scale);
            SourceRectangle = sourceRectangle;
            Dimensions = new Vector2(SourceRectangle.Width * scale, SourceRectangle.Height * scale);
        }
        public Icon(IconType type, Texture2D texture, Rectangle sourceRectangle, Vector2 scale)
        {
            Type = type;
            Texture = texture;
            Scale = scale;
            SourceRectangle = sourceRectangle;
            Dimensions = new Vector2(SourceRectangle.Width * scale.X, SourceRectangle.Height * scale.Y);
        }
    }
}
