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
        public SkeletonBasicAttack(float cooldownTime) : base(cooldownTime) { }
        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            AbilityDamage = 1;
            return;
        }
    }
    public class SkeletonDeath : Ability
    {
        public SkeletonDeath(float cooldownTime) : base(cooldownTime) { }
        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            return;
        }
    }
    public class SkeletonSpawn : Ability
    {
        public SkeletonSpawn(float cooldownTime) : base(cooldownTime) { }
        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            return;
        }
    }
}
