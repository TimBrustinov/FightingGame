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
        private Dictionary<AnimationType, Icon> AbilityIcons;
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

        private Vector2 ultimateBarPosition;
        private Point ultimateBarDimentions;

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

            ultimateBarDimentions = new Point(4 * (abilityDimentions.X + abilityOffset) - abilityOffset, 10);
            ultimateBarPosition = new Vector2(abilityPosition.X, abilityPosition.Y - ultimateBarDimentions.Y + 5);
        }
        public void Update()
        {
            healthBarPosition = new Vector2(BottomLeft.X + 150, BottomLeft.Y - 50);
            xpBarPosition = new Vector2(healthBarPosition.X + (healthBarDimentions.X - xpBarDimentions.X), healthBarPosition.Y - xpBarDimentions.Y - 5);
            abilityPosition = new Vector2(BottomRight.X - 400, BottomRight.Y - 80);
            ultimateBarPosition = new Vector2(abilityPosition.X, abilityPosition.Y - (ultimateBarDimentions.Y + 5));
        }
        public void Draw()
        {
            healthBarOutline();
            healthBar();
            overShield();
            xpBar();
            abilities();
            ultimateBar();
        }
        private void healthBar()
        {
            float healthPercentage = character.RemainingHealth / character.TotalHealth; 
            int foregroundWidth = (int)(healthPercentage * healthBarDimentions.X); 
            SpriteBatch.Draw(ContentManager.Instance.Pixel, healthBarPosition, new Rectangle(0, 0, healthBarDimentions.X, healthBarDimentions.Y), new Color(30, 30, 30, 200));
            SpriteBatch.Draw(ContentManager.Instance.Pixel, healthBarPosition, new Rectangle(0, 0, foregroundWidth, healthBarDimentions.Y), new Color(105, 187, 60));
            string text = $"{character.RemainingHealth} / {character.TotalHealth}";
            drawText(new Vector2(healthBarPosition.X + healthBarDimentions.X / 2 - ContentManager.Instance.Font.MeasureString(text).X / 2, healthBarPosition.Y + 3), text);
        }
        private void healthBarOutline()
        {
            SpriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(healthBarPosition.X - 21, xpBarPosition.Y - 21), new Rectangle(0, 0, healthBarDimentions.X + 42, healthBarDimentions.Y + 52), Color.White);
            SpriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(healthBarPosition.X - 20, xpBarPosition.Y - 20), new Rectangle(0, 0, healthBarDimentions.X + 40, healthBarDimentions.Y + 50), new Color(30, 30, 30, 255));
        }
        private void xpBar()
        {
            float xpPercentage = character.XP / character.xpToLevelUp; 
            int foregroundWidth = (int)(xpPercentage * xpBarDimentions.X);
            SpriteBatch.Draw(ContentManager.Instance.Pixel, xpBarPosition, new Rectangle(0, 0, xpBarDimentions.X, xpBarDimentions.Y), new Color(30, 30, 30, 200));
            SpriteBatch.Draw(ContentManager.Instance.Pixel, xpBarPosition, new Rectangle(0, 0, foregroundWidth, xpBarDimentions.Y), new Color(178, 102, 255));
            string text = $"Lv: {character.Level}";
            drawText(new Vector2(xpBarPosition.X - 50, xpBarPosition.Y - 5), text);
        }
        private void ultimateBar()
        {
            float ultimatePercentage = character.RemainingUltimateMeter / character.UltimateMeterMax;
            int foregroundWidth = (int)(ultimatePercentage * ultimateBarDimentions.X);
            SpriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(ultimateBarPosition.X - 1, ultimateBarPosition.Y - 1), new Rectangle(0, 0, ultimateBarDimentions.X + 2, ultimateBarDimentions.Y + 2), Color.White);
            SpriteBatch.Draw(ContentManager.Instance.Pixel, ultimateBarPosition, new Rectangle(0, 0, ultimateBarDimentions.X, ultimateBarDimentions.Y), new Color(30, 30, 30, 255));
            SpriteBatch.Draw(ContentManager.Instance.Pixel, ultimateBarPosition, new Rectangle(0, 0, foregroundWidth, ultimateBarDimentions.Y), Color.Gold);
        }
        private void overShield()
        {
            float overShieldPercentage = character.Overshield / character.MaxOvershield;
            int foregroundWidth = (int)(overShieldPercentage * character.OvershieldBarWidth);
            SpriteBatch.Draw(ContentManager.Instance.Pixel, healthBarPosition, new Rectangle(0, 0, foregroundWidth, healthBarDimentions.Y), new Color(153, 255, 255));
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
                        Vector2 position = new Vector2(abilityPosition.X + i * (abilityDimentions.X + abilityOffset), abilityPosition.Y);
                        drawIconBackground(position);
                        SpriteBatch.Draw(icon.Value.Texture, position, icon.Value.SourceRectangle, Color.White, 0, Vector2.Zero, icon.Value.Scale, SpriteEffects.None, 0);
                        
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
                        drawIconBackground(position);
                        SpriteBatch.Draw(icon.Value.Texture, position, icon.Value.SourceRectangle, Color.White, 0, Vector2.Zero, icon.Value.Scale, SpriteEffects.None, 0);
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
        private void drawIconBackground(Vector2 position)
        {
            SpriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(position.X - 1, position.Y - 1), new Rectangle(0, 0, abilityDimentions.X + 2, abilityDimentions.Y + 2), Color.White);
            SpriteBatch.Draw(ContentManager.Instance.Pixel, position, new Rectangle(0, 0, abilityDimentions.X, abilityDimentions.Y), new Color(30, 30, 30, 255));
        }
        private void powerUps()
        {
            SpriteBatch.Draw(ContentManager.Instance.Pixel, )
        }
        private void drawText(Vector2 position, string text)
        {
            SpriteBatch.DrawString(ContentManager.Instance.Font, text, position, Color.White);
        }
        
    }
}
