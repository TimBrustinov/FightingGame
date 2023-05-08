using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class Animation
    {
        public List<Rectangle> AnimationFrames;
        public Texture2D Texture;
        public Rectangle CurrerntFrame => AnimationFrames[animationFramesIndex];
        public bool IsAnimationDone;
        private int animationFramesIndex = 0;
        private float frameTime;
        private bool active = true;
        private float frameTimer = 0;

        public Animation(Texture2D texture, float frametime, List<Rectangle> sourceRectangles)
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
        public void Restart()
        {
            animationFramesIndex = 0;
            frameTimer = frameTime;
            IsAnimationDone = false;
        }

        public void Update()
        {
            if (active)
            {
                frameTimer += (float)Globals.CurrentTime.ElapsedGameTime.TotalSeconds;

                if (frameTimer >= frameTime)
                {
                    animationFramesIndex = (animationFramesIndex + 1) % AnimationFrames.Count;
                    if(animationFramesIndex == 0)
                    {
                        IsAnimationDone = true;
                    }
                    frameTimer = 0;
                }
            }
        }
        public void Draw(Vector2 position)
        {
            if(InputManager.IsMovingLeft)
            {
                Globals.SpriteBatch.Draw(Texture, position, AnimationFrames[animationFramesIndex], Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.FlipHorizontally, 1);
            }
            else
            {
                Globals.SpriteBatch.Draw(Texture, position, AnimationFrames[animationFramesIndex], Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
            }
        }
    }
}
