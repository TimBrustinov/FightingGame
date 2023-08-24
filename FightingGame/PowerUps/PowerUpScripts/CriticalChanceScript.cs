using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class CriticalChanceScript : PowerUpScript
    {
        Random random = new Random();
        public float CriticalChance;
        private bool hasDoubled;
        public CriticalChanceScript(PowerUpType type) : base(type)
        {
            CriticalChance = 0.1f;
        }

        public override void Update()
        {
            Character character = GameObjects.Instance.SelectedCharacter;
            if (random.NextDouble() < CriticalChance && character.IsAttacking && !hasDoubled)
            {
                character.CurrentAbilityDamage = character.CurrentAbilityDamage * 2;
                hasDoubled = true;
            }

            if (!character.IsAttacking)
            {
                hasDoubled = false;
            }
        }
    }
}
