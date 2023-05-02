using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame.Characters
{
    public class Character : AnimationSprite
    {
        public int Health;
        //public Dictionary<AnimationType, List<Rectangle>> Attacks;
        public CharacterName CharacterName;
        protected Character(CharacterName name, Texture2D texture, Vector2 position, Vector2 dimentions, Color color, int health, Dictionary<AnimationType, List<Rectangle>> attacks) : base(texture, position, dimentions, color)
        {
            Health = health;
            //Attacks = attacks;
            CharacterName = name;
        }
    }
}
