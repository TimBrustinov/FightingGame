using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FightingGame
{
    public abstract class Entity
    {
        public Rectangle HitBox;
        public Vector2 Position;
        public Vector2 TopLeft => Position - Dimentions / 2;
        public Vector2 TopRight => Position + Dimentions / 2;
        public Vector2 Dimentions;

        public Ability CurrentAbility;
        public Rectangle WeaponHitBox;
        private int weaponVerticalOffset;
        private int weaponHorizontalOffset;

        public bool IsActive = false;
        public bool IsDead = false;
        public bool IsFacingLeft;
        public bool HasBeenHit;
        public bool HasFrameChanged;

        public EntityName Name;

        public int TotalHealth;
        public int RemainingHealth;
        public int TotalStamina;
        public int RemainingStamina;

        public int Speed;
        public int AbilityDamage;
        public float Scale;

        public AnimationManager animationManager;
        public bool overrideAnimation = false;
        public FrameHelper currFrame;
        public AnimationType currentAnimation;
        public AnimationType savedAnimaton;

        public Dictionary<AnimationType, Ability> AnimationToAbility = new Dictionary<AnimationType, Ability>();
        public Entity(EntityName name, Texture2D texture, Dictionary<AnimationType, Ability> animationToAbility, float animationSpeed)
        {
            Name = name;
            AnimationToAbility = animationToAbility;
            animationManager = new AnimationManager();
            foreach (var animation in ContentManager.Instance.Animations[Name])
            {
                animationManager.AddAnimation(animation.Key.Item1, animation.Key.Item2, texture, animation.Value, animationSpeed);
            }
        }
        private bool canPerformAttack = false;

        public bool staminaSubtracted = false;

        public virtual void Update(AnimationType animation, Vector2 direction)
        {
            overrideAnimation = animation == AnimationType.Death ? true : false;
            currentAnimation = savedAnimaton != AnimationType.None ? savedAnimaton : animation;
            if (overrideAnimation)
            {
                currentAnimation = AnimationType.Death;
            }
            if (AnimationToAbility.ContainsKey(currentAnimation))
            {
                CurrentAbility = AnimationToAbility[currentAnimation];
                bool hasEnoughStamina = RemainingStamina >= CurrentAbility.StaminaDrain;

                if(CurrentAbility.StaminaDrain == 0)
                {
                    canPerformAttack = true;
                }
                else if (!staminaSubtracted && hasEnoughStamina)
                {
                    // Deduct stamina by the required amount
                    RemainingStamina -= CurrentAbility.StaminaDrain;
                    staminaSubtracted = true;
                    canPerformAttack = true;
                }

                if (staminaSubtracted || canPerformAttack)
                {
                    savedAnimaton = CurrentAbility.Update(animationManager, currentAnimation, ref Position, direction, Speed);
                    AbilityDamage = CurrentAbility.AbilityDamage;
                    
                    if (savedAnimaton == AnimationType.None)
                    {
                        IsDead = CurrentAbility.IsDead;
                        currentAnimation = AnimationType.Stand;
                        canPerformAttack = false;
                        staminaSubtracted = false; // Reset the flag for the next animation
                    }
                }
                else
                {
                    currentAnimation = AnimationType.Stand;
                }
            }
            else
            {
                if (direction != Vector2.Zero)
                {
                    Position += Vector2.Normalize(direction) * Speed;
                }
                else
                {
                    currentAnimation = AnimationType.Stand;
                }
            }

           
            UpdateHitbox();
            UpdateWeapon();
            animationManager.Update(currentAnimation, overrideAnimation);
            HasFrameChanged = animationManager.CurrentAnimation.hasFrameChanged;
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
                Dimentions.X = currFrame.CharacterHitbox.Width * Scale;
                Dimentions.Y = currFrame.CharacterHitbox.Height * Scale;
                HitBox = new Rectangle((int)TopLeft.X, (int)TopLeft.Y, (int)Dimentions.X, (int)Dimentions.Y);
            }
        }
        public virtual void Draw()
        {
            //Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, HitBox, Color.Red);
            //Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, TopLeft, new Rectangle(currFrame.SourceRectangle.X, currFrame.SourceRectangle.Y, (int)(currFrame.SourceRectangle.Width * Scale), (int)(currFrame.SourceRectangle.Height * Scale)),Color.Red);
            //if (savedAnimaton == AnimationType.BasicAttack || savedAnimaton == AnimationType.Ability1 || savedAnimaton == AnimationType.Ability2)
            //{
            //    Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, WeaponHitBox, Color.Aqua);
            //}
            animationManager.Draw(Position, IsFacingLeft, new Vector2(Scale, Scale), Color.White);
            //Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, Position, new Rectangle(0, 0, 5, 5), Color.Cyan);
        }

        public void Reset()
        {
            currentAnimation = AnimationType.None;
            savedAnimaton = AnimationType.None;
            RemainingHealth = TotalHealth;
            RemainingStamina = TotalStamina;
            IsDead = false;
            overrideAnimation = false;
        }
        public void TakeDamage(int damage)
        {
            RemainingHealth -= damage;
        }
        private void UpdateWeapon()
        {
            if (savedAnimaton == AnimationType.BasicAttack || savedAnimaton == AnimationType.Ability1 || savedAnimaton == AnimationType.Ability2)
            {
                (int, int) offsets = currFrame.GetWeaponHitboxOffsets();
                weaponVerticalOffset = offsets.Item2;
                weaponHorizontalOffset = offsets.Item1;
                WeaponHitBox.Width = (int)(currFrame.AttackHitbox.Width * Scale);
                WeaponHitBox.Height = (int)(currFrame.AttackHitbox.Height * Scale);
                if (InputManager.IsMovingLeft)
                {
                    WeaponHitBox.X = (int)(TopRight.X - WeaponHitBox.Width) - weaponHorizontalOffset;
                }
                else
                {
                    WeaponHitBox.X = (int)(TopLeft.X + weaponHorizontalOffset);
                }
                WeaponHitBox.Y = (int)(TopLeft.Y + weaponVerticalOffset * Scale);
            }
        }
    }
}