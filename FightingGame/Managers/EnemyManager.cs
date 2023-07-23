using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FightingGame
{
    public class EnemyManager
    {
        private Camera Camera;
        private DrawableObject Tilemap;
        private int enemySpawnRate = 5000;
        private double spawnTimer;

        private int enemyPoolIndex;
        private int deadEnemies;
        private List<Enemy> enemyPool;
        private List<Enemy> reservePool;

        #region Enemy Presets
        Enemy SkeletonPreset = new Enemy(EntityName.Skeleton, ContentManager.Instance.EntitySpriteSheets[EntityName.Skeleton], 30, 0.5f, 1.5f, ContentManager.Instance.EntityAbilites[EntityName.Skeleton]);
        Enemy GhostWarrior = new Enemy(EntityName.GhostWarrior, ContentManager.Instance.EntitySpriteSheets[EntityName.GhostWarrior], 100, 0.2f, 2f, ContentManager.Instance.EntityAbilites[EntityName.GhostWarrior]);
        #endregion
        public EnemyManager(DrawableObject tilemap)
        {
            Tilemap = tilemap;
            reservePool = new List<Enemy>();
            enemyPool = new List<Enemy>();
        }

        public void Update(Character SelectedCharacter, Camera camera)
        {
            Camera = camera;
            spawnTimer += Globals.GameTime.ElapsedGameTime.TotalMilliseconds;
            if (spawnTimer >= enemySpawnRate)
            {
                SpawnEnemies();
                spawnTimer = 0;
            }

            for (int i = 0; i < enemyPoolIndex - deadEnemies; i++)
            {
                if (enemyPool[i].IsDead || CheckEnemyDistanceToPlayer(enemyPool[i], SelectedCharacter))
                {
                    reservePool.Add(enemyPool[i]);
                    enemyPool.RemoveAt(i);
                    deadEnemies++;
                    continue;
                }

                if (SelectedCharacter.WeaponHitBox.Intersects(enemyPool[i].HitBox))
                {
                    if (SelectedCharacter.HasFrameChanged)
                    {
                        enemyPool[i].HasBeenHit = false;
                    }
                    if (SelectedCharacter.CurrentAbility != null && SelectedCharacter.CurrentAbility.CanHit && !enemyPool[i].HasBeenHit)
                    {
                        enemyPool[i].TakeDamage(SelectedCharacter.AbilityDamage);
                        enemyPool[i].HasBeenHit = true;
                    }
                }
                else
                {
                    enemyPool[i].HasBeenHit = false;
                }

                if (enemyPool[i].RemainingHealth <= 0)
                {
                    enemyPool[i].Update(AnimationType.Death, Vector2.Normalize(SelectedCharacter.Position - enemyPool[i].Position));
                    SelectedCharacter.XP += enemyPool[i].XPAmmount;
                }
                else if (CalculateDistance(SelectedCharacter.Position, enemyPool[i].Position) <= 50f && Math.Abs(SelectedCharacter.Position.Y - enemyPool[i].Position.Y) <= 20)
                {
                    enemyPool[i].Update(AnimationType.BasicAttack, Vector2.Normalize(SelectedCharacter.Position - enemyPool[i].Position));
                }
                else
                {
                    enemyPool[i].Update(AnimationType.Run, Vector2.Normalize(SelectedCharacter.Position - enemyPool[i].Position));
                }
            }
        }
        public void Draw()
        {
            for (int i = 0; i < enemyPoolIndex - deadEnemies; i++)
            {
                if (!enemyPool[i].IsDead)
                {
                    enemyPool[i].Draw();
                }
            }
        }
        private void SpawnEnemies()
        {
            int increaseEnemyPoolAmount = 1;
            if (reservePool.Count > 0)
            {
                deadEnemies = 0;
                foreach (var enemy in reservePool)
                {
                    enemy.Reset();
                    enemyPool.Add(enemy);
                    enemy.Spawn(GetSpawnLocation());
                }
                reservePool.Clear();
            }
            for (int i = 0; i < increaseEnemyPoolAmount; i++)
            {
                Enemy enemy = new Enemy(SkeletonPreset);
                enemyPool.Add(enemy);
                enemy.Spawn(GetSpawnLocation());
                enemy.SetBounds(Tilemap.HitBox);
            }
            enemyPoolIndex += increaseEnemyPoolAmount;
        }
        private Vector2 GetSpawnLocation()
        {
            int spawnAreaOffset = 50;
            int minSpawnX = Camera.CameraView.X - Camera.CameraView.Width / 2 + spawnAreaOffset;
            int maxSpawnX = Camera.CameraView.X + Camera.CameraView.Width / 2 - spawnAreaOffset;
            int minSpawnY = Camera.CameraView.Y - Camera.CameraView.Height / 2 + spawnAreaOffset;
            int maxSpawnY = Camera.CameraView.Y + Camera.CameraView.Height / 2 - spawnAreaOffset;

            int randomSpawnX = new Random().Next(minSpawnX, maxSpawnX);
            int randomSpawnY = new Random().Next(minSpawnY, maxSpawnY);
            return new Vector2(randomSpawnX, randomSpawnY);
        }
        private bool CheckEnemyDistanceToPlayer(Entity enemy, Entity selectedCharacter)
        {
            int threshold = 600;
            Vector2 viewportDimensions = new Vector2(Camera.CameraView.Width, Camera.CameraView.Height) / 2;

            if (enemy.Position.X > selectedCharacter.Position.X + (viewportDimensions.X + threshold) ||
                enemy.Position.X < selectedCharacter.Position.X - (viewportDimensions.X + threshold) ||
                enemy.Position.Y > selectedCharacter.Position.Y + (viewportDimensions.Y + threshold) ||
                enemy.Position.Y < selectedCharacter.Position.Y - (viewportDimensions.Y + threshold))
            {
                return true;
            }

            return false;
        }
        private float CalculateDistance(Vector2 playerPosition, Vector2 enemyPosition)
        {
            float distanceX = playerPosition.X - enemyPosition.X;
            float distanceY = playerPosition.Y - enemyPosition.Y;
            float distance = (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
            return distance;
        }
    }
}
