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

        public FrameHelper(int x, int y, int width, int height)
        {
            SourceRectangle = new Rectangle(x, y, width, height);
            Origin = new Vector2(width / 2, height / 2);
            AttackHitbox = new Rectangle(x, y, 0, 0);
            CharacterHitbox = SourceRectangle;
        }
        public FrameHelper(int x, int y, int width, int height, Vector2 origin)
        {
            SourceRectangle = new Rectangle(x, y, width,height);
            Origin = origin;
            AttackHitbox = new Rectangle(x, y, 0 , 0);
            CharacterHitbox = SourceRectangle;
            
        }
        public FrameHelper(int x, int y, int width, int height, Rectangle attackHitbox)
        {
            SourceRectangle = new Rectangle(x, y, width, height);
            Origin = new Vector2(width / 2, height / 2);
            AttackHitbox = attackHitbox;
            CharacterHitbox = SourceRectangle;
        }
        public FrameHelper(Rectangle sourceRectangle, Rectangle attackHitbox, Rectangle characterHitbox)
        {
            SourceRectangle = sourceRectangle;
            AttackHitbox = attackHitbox;
            CharacterHitbox = characterHitbox;
            //Origin = new Vector2(CharacterHitbox.Width / 2, CharacterHitbox.Height / 2);
            Origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
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
