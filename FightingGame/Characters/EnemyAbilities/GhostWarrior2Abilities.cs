using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class GhostWarrior2BasicAttack : Ability
    {
        public GhostWarrior2BasicAttack(float cooldownTime, int attackRange) : base(cooldownTime)
        {
            AttackReach = attackRange;
        }

        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            return;
        }
    }

    public class GhostWarrior2Death : Ability
    {
        public GhostWarrior2Death(float cooldownTime) : base(cooldownTime)
        {
        }

        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            return;
        }
    }
}
