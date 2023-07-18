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

            TotalHealth = 5;
            RemainingHealth = TotalHealth;
            objectColor = new Color(255, 255, 255, 255);
            speed = 0.5f;
        }

        protected override void Death()
        {
            savedAnimaton = AnimationType.Death;
            overrideAnimation = true;
            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
            {
                savedAnimaton = AnimationType.None;
                isDead = true;
            }
        }

        protected override void SideAttack()
        {
            (int, int) offsets = currFrame.GetWeaponHitboxOffsets();
            setWeaponHitbox(offsets.Item1, offsets.Item2, new Vector2(currFrame.AttackHitbox.Width, currFrame.AttackHitbox.Height));
            //if(currFrame.Frame == new Rectangle(514, 16, 62, 32))
            //{
            //    NumOfHits = 1;
            //}
            savedAnimaton = AnimationType.BasicAttack;
            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
            {
                savedAnimaton = AnimationType.None;
                currentAnimation = AnimationType.None;
                NumOfHits = 1;
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
