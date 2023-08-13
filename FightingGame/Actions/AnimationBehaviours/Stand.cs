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
        public Stand(AnimationType animationType) : base(animationType)
        {
        }

        public override void OnStateEnter(Animator animator)
        {
            return;
        }
        public override void OnStateUpdate(Animator animator)
        {
            if (animator.Entity.WantedAnimation != AnimationType && animator.Entity.CheckTransition(animator.Entity.WantedAnimation))
            {
                animator.SetAnimation(animator.Entity.WantedAnimation);
            }
        }

        public override void OnStateExit(Animator animator)
        {
            return;
        }

        public override AnimationBehaviour Clone()
        {
            return new Stand(AnimationType);
        }
    }
}
