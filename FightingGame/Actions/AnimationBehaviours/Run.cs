using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FightingGame
{
    public class Run : AnimationBehaviour
    {
        Entity entity;
        public Run(AnimationType animationType, Animation animation) : base(animationType, animation)
        {
        }

        public override void OnStateEnter(Animator animator)
        {
            entity = animator.Entity;
        }
        public override void OnStateUpdate(Animator animator)
        {
            if (animator.Entity.Direction == Vector2.Zero)
            {
                animator.SetAnimation(AnimationType.Stand);
            }
            else
            {
                entity.Position += Vector2.Normalize(entity.Direction) * entity.Speed;
            }
        }

        public override void OnStateExit(Animator animator)
        {
            if(animator.Entity.Direction == Vector2.Zero)
            {
                animator.SetAnimation(AnimationType.Stand);
            }
        }

        
    }
}
