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
        private Vector2 swordHitBoxDimentions = new Vector2(15, 22);
        private int verticalOffset;

        public Swordsman(CharacterName name, Texture2D texture) : base(name, texture)
        {
            Rectangle characterRectangle = ContentManager.Instance.CharacterTextures[name];

            Position = new Vector2(500, 350 - characterRectangle.Height);

            Dimentions = new Vector2(characterRectangle.Width, characterRectangle.Height);
            CharacterScale = new Vector2(1.85f, 1.85f);

            verticalOffset = (int)Dimentions.Y - (int)swordHitBoxDimentions.Y;
            swordHitBoxDimentions *= CharacterScale;

            WeaponHitBox = new Rectangle((int)Position.X + (int)Dimentions.X, (int)Position.Y + verticalOffset, (int)swordHitBoxDimentions.X , (int)swordHitBoxDimentions.Y);
            
            speed = 3;
            //TotalHealth = 8;
           // RemainingHealth = TotalHealth;
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
                NumOfHits = 1;
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
                NumOfHits = 1;
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
                NumOfHits = 1;
            }
        }

        protected override void UpdateWeapon()
        {
            if (InputManager.IsMovingLeft)
            {
                WeaponHitBox = new Rectangle((int)Position.X, (int)Position.Y + verticalOffset, (int)swordHitBoxDimentions.X, (int)swordHitBoxDimentions.Y);
            }
            else
            {
                WeaponHitBox = new Rectangle((int)Position.X + (int)swordHitBoxDimentions.X + (int)swordHitBoxDimentions.X / 4, (int)Position.Y + verticalOffset, (int)swordHitBoxDimentions.X, (int)swordHitBoxDimentions.Y);
            }
        }
    }
}
