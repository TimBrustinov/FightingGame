using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class GameObjects
    {
        public EnemyManager EnemyManager;
        public Character SelectedCharacter;
        public ProjectileManager ProjectileManager;
        public DropManager DropManager;
        private GameObjects()
        {

        }

        public static GameObjects Instance { get; } = new GameObjects();

        public void Update()
        {
            EnemyManager.Update(SelectedCharacter, Globals.Camera);
            ProjectileManager.UpdateEnemyProjectiles();
            DropManager.Update();
        }

        public void Draw()
        {
            EnemyManager.Draw();
            ProjectileManager.DrawEnemyProjectiles();
            DropManager.Draw();
        }
    }
}
