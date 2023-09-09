using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class LifeStealScript : PowerUpScript
    {
        public float HealthPerHit;
        public LifeStealScript(PowerUpType type) : base(type)
        {
            HealthPerHit = 0.05f;
        }

        public override void Update()
        {
            foreach (var enemy in GameObjects.Instance.EnemyManager.EnemyPool)
            {
                if (enemy.HasBeenHit)
                {
                    GameObjects.Instance.SelectedCharacter.RemainingHealth += HealthPerHit;
                    GameObjects.Instance.SelectedCharacter.RemainingHealth = (float)Math.Round(GameObjects.Instance.SelectedCharacter.RemainingHealth, 2);
                    if (GameObjects.Instance.SelectedCharacter.RemainingHealth > GameObjects.Instance.SelectedCharacter.TotalHealth)
                    {
                        GameObjects.Instance.SelectedCharacter.RemainingHealth = GameObjects.Instance.SelectedCharacter.TotalHealth;
                    }
                }
            }
        }
    }
}
