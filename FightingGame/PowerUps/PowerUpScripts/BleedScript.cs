using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FightingGame
{
    public class BleedScript : PowerUpScript
    {
        public int BleedDamage;
        private float bleedProcInterval;
        private Dictionary<Enemy, float> bleedingEnemies;
        private float bleedTime;

        public BleedScript(PowerUpType type) : base(type)
        {
            BleedDamage = 2;
            bleedProcInterval = 2f;
            bleedTime = 10;
            bleedingEnemies = new Dictionary<Enemy, float>();
        }

        public override void Update()
        {
            foreach (var enemy in GameObjects.Instance.EnemyManager.EnemyPool)
            {
                if (enemy.HasBeenHit && !bleedingEnemies.ContainsKey(enemy))
                {
                    bleedingEnemies.Add(enemy, bleedTime);
                }
            }
            UpdateBleedingEnemies();
        }


        private void UpdateBleedingEnemies()
        {
            foreach (var bleedingEnemy in bleedingEnemies)
            {
                if(bleedingEnemy.Value > 0 && !bleedingEnemy.Key.IsDead)
                {
                    bleedingEnemies[bleedingEnemy.Key] -= (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
                    bleedProcInterval += (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
                    if(bleedProcInterval >= 2)
                    {
                        bleedingEnemy.Key.TakeDamage(BleedDamage, Color.White);
                        bleedProcInterval = 0;
                    }
                }
                else
                {
                    bleedingEnemies.Remove(bleedingEnemy.Key);
                }
            }
        }
    }
}
