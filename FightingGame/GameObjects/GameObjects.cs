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
            DropManager.Update();
            EnemyManager.Update(SelectedCharacter, Globals.Camera);
            ProjectileManager.Update();
        }

        public void Draw()
        {
            DropManager.Draw();
            EnemyManager.Draw();
            ProjectileManager.Draw();
        }
    }
}
