﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Content = Microsoft.Xna.Framework.Content.ContentManager;
namespace FightingGame
{
    public class Globals
    {
        public static SpriteBatch SpriteBatch { get; set; }
        public static GraphicsDevice GraphicsDevice { get; set; }
        public static Content Content { get; set; }
        public static Camera Camera { get; set; }
        public static Rectangle Tilemap { get; set; }
        public static GameTime GameTime {get; set;}

        public static void Update(GameTime gameTime)
        {
            GameTime = gameTime;
        }
    }
}
