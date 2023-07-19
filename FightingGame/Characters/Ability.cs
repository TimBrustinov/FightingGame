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
        protected abstract void UpdateAbility(ref Vector2 position, int speed, Vector2 direction);
        public AnimationType Update(AnimationManager animationManager, AnimationType animationType, ref Vector2 position, Vector2 direction, int speed)
        {
            savedAnimation = animationType;
            UpdateAbility(ref position, speed, direction);
            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimation)
            {
                savedAnimation = AnimationType.None;
                return savedAnimation;
            }
            return savedAnimation;
        }
    }

    
}
