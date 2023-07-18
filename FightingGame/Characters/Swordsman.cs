//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace FightingGame.Characters
//{
//    public class Swordsman : Character
//    {
//        private Vector2 swordHitBoxDimentions = Vector2.Zero;
//        private int weaponVerticalOffset;
//        private int weaponHorizontalOffset;
//        private int numUpdates = 1;

//        public Swordsman(CharacterName name, Texture2D texture) : base(name, texture)
//        {
//            Rectangle characterRectangle = ContentManager.Instance.CharacterTextures[name];
//            CharacterScale = 1.85f;
//            Position = new Vector2(500, 350);

//            Dimentions = new Vector2(characterRectangle.Width, characterRectangle.Height) * CharacterScale;

//            speed = 3;
//            TotalHealth = 10;
//            RemainingHealth = TotalHealth;
//        }

//        protected override void Death()
//        {
//            savedAnimaton = AnimationType.Death;
//            overrideAnimation = true;
//            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
//            {
//                savedAnimaton = AnimationType.None;
//            }
//        }

//        protected override void DownAttack()
//        {
//            (int, int) offsets = currFrame.GetOffsets();
//            setWeaponHitbox(offsets.Item1, offsets.Item2, new Vector2(currFrame.AttackHitbox.Width, currFrame.AttackHitbox.Height));

//            savedAnimaton = AnimationType.DownAttack;
//            if (Direction != Vector2.Zero)
//            {
//                Position += Vector2.Normalize(InputManager.Direction) * speed;
//            }
//            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
//            {
//                savedAnimaton = AnimationType.None;
//                currentAnimation = AnimationType.Stand;
//                NumOfHits = 1;
//                numUpdates = 1;
//            }
//        }

//        protected override void SideAttack()
//        {
//            (int, int) offsets = currFrame.GetOffsets();
//            setWeaponHitbox(offsets.Item1, offsets.Item2, new Vector2(currFrame.AttackHitbox.Width, currFrame.AttackHitbox.Height));

//            savedAnimaton = AnimationType.SideAttack;
//            if (Direction != Vector2.Zero)
//            {
//                Position += Vector2.Normalize(InputManager.Direction) * speed;
//            }
//            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
//            {
//                savedAnimaton = AnimationType.None;
//                currentAnimation = AnimationType.Stand;
//                NumOfHits = 1;
//                numUpdates = 1;
//            }
//        }

//        protected override void UpAttack()
//        {
//            (int, int) offsets = currFrame.GetOffsets();
//            setWeaponHitbox(offsets.Item1, offsets.Item2, new Vector2(currFrame.AttackHitbox.Width, currFrame.AttackHitbox.Height));

//            savedAnimaton = AnimationType.UpAttack;
//            if (Direction != Vector2.Zero)
//            {
//                Position += Vector2.Normalize(InputManager.Direction) * speed;
//            }
//            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
//            {
//                savedAnimaton = AnimationType.None;
//                currentAnimation = AnimationType.Stand;
//                NumOfHits = 1;
//                numUpdates = 1;
//            }
//        }

//        protected override void UpdateWeapon()
//        {
//            if (savedAnimaton == AnimationType.SideAttack || savedAnimaton == AnimationType.DownAttack || savedAnimaton == AnimationType.UpAttack)
//            {
//                if (InputManager.IsMovingLeft)
//                {
//                    WeaponHitBox.X = (int)(TopRight.X - WeaponHitBox.Width) - weaponHorizontalOffset;
//                }
//                else
//                {
//                    WeaponHitBox.X = (int)(TopLeft.X + weaponHorizontalOffset);
//                }
//                WeaponHitBox.Y = (int)(TopLeft.Y + weaponVerticalOffset * CharacterScale);
//            }
//        }

//        private void setWeaponHitbox(int horizontalOffset, int verticalOffset, Vector2 dimenions)
//        {
//            weaponVerticalOffset = verticalOffset;
//            weaponHorizontalOffset = horizontalOffset;
//            swordHitBoxDimentions = dimenions * CharacterScale;
//            WeaponHitBox = new Rectangle((int)TopLeft.X + weaponHorizontalOffset, (int)TopLeft.Y + weaponVerticalOffset, (int)swordHitBoxDimentions.X, (int)swordHitBoxDimentions.Y);
//        }

//    }
//}
