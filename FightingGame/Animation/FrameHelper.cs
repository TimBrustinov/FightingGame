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
        public Rectangle Frame;
        public Rectangle AttackHitbox;

        public FrameHelper(int x, int y, int width, int height)
        {
            Frame = new Rectangle(x, y, width, height);
            Origin = new Vector2(width / 2, height / 2);
            AttackHitbox = new Rectangle(x, y, 0, 0);
        }
        public FrameHelper(int x, int y, int width, int height, Vector2 origin)
        {
            Frame = new Rectangle(x, y, width,height);
            Origin = origin;
            AttackHitbox = new Rectangle(x, y, 0 , 0);
        }
        public FrameHelper(int x, int y, int width, int height, Rectangle attackHitbox)
        {
            Frame = new Rectangle(x, y, width, height);
            Origin = new Vector2(width / 2, height / 2);
            AttackHitbox = attackHitbox;
        }
        public FrameHelper(int x, int y, int width, int height, Vector2 origin, Rectangle attackHitbox)
        {
            Frame = new Rectangle(x, y, width, height);
            Origin = origin;
            AttackHitbox = attackHitbox;
        }


    }
}
