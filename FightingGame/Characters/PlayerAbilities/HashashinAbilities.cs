using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class HashashinBasicAttack : Ability
    {
        public HashashinBasicAttack(float cooldownTime) : base(cooldownTime) { }

        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            AbilityDamage = 5;
            StaminaDrain = 0;
            if (direction != Vector2.Zero)
            {
                position += Vector2.Normalize(InputManager.Direction) * speed;
            }
        }
    }
    public class HashashinDodge : Ability
    {
        public HashashinDodge(float cooldownTime) : base(cooldownTime) { }

        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            StaminaDrain = 15;
            AbilityDamage = 0;
            if (direction != Vector2.Zero)
            {
                position += Vector2.Normalize(InputManager.Direction) * (speed + 5);
            }
        }
    }
    public class HashashinAbility1 : Ability
    {
        public HashashinAbility1(float cooldownTime) : base(cooldownTime) { }

        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            AbilityDamage = 10;
            if (direction != Vector2.Zero)
            {
                position += Vector2.Normalize(InputManager.Direction) * speed;
            }
            return;
        }
    }
    public class HashashinAbility2 : Ability
    {
        public HashashinAbility2(float cooldownTime) : base(cooldownTime) { }

        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            AbilityDamage = 20;
            if (direction != Vector2.Zero)
            {
                position += Vector2.Normalize(InputManager.Direction) * speed;
            }
            return;
        }
    }
    public class HashashinAbility3 : Ability
    {
        public HashashinAbility3(float cooldownTime) : base(cooldownTime) 
        {

        }
        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            AbilityDamage = 20;
            return;
        }
    }

    public class HashashinUltimateTransform : Ability
    {
        public HashashinUltimateTransform(float cooldownTime) : base(cooldownTime)
        {
        }

        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            AbilityDamage = 10;
            return;
        }
    }
    public class HashashinUltimateDodge : Ability
    {
        public HashashinUltimateDodge(float cooldownTime) : base(cooldownTime)
        {
        }

        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            StaminaDrain = 15;
            AbilityDamage = 0;
            if (direction != Vector2.Zero)
            {
                position += Vector2.Normalize(InputManager.Direction) * (speed + 7);
            }
        }
    }
    public class HashashinUltimateBasicAttack : Ability
    {
        public HashashinUltimateBasicAttack(float cooldownTime) : base(cooldownTime)
        {
        }

        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            AbilityDamage = 5;
            StaminaDrain = 0;
            if (direction != Vector2.Zero)
            {
                position += Vector2.Normalize(InputManager.Direction) * speed;
            }
        }
    }
    public class HashashinUltimateAbility1 : Ability
    {
        public HashashinUltimateAbility1(float cooldownTime) : base(cooldownTime)
        {
        }

        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            AbilityDamage = 10;
            StaminaDrain = 0;
            if (direction != Vector2.Zero)
            {
                position += Vector2.Normalize(InputManager.Direction) * speed;
            }
        }
    }

    public class HashashinUltimateAbility2 : Ability
    {
        public HashashinUltimateAbility2(float cooldownTime) : base(cooldownTime)
        {
        }

        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            AbilityDamage = 10;
            StaminaDrain = 0;
            if (direction != Vector2.Zero)
            {
                position += Vector2.Normalize(InputManager.Direction) * speed;
            }
        }
    }
    public class HashashinUndoTransform : Ability
    {
        public HashashinUndoTransform(float cooldownTime) : base(cooldownTime)
        {
        }

        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            return;
        }
    }
    public class HashashinUltimateAbility3 : Ability
    {
        public HashashinUltimateAbility3(float cooldownTime) : base(cooldownTime)
        {
        }

        protected override void UpdateAbility(ref Vector2 position, float speed, Vector2 direction)
        {
            AbilityDamage = 10;
            StaminaDrain = 0;
            if (direction != Vector2.Zero)
            {
                position += Vector2.Normalize(InputManager.Direction) * speed;
            }
        }
    }
}
