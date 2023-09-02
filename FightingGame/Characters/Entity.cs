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

        public int NUM;
        public Rectangle WeaponHitBox;
        private int weaponVerticalOffset;
        private int weaponHorizontalOffset;

        public bool IsDead = false;
        public bool IsFacingLeft;
        public bool HasBeenHit;
        public bool HasFrameChanged; 
        public bool IsAttacking { get; set; }

        public EntityName Name;
        public float BaseDamage;
        public float TotalHealth;
        public float RemainingHealth;
        public double staminaTimer;
        public float Speed;
        public AttackBehaviour CurrentAbility;
        public float EntityScale;

        public Animator Animator;
        public AnimationType WantedAnimation;
        public CooldownManager CooldownManager;
        public bool overrideAnimation = false;
        public FrameHelper currFrame;

        private Vector2 minPosition, maxPosition;


        public Dictionary<AnimationType, AttackBehaviour> Attacks;

        public Entity(EntityName name)
        {
            Name = name;
            CooldownManager = new CooldownManager();
            Animator = new Animator(this);
            Attacks = new Dictionary<AnimationType, AttackBehaviour>();
            foreach (var animation in ContentManager.Instance.EntityAnimations[name])
            {
                Animator.AddAnimation(animation.Key, animation.Value.Texture, animation.Value.frameTime, animation.Value.AnimationFrames);
            }
            foreach (var behaviour in ContentManager.Instance.EntityAnimationBehaviours[name])
            {
                Animator.AnimationBehaviours.Add(behaviour.Key, behaviour.Value.Clone());
                if(behaviour.Value is AttackBehaviour)
                {
                    var attack = (AttackBehaviour)behaviour.Value;
                    Attacks.Add(attack.AnimationType, (AttackBehaviour)attack.Clone());
                    CooldownManager.AddCooldown(attack.AnimationType, attack.Cooldown);
                }
            }
            Animator.Animations[Animator.CurrentAnimationType].Start();
        }

        public virtual void Update(AnimationType animation, Vector2 direction)
        {
            Direction = direction;
            Position = Vector2.Clamp(Position, minPosition, maxPosition);
            WantedAnimation = animation;
            Animator.Update();
            CooldownManager.Update();
            UpdateHitbox();
            UpdateWeapon();
            HasFrameChanged = Animator.CurrentAnimation.hasFrameChanged;
        }
        public virtual void Draw()
        {
            //Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, WeaponHitBox, Color.Aqua);
            //Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, HitBox, Color.Red);
            Animator.Draw();
            DrawShadow();
        }
        public bool CheckTransition(AnimationType animation)
        {
            if(CooldownManager.AnimationCooldown.ContainsKey(animation))
            {
                if(CooldownManager.AnimationCooldown[animation] != 0)
                {
                    return false;
                }
            }
            return true;
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
      
        public void SetBounds(Rectangle mapSize)
        {
            minPosition = new Vector2(mapSize.X + Dimentions.X / 2, mapSize.Y + Dimentions.Y / 2);
            maxPosition = new Vector2(mapSize.Width - Dimentions.X / 2, mapSize.Height - Dimentions.Y / 2);
        }
       

    }
}