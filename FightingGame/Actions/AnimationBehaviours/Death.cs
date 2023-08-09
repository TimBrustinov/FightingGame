using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class Death : AnimationBehaviour
    {
        public Death(AnimationType animationType) : base(animationType)
        {
        }

        public override AnimationBehaviour Clone()
        {
            return new Death(AnimationType);
        }

        public override void OnStateEnter(Animator animator)
        {
            return;
        }

        public override void OnStateExit(Animator animator)
        {
            animator.Entity.IsDead = true;
        }

        public override void OnStateUpdate(Animator animator)
        {
            return;
        }
    }
}
