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
        private List<Projectile> ProjectilePresets;
        private List<Projectile> EnemyProjectiles;
        private List<Projectile> CharacterProjectiles;
        public List<Projectile> ReserveProjectiles { get; set; }

        public ProjectileManager()
        {
            EnemyProjectiles = new List<Projectile>();
            ReserveProjectiles = new List<Projectile>();
        }
        public void AddEnemyProjectile(Projectile projectile)
        {
            EnemyProjectiles.Add(projectile);
        }
        private Projectile TryGetProjectile(ProjectileType type)
        {
            foreach (var item in ReserveProjectiles)
            {
                if(item.ProjectileType == type)
                {
                    return item;
                }
            }
            return null;
        }

        public void UpdateEnemyProjectiles()
        {
            for (int i = 0; i < EnemyProjectiles.Count; i++)
            {
                if (EnemyProjectiles[i].IsActive)
                {
                    EnemyProjectiles[i].Update();

                    if (EnemyProjectiles[i].Hitbox.Intersects(GameObjects.Instance.SelectedCharacter.HitBox))
                    {
                        EnemyProjectiles[i].HasHit = true;
                    }
                }
                else if(!EnemyProjectiles[i].IsActive)
                {
                    ReserveProjectiles.Add(EnemyProjectiles[i]);
                    EnemyProjectiles.Remove(EnemyProjectiles[i]);
                }
            }
            
        }

        public void DrawEnemyProjectiles()
        {
            foreach (var projectile in EnemyProjectiles)
            {
                if(projectile.IsActive)
                {
                    projectile.Draw();
                }
            }
        }
        
    }
}
