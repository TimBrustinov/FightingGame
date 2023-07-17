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
        public int Health;
        public float speed;
        public float EnemyScale;


        public bool IsActive = false;
        public bool isMovingLeft;
        public EnemyName EnemyName;

        public Rectangle WeaponHitBox;
        public Rectangle HitBox;
        public Vector2 Position;
        public Vector2 TopLeft => Position - Dimentions / 2;
        public Vector2 TopRight => Position + Dimentions / 2;
        public Vector2 Dimentions;
        public Vector2 Direction;

        public AnimationManager animationManager;
        public FrameHelper currFrame;
        public AnimationType currentAnimation;
        public AnimationType savedAnimaton;

        public Enemy(EnemyName name, Texture2D texture)
        {
            EnemyName = name;
            animationManager = new AnimationManager();
            foreach (var animation in ContentManager.Instance.EnemyAnimations[name])
            {
                animationManager.AddAnimation(animation.Key.Item1, animation.Key.Item2, texture, animation.Value, 0.8f);
            }
        }
        protected abstract void SideAttack();
        protected abstract void UpdateWeapon();
        public void Update(AnimationType animation, Vector2 direction)
        {
            if (direction.X > 0)
            {
                isMovingLeft = false;
            }
            else if (direction.X < 0)
            {
                isMovingLeft = true;
            }

            if (savedAnimaton != AnimationType.None)
            {
                currentAnimation = savedAnimaton;
            }
            else
            {
                currentAnimation = animation;
            }
           

            if (currentAnimation == AnimationType.SideAttack)
            {
                SideAttack();
            }
            else
            {
                if (direction != Vector2.Zero)
                {
                    Position += Vector2.Normalize(direction) * speed;
                }
                else
                {
                    currentAnimation = AnimationType.Stand;
                }
            }
            UpdateHitbox();
            UpdateWeapon();
            animationManager.Update(currentAnimation);
        }
        public void Draw()
        {
            if (savedAnimaton == AnimationType.SideAttack)
            {
                Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, WeaponHitBox, Color.Blue);
            }
            animationManager.Draw(Position, isMovingLeft, new Vector2(EnemyScale, EnemyScale));
            float healthPercentage = (float)Health / 10;
            int foregroundWidth = (int)(healthPercentage * 20);
          
            
            Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle((int)TopLeft.X, (int)TopLeft.Y - 10, foregroundWidth, 3), Color.Green);
        }
        public void UpdateHitbox()
        {
            if (animationManager.CurrentAnimation != null)
            {
                currFrame = animationManager.CurrentAnimation.PreviousFrame;
                Dimentions.X = currFrame.Frame.Width * EnemyScale;
                Dimentions.Y = currFrame.Frame.Height * EnemyScale;
                HitBox = new Rectangle((int)TopLeft.X, (int)TopLeft.Y, (int)Dimentions.X, (int)Dimentions.Y);
            }

        }
        public bool CanAnimationChange(AnimationType animation)
        {
            if (animationManager.CurrentAnimation != null)
            {
                if (animationManager.CurrentAnimation.CanBeCanceled)
                {
                    return true;
                }
                else if (animationManager.CurrentAnimation.CanBeCanceled == false && animationManager.CurrentAnimation.IsAnimationDone)
                {
                    return true;
                }
            }
            return false;
        }
     
    }
}