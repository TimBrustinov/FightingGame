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
    public class Character : Entity
    {
        public Character(EntityName name, Texture2D texture, int health, int speed, float animationSpeed, float scale, Dictionary<AnimationType, Ability> abilites) : base(name, texture, abilites, animationSpeed) 
        {
            Rectangle characterRectangle = ContentManager.Instance.EntityTextures[name];
            Scale = scale;
            Position = new Vector2(500, 350);
            Dimentions = new Vector2(characterRectangle.Width, characterRectangle.Height) * Scale;

            Speed = speed;
            TotalHealth = health;
            RemainingHealth = TotalHealth;

            //Rectangle characterRectangle = ContentManager.Instance.CharacterTextures[name];
            //    CharacterScale = 1.2f;
            //    Position = new Vector2(500, 350);
            //    Dimentions = new Vector2(characterRectangle.Width, characterRectangle.Height) * CharacterScale;

            //    speed = 3;
            //    TotalHealth = 10;
            //    RemainingHealth = TotalHealth;
        }
        public override void Update(AnimationType animation, Vector2 direction)
        {
            IsMovingLeft = InputManager.IsMovingLeft;
            base.Update(animation, direction);
            animationManager.Update(currentAnimation, overrideAnimation);
        }
        public override void Draw()
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