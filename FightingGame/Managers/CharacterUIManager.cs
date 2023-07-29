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
        Dictionary<CharacterPortrait, Texture2D> Portraits;
        private int offset;
        private Camera Camera;

        private float portraitScale = 0.7f;

        public CharacterUIManager(Character character, Camera camera)
        {
            this.character = character;
            AbilityIcons = ContentManager.Instance.CharacterAbilityIcons[character.Name];
            Portraits = ContentManager.Instance.CharacterPortraits[character.Name];
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
            //DrawCooldowns(spriteBatch, cameraCorner);
        }
        private void DrawIcons(SpriteBatch spriteBatch, Vector2 cameraCorner)
        {
            int i = 0;
            Vector2 position = new Vector2(cameraCorner.X + Camera.Viewport.Width / 2 + 15, cameraCorner.Y + Camera.Viewport.Height);
            //spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(cameraCorner.X + Camera.Viewport.Width / 2 - offset * 3, cameraCorner.Y + Camera.Viewport.Height - offset * 2), new Rectangle(0, 0, offset * 3, offset), new Color(30, 30, 30, 255));
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(position.X - 137, position.Y - 58), new Rectangle(0, 0, 235, 65), new Color(30, 30, 30, 255));
            if (!character.InUltimateForm)
            {
                //Draws the portrait 
                spriteBatch.Draw(Portraits[CharacterPortrait.HashashinBase], new Vector2(position.X - 130, position.Y - offset + 4), default, Color.White, 0f, Vector2.Zero, portraitScale, SpriteEffects.None, 1f);
                foreach (var item in AbilityIcons)
                {
                    if (item.Key != AnimationType.UltimateAbility1 && item.Key != AnimationType.UltimateAbility2 && item.Key != AnimationType.UltimateAbility3)
                    {
                        //Draws the ability Icon
                        spriteBatch.Draw(ContentManager.Instance.EntitySpriteSheets[character.Name], new Vector2(position.X - 75 + i * offset, position.Y - offset), AbilityIcons[item.Key], Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                        
                        //Draws the cooldown for the ability
                        float cooldownPercentage = (float)character.AbilityCooldowns[item.Key] / character.MaxAbilityCooldowns[item.Key]; // Calculate the percentage of remaining cooldown
                        int foregroundHeight = (int)(cooldownPercentage * 50); // Calculate the height of the foreground cooldown bar
                        Vector2 cooldownPosition = new Vector2(position.X - 75 + i * offset, position.Y - offset) + new Vector2(0, 50 - foregroundHeight);
                        spriteBatch.Draw(ContentManager.Instance.Pixel, cooldownPosition, new Rectangle(0, 0, 50, foregroundHeight), new Color(75, 75, 75, 0), 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                        i++;
                    }
                }
            }
            else
            {
                //Draws the portrait 
                spriteBatch.Draw(Portraits[CharacterPortrait.HashashinElemental], new Vector2(position.X - 130, position.Y - offset + 4), default, Color.White, 0f, Vector2.Zero, portraitScale, SpriteEffects.None, 1f);

                foreach (var item in AbilityIcons)
                {
                    if (item.Key == AnimationType.UltimateAbility1 || item.Key == AnimationType.UltimateAbility2 || item.Key == AnimationType.UltimateAbility3)
                    {
                        spriteBatch.Draw(ContentManager.Instance.EntitySpriteSheets[character.Name], new Vector2(position.X - 75 + i * offset, position.Y - offset), AbilityIcons[item.Key], Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);

                        //Draws the cooldown for the ability
                        float cooldownPercentage = (float)character.AbilityCooldowns[item.Key] / character.MaxAbilityCooldowns[item.Key]; // Calculate the percentage of remaining cooldown
                        int foregroundHeight = (int)(cooldownPercentage * 50); // Calculate the height of the foreground cooldown bar
                        Vector2 cooldownPosition = new Vector2(position.X - 75 + i * offset, position.Y - offset) + new Vector2(0, 50 - foregroundHeight);
                        spriteBatch.Draw(ContentManager.Instance.Pixel, cooldownPosition, new Rectangle(0, 0, 50, foregroundHeight), new Color(75, 75, 75, 0), 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                        i++;
                    }
                }
            }
        }

        
        private void DrawXpBar(SpriteBatch spriteBatch, Vector2 cameraCorner)
        {
            Vector2 position = new Vector2(cameraCorner.X, cameraCorner.Y);
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(position.X, position.Y + 5), new Rectangle(0, 0, Camera.CameraView.Width, 20), new Color(30, 30, 30, 255));
            float xpPercentage = character.XP / character.xpToLevelUp; // Calculate the percentage of XP progress
            int foregroundWidth = (int)(xpPercentage * Camera.CameraView.Width); // Calculate the width of the foreground XP bar
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(position.X + 5, position.Y + 7), new Rectangle(0, 0, foregroundWidth, 15), Color.MediumPurple);

            // Draw XP progress text
            string xpText = $"{character.XP} / {(int)character.xpToLevelUp}";
            Vector2 xpTextPosition = new Vector2(position.X + Camera.CameraView.Width / 2 - ContentManager.Instance.Font.MeasureString(xpText).X / 2, position.Y + 7);
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
