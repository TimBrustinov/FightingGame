using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public Dictionary<AnimationType, List<Rectangle>> Attacks;
        public CharacterName CharacterName;
        public bool IsActive = false;

        public CharacterDirection CurrentDirection;
        public AnimationManager animationManager;
        private AnimationType currentAnimation;
        public Character(CharacterName name, Texture2D texture) : base(texture, new Vector2(100, 100), new Vector2(texture.Width, texture.Height), Color.White)
        {
            CharacterName = name;
            animationManager = new AnimationManager();
            foreach(var animation in ContentManager.Instance.Animations[CharacterName])
            {
                animationManager.AddAnimation(animation.Key, texture, animation.Value, 0.3f);
            }
        }
        public void Update(AnimationType animation)
        {
            currentAnimation = animation;
            if(InputManager.Direction == Vector2.Zero)
            {
                currentAnimation = AnimationType.Stand;
            }
            else
            {
                Position += Vector2.Normalize(InputManager.Direction);
            }
            animationManager.Update(currentAnimation);
        }
        public void Draw()
        {
            animationManager.Draw(Position);
        }
    }
}