using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FightingGame
{
    public class Globals
    {
        public static SpriteBatch SpriteBatch {get; set;}
       // public static ContentManager ContentManager {get; set;}
        public static float GameSeconds {get; set;}

        public static void Update(GameTime gameTime)
        {
            GameSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
