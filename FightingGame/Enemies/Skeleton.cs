using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FightingGame.Enemies
{
    public class Skeleton : Enemy
    {
        private Vector2 maceHitboxDimentions = Vector2.Zero;
        private int weaponVerticalOffset;
        private int weaponHorizontalOffset;
        private int numUpdates = 1;

        public Skeleton(EnemyName name, Texture2D texture) : base(name, texture)
        {
            Rectangle enemyRectangle = ContentManager.Instance.EnemyTextures[name];
            EnemyScale = 1.3f;
            Position = new Vector2(1000, 350);

            Dimentions = new Vector2(enemyRectangle.Width, enemyRectangle.Height) * EnemyScale;

            Health = 30;
            speed = 0.5f;
        }

        protected override void SideAttack()
        {
            (int, int) offsets = currFrame.GetOffsets();
            setWeaponHitbox(offsets.Item1, offsets.Item2, new Vector2(currFrame.AttackHitbox.Width, currFrame.AttackHitbox.Height));
            
            savedAnimaton = AnimationType.SideAttack;
            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
            {
                savedAnimaton = AnimationType.None;
                currentAnimation = AnimationType.None;

            }
        }

        protected override void UpdateWeapon()
        {
            if(isMovingLeft)
            {
                WeaponHitBox.X = (int)(TopRight.X - WeaponHitBox.Width) - weaponHorizontalOffset;
            }
            else
            {
                WeaponHitBox.X = (int)(TopLeft.X + weaponHorizontalOffset);
            }
            WeaponHitBox.Y = (int)(TopLeft.Y + weaponVerticalOffset * EnemyScale);
        }

        private void setWeaponHitbox(int verticalOffset, int horizontalOffset, Vector2 dimenions)
        {
            weaponVerticalOffset = verticalOffset;
            weaponHorizontalOffset = horizontalOffset;
            maceHitboxDimentions = dimenions * EnemyScale;
            WeaponHitBox = new Rectangle((int)TopLeft.X + weaponHorizontalOffset, (int)TopLeft.Y + weaponVerticalOffset, (int)maceHitboxDimentions.X, (int)maceHitboxDimentions.Y);
        }
    }
}
