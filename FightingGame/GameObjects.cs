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
        private GameObjects()
        {

        }

        public static GameObjects Instance { get; } = new GameObjects();
    }
}
