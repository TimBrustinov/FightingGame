using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class BasicAttack : Attack
    {
        public BasicAttack(AnimationType animationType, float cooldown, float attackRange, int attackDamage, bool canMove) 
            : base(animationType, cooldown, attackRange, attackDamage, canMove)
        {
            
        }

        public override void Update(Entity entity)
        {
            if(CanMove)
            {
                if(entity.Direction != Vector2.Zero)
                {
                    entity.Position += Vector2.Normalize(entity.Direction) * entity.Speed;
                }
            }
        }
    }
}
