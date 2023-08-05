using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public abstract class Attack : EntityAction
    {
        public float AttackRange;
        public int Cooldown;
        public int AttackDamage;
        public bool CanMove;
        public bool CanHit;
        protected Attack(AnimationType animationType, List<FrameHelper> frames, bool canBeCanceled, float animationSpeed, int cooldown, float attackRange, int attackDamage, bool canMove) 
            : base(animationType, frames, canBeCanceled, animationSpeed)
        {
            AttackRange = attackRange;
            AttackDamage = attackDamage;
            CanMove = canMove;
            Cooldown = cooldown;
        }
    }
}
