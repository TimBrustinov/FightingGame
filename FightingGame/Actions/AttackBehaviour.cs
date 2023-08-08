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
        public AttackBehaviour(AnimationType animationType, Animation animation, int damage, int attackRange, int cooldown) : base(animationType, animation)
        {
            Damage = damage;
            AttackRange = attackRange;
            Cooldown = cooldown;
        }

        public override void OnStateEnter(Animator animator) { }
        public override void OnStateExit(Animator animator) { }
        public override void OnStateUpdate(Animator animator) { }
        
    }
}
