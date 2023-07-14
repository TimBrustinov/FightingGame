using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class AnimationManager
    {
        public Dictionary<AnimationType, Animation> Animations = new Dictionary<AnimationType, Animation>();
        public Animation CurrentAnimation;
        public Rectangle CurrentFrame;
        public Rectangle PreviousFrame;
        public bool IsAnimationDone;

        public AnimationType lastAnimation;
        public void AddAnimation(AnimationType animation, bool canBeCanceled, Texture2D texture, List<Rectangle> sourceRectangles, float timePerFrame)
        {
            if(Animations.ContainsKey(animation))
            {
                return;
            }
            Animation animationSprite = new Animation(texture, canBeCanceled, timePerFrame, sourceRectangles);
            Animations.Add(animation, animationSprite);
            lastAnimation = animation;
        }
        public void Update(AnimationType animationType)
        {
            CurrentAnimation = Animations[lastAnimation];
            if (Animations.ContainsKey(animationType))
            {
                //if animation is not the same, and the current animation can be canceled or has finished, then we change animation
                if(animationType != lastAnimation)
                { 
                    if(CurrentAnimation.CanBeCanceled || CurrentAnimation.IsAnimationDone)
                    {
                        //resets the last animation from frame 0
                        Animations[lastAnimation].Restart();
                        lastAnimation = animationType;
                        CurrentAnimation = Animations[lastAnimation];
                    }
                }
                // always starts the animation and updates
                Animations[lastAnimation].Start();
                Animations[lastAnimation].Update();
                CurrentFrame = Animations[lastAnimation].CurrerntFrame;
                PreviousFrame = Animations[lastAnimation].PreviousFrame;
            }
            else
            {
                Animations[lastAnimation].Stop();
                Animations[lastAnimation].Restart();
            }
        }
        public void Draw(Vector2 position, bool isMovingLeft, Vector2 EnemyScale)
        {
            Animations[lastAnimation].Draw(position, isMovingLeft, EnemyScale);
        }
    }
}
