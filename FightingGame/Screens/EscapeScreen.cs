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
    class EscapeScreen : Screen<Screenum>
    {
        public override Screenum ScreenType { get; protected set; }
        public override bool IsActive { get; set; }
        public override bool CanBeDrawnUnder { get; set; } = true;

        List<Button> buttons;

        Button ResumeButton;
        Button ReturnToMainMenuButton;
        Button QuitToDesktopButton;

        int rectWidth = 200;
        int rectHeight = 40;

        private float ButtonScale = 1f;
        private float SizeIncreaseFactor; // 10% increase
        private float[] buttonScales; // Initial scales for each card
        private int selectedButtonIndex = 0;
        private bool isLeftKeyPressed = false;
        private bool isRightKeyPressed = false;
        private bool isEnterKeyPressed = false;
        private bool isSpaceKeyPressed = false;

        public EscapeScreen()
        {
            SizeIncreaseFactor = ButtonScale + 0.05f;
            buttonScales = new float[] { ButtonScale, ButtonScale, ButtonScale };
            buttons = new List<Button>();

            ResumeButton = new Button(ContentManager.Instance.Pixel, Vector2.Zero, new Vector2(rectWidth, rectHeight), new Color(30, 30, 30, 255), ButtonScale, "Resume");
            buttons.Add(ResumeButton);
            ReturnToMainMenuButton = new Button(ContentManager.Instance.Pixel, Vector2.Zero, new Vector2(rectWidth, rectHeight), new Color(30, 30, 30, 255), ButtonScale, "Return To Main Menu");
            buttons.Add(ReturnToMainMenuButton);
            QuitToDesktopButton = new Button(ContentManager.Instance.Pixel, Vector2.Zero, new Vector2(rectWidth, rectHeight), new Color(30, 30, 30, 255), ButtonScale, "Quit To Desktop");
            buttons.Add(QuitToDesktopButton);
        }

        public override void PreferedScreenSize(GraphicsDeviceManager graphics)
        {
            return;
        }
        public override void Initialize()
        {
            // Calculate the total height of all buttons and the spacing between them
            int totalButtonHeight = buttons.Count * rectHeight;
            int verticalSpacing = 10; // Adjust this value to control the spacing between buttons

            // Calculate the starting Y position to center the buttons vertically
            int startY = (Globals.GraphicsDevice.Viewport.Height - totalButtonHeight - (buttons.Count - 1) * verticalSpacing) / 2;

            // Set the positions for each button
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Position = new Vector2((Globals.GraphicsDevice.Viewport.Width /* - buttons[i].Dimentions.X*/) / 2, startY + i * (buttons[i].Dimentions.Y + verticalSpacing));
            }
        }

        public override Screenum Update(MouseState ms)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.W) && !isLeftKeyPressed)
            {
                isLeftKeyPressed = true;
                selectedButtonIndex = (selectedButtonIndex - 1 + 3) % 3;
            }
            else if (ks.IsKeyUp(Keys.W))
            {
                isLeftKeyPressed = false;
            }

            if (ks.IsKeyDown(Keys.S) && !isRightKeyPressed)
            {
                isRightKeyPressed = true;
                selectedButtonIndex = (selectedButtonIndex + 1) % 3;
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
                    return Screenum.GameScreen;
                }
                else if (selectedButtonIndex == 1)
                {
                    ScreenManager<Screenum>.Instance.GoBack();
                    ScreenManager<Screenum>.Instance.GoBack();
                    return Screenum.StartMenuScreen;
                }
                else if (selectedButtonIndex == 2)
                {
                    Environment.Exit(0); // You can also use Application.Exit() if it's a Windows Forms application
                }
            }
            else if (ks.IsKeyUp(Keys.Enter) || ks.IsKeyUp(Keys.Space))
            {
                isEnterKeyPressed = false;
                isSpaceKeyPressed = false;
            }

            // Update button scaling and appearance based on selection
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

            return Screenum.EscapeScreen;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle(0, 0, Globals.GraphicsDevice.Viewport.Width, Globals.GraphicsDevice.Viewport.Height), new Color(30, 30, 30, 160));
            foreach (var button in buttons)
            {
                button.Draw();
            }
            spriteBatch.End();
            //Vector2 screenCenter = new Vector2(Globals.GraphicsDevice.Viewport.Width / 2, Globals.GraphicsDevice.Viewport.Height / 2);
            //int spacing = 10; 

            //// Calculate the total height of the stacked rectangles
            //int totalHeight = (rectHeight + spacing) * 3 - spacing;

            //// Calculate the starting position for the top rectangle
            //Vector2 startPosition = new Vector2(screenCenter.X - rectWidth / 2, screenCenter.Y - totalHeight / 2);

            //// Loop to draw three rectangles
            //for (int i = 0; i < 3; i++)
            //{
            //    // Calculate the position for the current rectangle
            //    Vector2 position = startPosition + new Vector2(0, i * (rectHeight + spacing));
            //    // Draw the rectangle as a 1x1 pixel texture
            //    spriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle((int)position.X - 1, (int)position.Y - 1, rectWidth + 2, rectHeight + 2), Color.White);
            //    spriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle((int)position.X, (int)position.Y, rectWidth, rectHeight), new Color(30, 30, 30, 255));
            //}


        }

    }
}
