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

    }
}
