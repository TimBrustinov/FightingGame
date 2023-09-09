using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Xna.Framework;

namespace FightingGame
{
    public class ProjectileManager
    {
        private Dictionary<ProjectileType, Projectile> ProjectilePresets;

        private List<Projectile> EnemyProjectiles;
        private List<Projectile> CharacterProjectiles;
        public List<Projectile> ReserveProjectiles { get; set; }

        public ProjectileManager()
        {
            ProjectilePresets = ContentManager.Instance.Projectiles;
            EnemyProjectiles = new List<Projectile>();
            CharacterProjectiles = new List<Projectile>();
            ReserveProjectiles = new List<Projectile>();
        }


        public void Update()
        {
            UpdateEnemyProjectiles();
            UpdateCharacterProjectiles();
        }
        public void Draw()
        {
            DrawEnemyProjectiles();
            DrawCharacterProjectiles();
        }

        #region Enemy
        public void AddEnemyProjectile(ProjectileType type, Vector2 attachmentPoint, Vector2 direction, float speed, int damage)
        {
            var projectile = TryGetProjectile(type);
            if(projectile != null)
            {
                projectile.Reset();
                ReserveProjectiles.Remove(projectile);
            }
            else
            {
                projectile = ProjectilePresets[type].Clone();
            }
            projectile.Activate(attachmentPoint, direction, speed, damage);
            EnemyProjectiles.Add(projectile);
        }

        private void UpdateEnemyProjectiles()
        {
            for (int i = 0; i < EnemyProjectiles.Count; i++)
            {
                if (EnemyProjectiles[i].IsActive )
                {
                    EnemyProjectiles[i].Update();

                    if (EnemyProjectiles[i].Hitbox.Intersects(GameObjects.Instance.SelectedCharacter.HitBox) && !EnemyProjectiles[i].HasHit)
                    {
                        EnemyProjectiles[i].HasHit = true;
                        GameObjects.Instance.SelectedCharacter.TakeDamage(EnemyProjectiles[i].Damage, Color.White);
                    }
                }
                else if(!EnemyProjectiles[i].IsActive)
                {
                    ReserveProjectiles.Add(EnemyProjectiles[i]);
                    EnemyProjectiles.Remove(EnemyProjectiles[i]);
                }
            }

        }

        private void DrawEnemyProjectiles()
        {
            foreach (var projectile in EnemyProjectiles)
            {
                if(projectile.IsActive)
                {
                    projectile.Draw();
                }
            }
        }
        #endregion

        #region Character
        public void AddCharacterProjectile(ProjectileType type, Vector2 attachmentPoint, Vector2 direction, float speed, int damage)
        {
            var projectile = TryGetProjectile(type);
            if (projectile != null)
            {
                projectile.Reset();
                CharacterProjectiles.Remove(projectile);
            }
            else
            {
                projectile = ProjectilePresets[type].Clone();
            }
            projectile.Activate(attachmentPoint, direction, speed, damage);
            CharacterProjectiles.Add(projectile);
        }
        private void UpdateCharacterProjectiles()
        {
            for (int i = 0; i < CharacterProjectiles.Count; i++)
            {
                if (CharacterProjectiles[i].IsActive)
                {
                    CharacterProjectiles[i].Update();

                    foreach (var enemy in GameObjects.Instance.EnemyManager.EnemyPool)
                    {
                        if(CharacterProjectiles[i].Hitbox.Intersects(enemy.HitBox) && !CharacterProjectiles[i].HitEntities.Contains(enemy))
                        {
                            CharacterProjectiles[i].HasHit = true;
                            CharacterProjectiles[i].HitEntities.Add(enemy);
                            enemy.TakeDamage(CharacterProjectiles[i].Damage, Color.White);
                        }
                    }
                }
                else if (!CharacterProjectiles[i].IsActive)
                {
                    ReserveProjectiles.Add(CharacterProjectiles[i]);
                    CharacterProjectiles.Remove(CharacterProjectiles[i]);
                }
            }
        }
        private void DrawCharacterProjectiles()
        {
            foreach (var projectile in CharacterProjectiles)
            {
                if (projectile.IsActive)
                {
                    projectile.Draw();
                }
            }
        }
        #endregion

        private Projectile TryGetProjectile(ProjectileType type)
        {
            foreach (var item in ReserveProjectiles)
            {
                if (item.ProjectileType == type)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
