using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace FightingGame
{
    public class DropManager
    {
        public Rarity SelectedRarity;
        private List<Drop> drops;
        private List<Drop> dropsPool;
        private Random random = new Random();

        private Dictionary<IconType, Drop> dropsDictionary;

        public DropManager()
        {
            drops = new List<Drop>();
            dropsPool = new List<Drop>();
            dropsDictionary = ContentManager.Instance.EnemyDrops;
            SelectedRarity = Rarity.None;
        }

        public void Update()
        {
            for (int i = 0; i < drops.Count; i++)
            {
                Drop drop = drops[i];
                if (GameObjects.Instance.SelectedCharacter.HitBox.Intersects(drop.Hitbox))
                {
                    SelectedRarity = drop.Rarity;
                    dropsPool.Add(drop);
                    drops.Remove(drop);
                }
            }
        }

        public void Draw()
        {
            foreach (Drop drop in drops)
            {
                drop.Draw();
            }
        }
        public void RollForDrop(Vector2 position)
        {
            if (random.NextDouble() < 1f)
            {
                double randomNumber = random.NextDouble();
                
                if (randomNumber < 0.03)
                {
                    AddDrop(IconType.LegendaryScroll, position);
                }
                else if (randomNumber < 0.5)
                {
                    AddDrop(IconType.RareScroll, position);
                }
                else
                {
                    AddDrop(IconType.CommonScroll, position);
                }
            }
        }
        private void AddDrop(IconType type, Vector2 position)
        {
            var drop = TryGetDrop(type);
            if(drop == null)
            {
                drop = dropsDictionary[type].Clone();
            }
            drop.Activate(position);
            drops.Add(drop);
        }
        private Drop TryGetDrop(IconType type)
        {
            if (dropsPool.Count > 0)
            {
                for (int i = 0; i < dropsPool.Count; i++)
                {
                    if (dropsPool[i].Icon.Type == type)
                    {
                        var drop = dropsPool[i];
                        dropsPool.RemoveAt(i);
                        return drop;
                    }
                }
            }
            return null;
        }
    }
}