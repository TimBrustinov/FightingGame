using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class PowerUps
    {
        private Character SelectedCharacter => GameObjects.Instance.SelectedCharacter;
        private PowerUps()
        {

        }

        public static PowerUps Instance { get; } = new PowerUps();

        public void MaxHealthIncrease()
        {
            SelectedCharacter.TotalHealth += 5;
        }
        public void HealthRegenAmmountIncrease()
        {
            SelectedCharacter.HealthRegen = SelectedCharacter.HealthRegen + 1;
        }
        public void HealthRegenRateIncrease()
        {
            var healthRegenScript = (HealthRegenScript)SelectedCharacter.PowerUps[PowerUpType.HealthRegenRateIncrease];
            healthRegenScript.regenerationInterval = Math.Max(1, healthRegenScript.regenerationInterval - healthRegenScript.regenerationInterval * 0.05f);
        }
        public void Overshield()
        {
            if (!SelectedCharacter.PowerUps.ContainsKey(PowerUpType.Overshield))
            {
                SelectedCharacter.PowerUps.Add(PowerUpType.Overshield, new OvershieldScript(PowerUpType.Overshield));
            }
            if(SelectedCharacter.MaxOvershield < 100)
            {
                if (SelectedCharacter.Overshield >= SelectedCharacter.MaxOvershield)
                {
                    SelectedCharacter.Overshield = SelectedCharacter.MaxOvershield + 5;
                }
                SelectedCharacter.MaxOvershield += 5;
            }
            //SelectedCharacter.MaxOvershield = SelectedCharacter.Overshield;
            SelectedCharacter.OvershieldBarWidth = Math.Min(SelectedCharacter.MaxOvershield, 100);
        }
        public void Bleed()
        {
            if (!SelectedCharacter.PowerUps.ContainsKey(PowerUpType.Bleed))
            {
                SelectedCharacter.PowerUps.Add(PowerUpType.Bleed, new BleedScript(PowerUpType.Bleed));
            }
            else
            {
                var bleed = (BleedScript)SelectedCharacter.PowerUps[PowerUpType.Bleed];
                bleed.BleedDamage += bleed.BleedDamage;
            }
        }
        public void LifesSteal()
        {
            if (!SelectedCharacter.PowerUps.ContainsKey(PowerUpType.LifeSteal))
            {
                SelectedCharacter.PowerUps.Add(PowerUpType.LifeSteal, new BleedScript(PowerUpType.LifeSteal));
            }
            else
            {
                var lifesteal = (LifeStealScript)SelectedCharacter.PowerUps[PowerUpType.LifeSteal];
                lifesteal.HealthPerHit += lifesteal.HealthPerHit;
            }
        }

    }
}
