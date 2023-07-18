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
        public List<FrameHelper> AnimationFrames;
        public Texture2D Texture;
        public FrameHelper PreviousFrame;
        public FrameHelper CurrerntFrame => AnimationFrames[animationFramesIndex];
        public bool IsAnimationDone;
        public bool CanBeCanceled = true;

        public int animationFramesIndex = 0;
        private float frameTime;
        private bool active = true;
        private float frameTimer = 0;

        public Animation(Texture2D texture, bool canBeCanceled, float frametime, List<FrameHelper> sourceRectangles)
        {
            Texture = texture;
            CanBeCanceled = canBeCanceled;
            AnimationFrames = new List<FrameHelper>();
            frameTime = frametime;
            foreach (var frame in sourceRectangles)
            {
                AnimationFrames.Add(frame);
            }
            PreviousFrame = CurrerntFrame;
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
                    PreviousFrame = CurrerntFrame;
                    animationFramesIndex = (animationFramesIndex + 1) % AnimationFrames.Count;
                    if (animationFramesIndex == 0)
                    {
                        IsAnimationDone = true;
                    }

                    frameTimer = 0;
                }
            }
        }
        public void Draw(Vector2 position, bool isMovingLeft, Vector2 scale, Color color)
        {
            SpriteEffects spriteEffect = isMovingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Vector2 adjustedPosition;
            int offsetX = (PreviousFrame.SourceRectangle.Width - PreviousFrame.CharacterHitbox.Width) / 2; // Calculate the X offset
            int offsetY = (PreviousFrame.SourceRectangle.Height - PreviousFrame.CharacterHitbox.Height) / 2; // Calculate the Y offset

            if (isMovingLeft)
            {
                adjustedPosition = position - new Vector2(offsetX, offsetY);
            }
            else
            {
                adjustedPosition = position + new Vector2(offsetX, -offsetY);

            }
            Console.WriteLine(PreviousFrame.Origin);
            // Globals.SpriteBatch.Draw(Texture, position, PreviousFrame.SourceRectangle, color, 0, PreviousFrame.Origin, scale, spriteEffect, 1);
            Globals.SpriteBatch.Draw(Texture, adjustedPosition, PreviousFrame.SourceRectangle, color, 0, PreviousFrame.Origin, scale, spriteEffect, 1);


        }
    }
}
