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
        public int Velocity = defaultVelocity;
        public int fallingSpeed;
        public int JumpCount = 0;

        public bool IsActive = false;
        public bool IsGrounded = false;
        public Dictionary<AnimationType, List<Rectangle>> Attacks;
        public CharacterName CharacterName;
        
        public CharacterDirection CurrentDirection;
        public AnimationManager animationManager;
        public AnimationType currentAnimation;
        protected const int defaultVelocity = 20;

        public Character(CharacterName name, Texture2D texture) : base(texture, new Vector2(0,0), new Vector2(texture.Width, texture.Height), Color.White)
        {
            CharacterName = name;
            Rectangle characterRectangle = ContentManager.Instance.CharacterTextures[name];
            Position = new Vector2(500, 350 - characterRectangle.Height);

            Dimentions = new Vector2(characterRectangle.Width, characterRectangle.Height);
            animationManager = new AnimationManager();
            foreach(var animation in ContentManager.Instance.Animations[CharacterName])
            {
                animationManager.AddAnimation(animation.Key.Item1, animation.Key.Item2, texture, animation.Value, 0.17f);
            }
        }
        protected abstract void NeutralAttack();
        protected abstract void DirectionalAttack();
        protected abstract void DownAttack();
        protected abstract void UpAttack();
        public void Update(AnimationType animation, Vector2 direction) 
        {
            bool canAnimationChange = CanAnimationChange(animation);
            if (IsGrounded)
            {
                fallingSpeed = 0;
            }
            //running, standing and falling animations
            if (canAnimationChange)
            {
                currentAnimation = animation;
                if(direction != Vector2.Zero)
                {
                    Position += Vector2.Normalize(InputManager.Direction) * 5;
                }
                else
                {
                    currentAnimation = AnimationType.Stand;
                }


                if (!IsGrounded && canAnimationChange)
                {
                    if (fallingSpeed != 5)
                    {
                        Position += new Vector2(0, 1 * fallingSpeed++);
                    }
                    else
                    {
                        Position += new Vector2(0, 1 * fallingSpeed);
                    }

                }
            }
            // Jumping animation
            else if(animation == AnimationType.Jump)
            {
                JumpCount = 1;
                if (IsGrounded)
                {
                    animationManager.CurrentAnimation.IsAnimationDone = true;
                    Velocity = defaultVelocity;
                    currentAnimation = AnimationType.Stand;
                    JumpCount = 0;
                }

                if (Velocity == -defaultVelocity - 1)
                {
                    Position += new Vector2(direction.X * 5, -1 * Velocity);
                }
                else if (currentAnimation == AnimationType.Jump)
                {
                    Position += new Vector2(direction.X * 5, -1 * Velocity--);
                }
            }
            else if (animation == AnimationType.NeutralAttack)
            {
                NeutralAttack();
            }
            else if(animation == AnimationType.DirectionalAttack)
            {
                DirectionalAttack();
            }


            

            //gravity is off
            //if (IsGrounded)
            //{
            //    fallingSpeed = 0;
            //}
            //if(animation == AnimationType.Jump)
            //{
            //    JumpCount = 1;
            //    if (IsGrounded)
            //    {
            //        animationManager.CurrentAnimation.IsAnimationDone = true;
            //        Velocity = defaultVelocity;
            //        currentAnimation = AnimationType.Stand;
            //        HasHit = false;
            //        JumpCount = 0;
            //    }

            //    if (Velocity == -defaultVelocity - 1)
            //    {
            //        Position += new Vector2(direction.X * 5, -1 * Velocity);
            //    }
            //    else if(currentAnimation == AnimationType.Jump)
            //    {
            //        Position += new Vector2(direction.X * 5, -1 * Velocity--);
            //    }


            //}
            //else if(animation == AnimationType.Stand)
            //{
            //    currentAnimation = AnimationType.Stand;
            //}
            //else
            //{
            //    if(InputManager.Direction != new Vector2(0, 0))
            //    {
            //        Position += Vector2.Normalize(InputManager.Direction) * 5;
            //    }
            //}

            //if(!IsGrounded && currentAnimation != AnimationType.Jump)
            //{
            //    if(fallingSpeed != 5)
            //    {
            //        Position += new Vector2(0, 1 * fallingSpeed++);
            //    }
            //    else
            //    {
            //        Position += new Vector2(0, 1 * fallingSpeed);
            //    }

            //}
            //Dimentions = new Vector2(animationManager.CurrentFrame.Width, animationManager.CurrentFrame.Height);
            animationManager.Update(currentAnimation);
        }
        public void Draw()
        {
            animationManager.Draw(Position);
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