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
            //foreach (var item in ContentManager.Instance.EntityActions[entity.Name].Values)
            //{
            //    AddAnimation(item.AnimationType, item.CanBeCanceled, ContentManager.Instance.EntitySpriteSheets[entity.Name], item.AnimationFrames, item.AnimationSpeed);
            //}
            Entity = entity;
         }
        public void AddAnimation(AnimationType animationType, Animation animation)
        {
            if (Animations.ContainsKey(animationType))
            {
                return;
            }
            Animations.Add(animationType, animation);
            CurrentAnimation = animation;
            CurrentAnimationType = animationType;
        }

        public void Update()
        {
            Animations[CurrentAnimationType].Update();
            AnimationBehaviours[CurrentAnimationType].OnStateUpdate(this);
            if(CurrentAnimation.IsAnimationDone)
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
            AnimationBehaviours[CurrentAnimationType].OnStateEnter(this);
            CurrentAnimation.Start();
        }
        public void OnStateExit()
        {
            AnimationBehaviours[CurrentAnimationType].OnStateExit(this);
        }
    }
}


