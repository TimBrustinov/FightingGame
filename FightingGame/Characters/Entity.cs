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
    //abstract class Ability
    //{
    //    protected abstract void DoStuffBody(Character me);
    //    public void DoStuff(Character me)
    //    {
    //        //beginning code
    //        DoStuffBody(me);
    //        //ending code
    //    }
    //}

    //class Attack : Ability
    //{
    //    int Damage;
    //    protected override void DoStuffBody(Character me)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public abstract class Entity
    {
        public Rectangle EntityHitBox;
        public Vector2 Position;
        public Vector2 TopLeft => Position - Dimentions / 2;
        public Vector2 TopRight => Position + Dimentions / 2;
        public Vector2 Dimentions;
        public Vector2 Direction;

        public Rectangle WeaponHitBox;
        private int weaponVerticalOffset;
        private int weaponHorizontalOffset;

        public bool IsActive = false;
        public EntityName Name;

        public int TotalHealth;
        public int RemainingHealth;
        public int speed;
        public int NumOfHits = 2;
        public float Scale;
        public float animationSpeed = 0.1f;

        
        public AnimationManager animationManager;
        public bool overrideAnimation = false;
        public FrameHelper currFrame;
        public AnimationType currentAnimation;
        public AnimationType savedAnimaton;

        Dictionary<AnimationType, Ability> AniamtionToAbility = new Dictionary<AnimationType, Ability>();

        public Entity(EntityName name, Texture2D texture)
        {
            Name = name;
            animationManager = new AnimationManager();
            foreach (var animation in ContentManager.Instance.Animations[Name])
            {
                animationManager.AddAnimation(animation.Key.Item1, animation.Key.Item2, texture, animation.Value, animationSpeed);
            }
        }
        public virtual void Update(AnimationType animation, Vector2 direction)
        {
            Direction = direction;

            if (savedAnimaton != AnimationType.None)
            {
                currentAnimation = savedAnimaton;
            }
            else
            {
                currentAnimation = animation;
            }

            AniamtionToAbility[currentAnimation].DoStuff();

            //switch (currentAnimation)
            //{
            //    case AnimationType.Death:
            //        Death();
            //        break;
            //    case AnimationType.Dodge:
            //        Dodge();
            //        break;
            //    case AnimationType.BasicAttack:
            //        BasicAttack();
            //        break;
            //    case AnimationType.Ability1:
            //        Ability1();
            //        break;
            //    case AnimationType.Ability2:
            //        Ability2();
            //        break;
            //    default:
            //        if (direction != Vector2.Zero)
            //        {
            //            Position += Vector2.Normalize(InputManager.Direction) * speed;
            //        }
            //        else
            //        {
            //            currentAnimation = AnimationType.Stand;
            //        }
            //        break;
            //}

            UpdateHitbox();
            UpdateWeapon();
            animationManager.Update(currentAnimation, overrideAnimation);
        }
        private void UpdateHitbox()
        {
            if (currentAnimation == AnimationType.Dodge)
            {
                Dimentions = Vector2.Zero;
                EntityHitBox = new Rectangle((int)TopLeft.X, (int)TopLeft.Y, (int)Dimentions.X, (int)Dimentions.Y);
            }
            else if (animationManager.CurrentAnimation != null)
            {
                currFrame = animationManager.CurrentAnimation.PreviousFrame;
                Dimentions.X = currFrame.CharacterHitbox.Width * Scale;
                Dimentions.Y = currFrame.CharacterHitbox.Height * Scale;
                EntityHitBox = new Rectangle((int)TopLeft.X, (int)TopLeft.Y, (int)Dimentions.X, (int)Dimentions.Y);
            }
        }
        public void Draw()
        {
            //Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, EntityHitBox, Color.Red);
            //Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, Position, Color.Blue);

            //if (savedAnimaton == AnimationType.BasicAttack || savedAnimaton == AnimationType.Ability1 || savedAnimaton == AnimationType.Ability2)
            //{
            //    Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, WeaponHitBox, Color.Aqua);
            //}
            animationManager.Draw(Position, InputManager.IsMovingLeft, new Vector2(Scale, Scale), Color.White);
            //DrawHealthBar(Globals.SpriteBatch);
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
            weaponVerticalOffset = verticalOffset;
            weaponHorizontalOffset = horizontalOffset;
            WeaponHitBox = new Rectangle((int)TopLeft.X + weaponHorizontalOffset, (int)TopLeft.Y + weaponVerticalOffset, (int)(dimenions.X * Scale), (int)(dimenions.Y * Scale));
            //WeaponHitBox = new Rectangle((int)TopLeft.X + weaponHorizontalOffset, (int)TopLeft.Y + weaponVerticalOffset, (int)swordHitBoxDimentions.X, (int)swordHitBoxDimentions.Y);
        }
    }
}