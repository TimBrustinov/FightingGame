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
            if (Animations.ContainsKey(animationType))
            {
                Animations[animationType].Start();
                Animations[animationType].Update();
                lastAnimation = animationType;
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
