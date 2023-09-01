using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FightingGame
{
    public class BleedScript : PowerUpScript
    {
        private class Bleed
        {
            public float BleedTime;
            public float BleedInterval;
            public Bleed(float time, float interval)
            {
                BleedTime = time;
                BleedInterval = interval;
            }
        }

        public int BleedDamage;
        private Dictionary<Enemy, Bleed> bleedingEnemies;
        private List<Enemy> enemiesToRemove;
        private float bleedTime;

        public BleedScript(PowerUpType type) : base(type)
        {
            BleedDamage = 2;
            bleedTime = 10;
            bleedingEnemies = new Dictionary<Enemy, Bleed>();
            enemiesToRemove = new List<Enemy>();
        }

        public override void Update()
        {
            foreach (var enemy in GameObjects.Instance.EnemyManager.HitEnemies)
            {
                if (!bleedingEnemies.ContainsKey(enemy))
                {
                    bleedingEnemies.Add(enemy, new Bleed(bleedTime, 0));
                }
            }
            UpdateBleedingEnemies();
        }


        private void UpdateBleedingEnemies()
        {
            foreach (var bleedingEnemy in bleedingEnemies)
            {
                if(bleedingEnemy.Value.BleedTime > 0 && !bleedingEnemy.Key.IsDead)
                {
                    bleedingEnemies[bleedingEnemy.Key].BleedTime -= (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
                    bleedingEnemies[bleedingEnemy.Key].BleedInterval += (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
                    if(bleedingEnemies[bleedingEnemy.Key].BleedInterval >= 2)
                    {
                        bleedingEnemy.Key.TakeDamage(BleedDamage, Color.White);
                        bleedingEnemies[bleedingEnemy.Key].BleedInterval = 0;
                    }
                }
                else
                {
                    enemiesToRemove.Add(bleedingEnemy.Key);
                }
            }

            foreach (var enemy in enemiesToRemove)
            {
                bleedingEnemies.Remove(enemy);
            }
            enemiesToRemove.Clear();
        }
    }
}
