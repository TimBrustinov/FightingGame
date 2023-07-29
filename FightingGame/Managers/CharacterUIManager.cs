using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FightingGame
{
    public class CharacterUIManager
    {
        private Character character;
        private Dictionary<AnimationType, Rectangle> AbilityIcons;
        private int offset;
        private Camera Camera;

        public CharacterUIManager(Character character, Camera camera)
        {
            this.character = character;
            AbilityIcons = ContentManager.Instance.CharacterAbilityIcons[character.Name];
            Camera = camera;
            offset = 55;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 cameraCorner)
        {
            //spriteBatch.Draw(ContentManager.Instance.Pixel, cameraCorner + new Vector2(0, 0), new Rectangle(0, 0, 250, 54), new Color(30, 30, 30, 255));
            //spriteBatch.Draw(ContentManager.Instance.Pixel, cameraCorner, new Rectangle(0, 0, chara))
            DrawHealthBar(spriteBatch, cameraCorner);
            DrawXpBar(spriteBatch, cameraCorner);
            DrawStaminaBar(spriteBatch, cameraCorner);
            DrawIcons(spriteBatch, cameraCorner);
            DrawCooldowns(spriteBatch, cameraCorner);
        }
        private void DrawIcons(SpriteBatch spriteBatch, Vector2 cameraCorner)
        {
            int i = 0;
            //spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(cameraCorner.X + Camera.Viewport.Width / 2 - offset * 3, cameraCorner.Y + Camera.Viewport.Height - offset * 2), new Rectangle(0, 0, offset * 3, offset), new Color(30, 30, 30, 255));
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(cameraCorner.X + Camera.Viewport.Width / 2 - 87, cameraCorner.Y + Camera.Viewport.Height - 58), new Rectangle(0, 0, 185, 65), new Color(30, 30, 30, 255));
            if (!character.InUltimateForm)
            {
                // Draw normal abilities when not in ultimate form
                foreach (var item in AbilityIcons)
                {
                    if (item.Key != AnimationType.UltimateAbility1 && item.Key != AnimationType.UltimateAbility2 && item.Key != AnimationType.UltimateAbility3)
                    {
                        spriteBatch.Draw(ContentManager.Instance.EntitySpriteSheets[character.Name], new Vector2(cameraCorner.X + Camera.Viewport.Width / 2 - 75 + i * offset, cameraCorner.Y + Camera.Viewport.Height - offset), AbilityIcons[item.Key], Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                        i++;
                    }
                }
            }
            else
            {
                // Draw ultimate abilities when in ultimate form
                foreach (var item in AbilityIcons)
                {
                    if (item.Key == AnimationType.UltimateAbility1 || item.Key == AnimationType.UltimateAbility2 || item.Key == AnimationType.UltimateAbility3)
                    {
                        spriteBatch.Draw(ContentManager.Instance.EntitySpriteSheets[character.Name], new Vector2(cameraCorner.X + Camera.Viewport.Width / 2 - 75 + i * offset, cameraCorner.Y + Camera.Viewport.Height - offset), AbilityIcons[item.Key], Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                        i++;
                    }
                }
            }
        }

        private void DrawCooldowns(SpriteBatch spriteBatch, Vector2 cameraCorner)
        {
            int i = 0;
            if(character.InUltimateForm)
            {
                i = -3;
            }
            foreach (var item in AbilityIcons)
            {
                float cooldownPercentage = (float)character.AbilityCooldowns[item.Key] / character.MaxAbilityCooldowns[item.Key]; // Calculate the percentage of remaining cooldown
                int foregroundHeight = (int)(cooldownPercentage * 50); // Calculate the height of the foreground cooldown bar
                Vector2 position = new Vector2(cameraCorner.X + Camera.Viewport.Width / 2 - 75 + i * offset, cameraCorner.Y + Camera.Viewport.Height - offset) + new Vector2(0, 50 - foregroundHeight);
                spriteBatch.Draw(ContentManager.Instance.Pixel, position, new Rectangle(0, 0, 50, foregroundHeight), new Color(75, 75, 75, 0), 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                i++;
            }
        }
        private void DrawXpBar(SpriteBatch spriteBatch, Vector2 cameraCorner)
        {
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(cameraCorner.X, cameraCorner.Y + 5), new Rectangle(0, 0, Camera.CameraView.Width, 20), new Color(30, 30, 30, 255));
            float xpPercentage = character.XP / character.xpToLevelUp; // Calculate the percentage of XP progress
            int foregroundWidth = (int)(xpPercentage * Camera.CameraView.Width); // Calculate the width of the foreground XP bar
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(cameraCorner.X + 5, cameraCorner.Y + 7), new Rectangle(0, 0, foregroundWidth, 15), Color.MediumPurple);

            // Draw XP progress text
            string xpText = $"{character.XP} / {character.xpToLevelUp}";
            Vector2 xpTextPosition = new Vector2(cameraCorner.X + Camera.CameraView.Width / 2 - ContentManager.Instance.Font.MeasureString(xpText).X / 2, cameraCorner.Y + 7);
            spriteBatch.DrawString(ContentManager.Instance.Font, xpText, xpTextPosition, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
        private void DrawHealthBar(SpriteBatch spriteBatch, Vector2 cameraCorner)
        {
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(cameraCorner.X, cameraCorner.Y + 35), new Rectangle(0, 0, 310, 30), new Color(30, 30, 30, 255));
            float healthPercentage = (float)character.RemainingHealth / character.TotalHealth; // Calculate the percentage of remaining health
            int foregroundWidth = (int)(healthPercentage * 300); // Calculate the width of the foreground health bar
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(cameraCorner.X + 5, cameraCorner.Y + 40), new Rectangle(0, 0, foregroundWidth, 20), Color.Green);
        }
        private void DrawStaminaBar(SpriteBatch spriteBatch, Vector2 cameraCorner)
        {
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(cameraCorner.X + 320, cameraCorner.Y + 35), new Rectangle(0, 0, 310, 30), new Color(30, 30, 30, 255));
            float staminaPercentage = (float)character.RemainingStamina / character.TotalStamina; // Calculate the percentage of remaining health
            int staminaForegroundWidth = (int)(staminaPercentage * 300); // Calculate the width of the foreground health bar
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(cameraCorner.X + 325, cameraCorner.Y + 40), new Rectangle(0, 0, staminaForegroundWidth, 20), Color.Gray);
        }
    }
}
