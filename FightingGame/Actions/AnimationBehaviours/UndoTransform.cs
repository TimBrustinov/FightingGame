using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class UndoTransform : AnimationBehaviour
    {
        public UndoTransform(AnimationType animationType) : base(animationType)
        {
        }

        public override void OnStateEnter(Animator animator)
        {
            return;
        }
        public override void OnStateUpdate(Animator animator)
        {
            return;
        }
        public override void OnStateExit(Animator animator)
        {
            animator.SetAnimation(AnimationType.Stand);
        }

        public override AnimationBehaviour Clone()
        {
            return new UndoTransform(AnimationType);
        }
    }
}
