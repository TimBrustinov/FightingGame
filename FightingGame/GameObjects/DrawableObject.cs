using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightingGame
{
    public class DrawableObject : DrawableObjectBase
    {
        public DrawableObject(Texture2D texture, Vector2 position, Vector2 dimentions, Color color) : base(texture, position, dimentions, color) { }
        public DrawableObject(Texture2D texture, Vector2 position) : this(texture, position, new Vector2(texture.Width, texture.Height), Color.White) { }
        public DrawableObject(Texture2D texture, Vector2 position, Color color) : this(texture, position, new Vector2(50, 50), color) { }

    }
}
