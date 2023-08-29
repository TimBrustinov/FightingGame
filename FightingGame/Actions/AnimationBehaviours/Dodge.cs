using System;
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
        float cooldown;
        public Dodge(AnimationType animationType, float dodgeSpeed, float cooldown) : base(animationType)
        {
            this.dodgeSpeed = dodgeSpeed;
            this.cooldown = cooldown;
        }

        public override void OnStateEnter(Animator animator)
        {
            entity = animator.Entity;
            if (!entity.CooldownManager.AnimationCooldown.ContainsKey(AnimationType))
            {
                entity.CooldownManager.AddCooldown(AnimationType, cooldown);
            }
            else
            {
                entity.CooldownManager.AnimationCooldown[AnimationType] = cooldown;
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
            return new Dodge(AnimationType, dodgeSpeed, cooldown);
        }
    }
}
