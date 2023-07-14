using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FightingGame.Enemies
{
    public class Skeleton : Enemy
    {
        public Skeleton(EnemyName name, Texture2D texture) : base(name, texture)
        {
            Rectangle enemyRectangle = ContentManager.Instance.EnemyTextures[name];

            Position = new Vector2(1000, 350 - enemyRectangle.Height);

            Dimentions = new Vector2(enemyRectangle.Width, enemyRectangle.Height);
            EnemyScale = new Vector2(2, 2);
            speed = 0.5f;
        }
        protected override void SideAttack()
        {
            savedAnimaton = AnimationType.SideAttack;
            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
            {
                savedAnimaton = AnimationType.None;
            }
        }
    }
}
