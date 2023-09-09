using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame.Screens
{
    public class GameOverScreen : Screen<Screenum>
    {
        public override Screenum ScreenType { get; protected set; } = Screenum.StartMenuScreen;
        public override bool IsActive { get; set; }
        public override bool CanBeDrawnUnder { get; set; } = true;

        public int buttonSpacing = 100;
        List<Button> buttons = new List<Button>();
        Icon GameOverText;
        Button ReturnToMainMenuButton;
        Button QuitToDesktopButton;
        int buttonWidth = 200;
        int buttonHeight = 40;
        private float ButtonScale = 1f;
        private float SizeIncreaseFactor;
        private float[] buttonScales;
        private int selectedButtonIndex = 0;
        
        private Vector2 backgroundScale = new Vector2(0.75f, 0.7f);

        private bool isLeftKeyPressed = false;
        private bool isRightKeyPressed = false;
        private bool isEnterKeyPressed = false;
        private bool isSpaceKeyPressed = false;

        public GameOverScreen(GraphicsDeviceManager graphics)
        {
            GameOverText = ContentManager.Instance.GameOverText;
            SizeIncreaseFactor = ButtonScale + 0.05f;
            buttonScales = new float[] { ButtonScale, ButtonScale };
            ReturnToMainMenuButton = new Button(ContentManager.Instance.Pixel, Vector2.Zero, new Vector2(buttonWidth, buttonHeight), new Color(30, 30, 30, 255), ButtonScale, "Return To Main Menu");
            QuitToDesktopButton = new Button(ContentManager.Instance.Pixel, Vector2.Zero, new Vector2(buttonWidth, buttonHeight), new Color(30, 30, 30, 255), ButtonScale, "Quit To Desktop");
            buttons.Add(ReturnToMainMenuButton);
            buttons.Add(QuitToDesktopButton);


            //PlayGameButton.Position = new Vector2(Globals.GraphicsDevice.Viewport.Width / 2, Globals.GraphicsDevice.Viewport.Height);
        }
        public override void PreferedScreenSize(GraphicsDeviceManager graphics)
        {
            return;
        }
        public override void Initialize()
        {
            ReturnToMainMenuButton.Position = new Vector2(Globals.GraphicsDevice.Viewport.Width / 2, Globals.GraphicsDevice.Viewport.Height / 2 + 200);
            QuitToDesktopButton.Position = new Vector2(ReturnToMainMenuButton.Position.X, ReturnToMainMenuButton.Position.Y + 70);
        }
        public override Screenum Update(MouseState ms)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.W) && !isLeftKeyPressed)
            {
                isLeftKeyPressed = true;
                selectedButtonIndex = (selectedButtonIndex - 1 + 2) % 2;
            }
            else if (ks.IsKeyUp(Keys.W))
            {
                isLeftKeyPressed = false;
            }

            if (ks.IsKeyDown(Keys.S) && !isRightKeyPressed)
            {
                isRightKeyPressed = true;
                selectedButtonIndex = (selectedButtonIndex + 1) % 2;
            }
            else if (ks.IsKeyUp(Keys.S))
            {
                isRightKeyPressed = false;
            }

            if ((ks.IsKeyDown(Keys.Enter) && !isEnterKeyPressed) || (ks.IsKeyDown(Keys.Space) && !isSpaceKeyPressed))
            {
                isSpaceKeyPressed = true;
                isEnterKeyPressed = true;

                if (selectedButtonIndex == 0)
                {
                    ScreenManager<Screenum>.Instance.GoBack();
                    ScreenManager<Screenum>.Instance.GoBack();
                    return Screenum.StartMenuScreen;
                }
                else if (selectedButtonIndex == 1)
                {
                    Environment.Exit(0);
                }
                
            }
            else if (ks.IsKeyUp(Keys.Enter) || ks.IsKeyUp(Keys.Space))
            {
                isEnterKeyPressed = false;
                isSpaceKeyPressed = false;
            }


            for (int i = 0; i < buttons.Count; i++)
            {
                if (i == selectedButtonIndex)
                {
                    // Increase the size of the selected button gradually
                    buttonScales[i] = MathHelper.Lerp(buttonScales[i], SizeIncreaseFactor, 0.1f); // Adjust the lerp speed if needed
                }
                else
                {
                    // Return unselected buttons to their original size gradually
                    buttonScales[i] = MathHelper.Lerp(buttonScales[i], ButtonScale, 0.1f); // Adjust the lerp speed if needed
                }

                // Apply the scale to the button
                buttons[i].Scale = buttonScales[i];
            }
            return Screenum.GameOverScreen;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(GameOverText.Texture, new Vector2(Globals.GraphicsDevice.Viewport.Width / 2, Globals.GraphicsDevice.Viewport.Height / 2), 
                GameOverText.SourceRectangle, Color.White, 0, new Vector2(GameOverText.SourceRectangle.Width / 2, GameOverText.SourceRectangle.Height / 2), GameOverText.Scale, SpriteEffects.None, 0);
            foreach (var button in  buttons)
            {
                button.Draw();
            }
            spriteBatch.End();
        }

    }
}
