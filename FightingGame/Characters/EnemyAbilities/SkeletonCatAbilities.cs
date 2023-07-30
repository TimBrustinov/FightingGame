using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class SkeletonCatBasicAttack : Ability
    {
        public SkeletonCatBasicAttack(float cooldownTime, int attackReach) : base(cooldownTime)
        {
            AttackReach = attackReach;
        }

        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            AbilityDamage = 5;

        }
    }

    public class SkeletonCatDeath : Ability
    {
        public SkeletonCatDeath(float cooldownTime) : base(cooldownTime)
        {
        }

        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            return;
        }
    }

    public class SkeletonCatSpawn : Ability
    {
        public SkeletonCatSpawn(float cooldownTime) : base(cooldownTime)
        {
        }

        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            return;
        }
    }
}
