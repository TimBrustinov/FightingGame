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
            Vector2 screenCenter = new Vector2(Globals.GraphicsDevice.Viewport.Width / 2, Globals.GraphicsDevice.Viewport.Height / 2);

            int spacing = 10; 
            int totalHeight = (rectHeight + spacing) * 3 - spacing;
            Vector2 startPosition = new Vector2(screenCenter.X - rectWidth / 2, screenCenter.Y - totalHeight / 2);

            ResumeButton = new Button(ContentManager.Instance.Pixel, startPosition, new Vector2(rectWidth, rectHeight), new Color(30, 30, 30, 255), ButtonScale);
            buttons.Add(ResumeButton);
            startPosition.Y += rectHeight + spacing;
            ReturnToMainMenuButton = new Button(ContentManager.Instance.Pixel, startPosition, new Vector2(rectWidth, rectHeight), new Color(30, 30, 30, 255), ButtonScale);
            buttons.Add(ReturnToMainMenuButton);
            startPosition.Y += rectHeight + spacing; 
            QuitToDesktopButton = new Button(ContentManager.Instance.Pixel, startPosition, new Vector2(rectWidth, rectHeight), new Color(30, 30, 30, 255), ButtonScale);
            buttons.Add(QuitToDesktopButton);
        }

        public override void PreferedScreenSize(GraphicsDeviceManager graphics)
        {
            return;
        }
        public override void Initialize()
        {
            
        }
        public override Screenum Update(MouseState ms)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.W) && !isLeftKeyPressed)
            {
                isLeftKeyPressed = true;
                selectedButtonIndex = (selectedButtonIndex - 1 + 3) % 3;
            }
            else if (ks.IsKeyUp(Keys.S))
            {
                isLeftKeyPressed = false;
            }

            if (ks.IsKeyDown(Keys.D) && !isRightKeyPressed)
            {
                isRightKeyPressed = true;
                selectedButtonIndex = (selectedButtonIndex + 1) % 3;
            }
            else if (ks.IsKeyUp(Keys.D))
            {
                isRightKeyPressed = false;
            }

            if ((ks.IsKeyDown(Keys.Enter) && !isEnterKeyPressed) || (ks.IsKeyDown(Keys.Space) && !isSpaceKeyPressed))
            {
                isSpaceKeyPressed = true;
                isEnterKeyPressed = true;

                if (selectedButtonIndex == 0)
                {
                    // Handle action for the first button
                }
                else if (selectedButtonIndex == 1)
                {
                    // Handle action for the second button
                }
                else if (selectedButtonIndex == 2)
                {
                    // Handle action for the third button
                }

                // Return to the previous screen or perform other actions as needed
                ScreenManager<Screenum>.Instance.GoBack();
                return Screenum.GameScreen;
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
