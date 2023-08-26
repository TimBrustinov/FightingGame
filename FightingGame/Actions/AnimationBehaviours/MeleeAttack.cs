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
        bool savedDirection;
        public MeleeAttack(AnimationType animationType, float damage, int attackRange, int cooldown, bool canMove) : base(animationType, damage, attackRange, cooldown, canMove)
        {

        }

        public override void OnStateEnter(Animator animator)
        {
            entity = animator.Entity;
            savedDirection = animator.Entity.IsFacingLeft;
            Damage = Damage + Damage * Multipliers.Instance.AbilityDamageMultiplier;
            if(entity.CooldownManager.AnimationCooldown.ContainsKey(AnimationType))
            {
                entity.CooldownManager.AnimationCooldown[AnimationType] = Cooldown;
            }
            base.OnStateEnter(animator);
            return;
        }
        public override void OnStateUpdate(Animator animator)
        {
            entity.IsFacingLeft = savedDirection;
            if(entity.RemainingHealth <= 0)
            {
                animator.SetAnimation(AnimationType.Death);
            }
            else if(entity.Direction != Vector2.Zero && canMove)
            {
                entity.Position += Vector2.Normalize(entity.Direction) * entity.Speed;
            }
            base.OnStateUpdate(animator);
        }

        public override void OnStateExit(Animator animator)
        {
            animator.SetAnimation(AnimationType.Stand);
            base.OnStateExit(animator);
        }

        public override AnimationBehaviour Clone()
        {
            return new MeleeAttack(AnimationType, Damage, AttackRange, Cooldown, canMove);
        }
    }
}
