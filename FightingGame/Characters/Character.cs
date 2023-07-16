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
    public abstract class Character
    {
        public bool IsActive = false;
        public CharacterName CharacterName;

        public int TotalHealth;
        public int RemainingHealth;
        public int speed;
        public int NumOfHits = 1;
        public float CharacterScale;
        private float animationSpeed = 0.18f;

        public Rectangle WeaponHitBox;
        public Rectangle HitBox;
        public Vector2 OriginPosition;
        public Vector2 Position;
        public Vector2 Dimentions;
        public Vector2 Direction;
        
        public AnimationManager animationManager;
        public AnimationType currentAnimation;
        public AnimationType savedAnimaton;

        public Character(CharacterName name, Texture2D texture)
        {
            CharacterName = name;
            animationManager = new AnimationManager();
            foreach (var animation in ContentManager.Instance.Animations[CharacterName])
            {
                animationManager.AddAnimation(animation.Key.Item1, animation.Key.Item2, texture, animation.Value, animationSpeed);
            }
        }
        protected abstract void SideAttack();
        protected abstract void UpAttack();
        protected abstract void DownAttack();
        protected abstract void UpdateWeapon();
        public void Update(AnimationType animation, Vector2 direction) 
        {
            //bool canAnimationChange = CanAnimationChange(animation);
            Direction = direction;
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
                    OriginPosition += Vector2.Normalize(InputManager.Direction) * speed;
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
        private void UpdateHitbox()
        {
            if (animationManager.CurrentAnimation != null)
            {
                Rectangle frame = animationManager.CurrentAnimation.PreviousFrame;
                Dimentions.X = frame.Width * CharacterScale;
                Dimentions.Y = frame.Height * CharacterScale;
                Position = OriginPosition - Dimentions / 2;
                HitBox = new Rectangle((int)Position.X, (int)Position.Y, (int)Dimentions.X, (int)Dimentions.Y);
            }
        }
        public void Draw()
        {
            Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, HitBox, Color.Red);
            if (savedAnimaton == AnimationType.SideAttack || savedAnimaton == AnimationType.DownAttack || savedAnimaton == AnimationType.UpAttack)
            {
                Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, WeaponHitBox, Color.Blue);
            }
            animationManager.Draw(OriginPosition, InputManager.IsMovingLeft, new Vector2(CharacterScale, CharacterScale));
            

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