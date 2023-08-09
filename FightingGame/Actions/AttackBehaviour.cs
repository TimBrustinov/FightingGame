using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public abstract class AttackBehaviour : AnimationBehaviour
    {
        public int Damage;
        public int AttackRange;
        public int Cooldown;
        protected bool canMove;
        public bool IsRanged = false;
        public AttackBehaviour(AnimationType animationType, int damage, int attackRange, int cooldown, bool canMove) : base(animationType)
        {
            Damage = damage;
            AttackRange = attackRange;
            Cooldown = cooldown;
            this.canMove = canMove;
        }

        public override void OnStateEnter(Animator animator) { }
        public override void OnStateExit(Animator animator) { }
        public override void OnStateUpdate(Animator animator) { }
        
    }
}
