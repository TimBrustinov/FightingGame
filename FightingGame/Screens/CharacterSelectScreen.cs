using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class CharacterSelectScreen : Screen<Screenum>
    {
        public override Screenum ScreenType { get; protected set; } = Screenum.CharacterSelectionScreen;
        public override bool IsActive { get; set; }
        public override bool CanBeDrawnUnder { get; set; } = false;
        
        Button StartButton;
        int buttonWidth = 200;
        int buttonHeight = 40;
        private float ButtonScale = 1f;
        
        Texture2D background;
        private Vector2 backgroundScale = new Vector2(0.75f, 0.7f);

        List<Character> characters;
        Character selectedCharacter;

        Vector2 iconBackgroundPosition;
        Vector2 iconPosition;
        Point iconDimensions;
        Point iconBackgroundDimensions;
        public CharacterSelectScreen()
        {
            background = ContentManager.Instance.CharacterSelectBackground;
            Vector2 backgroundDimentions = new Vector2(background.Width * backgroundScale.X, background.Height * backgroundScale.Y);
           
            iconBackgroundPosition = new Vector2(80, 200);
            iconBackgroundDimensions = new Point(400, 500);
            iconPosition = new Vector2(iconBackgroundPosition.X + 20, iconBackgroundPosition.Y + 150);
            iconDimensions = new Point(50, 50);

            selectedCharacter = ContentManager.Instance.Characters[EntityName.Hashashin];
            characters = new List<Character>();
            foreach (var item in ContentManager.Instance.Characters)
            {
                characters.Add(item.Value);
            }
        }
        public override void PreferedScreenSize(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferWidth = (int)(background.Width * backgroundScale.X);
            graphics.PreferredBackBufferHeight = (int)(background.Height * backgroundScale.Y);
            graphics.ApplyChanges();
        }
        public override void Initialize()
        {
            return;
        }
        public override Screenum Update(MouseState ms)
        {
            selectedCharacter.Update(AnimationType.Stand, Vector2.Zero);
            selectedCharacter.Position = new Vector2(iconBackgroundPosition.X + iconBackgroundDimensions.X + 500, iconBackgroundPosition.Y);
            return Screenum.CharacterSelectScreen;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Vector2(0, 0), new Rectangle(0, 0, background.Width, background.Height), Color.White, 0, Vector2.Zero, backgroundScale, SpriteEffects.None, 0);
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(80 - 1, 200 - 1), new Rectangle(0, 0, 400 + 2, 500 + 2), Color.White);
            spriteBatch.Draw(ContentManager.Instance.Pixel, iconBackgroundPosition, new Rectangle(0, 0, iconBackgroundDimensions.X, iconBackgroundDimensions.Y), new Color(30, 30, 30, 255));
            selectedCharacter.Draw();
            drawskills();
            spriteBatch.End();
        }

        public void drawskills()
        {
            int i = 0;
            foreach (var icon in ContentManager.Instance.CharacterAbilityIcons[selectedCharacter.Name])
            {
                if (icon.Key != AnimationType.Dodge && icon.Key != AnimationType.UltimateDodge)
                {
                    Vector2 position = new Vector2(iconPosition.X, iconPosition.Y + i * (iconDimensions.Y + 10));
                    iconBackground(position);
                    Globals.SpriteBatch.Draw(icon.Value.Texture, position, icon.Value.SourceRectangle, Color.White, 0, Vector2.Zero, icon.Value.Scale, SpriteEffects.None, 0);
                    i++;
                }
            }
        }
        private void iconBackground(Vector2 position)
        {
            Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, new Vector2(position.X - 1, position.Y - 1), new Rectangle(0, 0, iconDimensions.X + 2, iconDimensions.Y + 2), Color.White);
            Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, position, new Rectangle(0, 0, iconDimensions.X, iconDimensions.Y), new Color(30, 30, 30, 255));
        }
    }
}
