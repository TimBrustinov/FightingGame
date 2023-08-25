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
            GameObjects.Instance.DropManager.RollForDrop(animator.Entity.Position);
            GameObjects.Instance.DropManager.AddDrop(IconType.Coin, animator.Entity.Position);
        }

        public override void OnStateExit(Animator animator)
        {
            animator.SetAnimation(AnimationType.Stand);
           
            animator.Entity.IsDead = true;
        }

        public override void OnStateUpdate(Animator animator)
        {
            return;
        }
    }
}
