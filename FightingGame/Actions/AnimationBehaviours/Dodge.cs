﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FightingGame
{
    public class Dodge : AnimationBehaviour
    {
        Entity entity;
        float dodgeSpeed;
        public int StaminaDrain;
        public Dodge(AnimationType animationType, float dodgeSpeed, int staminaDrain) : base(animationType)
        {
            this.dodgeSpeed = dodgeSpeed;
            StaminaDrain = staminaDrain;
        }

        public override void OnStateEnter(Animator animator)
        {
            entity = animator.Entity;
            if(entity.RemainingStamina - StaminaDrain <= 0)
            {
                animator.SetAnimation(AnimationType.Stand);
            }
            else
            {
                entity.RemainingStamina -= StaminaDrain;
                entity.staminaTimer = 0;
            }
        }
        public override void OnStateUpdate(Animator animator)
        {
            entity.HitBox = new Rectangle(entity.HitBox.X, entity.HitBox.Y, 0, 0);
            if (entity.Direction != Vector2.Zero)
            {
                entity.Position += Vector2.Normalize(entity.Direction) * (entity.Speed + dodgeSpeed);
            }
        }

        public override void OnStateExit(Animator animator)
        {
            animator.SetAnimation(AnimationType.Stand);
        }

        public override AnimationBehaviour Clone()
        {
            return new Dodge(this.AnimationType, this.dodgeSpeed, this.StaminaDrain);
        }
    }
}
