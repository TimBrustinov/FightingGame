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
        Rectangle entityHitbox;
        float dodgeSpeed;
        public Dodge(AnimationType animationType, Animation animation, float dodgeSpeed) : base(animationType, animation)
        {
            this.dodgeSpeed = dodgeSpeed;
        }

        public override void OnStateEnter(Animator animator)
        {
            entity = animator.Entity;
            entityHitbox = entity.HitBox;
        }
        public override void OnStateUpdate(Animator animator)
        {
            entity.HitBox = new Rectangle(entityHitbox.X, entityHitbox.Y, 0, 0);
            if (entity.Direction != Vector2.Zero)
            {
                entity.Position += Vector2.Normalize(entity.Direction) * (entity.Speed + dodgeSpeed);
            }
        }

        public override void OnStateExit(Animator animator)
        {
            animator.SetAnimation(AnimationType.Stand);
        }

        
    }
}
