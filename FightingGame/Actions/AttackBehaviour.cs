using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public abstract class AttackBehaviour : AnimationBehaviour
    {
        public float DamageCoefficent;
        public float Damage;
        public int AttackRange;
        public int Cooldown;
        protected bool canMove;
        public bool IsRanged = false;
        public bool HasHit = false;
        public AttackBehaviour(AnimationType animationType, float damageCoefficent, int attackRange, int cooldown, bool canMove) : base(animationType)
        {
            //Damage = damageCoefficent;
            DamageCoefficent = damageCoefficent;
            AttackRange = attackRange;
            Cooldown = cooldown;
            this.canMove = canMove;
        }

        public override void OnStateEnter(Animator animator) 
        {
            Damage = animator.Entity.BaseDamage * DamageCoefficent;
            animator.Entity.IsAttacking = true;
            animator.Entity.CurrentAbility = this;
        }
        public override void OnStateUpdate(Animator animator) { }
        public override void OnStateExit(Animator animator) 
        {
            animator.Entity.IsAttacking = false;
            HasHit = false;
        }
    }
}
