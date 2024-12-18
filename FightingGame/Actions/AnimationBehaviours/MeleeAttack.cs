﻿using System;
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
        AnimationType returnAnimation;
        public MeleeAttack(AnimationType animationType, float damageCoefficent, int attackRange, int cooldown, bool canMove, AnimationType returnAnimation) : base(animationType, damageCoefficent, attackRange, cooldown, canMove)
        {
            this.returnAnimation = returnAnimation;
        }

        public override void OnStateEnter(Animator animator)
        {
            entity = animator.Entity;
            //Damage = Damage + Damage * Multipliers.Instance.AbilityDamageMultiplier;
            if(entity.CooldownManager.AnimationCooldown.ContainsKey(AnimationType))
            {
                entity.CooldownManager.AnimationCooldown[AnimationType] = Cooldown;
            }
            base.OnStateEnter(animator);
            return;
        }
        public override void OnStateUpdate(Animator animator)
        {
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
            animator.SetAnimation(returnAnimation);
            base.OnStateExit(animator);
        }

        public override AnimationBehaviour Clone()
        {
            return new MeleeAttack(AnimationType, DamageCoefficent, AttackRange, Cooldown, canMove, returnAnimation);
        }
    }
}
