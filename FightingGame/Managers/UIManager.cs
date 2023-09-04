using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FightingGame
{
    public class UIManager
    {
        private Character character;
        private SpriteBatch SpriteBatch => Globals.SpriteBatch;

        private Camera Camera;
        private Vector2 TopLeft => Camera.Corner;
        private Vector2 TopRight => Camera.Corner + new Vector2(Camera.CameraView.Width, 0);
        private Vector2 BottomLeft => Camera.Corner + new Vector2(0, Camera.CameraView.Height);
        private Vector2 BottomRight => Camera.Corner + new Vector2(Camera.CameraView.Width, Camera.CameraView.Height);

        private Vector2 HealthBarBackgroundPosition;
        private Point HealthbarBackgroundDimensions;
        private Vector2 healthBarPosition;
        private Point healthBarDimensions;

        private Vector2 xpBarPosition;
        private Point xpBarDimensions;

        private Vector2 abilityPosition;
        private Point abilityDimensions;
        private int abilityOffset;

        private Vector2 ultimateBarPosition;
        private Point ultimateBarDimensions;

        private Vector2 powerUpBackgroundPosition;
        private Point powerUpBackgroundDimensions;

        private Vector2 moneyBackgroundPosition;
        private Point moneyBackgroundDimensions;

        public UIManager(Character character, Camera camera)
        {
            this.character = character;
            Camera = camera;
            SetUI();
        }

        private void SetUI()
        {

            HealthbarBackgroundDimensions = new Point(340, 70);
            healthBarDimensions = new Point(300, 20);
            xpBarDimensions = new Point(200, 10);
            abilityDimensions = new Point(50, 50);
            abilityOffset = 5;
            ultimateBarDimensions = new Point(4 * (abilityDimensions.X + abilityOffset) - abilityOffset, 10);
            powerUpBackgroundDimensions = new Point(800, 70);
            moneyBackgroundDimensions = new Point(100, 20);
        }
        public void Update()
        {
            HealthBarBackgroundPosition = new Vector2(BottomLeft.X + 130, BottomLeft.Y - 95);
            healthBarPosition = new Vector2(HealthBarBackgroundPosition.X + 20, HealthBarBackgroundPosition.Y + 35);
            xpBarPosition = new Vector2(healthBarPosition.X + (healthBarDimensions.X - xpBarDimensions.X), healthBarPosition.Y - xpBarDimensions.Y - 8);
            abilityPosition = new Vector2(BottomRight.X - 350, BottomRight.Y - 75);
            ultimateBarPosition = new Vector2(abilityPosition.X, abilityPosition.Y - (ultimateBarDimensions.Y + 5));
            powerUpBackgroundPosition = new Vector2(HealthBarBackgroundPosition.X + HealthbarBackgroundDimensions.X + 150, BottomLeft.Y - 95);
            moneyBackgroundPosition = new Vector2(TopLeft.X + 130, TopLeft.Y + 955);
        }
        public void Draw()
        {
            bottomLeftUIBackground();
            healthBar();
            powerUps();
            overShield();
            xpBar();
            abilities();
            ultimateBar();
            money();
        }
        private void bottomLeftUIBackground()
        {
            SpriteBatch.Draw(ContentManager.Instance.Pixel, HealthBarBackgroundPosition - Vector2.One, new Rectangle(0, 0, HealthbarBackgroundDimensions.X + 2, HealthbarBackgroundDimensions.Y + 2), Color.White);
            SpriteBatch.Draw(ContentManager.Instance.Pixel, HealthBarBackgroundPosition, new Rectangle(0, 0, HealthbarBackgroundDimensions.X, HealthbarBackgroundDimensions.Y), new Color(30, 30, 30, 255));
        }
        private void healthBar()
        {
            float healthPercentage = character.RemainingHealth / character.TotalHealth; 
            int foregroundWidth = (int)(healthPercentage * healthBarDimensions.X); 
            SpriteBatch.Draw(ContentManager.Instance.Pixel, healthBarPosition, new Rectangle(0, 0, healthBarDimensions.X, healthBarDimensions.Y), new Color(30, 30, 30, 200));
            SpriteBatch.Draw(ContentManager.Instance.Pixel, healthBarPosition, new Rectangle(0, 0, foregroundWidth, healthBarDimensions.Y), new Color(105, 187, 60));
            string text = $"{character.RemainingHealth} / {character.TotalHealth}";
            drawText(new Vector2(healthBarPosition.X + healthBarDimensions.X / 2 - ContentManager.Instance.Font.MeasureString(text).X / 2, healthBarPosition.Y + 3), text);
        }
        private void xpBar()
        {
            float xpPercentage = character.XP / character.xpToLevelUp; 
            int foregroundWidth = (int)(xpPercentage * xpBarDimensions.X);
            SpriteBatch.Draw(ContentManager.Instance.Pixel, xpBarPosition, new Rectangle(0, 0, xpBarDimensions.X, xpBarDimensions.Y), new Color(30, 30, 30, 200));
            SpriteBatch.Draw(ContentManager.Instance.Pixel, xpBarPosition, new Rectangle(0, 0, foregroundWidth, xpBarDimensions.Y), new Color(178, 102, 255));
            string text = $"Lv:{character.Level}";
            drawText(new Vector2(xpBarPosition.X - 50, xpBarPosition.Y - 5), text);
        }
        private void ultimateBar()
        {
            float ultimatePercentage = character.RemainingUltimateMeter / character.UltimateMeterMax;
            int foregroundWidth = (int)(ultimatePercentage * ultimateBarDimensions.X);
            SpriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(ultimateBarPosition.X - 1, ultimateBarPosition.Y - 1), new Rectangle(0, 0, ultimateBarDimensions.X + 2, ultimateBarDimensions.Y + 2), Color.White);
            SpriteBatch.Draw(ContentManager.Instance.Pixel, ultimateBarPosition, new Rectangle(0, 0, ultimateBarDimensions.X, ultimateBarDimensions.Y), new Color(30, 30, 30, 255));
            SpriteBatch.Draw(ContentManager.Instance.Pixel, ultimateBarPosition, new Rectangle(0, 0, foregroundWidth, ultimateBarDimensions.Y), Color.Gold);
        }
        private void overShield()
        {
            float overShieldPercentage = character.Overshield / character.MaxOvershield;
            int foregroundWidth = (int)(overShieldPercentage * character.OvershieldBarWidth);
            SpriteBatch.Draw(ContentManager.Instance.Pixel, healthBarPosition, new Rectangle(0, 0, foregroundWidth, healthBarDimensions.Y), new Color(153, 255, 255));
        }
        private void abilities()
        {
            int i = 0;
            if(character.InUltimateForm)
            {
                foreach (var icon in ContentManager.Instance.CharacterAbilityIcons[character.Name])
                {
                    if(icon.Key != AnimationType.Ability1 && icon.Key != AnimationType.Ability2 && icon.Key != AnimationType.Ability3 && icon.Key != AnimationType.Dodge)
                    {
                        Vector2 position = new Vector2(abilityPosition.X + i * (abilityDimensions.X + abilityOffset), abilityPosition.Y);
                        iconBackground(position);
                        SpriteBatch.Draw(icon.Value.Texture, position, icon.Value.SourceRectangle, Color.White, 0, Vector2.Zero, icon.Value.Scale, SpriteEffects.None, 0);
                        
                        if (character.CooldownManager.AnimationCooldown.ContainsKey(icon.Key))
                        {
                            float cooldownPercentage = (float)character.CooldownManager.AnimationCooldown[icon.Key] / character.CooldownManager.MaxAnimationCooldown[icon.Key]; 
                            int foregroundHeight = (int)(cooldownPercentage * abilityDimensions.X); 
                            SpriteBatch.Draw(ContentManager.Instance.Pixel, position, new Rectangle(0, 0, 50, foregroundHeight), new Color(75, 75, 75, 0));
                        }
                        i++;
                    }
                }
            }
            else
            {
                foreach (var icon in ContentManager.Instance.CharacterAbilityIcons[character.Name])
                {
                    if (icon.Key != AnimationType.UltimateAbility1 && icon.Key != AnimationType.UltimateAbility2 && icon.Key != AnimationType.UltimateAbility3 && icon.Key != AnimationType.UltimateDodge)
                    {
                        Vector2 position = new Vector2(abilityPosition.X + i * (abilityDimensions.X + abilityOffset), abilityPosition.Y);
                        iconBackground(position);
                        SpriteBatch.Draw(icon.Value.Texture, position, icon.Value.SourceRectangle, Color.White, 0, Vector2.Zero, icon.Value.Scale, SpriteEffects.None, 0);
                        if (character.CooldownManager.AnimationCooldown.ContainsKey(icon.Key))
                        {
                            float cooldownPercentage = (float)character.CooldownManager.AnimationCooldown[icon.Key] / character.CooldownManager.MaxAnimationCooldown[icon.Key]; 
                            int foregroundHeight = (int)(cooldownPercentage * abilityDimensions.X); 
                            SpriteBatch.Draw(ContentManager.Instance.Pixel, position, new Rectangle(0, 0, 50, foregroundHeight), new Color(75, 75, 75, 0));
                        }
                        i++;
                    }
                }
            }
        }
        private void iconBackground(Vector2 position)
        {
            SpriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(position.X - 1, position.Y - 1), new Rectangle(0, 0, abilityDimensions.X + 2, abilityDimensions.Y + 2), Color.White);
            SpriteBatch.Draw(ContentManager.Instance.Pixel, position, new Rectangle(0, 0, abilityDimensions.X, abilityDimensions.Y), new Color(30, 30, 30, 255));
        }
        private void powerUps()
        {
            SpriteBatch.Draw(ContentManager.Instance.Pixel, powerUpBackgroundPosition - Vector2.One, new Rectangle(0, 0, powerUpBackgroundDimensions.X + 2, powerUpBackgroundDimensions.Y + 2), Color.White);
            SpriteBatch.Draw(ContentManager.Instance.Pixel, powerUpBackgroundPosition, new Rectangle(0, 0, powerUpBackgroundDimensions.X, powerUpBackgroundDimensions.Y), new Color(30, 30, 30, 255));
            int i = 0;
            foreach (var item in PowerUps.Instance.PowerUpIcons)
            {
                Icon icon = item.Value;
                Vector2 position = new Vector2(powerUpBackgroundPosition.X + i * 35, powerUpBackgroundPosition.Y + 5);
                SpriteBatch.Draw(icon.Texture, position, icon.SourceRectangle, Color.White, 0, Vector2.Zero, icon.Scale, SpriteEffects.None, 0);
                if(icon.Ammount > 1)
                {
                    drawText(new Vector2(position.X + icon.Dimensions.X - 10, position.Y + icon.Dimensions.Y - 5), $"x{icon.Ammount}");
                }
                i++;
            }
        }
        private void money()
        {
            var coin = ContentManager.Instance.EnemyDrops[IconType.Coin];
            SpriteBatch.Draw(ContentManager.Instance.Pixel, moneyBackgroundPosition - Vector2.One, new Rectangle(0, 0, moneyBackgroundDimensions.X + 2, moneyBackgroundDimensions.Y + 2), Color.White);
            SpriteBatch.Draw(ContentManager.Instance.Pixel, moneyBackgroundPosition, new Rectangle(0, 0, moneyBackgroundDimensions.X, moneyBackgroundDimensions.Y), new Color(30, 30, 30, 255));
            SpriteBatch.Draw(coin.Icon.Texture, moneyBackgroundPosition, coin.Icon.SourceRectangle, Color.White, 0, Vector2.Zero, coin.Icon.Scale, SpriteEffects.None, 0);
            string coinsText = $"{GameObjects.Instance.SelectedCharacter.Coins}";
            Vector2 textDimensions = ContentManager.Instance.Font.MeasureString(coinsText);
            Vector2 adjustedPosition = moneyBackgroundPosition + new Vector2(90 - textDimensions.X, 2);
            drawText(adjustedPosition, coinsText);
        }
        private void drawText(Vector2 position, string text)
        { 
            SpriteBatch.DrawString(ContentManager.Instance.Font, text, position, Color.White);
        }
        
    }
}
