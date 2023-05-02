using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FightingGame
{
    public abstract class Screen<TEnum> where TEnum : Enum
    {
        public abstract Screenum ScreenType { get; protected set; }
        public abstract bool IsActive { get; set; }
        public abstract void PreferedScreenSize(GraphicsDeviceManager graphics);
        public abstract TEnum Update(MouseState ms);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Initialize();
        public abstract bool CanBeDrawnUnder { get; set; }
    }
}
