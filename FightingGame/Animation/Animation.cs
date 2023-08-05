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
        public bool hasFrameChanged;
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
                frameTimer += (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
                hasFrameChanged = false;
                if (frameTimer >= frameTime)
                {
                    hasFrameChanged = true;
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
        public void Draw(Vector2 position, bool isMovingLeft, float scale, Color color)
        {
            SpriteEffects spriteEffect = isMovingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            //Vector2 adjustedPosition;
            //float offsetX = (PreviousFrame.SourceRectangle.Width - PreviousFrame.CharacterHitbox.Width) / 2;
            //float offsetY = (PreviousFrame.SourceRectangle.Height - PreviousFrame.CharacterHitbox.Height) / 2;

            //if (isMovingLeft)
            //{
            //    adjustedPosition = position - new Vector2(offsetX, offsetY);
            //}
            //else
            //{
            //    adjustedPosition = position + new Vector2(offsetX, -offsetY);
            //}
            //if(PreviousFrame.SourceRectangle != PreviousFrame.CharacterHitbox)
            //{
            //    origin = new Vector2(PreviousFrame.CharacterHitbox.Width / 2, PreviousFrame.CharacterHitbox.Height / 2);
            //}
            //else
            //{
            //    origin = PreviousFrame.Origin;
            //}

            Vector2 origin = PreviousFrame.Origin;

            int x = PreviousFrame.CharacterHitbox.X - PreviousFrame.SourceRectangle.X;
            int y = PreviousFrame.CharacterHitbox.Y - PreviousFrame.SourceRectangle.Y;
            Vector2 defaultOrigin = new Vector2(x + PreviousFrame.CharacterHitbox.Width / 2, y + PreviousFrame.CharacterHitbox.Height / 2);
            Vector2 flippedOrigin = new Vector2(PreviousFrame.SourceRectangle.Width - (x + PreviousFrame.CharacterHitbox.Width / 2), y + PreviousFrame.CharacterHitbox.Height / 2);

            if (PreviousFrame.CharacterHitbox != PreviousFrame.SourceRectangle)
            {
                origin = isMovingLeft ? flippedOrigin : defaultOrigin;
            }

            Globals.SpriteBatch.Draw(Texture, position, PreviousFrame.SourceRectangle, color, 0, origin, scale, spriteEffect, 1);
        }
    }
}
