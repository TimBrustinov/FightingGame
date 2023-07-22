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

        public float TotalHealth;
        public float RemainingHealth;
        public int TotalStamina;
        public int RemainingStamina;

        public float Speed;
        public float SpeedMultiplier = 0;
        public float AbilityDamage;
        public float AbilityDamageMultiplier = 0;
        public float Scale;

        public AnimationManager animationManager;
        public bool overrideAnimation = false;
        public FrameHelper currFrame;
        public AnimationType currentAnimation;
        public AnimationType savedAnimaton;

        private Vector2 minPosition, maxPosition;

        public Dictionary<AnimationType, Ability> AnimationToAbility = new Dictionary<AnimationType, Ability>();
        public Entity(EntityName name, Texture2D texture, Dictionary<AnimationType, Ability> animationToAbility)
        {
            Name = name;
            AnimationToAbility = animationToAbility;
            animationManager = new AnimationManager();
            foreach (var animation in ContentManager.Instance.Animations[Name])
            {
                animationManager.AddAnimation(animation.Key.Item1, animation.Key.Item2, texture, animation.Value, animation.Key.Item3);
            }
        }
        private bool canPerformAttack = false;

        public bool staminaSubtracted = false;

        public virtual void Update(AnimationType animation, Vector2 direction)
        {
            Speed += Speed * SpeedMultiplier;

            overrideAnimation = animation == AnimationType.Death;
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
                    AbilityDamage = CurrentAbility.AbilityDamage + (CurrentAbility.AbilityDamage * AbilityDamageMultiplier);

                    if (savedAnimaton == AnimationType.None)
                    {
                        IsDead = CurrentAbility.IsDead;
                        currentAnimation = AnimationType.Stand;
                        canPerformAttack = false;
                        staminaSubtracted = false;
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
            Position = Vector2.Clamp(Position, minPosition, maxPosition);
           
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
            DrawShadow();
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
        public void Spawn(Vector2 position)
        {
            Position = position;
            savedAnimaton = AnimationType.Spawn;
        }
        public void SetBounds(Rectangle mapSize)
        {
            minPosition = new Vector2(mapSize.X + Dimentions.X / 2, mapSize.Y + Dimentions.Y / 2);
            maxPosition = new Vector2(mapSize.Width - Dimentions.X / 2, mapSize.Height - Dimentions.Y / 2);
        }
        public void TakeDamage(float damage)
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
        public void DrawShadow()
        {
            if(currentAnimation == AnimationType.Dodge)
            {
                Globals.SpriteBatch.Draw(ContentManager.Instance.Shadow, new Rectangle((int)Position.X - currFrame.SourceRectangle.Width / 2, ((int)Position.Y + currFrame.SourceRectangle.Height / 2) + 5, currFrame.SourceRectangle.Width, 10), new Color(255, 255, 255, 100));
            }
            else
            {
                Globals.SpriteBatch.Draw(ContentManager.Instance.Shadow, new Rectangle(HitBox.X, (HitBox.Y + HitBox.Height / 2) + 25, HitBox.Width, 10), new Color(255, 255, 255, 100));
            }
        }
    }
}