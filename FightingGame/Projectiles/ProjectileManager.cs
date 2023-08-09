using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class ProjectileManager
    {
        private List<Projectile> Projectiles;

        public ProjectileManager()
        {
            Projectiles = new List<Projectile>();
        }
        public void AddProjectile(Projectile projectile)
        {
            Projectiles.Add(projectile);
        }

        public void Update()
        {
            foreach (var projectile in Projectiles)
            {
                if(projectile.IsActive)
                {
                    projectile.Update();
                }
            }
        }

        public void Draw()
        {
            foreach (var projectile in Projectiles)
            {
                if(projectile.IsActive)
                {
                    projectile.Draw();
                }
            }
        }
    }
}
