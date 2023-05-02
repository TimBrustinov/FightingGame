using FightingGame.Characters;
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
    public class GameScreen : Screen<Screenum>
    {
        public override Screenum ScreenType { get; protected set; }
        public override bool IsActive { get; set; }
        public override bool CanBeDrawnUnder { get; set; }

        //private Dictionary<CharacterName, >
        #region DrawableObjects
        DrawableObject GameScreenBackground;
        DrawableObject StageTile;
        #endregion
        public GameScreen(Dictionary<Texture, Texture2D> textures, GraphicsDeviceManager graphics)
        {
            GameScreenBackground = new DrawableObject(textures[Texture.GameScreenBackground], new Vector2(0, 0), new Vector2(1500, 700), Color.White);
            StageTile = new DrawableObject(textures[Texture.StageTile], new Vector2(GameScreenBackground.Dimentions.X / 2 - 500 /2 , GameScreenBackground.Dimentions.Y / 2 ), new Vector2(500, 80), Color.White);
        }
        public override void PreferedScreenSize(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferWidth = 1500;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();
        }

        public override void Initialize()
        {
            
        }

        
        public override Screenum Update(MouseState ms)
        {
            return Screenum.GameScreen;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            GameScreenBackground.Draw(spriteBatch);
            StageTile.Draw(spriteBatch);
        }

    }
}
