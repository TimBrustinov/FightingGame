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
        public Dictionary<IconType, Icon> PowerUpIcons = new Dictionary<IconType, Icon>();


        private PowerUps()
        {

        }

        public static PowerUps Instance { get; } = new PowerUps();
        public void AddPowerUpIcon(Icon icon)
        {
            if (PowerUpIcons.ContainsKey(icon.Type))
            {
                PowerUpIcons[icon.Type].Ammount++;
            }
            else
            {
                PowerUpIcons.Add(icon.Type, icon);
            }
        }
        public void Reset()
        {
            PowerUpIcons.Clear();
        }
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
                bleed.BleedDamage += 2;
            }
        }
        public void LifesSteal()
        {
            if (!SelectedCharacter.PowerUps.ContainsKey(PowerUpType.LifeSteal))
            {
                SelectedCharacter.PowerUps.Add(PowerUpType.LifeSteal, new LifeStealScript(PowerUpType.LifeSteal));
            }
            else
            {
                var lifesteal = (LifeStealScript)SelectedCharacter.PowerUps[PowerUpType.LifeSteal];
                lifesteal.HealthPerHit += lifesteal.HealthPerHit;
            }
        }
        public void SpeedIncrease()
        {
            float speedIncrease = SelectedCharacter.Speed * 0.08f;
            SelectedCharacter.Speed += speedIncrease;
        }
        public void BaseDamageIncrease()
        {
            //Multipliers.Instance.AbilityDamageMultiplier += 0.1f;
            SelectedCharacter.BaseDamage += SelectedCharacter.BaseDamage * 0.2f;
        }
        public void CriticalChanceIncrease()
        {
            Multipliers.Instance.CriticalChance += 0.1f;
        }
        public void CriticalDamageIncrease()
        {
            Multipliers.Instance.CriticalDamageMultiplier += 2f;
        }
        public void GoldDropIncrease()
        {
            Multipliers.Instance.CoinWorth *= 2;
        }
        public void LightningStrike()
        {
            if (!SelectedCharacter.PowerUps.ContainsKey(PowerUpType.LightningStrike))
            {
                SelectedCharacter.PowerUps.Add(PowerUpType.LightningStrike, new LightningStrikeScript(PowerUpType.LightningStrike));
                Multipliers.Instance.LightningDamageMultiplier = 10;
            }
            else
            {
                Multipliers.Instance.LightningDamageMultiplier *= 2;
            }
        }
    }
}
