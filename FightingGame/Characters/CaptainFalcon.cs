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
    public class CaptainFalcon : Character
    {
        public CaptainFalcon(CharacterName name, Texture2D texture) : base(name, texture)
        {
            CharacterName = name;
            Rectangle characterRectangle = ContentManager.Instance.CharacterTextures[name];
            Position = new Vector2(500, 350 - characterRectangle.Height);

            Dimentions = new Vector2(characterRectangle.Width, characterRectangle.Height);
            animationManager = new AnimationManager();
            foreach (var animation in ContentManager.Instance.Animations[CharacterName])
            {
                animationManager.AddAnimation(animation.Key.Item1, animation.Key.Item2, texture, animation.Value, 0.17f);
            }
        }
        protected override void NeutralAttack()
        {
            savedAnimaton = AnimationType.NeutralAttack;
            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
            {
                savedAnimaton = AnimationType.None;
            }
        }

        protected override void DirectionalAttack()
        {
            savedAnimaton = AnimationType.DirectionalAttack;
            if(InputManager.IsMovingLeft)
            {
                Position = new Vector2(Position.X - 6, Position.Y);
            }
            else
            {
                Position = new Vector2(Position.X + 6, Position.Y);
            }

            if(animationManager.PreviousFrame == new Rectangle(206, 297, 41, 70))
            {
                Position = new Vector2(Position.X, Position.Y - 2);
            }

            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
            {
                savedAnimaton = AnimationType.None;
            }
        }

        protected override void DownAttack()
        {
            throw new NotImplementedException();
        }

        protected override void UpAttack()
        {
            savedAnimaton = AnimationType.DirectionalAttack;
            Position = new Vector2(Position.X, Position.Y - 4);
            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
            {
                savedAnimaton = AnimationType.None;
            }
        }
    }
}
