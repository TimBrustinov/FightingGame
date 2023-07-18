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
    abstract class Ability 
    {
        protected abstract void DoStuffBody(Character me);
        public void DoStuff(Character me)
        {
            //beginning code
            DoStuffBody(me);
            //ending code
        }
    }

    class Attack : Ability
    {
        int Damage;
        protected override void DoStuffBody(Character me)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class Character : Entity
    {
        public Character(EntityName name, Texture2D texture) : base(name, texture) { }
        //protected abstract void BasicAttack();
        //protected abstract void Ability1();
        //protected abstract void Ability2();
        //protected abstract void Ability3();
        //protected abstract void Dodge();
        //protected abstract void UpdateWeapon();
        //protected abstract void Death();
        public override void Update(AnimationType animation, Vector2 direction)
        {
            //bool canAnimationChange = CanAnimationChange(animation);
            base.Update(animation, direction);

            //AniamtionToAbility[currentAnimation].DoStuff();
            //abilityMap[currentAnimation].DoStuff();

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
        public void Draw()
        {
            base.Draw();
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