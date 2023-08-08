using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class UltimateTransform : AttackBehaviour
    {
        public UltimateTransform(AnimationType animationType, Animation animation, int damage, int attackRange, int cooldown) : base(animationType, animation, damage, attackRange, cooldown)
        {
        }

        public override void OnStateEnter(Animator animator)
        {
            Entity entity = animator.Entity;
            if(entity.GetType() == typeof(Character))
            {
                var character = (Character)entity;
                character.InUltimateForm = true;
            }
        }

        public override void OnStateExit(Animator animator)
        {
            animator.SetAnimation(AnimationType.UltimateStand);
        }

        public override void OnStateUpdate(Animator animator)
        {
            return ;
        }
    }
}
