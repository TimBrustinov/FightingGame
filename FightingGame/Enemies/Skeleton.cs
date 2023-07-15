using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FightingGame.Enemies
{
    public class Skeleton : Enemy
    {
        private Vector2 maceHitboxDimentions = new Vector2(40, 30);
        private int verticalOffset;
        private int horizontalOffset;

        public Skeleton(EnemyName name, Texture2D texture) : base(name, texture)
        {
            Rectangle enemyRectangle = ContentManager.Instance.EnemyTextures[name];

            Position = new Vector2(1000, 350 - enemyRectangle.Height);

            Dimentions = new Vector2(enemyRectangle.Width, enemyRectangle.Height);
            EnemyScale = new Vector2(1.3f, 1.3f);

            maceHitboxDimentions *= EnemyScale;

            verticalOffset = (int)maceHitboxDimentions.Y - (int)Dimentions.Y;
            horizontalOffset = (int)maceHitboxDimentions.X - (int)Dimentions.X;

            EnemyWeaponHitbox = new Rectangle(281 * (int)EnemyScale.X, 6 * (int)EnemyScale.X, 51 * (int)EnemyScale.X, 28 * (int)EnemyScale.X);

            Health = 30;
            speed = 0.5f;
        }
        protected override void UpdateHitbox()
        {
            if(animationManager.CurrentAnimation != null)
            {

                Rectangle frame = animationManager.CurrentAnimation.CurrerntFrame;
                Dimentions = new Vector2((int)(frame.Width * EnemyScale.X), (int)(frame.Height * EnemyScale.Y));
            }
           
            verticalOffset = (int)maceHitboxDimentions.Y - (int)Dimentions.Y;
            horizontalOffset = (int)maceHitboxDimentions.X - (int)Dimentions.X;
        }

        protected override void SideAttack()
        {
            savedAnimaton = AnimationType.SideAttack;
            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
            {
                savedAnimaton = AnimationType.None;
            }
        }

        protected override void WeaponUpdate()
        {
            if(isMovingLeft)
            {
                EnemyWeaponHitbox.X = (int)(Position.X - horizontalOffset);
            }
            else
            {
                EnemyWeaponHitbox.X = (int)(Position.X + horizontalOffset);
            }

            EnemyWeaponHitbox.Y = (int)(Position.Y - verticalOffset);

        }
    }
}
