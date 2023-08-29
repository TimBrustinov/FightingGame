using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public abstract class PowerUpScript
    {
        public PowerUpType PowerUpType;
        public bool IsActive;
        public PowerUpScript(PowerUpType type)
        {
            PowerUpType = type;
        }
        public abstract void Update();
    }
}
