using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FightingGame.Characters
{
    public class Swordsman : Character
    {

        public Swordsman(CharacterName name, Texture2D texture) : base(name, texture)
        {
            Rectangle characterRectangle = ContentManager.Instance.CharacterTextures[name];

            Position = new Vector2(500, 350 - characterRectangle.Height);

            Dimentions = new Vector2(characterRectangle.Width, characterRectangle.Height);
            speed = 3;
        }

        protected override void DownAttack()
        {
            savedAnimaton = AnimationType.DownAttack;
            if (Direction != Vector2.Zero)
            {
                Position += Vector2.Normalize(InputManager.Direction) * speed;
            }
            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
            {
                savedAnimaton = AnimationType.None;
            }
        }

        protected override void SideAttack()
        {
            savedAnimaton = AnimationType.SideAttack;
            if (Direction != Vector2.Zero)
            {
                Position += Vector2.Normalize(InputManager.Direction) * speed;
            }
            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
            {
                savedAnimaton = AnimationType.None;
            }
        }

        protected override void UpAttack()
        {
            savedAnimaton = AnimationType.UpAttack;
            if (Direction != Vector2.Zero)
            {
                Position += Vector2.Normalize(InputManager.Direction) * speed;
            }
            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
            {
                savedAnimaton = AnimationType.None;
            }
        }
    }
}
