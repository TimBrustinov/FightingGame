using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FightingGame
{
    public abstract class Action
    {
        public AnimationType AnimationType;
        public float Cooldown;
        //public Rectangle CurrentFrame;
        //public int AbilityDamage;
        //public int StaminaDrain;
        //public bool CanHit;
        //public bool IsDead = false;
        //public int AttackReach = 50;

        public Action(AnimationType animationType, float cooldown)
        {
            AnimationType = animationType;
            Cooldown = cooldown;
        }
        public abstract void Update(Entity entity);
        //savedAnimation = animationType;
        //CurrentFrame = animationManager.CurrentFrame;
        //UpdateAbility(ref position, speed, direction);
        //if(animationManager.CurrentAnimation != null)
        //{
        //    CanHit = animationManager.CurrentAnimation.CurrerntFrame.CanHit;
        //}
        //if (animationManager.CurrentAnimation != null && animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimation)
        //{
        //    if(savedAnimation == AnimationType.Death)
        //    {
        //        IsDead = true;
        //    }
        //    savedAnimation = AnimationType.None;
        //    return savedAnimation;
        //}
        //return savedAnimation;
    }
}
