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
        public float XP;
        public int Level;

        private float xpToLevelUp;
        private float maxXpForCurrentLevel;

        private double staminaRegenInterval = 800;
        private double timer;

        public Character(EntityName name, Texture2D texture, int health, float speed, float scale, Dictionary<AnimationType, Ability> abilites) : base(name, texture, abilites) 
        {
            Rectangle characterRectangle = ContentManager.Instance.EntityTextures[name];
            Scale = scale;
            Position = new Vector2(500, 350);
            Dimentions = new Vector2(characterRectangle.Width, characterRectangle.Height) * Scale;

            Speed = speed;
            TotalHealth = health;
            RemainingHealth = TotalHealth;
            TotalStamina = 50;
            RemainingStamina = TotalStamina;

            XP = 0;
            Level = 1;
            xpToLevelUp = 100;
            maxXpForCurrentLevel = 100;
        }
        public override void Update(AnimationType animation, Vector2 direction)
        {
            IsFacingLeft = InputManager.IsMovingLeft;
            base.Update(animation, direction);
            if (XP >= xpToLevelUp)
            {
                LevelUp();
            }
            animationManager.Update(currentAnimation, overrideAnimation);
        }
        
        public override void Draw()
        {
            base.Draw();
            //Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, HitBox, Color.Red);
            DrawHealthBar(Globals.SpriteBatch);
            DrawStaminaBar();
        }
        public void DrawHealthBar(SpriteBatch spriteBatch)
        {
            int width = 75;
            int height = 10;
            float healthPercentage = (float)RemainingHealth / TotalHealth; // Calculate the percentage of remaining health
            int foregroundWidth = (int)(healthPercentage * width); // Calculate the width of the foreground health bar
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle((int)Position.X - width / 2, (int)Position.Y - height * 5, foregroundWidth, 3), Color.Green);
        }
        public void DrawStaminaBar()
        {
            int width = 75;
            int height = 10;
            timer += Globals.CurrentTime.ElapsedGameTime.TotalMilliseconds;

            if (timer >= staminaRegenInterval && RemainingStamina < TotalStamina)
            {
                RemainingStamina++;
            }

            if(RemainingStamina >= TotalStamina || staminaSubtracted)
            {
                timer = 0;
            }

            float healthPercentage = (float)RemainingStamina / TotalStamina; // Calculate the percentage of remaining health
            int foregroundWidth = (int)(healthPercentage * width); // Calculate the width of the foreground health bar

            Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle((int)Position.X - width / 2, (int)Position.Y - (height * 4) - 5, foregroundWidth, 3), Color.Gray);
        }
        private void LevelUp()
        {
            Level++;
            xpToLevelUp *= 1.25f;
            maxXpForCurrentLevel = xpToLevelUp;
            XP = 0; 
        }
    }
}