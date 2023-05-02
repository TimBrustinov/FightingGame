using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public abstract class AnimationSprite 
    {
        public List<Rectangle> AnimationFrames;
        public Texture2D Texture;
        public Rectangle CurrerntFrame => AnimationFrames[animationFramesIndex];
        private int animationFramesIndex = 0;
        private int numFrames;
        private int frameTime;
        private bool active = true;
        public AnimationSprite(Texture2D texture, int frametime, List<Rectangle> sourceRectangles)
        {
            Texture = texture;
            AnimationFrames = new List<Rectangle>();
            frameTime = frametime;
            foreach (var frame in sourceRectangles)
            {
                AnimationFrames.Add(frame);
            }
        }

        public void Start()
        {
            active = true;
        }
        public void Stop()
        {
            active = false;
        }

        public void Update()
        {
            if(active)
            {
                while (animationFramesIndex < AnimationFrames.Count)
                {
                    animationFramesIndex++;
                }
            }
        }
        public void Draw(Vector2 position)
        {
            Globals.SpriteBatch.Draw(Texture, position, CurrerntFrame, Color.White);
        }
    }
}
