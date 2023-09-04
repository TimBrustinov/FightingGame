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
    public class StartMenuScreen : Screen<Screenum>
    {
        public override Screenum ScreenType { get; protected set; } = Screenum.StartMenuScreen;
        public override bool IsActive { get; set; }
        public override bool CanBeDrawnUnder { get; set; } = false;

        public int buttonSpacing = 100;

        Button PlayGameButton;
        Button QuitToDesktopButton;
        Texture2D background;
        int buttonWidth = 200;
        int buttonHeight = 40;
        private float ButtonScale = 1f;
        private Vector2 backgroundScale = new Vector2(0.75f, 0.7f);

        public StartMenuScreen(GraphicsDeviceManager graphics)
        {
            background = ContentManager.Instance.StartMenuBackground;
            Vector2 backgroundDimentions = new Vector2(background.Width * backgroundScale.X, background.Height * backgroundScale.Y);
            PlayGameButton = new Button(ContentManager.Instance.Pixel, new Vector2(backgroundDimentions.X / 2, backgroundDimentions.Y - 120), new Vector2(buttonWidth, buttonHeight), new Color(30, 30, 30, 255), ButtonScale, "Play Game");
            QuitToDesktopButton = new Button(ContentManager.Instance.Pixel, new Vector2(backgroundDimentions.X / 2, backgroundDimentions.Y - 70), new Vector2(buttonWidth, buttonHeight), new Color(30, 30, 30, 255), ButtonScale, "Quit To Desktop");

            //PlayGameButton.Position = new Vector2(Globals.GraphicsDevice.Viewport.Width / 2, Globals.GraphicsDevice.Viewport.Height);
        }
        public override void PreferedScreenSize(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferWidth = (int)(background.Width * backgroundScale.X);
            graphics.PreferredBackBufferHeight = (int)(background.Height * backgroundScale.Y);
            graphics.ApplyChanges();
        }
        public override void Initialize()
        {
        }
        public override Screenum Update(MouseState ms)
        {
            if(PlayGameButton.GetMouseAction(ms) == ClickResult.LeftClicked)
            {
                return Screenum.GameScreen;
                //return Screenum.CharacterSelectScreen;
            }

            if (QuitToDesktopButton.GetMouseAction(ms) == ClickResult.LeftClicked)
            {
                Environment.Exit(0);
            }
            return Screenum.StartMenuScreen;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Vector2(0, 0), new Rectangle(0, 0, background.Width, background.Height), Color.White, 0, Vector2.Zero, backgroundScale, SpriteEffects.None, 0);
            PlayGameButton.Draw();
            QuitToDesktopButton.Draw();
            spriteBatch.End();
        }

    }
}
