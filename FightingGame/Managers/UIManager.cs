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
        private Dictionary<AnimationType, Rectangle> AbilityIcons;
        private Camera Camera;
        private Vector2 TopLeft => Camera.Corner;
        private Vector2 TopRight => Camera.Corner + new Vector2(Camera.CameraView.Width, 0);
        private Vector2 BottomLeft => Camera.Corner + new Vector2(0, Camera.CameraView.Height);
        private Vector2 BottomRight => Camera.Corner + new Vector2(Camera.CameraView.Width, Camera.CameraView.Height);

        private Vector2 healthBarPosition;
        private Point healthBarDimentions;

        private Vector2 xpBarPosition;
        private Point xpBarDimentions;

        private Vector2 abilityPosition;
        private Point abilityDimentions;
        private int abilityOffset;

        //private Vector2 iconPosition;
        //private Vector2 xpBarPosition;
        //private Vector2 healthBarPosition;
        //private Vector2 staminaBarPosition;
        //private Vector2 ultimateMeterPosition;


        public UIManager(Character character, Camera camera)
        {
            this.character = character;
            AbilityIcons = ContentManager.Instance.CharacterAbilityIcons[character.Name];
            Camera = camera;
            SetUI();
        }

        private void SetUI()
        {
            healthBarPosition = new Vector2(BottomLeft.X + 150, BottomLeft.Y - 50);
            healthBarDimentions = new Point(300, 20);

            xpBarDimentions = new Point(200, 10);
            xpBarPosition = new Vector2(healthBarPosition.X + (healthBarDimentions.X - xpBarDimentions.X), healthBarPosition.Y + xpBarDimentions.Y + 5);

            abilityDimentions = new Point(50, 50);
            //abilityPosition = new Vector2(BottomRight.X - 120, BottomRight.Y - 70);
            abilityOffset = 5;
        }
        public void Update()
        {
            healthBarPosition = new Vector2(BottomLeft.X + 150, BottomLeft.Y - 50);
            xpBarPosition = new Vector2(healthBarPosition.X + (healthBarDimentions.X - xpBarDimentions.X), healthBarPosition.Y - xpBarDimentions.Y - 5);
            abilityPosition = new Vector2(BottomRight.X - 400, BottomRight.Y - 80);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 cameraCorner)
        {
            //DrawHealthBar(spriteBatch, cameraCorner);
            //DrawXpBar(spriteBatch, cameraCorner);
            //DrawStaminaBar(spriteBatch, cameraCorner); 
            //DrawIcons(spriteBatch, cameraCorner);
            //DrawUltimateMeter(spriteBatch, cameraCorner);
            healthBar();
            xpBar();
            abilities();
        }
        //private void DrawXpBar(SpriteBatch spriteBatch, Vector2 cameraCorner)
        //{
        //    Vector2 xpBarPosition = new Vector2(cameraCorner.X, cameraCorner.Y);
        //    spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(xpBarPosition.X, xpBarPosition.Y + 5), new Rectangle(0, 0, Camera.CameraView.Width, 20), new Color(30, 30, 30, 255));
        //    float xpPercentage = character.XP / character.xpToLevelUp; // Calculate the percentage of XP progress
        //    int foregroundWidth = (int)(xpPercentage * Camera.CameraView.Width); // Calculate the width of the foreground XP bar
        //    spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(xpBarPosition.X + 5, xpBarPosition.Y + 7), new Rectangle(0, 0, foregroundWidth, 15), Color.MediumPurple);

        //    // Draw XP progress text
        //    string xpText = $"{character.XP} / {(int)character.xpToLevelUp}";
        //    Vector2 xpTextPosition = new Vector2(xpBarPosition.X + Camera.CameraView.Width / 2 - ContentManager.Instance.Font.MeasureString(xpText).X / 2, xpBarPosition.Y + 7);
        //    spriteBatch.DrawString(ContentManager.Instance.Font, xpText, xpTextPosition, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        //}
        private void healthBar()
        {
            float healthPercentage = character.RemainingHealth / character.TotalHealth; 
            int foregroundWidth = (int)(healthPercentage * healthBarDimentions.X); 
            SpriteBatch.Draw(ContentManager.Instance.Pixel, healthBarPosition, new Rectangle(0, 0, healthBarDimentions.X, healthBarDimentions.Y), new Color(30, 30, 30, 200));
            SpriteBatch.Draw(ContentManager.Instance.Pixel, healthBarPosition, new Rectangle(0, 0, foregroundWidth, healthBarDimentions.Y), new Color(105, 187, 60));
            string text = $"{character.RemainingHealth} / {character.TotalHealth}";
            DrawText(new Vector2(healthBarPosition.X + healthBarDimentions.X / 2 - ContentManager.Instance.Font.MeasureString(text).X / 2, healthBarPosition.Y + 3), text);
        }

        private void xpBar()
        {
            float xpPercentage = character.XP / character.xpToLevelUp; 
            int foregroundWidth = (int)(xpPercentage * xpBarDimentions.X);
            SpriteBatch.Draw(ContentManager.Instance.Pixel, xpBarPosition, new Rectangle(0, 0, xpBarDimentions.X, xpBarDimentions.Y), new Color(30, 30, 30, 200));
            SpriteBatch.Draw(ContentManager.Instance.Pixel, xpBarPosition, new Rectangle(0, 0, foregroundWidth, xpBarDimentions.Y), new Color(138, 43, 226));
            string text = $"Lv: {character.Level}";
            DrawText(new Vector2(xpBarPosition.X - 50, xpBarPosition.Y - 5), text);
        }

        public void abilities()
        {
            int i = 0;
            if(character.InUltimateForm)
            {
                foreach (var icon in ContentManager.Instance.CharacterAbilityIcons[character.Name])
                {
                    if(icon.Key != AnimationType.Ability1 && icon.Key != AnimationType.Ability2 && icon.Key != AnimationType.Ability3 && icon.Key != AnimationType.Dodge)
                    {
                        Vector2 position = new Vector2(abilityPosition.X + i * (abilityDimentions.X + abilityOffset), abilityPosition.Y);
                        SpriteBatch.Draw(ContentManager.Instance.EntitySpriteSheets[character.Name], position, icon.Value, Color.White);
                        if (character.CooldownManager.AnimationCooldown.ContainsKey(icon.Key))
                        {
                            float cooldownPercentage = (float)character.CooldownManager.AnimationCooldown[icon.Key] / character.CooldownManager.MaxAnimationCooldown[icon.Key]; 
                            int foregroundHeight = (int)(cooldownPercentage * abilityDimentions.X); 
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
                        Vector2 position = new Vector2(abilityPosition.X + i * (abilityDimentions.X + abilityOffset), abilityPosition.Y);
                        DrawIconBackground(position);
                        SpriteBatch.Draw(ContentManager.Instance.EntitySpriteSheets[character.Name], position, icon.Value, Color.White);
                        if (character.CooldownManager.AnimationCooldown.ContainsKey(icon.Key))
                        {
                            float cooldownPercentage = (float)character.CooldownManager.AnimationCooldown[icon.Key] / character.CooldownManager.MaxAnimationCooldown[icon.Key]; 
                            int foregroundHeight = (int)(cooldownPercentage * abilityDimentions.X); 
                            SpriteBatch.Draw(ContentManager.Instance.Pixel, position, new Rectangle(0, 0, 50, foregroundHeight), new Color(75, 75, 75, 0));
                        }
                        i++;
                    }
                }
            }
        }

        private void DrawIconBackground(Vector2 position)
        {
            SpriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(position.X - 1, position.Y - 1), new Rectangle(0, 0, abilityDimentions.X + 2, abilityDimentions.Y + 2), Color.White);
            SpriteBatch.Draw(ContentManager.Instance.Pixel, position, new Rectangle(0, 0, abilityDimentions.X, abilityDimentions.Y), new Color(30, 30, 30, 255));
        }

      
        private void DrawText(Vector2 position, string text)
        {
            SpriteBatch.DrawString(ContentManager.Instance.Font, text, position, Color.White);
        }
        //private void xpBar()
        //{

        //}

        //private void DrawIcons(SpriteBatch spriteBatch, Vector2 cameraCorner)
        //{
        //    int i = 0;
        //    iconPosition = new Vector2(cameraCorner.X + Camera.Viewport.Width / 2 + 15, cameraCorner.Y + Camera.Viewport.Height - 10);
        //    //spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(cameraCorner.X + Camera.Viewport.Width / 2 - offset * 3, cameraCorner.Y + Camera.Viewport.Height - offset * 2), new Rectangle(0, 0, offset * 3, offset), new Color(30, 30, 30, 255));
        //    spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(iconPosition.X - 137, iconPosition.Y - 58), new Rectangle(0, 0, 235, 60), new Color(20, 20, 20, 210));
        //    Vector2 dimentions = new Vector2(Portraits[CharacterPortrait.HashashinBase].Width * portraitScale, Portraits[CharacterPortrait.HashashinBase].Height * portraitScale);

        //    if (!character.InUltimateForm)
        //    {
        //        //Draws the portrait 
        //        spriteBatch.Draw(Portraits[CharacterPortrait.HashashinBase], new Vector2(iconPosition.X - 130, iconPosition.Y - offset + 4), default, Color.White, 0f, Vector2.Zero, portraitScale, SpriteEffects.None, 1f);
        //        foreach (var item in AbilityIcons)
        //        {
        //            if (item.Key != AnimationType.UltimateAbility1 && item.Key != AnimationType.UltimateAbility2 && item.Key != AnimationType.UltimateAbility3)
        //            {
        //                //Draws the ability Icon
        //                spriteBatch.Draw(ContentManager.Instance.EntitySpriteSheets[character.Name], new Vector2(iconPosition.X - 75 + i * offset, iconPosition.Y - offset), AbilityIcons[item.Key], Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        //                //Draws the cooldown for the ability
        //                if(character.CooldownManager.AnimationCooldown.ContainsKey(item.Key))
        //                {
        //                    float cooldownPercentage = (float)character.CooldownManager.AnimationCooldown[item.Key] / character.CooldownManager.MaxAnimationCooldown[item.Key]; // Calculate the percentage of remaining cooldown
        //                    int foregroundHeight = (int)(cooldownPercentage * 50); // Calculate the height of the foreground cooldown bar
        //                    Vector2 cooldownPosition = new Vector2(iconPosition.X - 75 + i * offset, iconPosition.Y - offset) + new Vector2(0, 50 - foregroundHeight);
        //                    spriteBatch.Draw(ContentManager.Instance.Pixel, cooldownPosition, new Rectangle(0, 0, 50, foregroundHeight), new Color(75, 75, 75, 0), 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        //                }
        //                i++;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //Draws the portrait 
        //        spriteBatch.Draw(Portraits[CharacterPortrait.HashashinElemental], new Vector2(iconPosition.X - 130, iconPosition.Y - offset + 4), default, Color.White, 0f, Vector2.Zero, portraitScale, SpriteEffects.None, 1f);

        //        foreach (var item in AbilityIcons)
        //        {
        //            if (item.Key == AnimationType.UltimateAbility1 || item.Key == AnimationType.UltimateAbility2 || item.Key == AnimationType.UltimateAbility3)
        //            {
        //                spriteBatch.Draw(ContentManager.Instance.EntitySpriteSheets[character.Name], new Vector2(iconPosition.X - 75 + i * offset, iconPosition.Y - offset), AbilityIcons[item.Key], Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        //                //Draws the cooldown for the ability
        //                if (character.CooldownManager.AnimationCooldown.ContainsKey(item.Key))
        //                {
        //                    float cooldownPercentage = (float)character.CooldownManager.AnimationCooldown[item.Key] / character.CooldownManager.MaxAnimationCooldown[item.Key]; // Calculate the percentage of remaining cooldown
        //                    int foregroundHeight = (int)(cooldownPercentage * 50); // Calculate the height of the foreground cooldown bar
        //                    Vector2 cooldownPosition = new Vector2(iconPosition.X - 75 + i * offset, iconPosition.Y - offset) + new Vector2(0, 50 - foregroundHeight);
        //                    spriteBatch.Draw(ContentManager.Instance.Pixel, cooldownPosition, new Rectangle(0, 0, 50, foregroundHeight), new Color(75, 75, 75, 0), 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        //                }
        //                i++;
        //            }
        //        }
        //    }
        //}
        //private void DrawXpBar(SpriteBatch spriteBatch, Vector2 cameraCorner)
        //{
        //    Vector2 xpBarPosition = new Vector2(cameraCorner.X, cameraCorner.Y);
        //    spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(xpBarPosition.X, xpBarPosition.Y + 5), new Rectangle(0, 0, Camera.CameraView.Width, 20), new Color(30, 30, 30, 255));
        //    float xpPercentage = character.XP / character.xpToLevelUp; // Calculate the percentage of XP progress
        //    int foregroundWidth = (int)(xpPercentage * Camera.CameraView.Width); // Calculate the width of the foreground XP bar
        //    spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(xpBarPosition.X + 5, xpBarPosition.Y + 7), new Rectangle(0, 0, foregroundWidth, 15), Color.MediumPurple);

        //    // Draw XP progress text
        //    string xpText = $"{character.XP} / {(int)character.xpToLevelUp}";
        //    Vector2 xpTextPosition = new Vector2(xpBarPosition.X + Camera.CameraView.Width / 2 - ContentManager.Instance.Font.MeasureString(xpText).X / 2, xpBarPosition.Y + 7);
        //    spriteBatch.DrawString(ContentManager.Instance.Font, xpText, xpTextPosition, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        //}
        //private void DrawHealthBar(SpriteBatch spriteBatch, Vector2 cameraCorner)
        //{
        //    healthBarPosition = new Vector2(cameraCorner.X, cameraCorner.Y + 35);
        //    //spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(healthBarPosition.X, healthBarPosition.Y), new Rectangle(0, 0, 310, 30), new Color(30, 30, 30, 255));

        //    float overShieldPercentage = character.Overshield / character.MaxOvershield;
        //    int overShieldForegroundWidth = (int)(overShieldPercentage * character.OvershieldBarWidth);
        //    spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(healthBarPosition.X + 2.5f, healthBarPosition.Y + 2.5f), new Rectangle(0, 0, overShieldForegroundWidth + 2, 25), Color.Cyan);
        //    spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(healthBarPosition.X + 5, healthBarPosition.Y + 5), new Rectangle(0, 0, 300, 20), new Color(30, 30, 30, 255));

        //    float healthPercentage = (float)character.RemainingHealth / character.TotalHealth; // Calculate the percentage of remaining health
        //    int foregroundWidth = (int)(healthPercentage * 300); // Calculate the width of the foreground health bar
        //    spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(healthBarPosition.X + 5, healthBarPosition.Y + 5), new Rectangle(0, 0, foregroundWidth, 20), Color.Green);

        //    // Draw health progress text
        //    string healthText = $"{character.RemainingHealth} / {character.TotalHealth}";
        //    Vector2 healthTextPosition = new Vector2(healthBarPosition.X + 310 / 2 - ContentManager.Instance.Font.MeasureString(healthText).X / 2, healthBarPosition.Y + 7); 
        //    spriteBatch.DrawString(ContentManager.Instance.Font, healthText, healthTextPosition, Color.White);

        //    //spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(healthBarPosition.X + 5 + foregroundWidth, healthBarPosition.Y + 5), new Rectangle(0, 0, overShieldWidth, 20), Color.Yellow);

        //}
        //private void DrawStaminaBar(SpriteBatch spriteBatch, Vector2 cameraCorner)
        //{
        //    staminaBarPosition = new Vector2(cameraCorner.X + 320, cameraCorner.Y + 35);

        //    //spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(staminaBarPosition.X, staminaBarPosition.Y), new Rectangle(0, 0, 310, 30), new Color(30, 30, 30, 255));
        //    float staminaPercentage = (float)character.RemainingStamina / character.TotalStamina; // Calculate the percentage of remaining stamina
        //    int staminaForegroundWidth = (int)(staminaPercentage * 300); // Calculate the width of the foreground stamina bar
        //    spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(staminaBarPosition.X + 5, staminaBarPosition.Y + 5), new Rectangle(0, 0, staminaForegroundWidth, 20), Color.Gray);

        //    // Draw stamina progress text
        //    string staminaText = $"{character.RemainingStamina} / {character.TotalStamina}";
        //    Vector2 staminaTextPosition = new Vector2(staminaBarPosition.X + 310 / 2 - ContentManager.Instance.Font.MeasureString(staminaText).X / 2, staminaBarPosition.Y + 7); 
        //    spriteBatch.DrawString(ContentManager.Instance.Font, staminaText, staminaTextPosition, Color.White);
        //}
        //private void DrawUltimateMeter(SpriteBatch spriteBatch, Vector2 cameraCorner)
        //{
        //    Color baseColor = character.MeterColor;
        //    ultimateMeterPosition = new Vector2(cameraCorner.X + 640, cameraCorner.Y + 35);
        //   // spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(ultimateMeterPosition.X, ultimateMeterPosition.Y), new Rectangle(0, 0, 310, 30), new Color(30, 30, 30, 255));
        //    float meterPercentage = (float)character.RemainingUltimateMeter / character.UltimateMeterMax; // Calculate the percentage of remaining stamina
        //    int meterForegroundWidth = (int)(meterPercentage * 300); // Calculate the width of the foreground stamina bar

        //    if (meterForegroundWidth == 300)
        //    {
        //        baseColor = Color.Red;
        //    }

        //    spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(ultimateMeterPosition.X + 5, ultimateMeterPosition.Y + 5), new Rectangle(0, 0, meterForegroundWidth, 20), baseColor);


        //}
    }
}
