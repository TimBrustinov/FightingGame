using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class Animator
    {
        public Entity Entity;
        public AnimationType CurrentAnimationType;
        public Animation CurrentAnimation { get; private set; }
        public Dictionary<AnimationType, Animation> Animations;
        public Dictionary<AnimationType, AnimationBehaviour> AnimationBehaviours;
        public bool IsAnimationDone;

        public Animator(Entity entity)
        {
            Animations = new Dictionary<AnimationType, Animation>();
            AnimationBehaviours = new Dictionary<AnimationType, AnimationBehaviour>();
            Entity = entity;
            CurrentAnimationType = AnimationType.Stand;
         }
        public void AddAnimation(AnimationType animationType, Texture2D texture, float frameTime, List<FrameHelper> sourceRectangles)
        {
            if (Animations.ContainsKey(animationType))
            {
                return;
            }
            Animations.Add(animationType, new Animation(texture, frameTime, sourceRectangles));
        }

        public void Update()
        {
            Animations[CurrentAnimationType].Update();
            CurrentAnimation = Animations[CurrentAnimationType];
            if(AnimationBehaviours.ContainsKey(CurrentAnimationType))
            {
                AnimationBehaviours[CurrentAnimationType].OnStateUpdate(this);
            }

            if (Animations[CurrentAnimationType].IsAnimationDone)
            {
                OnStateExit();
            }
        }
        public void Draw()
        {
            //Animations[CurrentAnimationType].Draw(Entity.Position, Entity.IsFacingLeft, Entity.EntityScale, Color.White);
            Animations[CurrentAnimationType].Draw(Entity.Position, Entity.IsFacingLeft, Entity.EntityScale, Color.White);
        }

        public void SetAnimation(AnimationType animationType)
        {
            CurrentAnimationType = animationType;
            CurrentAnimation.Restart();
            CurrentAnimation = Animations[CurrentAnimationType];
            if(AnimationBehaviours.ContainsKey(animationType))
            {
                AnimationBehaviours[CurrentAnimationType].OnStateEnter(this);
            }
            CurrentAnimation.Start();
        }
        public void OnStateExit()
        {
            if(AnimationBehaviours.ContainsKey(CurrentAnimationType))
            {
                AnimationBehaviours[CurrentAnimationType].OnStateExit(this);
            }
        }
    }
}


