﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FightingGame
{
    public struct FrameHelper
    {
        public Vector2 Origin;
        public Rectangle SourceRectangle;
        public Rectangle AttackHitbox;
        public Rectangle CharacterHitbox;

        public FrameHelper(Rectangle SourceRect)            
        {
            SourceRectangle = SourceRect;
            Origin = new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2);
            AttackHitbox = new Rectangle(SourceRectangle.X, SourceRectangle.Y, 0, 0);
            CharacterHitbox = SourceRectangle;
        }
        public FrameHelper(Rectangle SourceRect, Vector2 origin)
        {
            SourceRectangle = SourceRect;
            Origin = origin;
            AttackHitbox = new Rectangle(SourceRectangle.X, SourceRectangle.Y, 0, 0);
            CharacterHitbox = SourceRectangle;

        }
        public FrameHelper(Rectangle SourceRect, Rectangle attackHitbox)
        {
            SourceRectangle = SourceRect;
            AttackHitbox = attackHitbox;
            Origin = new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2);
            CharacterHitbox = SourceRectangle;
            
        }
        public FrameHelper(Rectangle sourceRectangle, Rectangle attackHitbox, Rectangle characterHitbox)
        {
            SourceRectangle = sourceRectangle;
            AttackHitbox = attackHitbox;
            CharacterHitbox = characterHitbox;
            //Origin = new Vector2(CharacterHitbox.Width / 2, CharacterHitbox.Height / 2);
            Origin = new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2);
        }
        public FrameHelper(int x, int y, int width, int height, Vector2 origin, Rectangle attackHitbox)
        {
            SourceRectangle = new Rectangle(x, y, width, height);
            Origin = origin;
            AttackHitbox = attackHitbox;
            CharacterHitbox = SourceRectangle;
        }
        
    }
}
