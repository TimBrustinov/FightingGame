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
        Button Settings;

        int buttonWidth = 200;
        int buttonHeight = 40;
        private float ButtonScale = 1f;

        public StartMenuScreen(Dictionary<Texture, Texture2D> textures, GraphicsDeviceManager graphics)
        {
            PlayGameButton = new Button(ContentManager.Instance.Pixel, Vector2.Zero, new Vector2(buttonWidth, buttonHeight), Color.Black, ButtonScale, "Play Game");
            //PlayGameButton.Position = new Vector2(Globals.GraphicsDevice.Viewport.Width / 2, Globals.GraphicsDevice.Viewport.Height);

        }
        public override void PreferedScreenSize(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000;
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
            }
            return Screenum.StartMenuScreen;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            PlayGameButton.Draw();
            spriteBatch.DrawString(ContentManager.Instance.Font, "Most handsome supporter: Beezer", new Vector2(0, 980), Color.White);
            spriteBatch.End();
        }

    }
}
