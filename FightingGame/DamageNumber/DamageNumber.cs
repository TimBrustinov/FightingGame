using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace FightingGame
{
    public class DamageNumber
    {
        public float Damage { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public float TimeToLive { get; set; }
        public float Height { get; set; }

        public void Activate(float damage, Vector2 position, Color color, float timeToLive)
        {
            Damage = damage;
            Position = position;
            Color = color;
            TimeToLive = timeToLive;
            Height = 0;
        }
    }

}
