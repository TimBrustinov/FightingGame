using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public int NumOfHits = 2;
        public float CharacterScale;
        public float animationSpeed = 0.1f;

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

        public Character(CharacterName name, Texture2D texture)
        {
            CharacterName = name;
            animationManager = new AnimationManager();
            foreach (var animation in ContentManager.Instance.Animations[CharacterName])
            {
                animationManager.AddAnimation(animation.Key.Item1, animation.Key.Item2, texture, animation.Value, animationSpeed);
            }
        }
        protected abstract void BasicAttack();
        protected abstract void Ability1();
        protected abstract void Ability2();
        protected abstract void Ability3();
        protected abstract void Dodge();
        protected abstract void UpdateWeapon();
        protected abstract void Death();
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

            switch (currentAnimation)
            {
                case AnimationType.Death:
                    Death();
                    break;
                case AnimationType.Dodge:
                    Dodge();
                    break;
                case AnimationType.BasicAttack:
                    BasicAttack();
                    break;
                case AnimationType.Ability1:
                    Ability1();
                    break;
                case AnimationType.Ability2:
                    Ability2();
                    break;
                default:
                    if (direction != Vector2.Zero)
                    {
                        Position += Vector2.Normalize(InputManager.Direction) * speed;
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
        private void UpdateHitbox()
        {
            if (currentAnimation == AnimationType.Dodge)
            {
                Dimentions = Vector2.Zero;
                HitBox = new Rectangle((int)TopLeft.X, (int)TopLeft.Y, (int)Dimentions.X, (int)Dimentions.Y);
            }
            else if (animationManager.CurrentAnimation != null)
            {
                currFrame = animationManager.CurrentAnimation.PreviousFrame;
                Dimentions.X = currFrame.CharacterHitbox.Width * CharacterScale;
                Dimentions.Y = currFrame.CharacterHitbox.Height * CharacterScale;
                HitBox = new Rectangle((int)TopLeft.X, (int)TopLeft.Y, (int)Dimentions.X, (int)Dimentions.Y);
            }
        }
        public void Draw()
        {
            Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, HitBox, Color.Red);
            Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, Position, Color.Blue);

            if (savedAnimaton == AnimationType.BasicAttack || savedAnimaton == AnimationType.Ability1 || savedAnimaton == AnimationType.Ability2)
            {
                Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, WeaponHitBox, Color.Aqua);
            } 
            animationManager.Draw(Position, InputManager.IsMovingLeft, new Vector2(CharacterScale, CharacterScale), Color.White);
            DrawHealthBar(Globals.SpriteBatch);
        }
        public void DrawHealthBar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle(10, 20, 200, 20), Color.White);
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle(12, 22, 196, 16), Color.Black);

            float healthPercentage = (float)RemainingHealth / TotalHealth; // Calculate the percentage of remaining health
            int foregroundWidth = (int)(healthPercentage * 196); // Calculate the width of the foreground health bar

            spriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle(12, 22, foregroundWidth, 16), Color.Green);
        }

    }
}