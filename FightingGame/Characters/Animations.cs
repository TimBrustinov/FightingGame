using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public abstract class AnimationSprite : DrawableObjectBase
    {
        public List<Rectangle> AnimationFrames;
        public Rectangle CurrerntFrame => AnimationFrames[animationFramesIndex];

        int animationFramesIndex = 0;
        public AnimationSprite(Texture2D texture, Vector2 position, Vector2 dimentions, Color color) : base(texture, position, dimentions, color)
        {
            AnimationFrames = new List<Rectangle>();
        }

        public void Update()
        {
            while(animationFramesIndex < AnimationFrames.Count)
            {
                animationFramesIndex++;
            }
            //animationFrames[animationFramesIndex];
        }
    }
}
