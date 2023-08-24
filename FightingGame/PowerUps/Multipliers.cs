using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class Multipliers
    {
        public float AbilityDamageMultiplier;
        private Multipliers()
        {

        }

        public static Multipliers Instance { get; } = new Multipliers();
    }
}
