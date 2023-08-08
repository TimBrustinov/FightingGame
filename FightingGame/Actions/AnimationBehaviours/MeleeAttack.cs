using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace FightingGame
{
    public class MeleeAttack : AttackBehaviour
    {
        Entity entity;
        public MeleeAttack(AnimationType animationType, Animation animation, int damage, int attackRange, int cooldown) : base(animationType, animation, damage, attackRange, cooldown)
        {

        }

        public override void OnStateEnter(Animator animator)
        {
            entity = animator.Entity;
            entity.CurrentAttackDamage = Damage;
            if(entity.CooldownManager.AnimationCooldown.ContainsKey(AnimationType))
            {
                entity.CooldownManager.AnimationCooldown[AnimationType] = Cooldown;
            }
            return;
        }
        public override void OnStateUpdate(Animator animator)
        {
            if(entity.Direction != Vector2.Zero)
            {
                entity.Position += Vector2.Normalize(entity.Direction) * entity.Speed;
            }
            return;
        }

        public override void OnStateExit(Animator animator)
        {
            animator.SetAnimation(AnimationType.Stand);
        }

        
    }
}
