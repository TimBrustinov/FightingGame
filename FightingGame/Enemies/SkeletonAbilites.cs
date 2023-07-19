using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class SkeletonBasicAttack : Ability
    {
        protected override void UpdateAbility(ref Vector2 position, int speed, Vector2 direction)
        {
            AbilityDamage = 1;
            return;
        }
    }
}
