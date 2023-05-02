using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame.Characters
{
    public class Character : DrawableObjectBase
    {
        public int Health = 100;
        //public Dictionary<AnimationType, List<Rectangle>> Attacks;
        public CharacterName CharacterName;

        private AnimationSprite animation;
        public Character(CharacterName name, Texture2D texture) : base(texture, default, new Vector2(texture.Width, texture.Height), Color.White)
        {
            CharacterName = name;
        }
        public void Update()
        {

        }
    }
}
