using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class Ability : Attack
    {
        public Ability(AnimationType animationType, List<FrameHelper> frames, bool canBeCanceled, float animationSpeed, int cooldown, float attackRange, int attackDamage, bool canMove) 
            : base(animationType, frames, canBeCanceled, animationSpeed, cooldown, attackRange, attackDamage, canMove)
        {

        }

        public override bool MetCondition(Entity entity)
        {
            if(!entity.CooldownManager.AnimationCooldown.ContainsKey(AnimationType))
            {
                if(!entity.Animator.CurrentAnimation.CanBeCanceled)
                {
                    entity.CooldownManager.AnimationCooldown.Add(AnimationType, 0);
                }
                else
                {
                    entity.CooldownManager.AnimationCooldown.Add(AnimationType, Cooldown);
                }
                entity.CooldownManager.MaxAnimationCooldown.Add(AnimationType, Cooldown);
                return true;
            }
            else if(entity.CooldownManager.AnimationCooldown[AnimationType] == 0 && entity.Animator.CurrentAnimation.CanBeCanceled)
            {
                entity.CooldownManager.AnimationCooldown[AnimationType] = Cooldown;
                return true;
            }
            return false;
        }
        public override void Update(Entity entity)
        {
            CanHit = entity.Animator.CurrentAnimation.CurrerntFrame.CanHit;
            if (CanMove)
            {
                Move(entity);
            }
        }
    }
}
