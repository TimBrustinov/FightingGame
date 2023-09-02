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

        public float UltimateMeterMax;
        public float RemainingUltimateMeter;
        private float ultimateFillRate;
        private float ultimateDrainRate;
        public Color MeterColor;

        public float HealthRegen;
        public float MaxOvershield;
        public float OvershieldBarWidth;
        public float Overshield;
        public float Coins;

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
        public Dictionary<PowerUpType, PowerUpScript> PowerUps = new Dictionary<PowerUpType, PowerUpScript>();
        public Character(EntityName name, int health, int baseDamage, float speed, float scale) : base(name) 
        {
            Rectangle characterRectangle = ContentManager.Instance.EntityTextures[name];
            EntityScale = scale;
            BaseDamage = baseDamage;
            Position = new Vector2(500, 350);
            Dimentions = new Vector2(characterRectangle.Width, characterRectangle.Height) * EntityScale;
            Speed = speed;
            TotalHealth = health;
            RemainingHealth = TotalHealth;
            UltimateMeterMax = 10;
            RemainingUltimateMeter = 0;
            ultimateFillRate = UltimateMeterMax / 5;
            ultimateDrainRate = UltimateMeterMax / 20f;
            MeterColor = Color.Gold;

            XP = 0;
            Level = 1;
            xpToLevelUp = 15;

            HealthRegen = 1;
            MaxOvershield = 0;
            Coins = 0;
            PowerUps.Add(PowerUpType.HealthRegenRateIncrease, new HealthRegenScript(PowerUpType.HealthRegenRateIncrease));
            //PowerUps.Add(PowerUpType.LifeSteal, new LifeStealScript(PowerUpType.LifeSteal));
            //PowerUps.Add(PowerUpType.Bleed, new BleedScript(PowerUpType.Bleed));

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
                }
            }
            else
            {
                // Increase RemainingUltimateMeter by ultimateFillRate per second
                RemainingUltimateMeter += ultimateFillRate * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
                RemainingUltimateMeter = MathHelper.Clamp(RemainingUltimateMeter, 0, UltimateMeterMax);
                MeterColor = Color.Gold;
                if (RemainingUltimateMeter < UltimateMeterMax && animation == AnimationType.UltimateTransform)
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

            

            if (XP >= xpToLevelUp)
            {
                LevelUp();
            }

            base.Update(animation, direction);
            foreach (var powerUp in PowerUps)
            {
                powerUp.Value.Update();
            }
        }

        public override void Draw()
        {
            base.Draw();
            DrawHealthBar(Globals.SpriteBatch);
        }
        public void DrawHealthBar(SpriteBatch spriteBatch)
        {
            int width = 75;
            int height = 10;

            float healthPercentage = RemainingHealth / TotalHealth; // Calculate the percentage of remaining health
            int foregroundWidth = (int)(healthPercentage * width); // Calculate the width of the foreground health bar
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(Position.X - width / 2, Position.Y - height * 5), new Rectangle(0, 0, foregroundWidth, 3), Color.Green); // Draw the foreground health bar
        }
        private void LevelUp()
        {
            Level++;
            xpToLevelUp *= 1.1f;
            XP = 0;
        }
        public void TakeDamage(float damage, Color damageColor)
        {
            HasBeenHit = true;
            if (Overshield > 0)
            {
                if (damage <= Overshield)
                {
                    Overshield -= damage;
                }
                else
                {
                    damage -= Overshield;
                    Overshield = 0;
                    RemainingHealth -= damage;
                    DamageNumberManager.Instance.AddDamageNumber(damage, Position - new Vector2(0, 40), damageColor);
                }
            }
            else
            {
                RemainingHealth -= damage;
                DamageNumberManager.Instance.AddDamageNumber(damage, Position - new Vector2(0, 40), damageColor);
            }
        }

        public void Reset()
        {
            PowerUps.Clear();
            Position = new Vector2(500, 350);
            PowerUps.Add(PowerUpType.HealthRegenRateIncrease, new HealthRegenScript(PowerUpType.HealthRegenRateIncrease));
        }

    }
}