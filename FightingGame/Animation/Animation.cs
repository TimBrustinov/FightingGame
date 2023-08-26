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
        public float frameTime;
        public bool active { get; private set; }
        private float frameTimer = 0;

        public Animation(Texture2D texture, float frametime, List<FrameHelper> sourceRectangles)
        {
            Texture = texture;
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
            PreviousFrame = CurrerntFrame;
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
        public void Draw(Vector2 position, bool isMovingLeft, float scale, float rotationAngle, Color color)
        {
            SpriteEffects spriteEffect = isMovingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Vector2 origin = PreviousFrame.Origin;

            int x = PreviousFrame.CharacterHitbox.X - PreviousFrame.SourceRectangle.X;
            int y = PreviousFrame.CharacterHitbox.Y - PreviousFrame.SourceRectangle.Y;
            Vector2 defaultOrigin = new Vector2(x + PreviousFrame.CharacterHitbox.Width / 2, y + PreviousFrame.CharacterHitbox.Height / 2);
            Vector2 flippedOrigin = new Vector2(PreviousFrame.SourceRectangle.Width - (x + PreviousFrame.CharacterHitbox.Width / 2), y + PreviousFrame.CharacterHitbox.Height / 2);

            if (PreviousFrame.CharacterHitbox != PreviousFrame.SourceRectangle)
            {
                origin = isMovingLeft ? flippedOrigin : defaultOrigin;
            }

            Globals.SpriteBatch.Draw(Texture, position, PreviousFrame.SourceRectangle, color, rotationAngle, origin, scale, spriteEffect, 1);
        }

    }
}
