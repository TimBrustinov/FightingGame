using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FightingGame
{
    public class EnemyManager
    {
        private Camera Camera;
        private DrawableObject Tilemap;

        private int enemySpawnRate = 5000;
        private double enemySpawnTimer;
        private int enemyPoolIndex;
        private int deadEnemies;
        public List<Enemy> EnemyPool;
        private List<Enemy> ReserveEnemyPool;

        public List<Enemy> BossPool;
        private List<Enemy> ReserveBossPool;

        #region Enemy Presets
        Enemy SkeletonPreset = new Enemy(EntityName.Skeleton, ContentManager.Instance.EntitySpriteSheets[EntityName.Skeleton], 30, 0.5f, 1.5f, ContentManager.Instance.EntityAbilites[EntityName.Skeleton]);
        Enemy GhostWarriorPreset = new Enemy(EntityName.GhostWarrior, ContentManager.Instance.EntitySpriteSheets[EntityName.GhostWarrior], 100, 0.5f, 1.3f, ContentManager.Instance.EntityAbilites[EntityName.GhostWarrior]);
        #endregion
        public EnemyManager(DrawableObject tilemap)
        {
            Tilemap = tilemap;
            ReserveEnemyPool = new List<Enemy>();
            EnemyPool = new List<Enemy>();
        }

        public void Update(Character SelectedCharacter, Camera camera)
        {
            Camera = camera;
            enemySpawnTimer += Globals.GameTime.ElapsedGameTime.TotalMilliseconds;
            if (enemySpawnTimer >= enemySpawnRate)
            {
                SpawnEnemies(EnemyPool, ReserveEnemyPool);
                enemySpawnTimer = 0;
            }

            for (int i = 0; i < enemyPoolIndex - deadEnemies; i++)
            {
                UpdateEnemy(EnemyPool, ReserveEnemyPool, EnemyPool[i], SelectedCharacter);
            }
        }

        private void UpdateEnemy(List<Enemy> mainPool, List<Enemy> reservePool, Enemy enemy, Character selectedCharacter)
        {
            if (enemy.IsDead)
            {
                selectedCharacter.XP += enemy.XPAmmount;
            }

            if (enemy.IsDead || CheckEnemyDistanceToPlayer(enemy, selectedCharacter))
            {
                reservePool.Add(enemy);
                mainPool.Remove(enemy);
                deadEnemies++;
                return;
            }

            if (selectedCharacter.WeaponHitBox.Intersects(enemy.HitBox))
            {
                if (selectedCharacter.HasFrameChanged)
                {
                    enemy.HasBeenHit = false;
                }
                if (selectedCharacter.CurrentAbility != null && selectedCharacter.CurrentAbility.CanHit && !enemy.HasBeenHit)
                {
                    enemy.TakeDamage(selectedCharacter.AbilityDamage);
                    enemy.HasBeenHit = true;
                }
            }
            else
            {
                enemy.HasBeenHit = false;
            }

            if (enemy.RemainingHealth <= 0)
            {
                enemy.Update(AnimationType.Death, Vector2.Normalize(selectedCharacter.Position - enemy.Position));
            }
            else if (CalculateDistance(selectedCharacter.Position, enemy.Position) <= 50f && Math.Abs(selectedCharacter.Position.Y - enemy.Position.Y) <= 20)
            {
                enemy.Update(AnimationType.BasicAttack, Vector2.Normalize(selectedCharacter.Position - enemy.Position));
            }
            else
            {
                enemy.Update(AnimationType.Run, Vector2.Normalize(selectedCharacter.Position - enemy.Position));
            }
        }

        public void Draw(List<Enemy> pool)
        {
            for (int i = 0; i < enemyPoolIndex - deadEnemies; i++)
            {
                if (!pool[i].IsDead)
                {
                    pool[i].Draw();
                }
            }
        }
        private void SpawnEnemies(List<Enemy> mainPool, List<Enemy> reservePool)
        {
            int increaseEnemyPoolAmount = 1;
            if (reservePool.Count > 0)
            {
                deadEnemies = 0;
                foreach (var enemy in reservePool)
                {
                    enemy.Reset();
                    mainPool.Add(enemy);
                    if (enemy.AnimationToAbility.ContainsKey(AnimationType.Spawn))
                    {
                        enemy.Spawn(GetSpawnLocation(), AnimationType.Spawn);
                    }
                    else
                    {
                        enemy.Spawn(GetSpawnLocation());
                    }
                }
                reservePool.Clear();
            }
            for (int i = 0; i < increaseEnemyPoolAmount; i++)
            {
                Enemy enemy = new Enemy(SkeletonPreset);
                mainPool.Add(enemy);
                if(enemy.AnimationToAbility.ContainsKey(AnimationType.Spawn))
                {
                    enemy.Spawn(GetSpawnLocation(), AnimationType.Spawn);
                }
                else
                {
                    enemy.Spawn(GetSpawnLocation());
                }
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
