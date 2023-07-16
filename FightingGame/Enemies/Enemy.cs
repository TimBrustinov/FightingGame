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
    public abstract class Enemy : DrawableObjectBase
    {
        public Rectangle EnemyWeaponHitbox;
        public int Health;
        public int fallingSpeed;
        public float speed;
        public Vector2 EnemyScale;

        public bool IsActive = false;
        public bool isMovingLeft;
        public EnemyName EnemyName;

        public AnimationManager animationManager;
        public AnimationType currentAnimation;
        public AnimationType savedAnimaton;

        public Enemy(EnemyName name, Texture2D texture) : base(texture, new Vector2(0, 0), new Vector2(texture.Width, texture.Height), Color.White) 
        {
            EnemyName = name;
            animationManager = new AnimationManager();
            foreach (var animation in ContentManager.Instance.EnemyAnimations[name])
            {
                animationManager.AddAnimation(animation.Key.Item1, animation.Key.Item2, texture, animation.Value, 0.8f);
            }
        }
        protected abstract void UpdateHitbox();
        protected abstract void SideAttack();
        protected abstract void WeaponUpdate();
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
            WeaponUpdate();
            UpdateHitbox();

            animationManager.Update(currentAnimation);
        }
        public void Draw()
        {
            animationManager.Draw(Position, isMovingLeft, EnemyScale);
            float healthPercentage = (float)Health / 10;
            int foregroundWidth = (int)(healthPercentage * 20);
          
            //Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, HitBox, Color.Pink);
            if (savedAnimaton == AnimationType.SideAttack)
            {
                //Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, EnemyWeaponHitbox, Color.Blue);
            }
            Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle((int)Position.X, (int)Position.Y - 10, foregroundWidth, 3), Color.Green);
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