using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public abstract class AttackBehaviour : AnimationBehaviour
    {
        public float Damage;
        public int AttackRange;
        public int Cooldown;
        protected bool canMove;
        public bool IsRanged = false;
        public bool HasHit = false;
        public float baseDamage {  get; private set; }
        public AttackBehaviour(AnimationType animationType, float damage, int attackRange, int cooldown, bool canMove) : base(animationType)
        {
            baseDamage = damage;
            Damage = damage;
            AttackRange = attackRange;
            Cooldown = cooldown;
            this.canMove = canMove;
        }

        public override void OnStateEnter(Animator animator) 
        {
            Damage = animator.Entity.BaseDamage + baseDamage;
            animator.Entity.IsAttacking = true;
            animator.Entity.CurrentAbility = this;
            //Damage = Damage + Damage * Multipliers.Instance.AbilityDamageMultiplier;
        }
        public override void OnStateUpdate(Animator animator) { }
        public override void OnStateExit(Animator animator) 
        {
            animator.Entity.IsAttacking = false;
            HasHit = false;
            Damage = baseDamage;
        }
        public void Crit()
        {
            Damage += Damage * 2;
        }
    }
}
