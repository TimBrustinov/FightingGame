using Microsoft.Xna.Framework;
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
        public bool CanHit = false;

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
        public FrameHelper(Rectangle SourceRect, Rectangle characterHitbox)
        {
            SourceRectangle = SourceRect;
            Origin = new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2);
            CharacterHitbox = characterHitbox;
            AttackHitbox = new Rectangle(SourceRectangle.X, SourceRectangle.Y, 0, 0);
        }
        public FrameHelper(Rectangle SourceRect, Rectangle attackHitbox, bool canHit)
        {
            SourceRectangle = SourceRect;
            AttackHitbox = attackHitbox;
            Origin = new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2);
            CharacterHitbox = SourceRectangle;
            CanHit = canHit; 
        }
        public FrameHelper(Rectangle sourceRectangle, Rectangle attackHitbox, Rectangle characterHitbox, bool canHit)
        {
            SourceRectangle = sourceRectangle;
            AttackHitbox = attackHitbox;
            CharacterHitbox = characterHitbox;
            int x = CharacterHitbox.X - SourceRectangle.X;
            int y = CharacterHitbox.Y - SourceRectangle.Y;
            Origin = new Vector2(x + CharacterHitbox.Width / 2, y + CharacterHitbox.Height / 2);
            CanHit = canHit;
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
