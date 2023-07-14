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

namespace FightingGame.Characters
{
    public abstract class Character : DrawableObjectBase
    {
        public int Health = 100;
        public int speed;
        public int Velocity = defaultVelocity;
        public int fallingSpeed;
        public Vector2 Direction;

        public bool IsActive = false;
        public bool IsGrounded = false;
        public CharacterName CharacterName;
        
        public AnimationManager animationManager;
        public AnimationType currentAnimation;
        protected const int defaultVelocity = 20;
        int JumpCount = 0;
        public AnimationType savedAnimaton;

        public Character(CharacterName name, Texture2D texture) : base(texture, new Vector2(0,0), new Vector2(texture.Width, texture.Height), Color.White)
        {
            CharacterName = name;
            animationManager = new AnimationManager();
            foreach (var animation in ContentManager.Instance.Animations[CharacterName])
            {
                animationManager.AddAnimation(animation.Key.Item1, animation.Key.Item2, texture, animation.Value, 0.17f);
            }
        }
        protected abstract void SideAttack();
        protected abstract void UpAttack();
        protected abstract void DownAttack();
        public void Update(AnimationType animation, Vector2 direction) 
        {
            Direction = direction;
            //bool canAnimationChange = CanAnimationChange(animation);
            if(savedAnimaton != AnimationType.None)
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
            else if (currentAnimation == AnimationType.UpAttack)
            {
                UpAttack();
            }
            else if(currentAnimation == AnimationType.DownAttack)
            {
                DownAttack();
            }
            else
            {
                if (direction != Vector2.Zero)
                {
                    Position += Vector2.Normalize(InputManager.Direction) * speed;
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
            animationManager.Draw(Position, InputManager.IsMovingLeft);
        }

        public bool CanAnimationChange(AnimationType animation)
        {
            if (animationManager.CurrentAnimation != null)
            {
                if(animationManager.CurrentAnimation.CanBeCanceled)
                {
                    return true;
                }
                else if(animationManager.CurrentAnimation.CanBeCanceled == false && animationManager.CurrentAnimation.IsAnimationDone)
                {
                    return true;
                }
            }
            return false;
        }
    }
}