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

        public AnimationBehaviour CurrentAction;
       // public Attack CurrentAttack;
        public Rectangle WeaponHitBox;
        private int weaponVerticalOffset;
        private int weaponHorizontalOffset;

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
        public float AbilityDamageMultiplier = 0;
        public float EntityScale;

        public Animator Animator;
        public CooldownManager CooldownManager;
        public bool overrideAnimation = false;
        public FrameHelper currFrame;

        private Vector2 minPosition, maxPosition;
        public Dictionary<AnimationType, AnimationBehaviour> AnimationToEntityAction = new Dictionary<AnimationType, AnimationBehaviour>();
        //public Dictionary<AnimationType, Attack> Attacks = new Dictionary<AnimationType, Attack>();
        public Entity(EntityName name, Texture2D texture, Dictionary<AnimationType, AnimationBehaviour> entityActions)
        {
            Name = name;
            AnimationToEntityAction = entityActions;
            CooldownManager = new CooldownManager();
            Animator = new Animator(this);
            Animator.AnimationBehaviours = ContentManager.Instance.EntityAnimationBehaviours[Name];
            foreach (var item in ContentManager.Instance.EntityAnimationBehaviours[Name])
            {
                Animator.AddAnimation(item.Key, item.Value.Animation);
                //Animator.AddAnimation(item.Value.AnimationType, item.Value.CanBeCanceled, ContentManager.Instance.EntitySpriteSheets[Name], item.Value.AnimationFrames, item.Value.AnimationSpeed);
                //if(item.Value is Attack)
                //{
                //    Attacks.Add(item.Key, (Attack)item.Value);
                //}
            }
        }

        public virtual void Update(AnimationType animation, Vector2 direction)
        {
            Direction = direction;
            Speed += Speed * SpeedMultiplier;
            Position = Vector2.Clamp(Position, minPosition, maxPosition);
            if (!Animator.Animations[animation].CanBeCanceled)
            {
                Animator.SetAnimation(animation);
            }
            Animator.Update();
            //CurrentAction = Animator.CurrentAction;
            //if(Attacks.ContainsKey(CurrentAction.AnimationType))
            //{
            //    CurrentAttack = Attacks[CurrentAction.AnimationType];
            //}
            CooldownManager.Update();
            UpdateHitbox();
            UpdateWeapon();
            HasFrameChanged = Animator.CurrentAnimation.hasFrameChanged;
        }
        public virtual void Draw()
        {
            Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, WeaponHitBox, Color.Aqua);
            Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, HitBox, Color.Red);
            Animator.Draw();
            DrawShadow();
        }

        private void UpdateHitbox()
        {
            if (Animator.CurrentAnimationType == AnimationType.Dodge || Animator.CurrentAnimationType == AnimationType.UltimateDodge)
            {
                Dimentions = Vector2.Zero;
                HitBox = new Rectangle((int)TopLeft.X, (int)TopLeft.Y, (int)Dimentions.X, (int)Dimentions.Y);
            }
            else if (Animator.CurrentAnimation != null)
            {
                currFrame = Animator.CurrentAnimation.PreviousFrame;
                Dimentions.X = currFrame.CharacterHitbox.Width * EntityScale;
                Dimentions.Y = currFrame.CharacterHitbox.Height * EntityScale;
                HitBox = new Rectangle((int)TopLeft.X, (int)TopLeft.Y, (int)Dimentions.X, (int)Dimentions.Y);
            }
        }
        private void UpdateWeapon()
        {
            (int, int) offsets = currFrame.GetWeaponHitboxOffsets();
            weaponVerticalOffset = offsets.Item2;
            weaponHorizontalOffset = offsets.Item1;
            WeaponHitBox.Width = (int)(currFrame.AttackHitbox.Width * EntityScale);
            WeaponHitBox.Height = (int)(currFrame.AttackHitbox.Height * EntityScale);
            if (IsFacingLeft)
            {
                WeaponHitBox.X = (int)(TopRight.X - WeaponHitBox.Width) - weaponHorizontalOffset;
            }
            else
            {
                WeaponHitBox.X = (int)(TopLeft.X + weaponHorizontalOffset);
            }
            WeaponHitBox.Y = (int)(TopLeft.Y + weaponVerticalOffset * EntityScale);
        }
        private void DrawShadow()
        {
            if (Animator.CurrentAnimationType == AnimationType.Dodge || Animator.CurrentAnimationType == AnimationType.UltimateDodge)
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