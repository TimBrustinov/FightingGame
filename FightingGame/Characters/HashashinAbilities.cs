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
        protected override void UpdateAbility(ref Vector2 position, int speed, Vector2 direction)
        {
            AbilityDamage = 1;
            StaminaDrain = 0;
            if (direction != Vector2.Zero)
            {
                position += Vector2.Normalize(InputManager.Direction) * speed;
            }
        }
    }
    public class HashashinDodge : Ability
    {
        protected override void UpdateAbility(ref Vector2 position, int speed, Vector2 direction)
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
        protected override void UpdateAbility(ref Vector2 position, int speed, Vector2 direction)
        {
            AbilityDamage = 2;
            if (direction != Vector2.Zero)
            {
                position += Vector2.Normalize(InputManager.Direction) * speed;
            }
            return;
        }
    }
    public class HashashinAbility2 : Ability
    {
        protected override void UpdateAbility(ref Vector2 position, int speed, Vector2 direction)
        {
            AbilityDamage = 5;
            if (direction != Vector2.Zero)
            {
                position += Vector2.Normalize(InputManager.Direction) * speed;
            }
            return;
        }
    }
}
