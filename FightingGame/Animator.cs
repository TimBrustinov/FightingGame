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

        public AnimationManager AnimationManager;
        public EntityAction CurrentAction;
        public Dictionary<AnimationType, EntityAction> AnimationToAction = new Dictionary<AnimationType, EntityAction>();


        public Dictionary<AnimationType, Animation> Animations = new Dictionary<AnimationType, Animation>();
        public AnimationType lastAnimation;
        public Animation CurrentAnimation;
        public Rectangle CurrentFrame;
        public Rectangle PreviousFrame;
        public bool IsAnimationDone;

        public bool canPerformAction = false;
        private bool overrideAnimation = false;


        public Animator(Entity entity)
        {
            AnimationManager = new AnimationManager();
            AnimationToAction = ContentManager.Instance.EntityActions[entity.Name];
            foreach (var item in ContentManager.Instance.EntityActions[entity.Name].Values)
            {
                AddAnimation(item.AnimationType, item.CanBeCanceled, ContentManager.Instance.EntitySpriteSheets[entity.Name], item.AnimationFrames, item.AnimationSpeed);
            }
            Entity = entity;
        }
        private void AddAnimation(AnimationType animation, bool canBeCanceled, Texture2D texture, List<FrameHelper> sourceRectangles, float timePerFrame)
        {
            if (Animations.ContainsKey(animation))
            {
                return;
            }
            Animation animationSprite = new Animation(texture, canBeCanceled, timePerFrame, sourceRectangles);
            Animations.Add(animation, animationSprite);
            lastAnimation = animation;
        }

        public void Update(AnimationType wantedAnimation)
        {

            overrideAnimation = wantedAnimation == AnimationType.Death;
            CurrentAnimation = Animations[lastAnimation];

            if (CurrentAnimation.IsAnimationDone && !CurrentAction.CanBeCanceled)
            {
                wantedAnimation = CurrentAction.TransitionBack();
            }

            if (wantedAnimation != lastAnimation)
            {
                if (AnimationToAction[wantedAnimation].MetCondition(Entity) && (CurrentAnimation.CanBeCanceled || CurrentAnimation.IsAnimationDone || overrideAnimation))
                {
                    CurrentAction = AnimationToAction[wantedAnimation];
                    Animations[lastAnimation].Restart();
                    lastAnimation = wantedAnimation;
                    CurrentAnimation = Animations[lastAnimation];
                    Animations[lastAnimation].Start();
                }
            }
            CurrentAction.Update(Entity);
            Animations[lastAnimation].Update();
            CurrentFrame = Animations[lastAnimation].CurrerntFrame.SourceRectangle;
            PreviousFrame = Animations[lastAnimation].PreviousFrame.SourceRectangle;
        }
        public void Draw()
        {
            Animations[lastAnimation].Draw(Entity.Position, InputManager.IsMovingLeft, Entity.Scale, Color.White);
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


