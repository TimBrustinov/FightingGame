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
        private Entity Entity;

        public EntityAction CurrentAction;
        public AnimationType CurrentAnimationType;
        public Animation CurrentAnimation; 
        public Dictionary<AnimationType, EntityAction> AnimationToAction = new Dictionary<AnimationType, EntityAction>();
        public Dictionary<AnimationType, Animation> Animations = new Dictionary<AnimationType, Animation>();

        public bool IsAnimationDone;
        private bool overrideAnimation = false;

        public Animator(Entity entity)
        {
            AnimationToAction = ContentManager.Instance.EntityActions[entity.Name];
            foreach (var item in ContentManager.Instance.EntityActions[entity.Name].Values)
            {
                AddAnimation(item.AnimationType, item.CanBeCanceled, ContentManager.Instance.EntitySpriteSheets[entity.Name], item.AnimationFrames, item.AnimationSpeed);
            }
            Entity = entity;
        }
        public void AddAnimation(AnimationType animation, bool canBeCanceled, Texture2D texture, List<FrameHelper> sourceRectangles, float timePerFrame)
        {
            if (Animations.ContainsKey(animation))
            {
                return;
            }
            Animation animationSprite = new Animation(texture, canBeCanceled, timePerFrame, sourceRectangles);
            Animations.Add(animation, animationSprite);
            CurrentAnimationType = animation;
        }

        public void Update(AnimationType wantedAnimation)
        {

            overrideAnimation = wantedAnimation == AnimationType.Death;
            CurrentAnimation = Animations[CurrentAnimationType];

            if (CurrentAnimation.IsAnimationDone && !CurrentAction.CanBeCanceled)
            {
                wantedAnimation = CurrentAction.Transition();
            }

            if (wantedAnimation != CurrentAnimationType)
            {
                if (AnimationToAction[wantedAnimation].MetCondition(Entity) && (CurrentAnimation.CanBeCanceled || CurrentAnimation.IsAnimationDone || overrideAnimation))
                {
                    CurrentAction = AnimationToAction[wantedAnimation];
                    Animations[CurrentAnimationType].Restart();
                    CurrentAnimationType = CurrentAction.AnimationType;
                    CurrentAnimation = Animations[CurrentAnimationType];
                    Animations[CurrentAnimationType].Start();
                }
            }
            CurrentAction.Update(Entity);
            Animations[CurrentAnimationType].Update();
        }
        public void Draw()
        {
            Animations[CurrentAnimationType].Draw(Entity.Position, Entity.IsFacingLeft, Entity.EntityScale, Color.White);
        }

    }


    //public class AnimationTree
    //{
    //    public AnimationVertex SentinalHead; 

    //    public void AddBoolTransition(AnimationVertex animationVertex, bool condition)
    //    {

    //    }
    //}


    //public class AnimationVertex
    //{
    //    AnimationType AnimationType;
    //    List<AnimationVertex> Neighbors;

    //    public AnimationVertex(AnimationType animationType)
    //    {
    //        this.AnimationType = animationType;
    //        Neighbors = new List<AnimationVertex>();
    //    }

    //}

}


