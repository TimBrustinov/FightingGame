using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class LightningStrikeScript : PowerUpScript
    {
        public float DamagePercentage;
        private Enemy enemyToHit;
        public LightningStrikeScript(PowerUpType type) : base(type)
        {
            DamagePercentage = 5;
        }
        public override void Update()
        {
            foreach (var enemy in GameObjects.Instance.EnemyManager.EnemyPool)
            {
                if()
            }
        }
    }
}
