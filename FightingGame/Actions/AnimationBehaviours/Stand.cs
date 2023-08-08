using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FightingGame
{
    public class Stand : AnimationBehaviour
    {
        Entity entity;
        public Stand(AnimationType animationType, Animation animation) : base(animationType, animation)
        {
        }

        public override void OnStateEnter(Animator animator)
        {
            entity = animator.Entity;
        }
        public override void OnStateUpdate(Animator animator)
        {
            //if (animator.Entity.Direction != Vector2.Zero)
            //{
            //    animator.SetAnimation(AnimationType.Run);
            //}
            if (animator.Entity.WantedAnimation != AnimationType && animator.Entity.CheckTransition(animator.Entity.WantedAnimation))
            {
                animator.SetAnimation(animator.Entity.WantedAnimation);
            }
        }

        public override void OnStateExit(Animator animator)
        {
            return;
        }
    }
}
