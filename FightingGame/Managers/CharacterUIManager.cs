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
            offset = 50;
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
           // spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(cameraCorner.X, cameraCorner.Y + 60), new Rectangle(0, 0, offset * 3, offset), new Color(30, 30, 30, 200));
            foreach (var item in AbilityIcons)
            {
                spriteBatch.Draw(ContentManager.Instance.EntitySpriteSheets[character.Name], new Vector2(cameraCorner.X + i * offset, cameraCorner.Y + 60), AbilityIcons[item.Key], Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                i++;
            }

            //spriteBatch.Draw(ContentManager.Instance.EntitySpriteSheets[character.Name], cameraCorner + new Vector2(UIPosition.X + 50, UIPosition.Y), AbilityIcons[AnimationType.Ability2], Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            //spriteBatch.Draw(ContentManager.Instance.EntitySpriteSheets[character.Name], cameraCorner + new Vector2(UIPosition.X + 100, UIPosition.Y), AbilityIcons[AnimationType.Ability3], Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }
        private void DrawCooldowns(SpriteBatch spriteBatch, Vector2 cameraCorner)
        {
            int i = -2;
            foreach (var item in character.MaxAbilityCooldowns)
            {
                float cooldownPercentage = (float)character.AbilityCooldowns[item.Key] / item.Value; // Calculate the percentage of remaining cooldown
                int foregroundHeight = (int)(cooldownPercentage * 50); // Calculate the height of the foreground cooldown bar
                Vector2 position = new Vector2(cameraCorner.X + i * offset, cameraCorner.Y + 60) + new Vector2(0, 50 - foregroundHeight);
                spriteBatch.Draw(ContentManager.Instance.Pixel, position, new Rectangle(0, 0, 50, foregroundHeight), new Color(75, 75, 75, 0), 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                i++;
            }
        }
        private void DrawXpBar(SpriteBatch spriteBatch, Vector2 cameraCorner)
        {
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(cameraCorner.X, cameraCorner.Y + 5), new Rectangle(0, 0, Camera.CameraView.Width, 15), new Color(30, 30, 30, 255));
            float xpPercentage = character.XP / character.xpToLevelUp; // Calculate the percentage of remaining health
            int foregroundWidth = (int)(xpPercentage * Camera.CameraView.Width); // Calculate the width of the foreground health bar
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(cameraCorner.X + 5, cameraCorner.Y + 7), new Rectangle(0, 0, foregroundWidth, 10), Color.Aqua);
        }
        private void DrawHealthBar(SpriteBatch spriteBatch, Vector2 cameraCorner)
        {
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(cameraCorner.X, cameraCorner.Y + 25), new Rectangle(0, 0, 310, 30), new Color(30, 30, 30, 255));
            float healthPercentage = (float)character.RemainingHealth / character.TotalHealth; // Calculate the percentage of remaining health
            int foregroundWidth = (int)(healthPercentage * 300); // Calculate the width of the foreground health bar
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(cameraCorner.X + 5, cameraCorner.Y + 30), new Rectangle(0, 0, foregroundWidth, 20), Color.Green);
        }
        private void DrawStaminaBar(SpriteBatch spriteBatch, Vector2 cameraCorner)
        {
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(cameraCorner.X + 320, cameraCorner.Y + 25), new Rectangle(0, 0, 310, 30), new Color(30, 30, 30, 255));
            float staminaPercentage = (float)character.RemainingStamina / character.TotalStamina; // Calculate the percentage of remaining health
            int staminaForegroundWidth = (int)(staminaPercentage * 300); // Calculate the width of the foreground health bar
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(cameraCorner.X + 325, cameraCorner.Y + 30), new Rectangle(0, 0, staminaForegroundWidth, 20), Color.Gray);
        }
    }
}
