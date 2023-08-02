using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class Ability : Attack
    {
        public Ability(AnimationType animationType, float cooldown, float attackRange, int attackDamage, bool canMove) : base(animationType, cooldown, attackRange, attackDamage, canMove)
        {

        }

        public override void Update(Entity entity)
        {
            CanHit = entity.animationManager.CurrentAnimation.CurrerntFrame.CanHit;
            if (CanMove)
            {
                Move(entity);
            }
        }
    }
}
