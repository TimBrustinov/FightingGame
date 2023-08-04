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
        public Vector2 Direction;
        public Vector2 TopLeft => Position - Dimentions / 2;
        public Vector2 TopRight => Position + Dimentions / 2;
        public Vector2 Dimentions;

        public EntityAction CurrentAction;
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
        public float TotalStamina;
        public float RemainingStamina;
        public double staminaTimer;

        public float Speed;
        public float SpeedMultiplier = 0;
        public float AbilityDamage;
        public float AbilityDamageMultiplier = 0;
        public float Scale;

        public Animator Animator;
        public CooldownManager CooldownManager;
        public AnimationManager animationManager;
        public bool overrideAnimation = false;
        public FrameHelper currFrame;
        public AnimationType currentAnimation;
        public AnimationType savedAnimaton;

        public bool canPerformAttack = false;
        public bool staminaSubtracted = false;

        private Vector2 minPosition, maxPosition;

        public Dictionary<AnimationType, EntityAction> AnimationToEntityAction = new Dictionary<AnimationType, EntityAction>();
        public Dictionary<AnimationType, Attack> Attacks = new Dictionary<AnimationType, Attack>();

        public Entity(EntityName name, Texture2D texture, Dictionary<AnimationType, EntityAction> animationToAbility)
        {
            Name = name;
            AnimationToEntityAction = animationToAbility;
            animationManager = new AnimationManager();
            CooldownManager = new CooldownManager();
            Animator = new Animator(this);
            //foreach (var item in AnimationToAbility)
            //{
            //    MaxAbilityCooldowns.Add(item.Key, animationToAbility[item.Key].Cooldown);
            //    AbilityCooldowns.Add(item.Key, 0);
            //}
        }

        public virtual void Update(AnimationType animation, Vector2 direction)
        {
            Direction = direction;
            Speed += Speed * SpeedMultiplier;
            Position = Vector2.Clamp(Position, minPosition, maxPosition);
            Animator.Update(animation);
            CurrentAction = Animator.CurrentAction;
            CooldownManager.Update();
            UpdateHitbox();
            UpdateWeapon();
            //animationManager.Update(currentAnimation, overrideAnimation);
            //HasFrameChanged = animationManager.CurrentAnimation.hasFrameChanged;
        }
        public virtual void Draw()
        {
            Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, WeaponHitBox, Color.Aqua);
            Animator.Draw();
            DrawShadow();
        }

        private void UpdateHitbox()
        {
            if (currentAnimation == AnimationType.Dodge || currentAnimation == AnimationType.UltimateDodge)
            {
                Dimentions = Vector2.Zero;
                HitBox = new Rectangle((int)TopLeft.X, (int)TopLeft.Y, (int)Dimentions.X, (int)Dimentions.Y);
            }
            else if (Animator.CurrentAnimation != null)
            {
                currFrame = Animator.CurrentAnimation.PreviousFrame;
                Dimentions.X = currFrame.CharacterHitbox.Width * Scale;
                Dimentions.Y = currFrame.CharacterHitbox.Height * Scale;
                HitBox = new Rectangle((int)TopLeft.X, (int)TopLeft.Y, (int)Dimentions.X, (int)Dimentions.Y);
            }
        }
        private void UpdateWeapon()
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
        private void DrawShadow()
        {
            if (currentAnimation == AnimationType.Dodge || currentAnimation == AnimationType.UltimateDodge)
            {
                Globals.SpriteBatch.Draw(ContentManager.Instance.Shadow, new Rectangle((int)Position.X - currFrame.SourceRectangle.Width / 2, ((int)Position.Y + currFrame.SourceRectangle.Height / 2) + 5, currFrame.SourceRectangle.Width, 10), new Color(255, 255, 255, 100));
            }
            else
            {
                Globals.SpriteBatch.Draw(ContentManager.Instance.Shadow, new Rectangle(HitBox.X, HitBox.Y + HitBox.Height - 2, HitBox.Width, 10), new Color(255, 255, 255, 100));
            }
        }
        public void Reset()
        {
            currentAnimation = AnimationType.None;
            savedAnimaton = AnimationType.None;
            RemainingHealth = TotalHealth;
            RemainingStamina = TotalStamina;
            CurrentAction = null;
            IsDead = false;
            overrideAnimation = false;
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

    }
}