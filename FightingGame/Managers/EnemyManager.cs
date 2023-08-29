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

        private int enemySpawnAmmountMax = 3;
        private int enemySpawnRate = 5000;
        private double enemySpawnTimer;
        private int bossSpawnRate = 4000;
        private double bossSpawnTimer;

        public int enemyPoolIndex;
        public List<Enemy> EnemyPool;
        public List<Enemy> ReservePool;
        private Random random;

        private int num = 0;
        private Dictionary<int, List<Enemy>> EnemyWaves;
        private Dictionary<int, List<Enemy>> BossWaves;
        private int currentWave = 0;
         
        #region Enemy Presets
        public Enemy SkeletonPreset = new Enemy(EntityName.Skeleton, false, 100, 0.5f, 1.3f, false, 0);
        //Enemy GhostWarriorPreset = new Enemy(EntityName.GhostWarrior, true, 100, 0.6f, 1.7f, false, 0);
        Enemy GhostWarrior2Preset = new Enemy(EntityName.GhostWarrior2, false, 200, 0.8f, 1f, false, 0);
        Enemy NecromancerPreset = new Enemy(EntityName.Necromancer, false, 200, 0.5f, 1.2f, false, 0);
        Enemy BringerOfDeathPreset = new Enemy(EntityName.BringerOfDeath, true, 600, 0.5f, 1f, true, 0);
        Enemy RangedCultistPreset = new Enemy(EntityName.RangedCultist, false, 30, 0.5f, 1.5f, true, 0);
        Enemy AssassinCultistPreset = new Enemy(EntityName.AssassinCultist, false, 20, 1.5f, 1.4f, true, 0);
        //Enemy GhostWarriorPreset = new Enemy(EntityName.GhostWarrior, true, ContentManager.Instance.EntitySpriteSheets[EntityName.GhostWarrior], 150, 0.8f, 1.5f, ContentManager.Instance.EntityAnimationBehaviours[EntityName.GhostWarrior]);
        //Enemy GhostWarrior2Preset = new Enemy(EntityName.GhostWarrior2, true, ContentManager.Instance.EntitySpriteSheets[EntityName.GhostWarrior2], 150, 1f, 1.5f, ContentManager.Instance.EntityAnimationBehaviours[EntityName.GhostWarrior2]);
        #endregion
        public EnemyManager(DrawableObject tilemap)
        {
            Tilemap = tilemap;
            ReservePool = new List<Enemy>();
            EnemyPool = new List<Enemy>();
            EnemyWaves = new Dictionary<int, List<Enemy>>();
            BossWaves = new Dictionary<int, List<Enemy>>();
            random = new Random();
            CreateEnemyWaves();
        }

        public void Update(Character SelectedCharacter, Camera camera)
        {
            Camera = camera;
            enemySpawnTimer += Globals.GameTime.ElapsedGameTime.TotalMilliseconds;

            if (enemyPoolIndex <= 3)
            {
                if (enemySpawnTimer >= enemySpawnRate)
                {
                    SpawnEnemies();
                    enemySpawnTimer = 0;
                }

                bossSpawnTimer += Globals.GameTime.ElapsedGameTime.TotalMilliseconds;
                if (bossSpawnTimer >= bossSpawnRate)
                {
                    int randomNumber = new Random().Next(0, BossWaves[currentWave].Count);
                    SpawnBoss(BossWaves[currentWave][randomNumber]);
                    bossSpawnTimer = 0;
                }
            }
           
            
            for (int i = 0; i < enemyPoolIndex; i++)
            {
                if (EnemyPool[i].IsBoss && EnemyPool[i].IsDead)
                {
                    EnemyPool.Remove(EnemyPool[i]);
                    bossSpawnTimer = 0;
                    enemyPoolIndex--;
                }
                else if (EnemyPool[i].IsDead || CheckEnemyDistanceToPlayer(EnemyPool[i], SelectedCharacter))
                {
                    ReservePool.Add(EnemyPool[i]);
                    SelectedCharacter.XP += EnemyPool[i].XPAmmount;
                    EnemyPool.Remove(EnemyPool[i]);
                    enemyPoolIndex--;
                }
                else
                {
                    EnemyPool[i].Update(SelectedCharacter);
                }
                //UpdateEnemy(EnemyPool[i], SelectedCharacter);
                //Console.WriteLine(EnemyPool[enemyPoolIndex - 1].Animator.CurrentAnimation.frameTime);
                //Console.WriteLine(EnemyPool[0].Direction);
            }
        }
        public void Draw()
        {
            for (int i = 0; i < enemyPoolIndex; i++)
            {
                if (!EnemyPool[i].IsDead)
                {
                    EnemyPool[i].Draw();
                }
            }
        }
        private void SpawnEnemies()
        {
            int RandomAmmountOfEnemies = random.Next(1, enemySpawnAmmountMax);

            for (int i = 0; i < RandomAmmountOfEnemies; i++)
            {
                var enemyFromReserve = GetEnemyFromReserve();
                if(enemyFromReserve != null)
                {
                    enemyFromReserve.Reset();
                    enemyFromReserve.Spawn(GetSpawnLocation());
                    EnemyPool.Add(enemyFromReserve);
                    ReservePool.Remove(enemyFromReserve);
                }
                else
                {
                    int randomEnemy = random.Next(0, EnemyWaves[currentWave].Count);
                    var newEnemy = EnemyWaves[currentWave][randomEnemy].Clone();
                    //var newEnemy = new Enemy(EnemyWaves[currentWave][randomEnemy]);
                    newEnemy.SetBounds(Tilemap.HitBox);
                    newEnemy.Spawn(GetSpawnLocation());
                    newEnemy.NUM = num;
                    num++;
                    EnemyPool.Add(newEnemy);
                }
            }
            enemyPoolIndex += RandomAmmountOfEnemies;
        }
        private void SpawnBoss(Enemy boss)
        {
            Enemy bossSpawn = new Enemy(boss);
            bossSpawn.HealthBarColor = Color.Red;
            EnemyPool.Add(bossSpawn);
            if (bossSpawn.Animator.AnimationBehaviours.ContainsKey(AnimationType.Spawn))
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
            wave1Bosses.Add(BringerOfDeathPreset);
            //wave1Bosses.Add(GhostWarrior2Preset);

            List<Enemy> wave1Enemies = new List<Enemy>();
            //wave1Enemies.Add(SkeletonPreset);
            //wave1Enemies.Add(RangedCultistPreset);
            //wave1Enemies.Add(BringerOfDeathPreset);
            //wave1Enemies.Add(AssassinCultistPreset);
            wave1Enemies.Add(NecromancerPreset);
            wave1Enemies.Add(GhostWarrior2Preset);

            EnemyWaves.Add(0, wave1Enemies);
            BossWaves.Add(0, wave1Bosses);
            #endregion 
            //add other waves
        }

        public Enemy GetEnemyFromReserve()
        {
            if(ReservePool.Count > 0)
            {
                for (int i = 0; i < ReservePool.Count; i++)
                {
                    if(ReservePool[i].WaveNum == currentWave)
                    {
                        return ReservePool[i];
                    }
                }
            }
            return null;
        }
        public Enemy GetEnemyFromReserve(EntityName enemyName)
        {
            if (ReservePool.Count > 0)
            {
                for (int i = 0; i < ReservePool.Count; i++)
                {
                    if (ReservePool[i].Name == enemyName)
                    {
                        return ReservePool[i];
                    }
                }
            }
            return null;
        }
    }
}
