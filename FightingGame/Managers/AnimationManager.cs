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

        public bool IsAnimationDone;

        private AnimationType lastAnimation;
        public void AddAnimation(AnimationType animation, Texture2D texture, List<Rectangle> sourceRectangles, float timePerFrame)
        {
            if(Animations.ContainsKey(animation))
            {
                return;
            }
            Animation animationSprite = new Animation(texture, timePerFrame, sourceRectangles);
            Animations.Add(animation, animationSprite);
            lastAnimation = animation;
        }
        public void Update(AnimationType animationType)
        {
            IsAnimationDone = Animations[lastAnimation].IsAnimationDone;
            if (Animations.ContainsKey(animationType))
            {
                if(animationType != lastAnimation)
                { 
                    Animations[lastAnimation].Restart();
                    lastAnimation = animationType;
                }

                Animations[lastAnimation].Start();
                Animations[lastAnimation].Update();
            }
            else
            {
                Animations[lastAnimation].Stop();
                Animations[lastAnimation].Restart();
            }
        }
        public void Draw(Vector2 position)
        {
            Animations[lastAnimation].Draw(position);
        }
    }
}
