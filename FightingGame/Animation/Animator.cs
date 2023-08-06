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
        public Dictionary<AnimationType, Animation> Animations = new Dictionary<AnimationType, Animation>();
        public Dictionary<AnimationType, AnimationBehaviour> AnimationBehaviours = new Dictionary<AnimationType, AnimationBehaviour>();
        public bool IsAnimationDone;

        public Animator(Entity entity)
        {
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
            //overrideAnimation = wantedAnimation == AnimationType.Death;
            //CurrentAnimation = Animations[CurrentAnimationType];

            //if (CurrentAnimation.IsAnimationDone && !CurrentAction.CanBeCanceled)
            //{
            //    wantedAnimation = CurrentAction.Transition();
            //}

            //if (wantedAnimation != CurrentAnimationType)
            //{
            //    if (AnimationToAction[wantedAnimation].MetCondition(Entity) && (CurrentAnimation.CanBeCanceled || CurrentAnimation.IsAnimationDone || overrideAnimation))
            //    {
            //        CurrentAction = AnimationToAction[wantedAnimation];
            //        Animations[CurrentAnimationType].Restart();
            //        CurrentAnimationType = CurrentAction.AnimationType;
            //        CurrentAnimation = Animations[CurrentAnimationType];
            //        Animations[CurrentAnimationType].Start();
            //    }
            //}
            //CurrentAction.Update(Entity);
            //Animations[CurrentAnimationType].Update();
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


