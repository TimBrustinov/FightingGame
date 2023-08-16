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

        #region DrawableObjects
        DrawableObject PlayGame;
        DrawableObject QuitGame;
        #endregion

        public StartMenuScreen(Dictionary<Texture, Texture2D> textures, GraphicsDeviceManager graphics)
        {
            PlayGame = new DrawableObject(textures[Texture.PlayGame], new Vector2(graphics.PreferredBackBufferWidth / 2 - 150, 320));
            QuitGame = new DrawableObject(textures[Texture.QuitGame], new Vector2(PlayGame.Position.X, PlayGame.Position.Y + buttonSpacing));
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
            if(PlayGame.GetMouseAction(ms) == ClickResult.LeftClicked)
            {
                return Screenum.GameScreen;
            }
            return Screenum.StartMenuScreen;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            PlayGame.Draw(spriteBatch);
            QuitGame.Draw(spriteBatch);
            spriteBatch.DrawString(ContentManager.Instance.Font, "Most handsome supporter: Beezer", new Vector2(0, 980), Color.White);
            spriteBatch.End();
        }

    }
}
