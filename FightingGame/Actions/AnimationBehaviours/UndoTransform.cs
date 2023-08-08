using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class UndoTransform : AnimationBehaviour
    {
        public UndoTransform(AnimationType animationType, Animation animation) : base(animationType, animation)
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

       
    }
}
