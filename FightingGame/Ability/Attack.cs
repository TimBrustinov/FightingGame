using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public abstract class Attack : Action
    {
        public float AttackRange;
        public int AttackDamage;
        public bool CanMove;
        protected Attack(AnimationType animationType, float cooldown, float attackRange, int attackDamage, bool canMove) : base(animationType, cooldown)
        {
            AttackRange = attackRange;
            AttackDamage = attackDamage;
            CanMove = canMove;
        }
    }
}
