using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FightingGame
{
    public class LightningStrikeScript : PowerUpScript
    {
        private ProjectileType projectileType;
        public float DamageCoefficent;
        private double timeInterval = 5;
        private double currentTime;
        Random random = new Random();

        public LightningStrikeScript(PowerUpType type) : base(type)
        {
            DamageCoefficent = 10;
            projectileType = ProjectileType.LightningStrike;
        }
        public override void Update()
        {
            currentTime += Globals.GameTime.ElapsedGameTime.TotalSeconds;
            if (currentTime >= timeInterval)
            {
                currentTime = 0;

                // Loop through your enemies and select a random enemy within range
                List<Enemy> enemiesInRange = new List<Enemy>();
                foreach (var enemy in GameObjects.Instance.EnemyManager.EnemyPool)
                {
                    float distanceToEnemy = Vector2.Distance(GameObjects.Instance.SelectedCharacter.Position, enemy.Position);
                    if (distanceToEnemy <= 600) // Adjust the range as needed
                    {
                        enemiesInRange.Add(enemy);
                    }
                }

                if (enemiesInRange.Count > 0)
                {
                    int randomIndex = random.Next(enemiesInRange.Count);
                    Enemy randomEnemy = enemiesInRange[randomIndex];
                    GameObjects.Instance.ProjectileManager.AddCharacterProjectile(projectileType, randomEnemy.Position - new Vector2(0, 120), Vector2.Zero, 0, (int)(GameObjects.Instance.SelectedCharacter.BaseDamage * DamageCoefficent));
                    // Now you have a random enemy within range, you can perform actions with it
                }
            }
        }
    }
}
