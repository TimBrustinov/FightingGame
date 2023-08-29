using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FightingGame
{
    public class DamageNumberManager
    {
        private List<DamageNumber> damageNumbers = new List<DamageNumber>();
        private Queue<DamageNumber> pool = new Queue<DamageNumber>();
        private Random random = new Random(); 

        private DamageNumberManager()
        {

        }
        public static DamageNumberManager Instance { get; } = new DamageNumberManager();
        
        private float timeToLive = 1;
        public void AddDamageNumber(float damage, Vector2 position, Color color)
        {
            if(pool.Count > 0)
            {
                var damageNumber = pool.Dequeue();
                damageNumber.Activate(damage, new Vector2(position.X + random.Next(-15, 15), position.Y), color, timeToLive);
                damageNumbers.Add(damageNumber);
            }
            else
            {
                DamageNumber number = new DamageNumber
                {
                    Damage = damage,
                    Position = new Vector2(position.X + random.Next(-15, 15), position.Y),
                    Color = color,
                    TimeToLive = timeToLive,
                };
                damageNumbers.Add(number);
            }

        }

        public void Update()
        {
            for (int i = damageNumbers.Count - 1; i >= 0; i--)
            {
                DamageNumber number = damageNumbers[i];
                number.TimeToLive -= (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
                number.Position = new Vector2(number.Position.X, number.Position.Y - 1);
                if (number.TimeToLive <= 0)
                {
                    pool.Enqueue(number);
                    damageNumbers.RemoveAt(i);
                }
            }
        }

        public void Draw()
        {
            foreach (DamageNumber number in damageNumbers)
            {
                Globals.SpriteBatch.DrawString(ContentManager.Instance.Font, number.Damage.ToString(), number.Position, number.Color);
            }
        }


    }

}
