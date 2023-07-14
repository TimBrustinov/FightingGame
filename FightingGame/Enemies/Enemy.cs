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
        public int Health = 10;
        public int fallingSpeed;
        public float speed;

        public bool IsActive = false;
        public bool IsGrounded = false;
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
                animationManager.AddAnimation(animation.Key.Item1, animation.Key.Item2, texture, animation.Value, 0.17f);
            }
        }
       
        protected abstract void SideAttack();
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
            animationManager.Update(currentAnimation);
        }
        public void Draw()
        {
            animationManager.Draw(Position, isMovingLeft);
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