using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class HealthRegenScript : PowerUpScript
    {
        private float timeElapsed;
        public float regenerationInterval = 8;
        public HealthRegenScript(PowerUpType type) : base(type)
        {
        }

        public override void Update()
        {
            var character = GameObjects.Instance.SelectedCharacter;
            if (character.RemainingHealth < character.TotalHealth)
            {
                timeElapsed += (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
                if (timeElapsed >= regenerationInterval)
                {
                    character.RemainingHealth = Math.Min(character.TotalHealth, character.RemainingHealth + character.HealthRegen);
                    timeElapsed -= regenerationInterval;
                }
            }
        }
    }
}
