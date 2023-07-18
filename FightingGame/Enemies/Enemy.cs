using FightingGame.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FightingGame.Enemies
{
    public abstract class Enemy
    {
        public int TotalHealth = 20;
        public int RemainingHealth;
        public float speed;
        public float EnemyScale;
        public int NumOfHits = 1;
        private float animationSpeed = 0.18f;
        public Color objectColor;

        public bool IsActive = false;
        public bool isMovingLeft;
        public bool isDead = false;
        public bool isHit = false;

        public Rectangle WeaponHitBox;
        public Rectangle HitBox;
        public Vector2 Position;
        public Vector2 TopLeft => Position - Dimentions / 2;
        public Vector2 TopRight => Position + Dimentions / 2;
        public Vector2 Dimentions;
        public Vector2 Direction;

        public AnimationManager animationManager;
        public bool overrideAnimation = false;
        public FrameHelper currFrame;
        public AnimationType currentAnimation;
        public AnimationType savedAnimaton;

        
        public Enemy(EnemyName name, Texture2D texture)
        {
            animationManager = new AnimationManager();
            foreach (var animation in ContentManager.Instance.EnemyAnimations[name])
            {
                animationManager.AddAnimation(animation.Key.Item1, animation.Key.Item2, texture, animation.Value, animationSpeed);
            }
            
        }
        protected abstract void SideAttack();
        protected abstract void UpdateWeapon();
        protected abstract void Death();
        public void Update(AnimationType animation, Vector2 direction)
        {
            isMovingLeft = direction.X < 0 ? true : false;

            if (RemainingHealth <= 0)
            {
                savedAnimaton = AnimationType.None;
            }
            currentAnimation = savedAnimaton != AnimationType.None ? savedAnimaton : animation;

            switch (currentAnimation)
            {
                case AnimationType.Death:
                    Death();
                    break;
                case AnimationType.BasicAttack:
                    SideAttack();
                    break;
                default:
                    if (direction != Vector2.Zero)
                    {
                        Position += Vector2.Normalize(direction) * speed;
                    }
                    else
                    {
                        currentAnimation = AnimationType.Stand;
                    }
                    break;
            }
            UpdateHitbox();
            UpdateWeapon();
            animationManager.Update(currentAnimation, overrideAnimation);
        }
        public void Draw()
        {
            if (savedAnimaton == AnimationType.BasicAttack)
            {
                Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, WeaponHitBox, Color.Blue);
            }
            animationManager.Draw(Position, isMovingLeft, new Vector2(EnemyScale, EnemyScale), objectColor);
            float healthPercentage = (float)RemainingHealth / TotalHealth;
            int foregroundWidth = (int)(healthPercentage * 30);
            Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle((int)TopLeft.X, (int)TopLeft.Y - 10, foregroundWidth, 3), Color.Green);
        }
        public void UpdateHitbox()
        {
            if (animationManager.CurrentAnimation != null)
            {
                currFrame = animationManager.CurrentAnimation.PreviousFrame;
                Dimentions.X = currFrame.SourceRectangle.Width * EnemyScale;
                Dimentions.Y = currFrame.SourceRectangle.Height * EnemyScale;
                HitBox = new Rectangle((int)TopLeft.X, (int)TopLeft.Y, (int)Dimentions.X, (int)Dimentions.Y);
            }

        }
    }
}