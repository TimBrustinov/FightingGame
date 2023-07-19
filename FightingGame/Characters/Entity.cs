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

        public Rectangle WeaponHitBox;
        private int weaponVerticalOffset;
        private int weaponHorizontalOffset;

        public bool IsActive = false;
        public bool IsDead = false;
        public bool IsMovingLeft;
        public EntityName Name;

        public int TotalHealth;
        public int RemainingHealth;
        public int Speed;
        public int NumOfHits = 1;
        public int AbilityDamage;
        public float Scale;

        public AnimationManager animationManager;
        public bool overrideAnimation = false;
        public FrameHelper currFrame;
        public AnimationType currentAnimation;
        public AnimationType savedAnimaton;

        public Dictionary<AnimationType, Ability> AniamtionToAbility = new Dictionary<AnimationType, Ability>();

        public Entity(EntityName name, Texture2D texture, Dictionary<AnimationType, Ability> animationToAbility, float animationSpeed)
        {
            Name = name;
            AniamtionToAbility = animationToAbility;
            animationManager = new AnimationManager();
            foreach (var animation in ContentManager.Instance.Animations[Name])
            {
                animationManager.AddAnimation(animation.Key.Item1, animation.Key.Item2, texture, animation.Value, animationSpeed);
            }
        }
        public virtual void Update(AnimationType animation, Vector2 direction)
        {
            currentAnimation = savedAnimaton != AnimationType.None ? savedAnimaton : animation;
            overrideAnimation = currentAnimation == AnimationType.Death ? true : false;

            if (AniamtionToAbility.ContainsKey(currentAnimation))
            {
                savedAnimaton = AniamtionToAbility[currentAnimation].Update(animationManager, currentAnimation, ref Position, direction, Speed);
                if(savedAnimaton == AnimationType.None)
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
            (int, int) offsets = currFrame.GetWeaponHitboxOffsets();
            setWeaponHitbox(offsets.Item1, offsets.Item2, new Vector2(currFrame.AttackHitbox.Width, currFrame.AttackHitbox.Height));
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
            animationManager.Draw(Position, IsMovingLeft, new Vector2(Scale, Scale), Color.White);
            //Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, Position, new Rectangle(0, 0, 5, 5), Color.Cyan);

        }
        private void UpdateWeapon()
        {
            if (savedAnimaton == AnimationType.BasicAttack || savedAnimaton == AnimationType.Ability1 || savedAnimaton == AnimationType.Ability2)
            {
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
        private void setWeaponHitbox(int horizontalOffset, int verticalOffset, Vector2 dimenions)
        {
            Vector2 weaponHitboxDimension = dimenions * Scale;
            weaponVerticalOffset = verticalOffset;
            weaponHorizontalOffset = horizontalOffset;
            //WeaponHitBox = new Rectangle((int)TopLeft.X + weaponHorizontalOffset, (int)TopLeft.Y + weaponVerticalOffset, (int)(dimenions.X * Scale), (int)(dimenions.Y * Scale));
            WeaponHitBox = new Rectangle((int)TopLeft.X + weaponHorizontalOffset, (int)TopLeft.Y + weaponVerticalOffset, (int)weaponHitboxDimension.X, (int)weaponHitboxDimension.Y);
        }
    }
}