using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class GhostWarriorDeath : Ability
    {
        public GhostWarriorDeath(float cooldownTime) : base(cooldownTime)
        {
        }

        public GhostWarriorDeath(float cooldownTime, int attackReach) : base(cooldownTime)
        {
            AttackReach = attackReach;
        }

        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            return;
        }
    }

    public class GhostWarriorBasicAttack : Ability
    {
        public GhostWarriorBasicAttack(float cooldownTime) : base(cooldownTime)
        {
        }
        public GhostWarriorBasicAttack(float cooldownTime, int attackReach) : base(cooldownTime)
        {
            AttackReach = attackReach;
        }
        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            AbilityDamage = 15;
        }
    }
}
