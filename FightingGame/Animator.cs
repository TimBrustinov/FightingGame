using Microsoft.Xna.Framework;
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

        public AnimationType currentAnimation;
        public AnimationType savedAnimaton;
        public bool canPerformAction = false;
        private bool overrideAnimation = false;


        public Animator(Entity entity)
        {
            AnimationManager = new AnimationManager();
            AnimationToAction = ContentManager.Instance.EntityActions[entity.Name];
            foreach(var item in ContentManager.Instance.EntityActions[entity.Name].Values)
            {
                AnimationManager.AddAnimation(item.AnimationType, item.CanBeCanceled, ContentManager.Instance.EntitySpriteSheets[entity.Name], item.AnimationFrames, item.AnimationSpeed);
            }
            Entity = entity;
        }

        public void Update(AnimationType animation)
        {
            overrideAnimation = animation == AnimationType.Death;
            if (AnimationToAction.ContainsKey(animation))
            {
                if (AnimationToAction[animation].MetCondition(Entity))
                {
                    CurrentAction = AnimationToAction[animation];
                }
            }
            AnimationManager.Update(CurrentAction.AnimationType, overrideAnimation);
        }
        public void Draw()
        {
            AnimationManager.Draw(Entity.Position, InputManager.IsMovingLeft, Entity.Scale, Color.White);
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


