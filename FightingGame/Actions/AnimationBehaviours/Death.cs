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



        public override void OnStateEnter(Animator animator)
        {
            for (int i = 0; i < Multipliers.Instance.CoinWorth; i++)
            {
                GameObjects.Instance.DropManager.AddDrop(IconType.Coin, animator.Entity.Position);
            }
        }
        public override void OnStateUpdate(Animator animator)
        {
            return;
        }
        public override void OnStateExit(Animator animator)
        {
            animator.SetAnimation(AnimationType.Stand);
            animator.Entity.IsDead = true;
        }

        
        public override AnimationBehaviour Clone()
        {
            return new Death(AnimationType);
        }
    }
}
