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
        public Ability(AnimationType animationType, List<FrameHelper> frames, bool canBeCanceled, float animationSpeed, int cooldown, float attackRange, int attackDamage, bool canMove) 
            : base(animationType, frames, canBeCanceled, animationSpeed, cooldown, attackRange, attackDamage, canMove)
        {

        }

        public override bool MetCondition(Entity entity)
        {
            return true;
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
