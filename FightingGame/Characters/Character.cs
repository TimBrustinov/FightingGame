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

        public float xpToLevelUp;
        private float maxXpForCurrentLevel;

        public float UltimateMeterMax;
        public float RemainingUltimateMeter;
        private float ultimateFillRate;
        private float ultimateDrainRate;
        public Color MeterColor;

        private double staminaRegenInterval = 1500;
        

        public float healthRegenPerSecond; 
        private float timeElapsed = 0f;
        private float regenerationInterval; // Time interval for health regeneration in seconds

        public bool InUltimateForm = false;
        private Dictionary<AnimationType, AnimationType> UltimateAblities = new Dictionary<AnimationType, AnimationType>()
        {
            [AnimationType.UltimateTransform] = AnimationType.UltimateTransform,
            [AnimationType.UndoTransform] = AnimationType.UndoTransform,
            [AnimationType.BasicAttack] = AnimationType.UltimateBasicAttack,
            [AnimationType.Ability1] = AnimationType.UltimateAbility1,
            [AnimationType.Ability2] = AnimationType.UltimateAbility2,
            [AnimationType.Ability3] = AnimationType.UltimateAbility3,
            [AnimationType.Run] = AnimationType.UltimateRun,
            [AnimationType.Dodge] = AnimationType.UltimateDodge,
            [AnimationType.Stand] = AnimationType.UltimateStand,
        };

        public Character(EntityName name, Texture2D texture, int health, float speed, float scale, Dictionary<AnimationType, EntityAction> abilites) : base(name, texture, abilites) 
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

            UltimateMeterMax = 10;
            RemainingUltimateMeter = 0;
            ultimateFillRate = UltimateMeterMax / 60;
             ultimateDrainRate = UltimateMeterMax / 20f;
            MeterColor = Color.Gold;

            XP = 0;
            Level = 1;
            xpToLevelUp = 100;
            maxXpForCurrentLevel = 100;

            regenerationInterval = 8f;
            healthRegenPerSecond = 0.02f;
        }
        public override void Update(AnimationType animation, Vector2 direction)
        {
            IsFacingLeft = InputManager.IsMovingLeft;

            if (!InUltimateForm && animation == AnimationType.UndoTransform)
            {
                animation = Direction != Vector2.Zero ? AnimationType.Run : AnimationType.Stand;
            }

            if (InUltimateForm)
            {
                // Reduce RemainingUltimateMeter by ultimateDrainRate per second
                RemainingUltimateMeter -= ultimateDrainRate * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
                RemainingUltimateMeter = MathHelper.Clamp(RemainingUltimateMeter, 0, UltimateMeterMax);
                MeterColor = Color.Red;
                if (RemainingUltimateMeter <= 0)
                {
                    // Exit the ultimate form when the meter is fully drained
                    RemainingUltimateMeter = 0;
                    InUltimateForm = false;
                    animation = AnimationType.UndoTransform;
                    savedAnimaton = AnimationType.None;
                }
            }
            else
            {
                // Increase RemainingUltimateMeter by ultimateFillRate per second
                RemainingUltimateMeter += ultimateFillRate * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
                RemainingUltimateMeter = MathHelper.Clamp(RemainingUltimateMeter, 0, UltimateMeterMax);
                MeterColor = Color.Gold;
                if (RemainingUltimateMeter >= UltimateMeterMax && animation == AnimationType.UltimateTransform)
                {
                    InUltimateForm = true;
                }
                else if (RemainingUltimateMeter < UltimateMeterMax && animation == AnimationType.UltimateTransform)
                {
                    animation = Direction != Vector2.Zero ? AnimationType.Run : AnimationType.Stand;
                }
            }


            if (InUltimateForm)
            {
                animation = UltimateAblities[animation];
            }

            if (animation == AnimationType.UndoTransform && InUltimateForm)
            {
                InUltimateForm = false;
                RemainingUltimateMeter = 0;
            }

            if (CooldownManager.AnimationCooldown[animation] != 0)
            {
                if(direction != Vector2.Zero)
                {
                    animation = InUltimateForm ? AnimationType.UltimateRun : AnimationType.Run;
                }
                else
                {
                    animation = InUltimateForm ?AnimationType.UltimateStand : AnimationType.Stand;
                }
            }
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
            DrawHealthBar(Globals.SpriteBatch);
            DrawStaminaBar();
        }
        

        public void DrawHealthBar(SpriteBatch spriteBatch)
        {
            int width = 75;
            int height = 10;

            // Calculate health regeneration amount based on time elapsed and regeneration rate
            if (RemainingHealth < TotalHealth)
            {
                timeElapsed += (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
                if (timeElapsed >= regenerationInterval)
                {
                    int healthToRegen = (int)(TotalHealth * healthRegenPerSecond);
                    RemainingHealth = Math.Min(TotalHealth, RemainingHealth + healthToRegen);
                    timeElapsed -= regenerationInterval;
                }
            }

            float healthPercentage = RemainingHealth / TotalHealth; // Calculate the percentage of remaining health
            int foregroundWidth = (int)(healthPercentage * width); // Calculate the width of the foreground health bar

            //spriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle((int)Position.X - width / 2, (int)Position.Y - height * 5, width, 3), Color.Red); // Draw the background health bar
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(Position.X - width / 2, Position.Y - height * 5), new Rectangle(0, 0, foregroundWidth, 3), Color.Green); // Draw the foreground health bar
        }
        public void DrawStaminaBar()
        {
            int width = 75;
            int height = 10;
            staminaTimer += Globals.GameTime.ElapsedGameTime.TotalMilliseconds;

            if (staminaTimer >= staminaRegenInterval && RemainingStamina < TotalStamina)
            {
                RemainingStamina++;
            }

            if(RemainingStamina >= TotalStamina)
            {
                staminaTimer = 0;
            }

            float healthPercentage = (float)RemainingStamina / TotalStamina; // Calculate the percentage of remaining health
            int foregroundWidth = (int)(healthPercentage * width); // Calculate the width of the foreground health bar
            Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(Position.X - width / 2, Position.Y - (height * 4) - 5), new Rectangle(0, 0, foregroundWidth, 3), Color.Gray);
        }
        private void LevelUp()
        {
            Level++;
            xpToLevelUp *= 1.25f;
            maxXpForCurrentLevel = xpToLevelUp;
            XP = 0; 
        }
      
        private void HandleUltimateForm(AnimationType animation)
        {
           

        }
    }
}