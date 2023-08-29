using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class UltimateTransform : AttackBehaviour
    {
        public UltimateTransform(AnimationType animationType, float damage, int attackRange, int cooldown, bool canMove) : base(animationType, damage, attackRange, cooldown, canMove)
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
            base.OnStateEnter(animator);
        }
        public override void OnStateUpdate(Animator animator)
        {
            return;
        }
        public override void OnStateExit(Animator animator)
        {
            animator.SetAnimation(AnimationType.UltimateStand);
            base.OnStateExit(animator);
        }
        public override AnimationBehaviour Clone()
        {
            return new UltimateTransform(AnimationType, DamageCoefficent, AttackRange, Cooldown, canMove);
        }

    }
}
