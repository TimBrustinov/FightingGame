using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class PowerUps
    {
        private PowerUps()
        {

        }

        public static PowerUps Instance { get; } = new PowerUps();

        public void MaxHealthIncrease()
        {
            GameObjects.Instance.SelectedCharacter.TotalHealth += 5;
        }
        public void HealthRegenRateIncrease()
        {
            GameObjects.Instance.SelectedCharacter.healthRegenPerSecond = GameObjects.Instance.SelectedCharacter.healthRegenPerSecond * 0.1f;
        }
        public void Bleed()
        {
            if(!GameObjects.Instance.SelectedCharacter.PowerUps.ContainsKey(PowerUpType.Bleed))
            {
                GameObjects.Instance.SelectedCharacter.PowerUps.Add(PowerUpType.Bleed, new BleedScript(PowerUpType.Bleed));
            }
            else
            {
                var bleed = (BleedScript)GameObjects.Instance.SelectedCharacter.PowerUps[PowerUpType.Bleed];
                bleed.BleedDamage += bleed.BleedDamage;
            }
        }
        public void LifesSteal()
        {
            if (!GameObjects.Instance.SelectedCharacter.PowerUps.ContainsKey(PowerUpType.LifeSteal))
            {
                GameObjects.Instance.SelectedCharacter.PowerUps.Add(PowerUpType.LifeSteal, new BleedScript(PowerUpType.LifeSteal));
            }
            else
            {
                var lifesteal = (LifeStealScript)GameObjects.Instance.SelectedCharacter.PowerUps[PowerUpType.LifeSteal];
                lifesteal.HealthPerHit += lifesteal.HealthPerHit;
            }
        }
    }
}
