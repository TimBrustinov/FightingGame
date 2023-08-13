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
        private List<Projectile> EnemyProjectiles;
        public List<Projectile> ReserveEnemyProjectiles;

        public ProjectileManager()
        {
            EnemyProjectiles = new List<Projectile>();
            ReserveEnemyProjectiles = new List<Projectile>();
        }
        public void AddEnemyProjectile(Projectile projectile)
        {
            EnemyProjectiles.Add(projectile);
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
                    ReserveEnemyProjectiles.Add(EnemyProjectiles[i]);
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
