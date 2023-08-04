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
        private int bossSpawnRate = 4000;
        private double bossSpawnTimer;

        private int MaxEnemyPool = 50;
        private int enemyPoolIndex;
        private int deadEnemies;
        public List<Enemy> EnemyPool;
        private List<Enemy> ReservePool;

        private Dictionary<int, (int, List<Enemy>)> EnemyWaves;
        private Dictionary<int, List<Enemy>> BossWaves;
        private int currentWave = 0;

        #region Enemy Presets
        Enemy SkeletonPreset = new Enemy(EntityName.Skeleton, false, ContentManager.Instance.EntitySpriteSheets[EntityName.Skeleton], 30, 0.5f, 1.5f, ContentManager.Instance.EntityActions[EntityName.Skeleton]);
        Enemy SkeletonCatPreset = new Enemy(EntityName.SkeletonCat, false, ContentManager.Instance.EntitySpriteSheets[EntityName.SkeletonCat], 30, 1f, 1.5f, ContentManager.Instance.EntityActions[EntityName.SkeletonCat]);
        Enemy GhostWarriorPreset = new Enemy(EntityName.GhostWarrior, true, ContentManager.Instance.EntitySpriteSheets[EntityName.GhostWarrior], 150, 0.8f, 1.5f, ContentManager.Instance.EntityActions[EntityName.GhostWarrior]);   

        //Enemy GhostWarriorPreset = new Enemy(EntityName.GhostWarrior, true, ContentManager.Instance.EntitySpriteSheets[EntityName.GhostWarrior], 100, 0.5f, 1.3f, ContentManager.Instance.EntityActions[EntityName.GhostWarrior]);
        //Enemy GhostWarrior2Preset = new Enemy(EntityName.GhostWarrior2, true, ContentManager.Instance.EntitySpriteSheets[EntityName.GhostWarrior2], 100, 1.5f, 1.3f, ContentManager.Instance.EntityActions[EntityName.GhostWarrior2]);
        //Enemy SkeletonCatPreset = new Enemy(EntityName.SkeletonCat, false, ContentManager.Instance.EntitySpriteSheets[EntityName.SkeletonCat], 30, 2.5f, 1.5f, ContentManager.Instance.EntityActions[EntityName.SkeletonCat]);
        #endregion
        public EnemyManager(DrawableObject tilemap)
        {
            Tilemap = tilemap;
            ReservePool = new List<Enemy>();
            EnemyPool = new List<Enemy>();
            EnemyWaves = new Dictionary<int, (int, List<Enemy>)>();
            BossWaves = new Dictionary<int, List<Enemy>>();
            CreateEnemyWaves();
        }

        public void Update(Character SelectedCharacter, Camera camera)
        {
            Camera = camera;

            enemySpawnTimer += Globals.GameTime.ElapsedGameTime.TotalMilliseconds;
            if (enemySpawnTimer >= enemySpawnRate)
            {
                int randomNumber = new Random().Next(0, EnemyWaves[currentWave].Item2.Count);
                SpawnEnemies(EnemyWaves[currentWave].Item2[randomNumber]);
                enemySpawnTimer = 0;
            }

            bossSpawnTimer += Globals.GameTime.ElapsedGameTime.TotalMilliseconds;
            if(bossSpawnTimer >= bossSpawnRate)
            {
                int randomNumber = new Random().Next(0, BossWaves[currentWave].Count);
                SpawnBoss(BossWaves[currentWave][randomNumber]);
                bossSpawnTimer = 0;
            }

            for (int i = 0; i < enemyPoolIndex - deadEnemies; i++)
            {
                UpdateEnemy(EnemyPool[i], SelectedCharacter);
            }
        }

        private void UpdateEnemy(Enemy enemy, Character selectedCharacter)
        {
            if(enemy.IsBoss && enemy.IsDead)
            {
                EnemyPool.Remove(enemy);
                bossSpawnTimer = 0;
                deadEnemies++;
            }
            else if (enemy.IsDead || CheckEnemyDistanceToPlayer(enemy, selectedCharacter))
            {
                ReservePool.Add(enemy);
                EnemyPool.Remove(enemy);
                deadEnemies++;
            }

           
            
            enemy.Update(selectedCharacter);
            
        }
        public void Draw()
        {
            for (int i = 0; i < enemyPoolIndex - deadEnemies; i++)
            {
                if (!EnemyPool[i].IsDead)
                {
                    EnemyPool[i].Draw();
                }
            }
        }
        private void SpawnEnemies(Enemy enemy)
        {
            int increaseEnemyPoolAmount = EnemyWaves[currentWave].Item1;
            if (ReservePool.Count > 0)
            {
                for (int i = 0; i < ReservePool.Count / 2; i++)
                {
                    if(EnemyWaves[currentWave].Item2.Contains(enemy))
                    {
                        ReservePool[i].Reset();
                        EnemyPool.Add(ReservePool[i]);
                        if (ReservePool[i].AnimationToEntityAction.ContainsKey(AnimationType.Spawn))
                        {
                            ReservePool[i].SpawnWithAnimation(GetSpawnLocation());
                        }
                        else
                        {
                            ReservePool[i].Spawn(GetSpawnLocation());
                        }
                    }
                    ReservePool.RemoveAt(i);
                    deadEnemies--;
                }
            }

            if(EnemyPool.Count < MaxEnemyPool)
            {
                for (int i = 0; i < increaseEnemyPoolAmount; i++)
                {
                    Enemy enemySpawn = new Enemy(enemy);
                    EnemyPool.Add(enemySpawn);
                    if (enemySpawn.AnimationToEntityAction.ContainsKey(AnimationType.Spawn))
                    {
                        enemySpawn.SpawnWithAnimation(GetSpawnLocation());
                    }
                    else
                    {
                        enemySpawn.Spawn(GetSpawnLocation());
                    }
                    enemySpawn.SetBounds(Tilemap.HitBox);
                }
                enemyPoolIndex += increaseEnemyPoolAmount;
            }
        }
        private void SpawnBoss(Enemy boss)
        {
            Enemy bossSpawn = new Enemy(boss);
            bossSpawn.HealthBarColor = Color.Red;
            EnemyPool.Add(bossSpawn);
            if (bossSpawn.AnimationToEntityAction.ContainsKey(AnimationType.Spawn))
            {
                bossSpawn.SpawnWithAnimation(GetSpawnLocation());
            }
            else
            {
                bossSpawn.Spawn(GetSpawnLocation());
            }
            bossSpawn.SetBounds(Tilemap.HitBox);
            enemyPoolIndex += 1;
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
        private void CreateEnemyWaves()
        {
            #region Wave 1
            List<Enemy> wave1Bosses = new List<Enemy>();
            wave1Bosses.Add(GhostWarriorPreset);
            //wave1Bosses.Add(GhostWarrior2Preset);

            List<Enemy> wave1Enemies = new List<Enemy>();
            wave1Enemies.Add(SkeletonCatPreset);
            wave1Enemies.Add(SkeletonPreset);

            EnemyWaves.Add(0, (1, wave1Enemies));
            BossWaves.Add(0, wave1Bosses);
            #endregion 

            //add other waves
        }
    }
}
