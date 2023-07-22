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
    public abstract class Ability
    {
        AnimationType savedAnimation;
        public int AbilityDamage;
        public int StaminaDrain;
        public float Cooldown;
        public bool CanHit;
        public bool IsDead = false;
        protected abstract void UpdateAbility(ref Vector2 position, float speed, Vector2 direction);
        public AnimationType Update(AnimationManager animationManager, AnimationType animationType, ref Vector2 position, Vector2 direction, float speed)
        {
            savedAnimation = animationType;
            UpdateAbility(ref position, speed, direction);
            if(animationManager.CurrentAnimation != null)
            {
                CanHit = animationManager.CurrentAnimation.CurrerntFrame.CanHit;
            }
            if (animationManager.CurrentAnimation != null && animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimation)
            {
                if(savedAnimation == AnimationType.Death)
                {
                    IsDead = true;
                }
                savedAnimation = AnimationType.None;
                return savedAnimation;
            }
            return savedAnimation;
        }
    }

    
}
