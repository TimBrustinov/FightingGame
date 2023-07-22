using FightingGame.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Net.Mime;

namespace FightingGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public Character CaptainFalcon;
        #region Game Screen Textures
        Dictionary<Texture, Texture2D> gameScreenTextures = new Dictionary<Texture, Texture2D>();
        Texture2D GameScreenBackground;
        Texture2D StageTile;
        #endregion

        #region Start Menu Textures
        Dictionary<Texture, Texture2D> startMenuTextures = new Dictionary<Texture, Texture2D>();
        Texture2D PlayGame;
        Texture2D QuitGame;
        #endregion
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentManager.Instance.Pixel = new Texture2D(GraphicsDevice, 1, 1);
            ContentManager.Instance.Pixel.SetData(new[] { Color.White });
            //CaptainFalcon = new Character();
            Globals.SpriteBatch = spriteBatch;
            ContentManager.Instance.LoadContent(Content);
            #region Start Menu
            PlayGame = Content.Load<Texture2D>("button_play-game");
            startMenuTextures.Add(Texture.PlayGame, PlayGame);

            QuitGame = Content.Load<Texture2D>("button_quit-game");
            startMenuTextures.Add(Texture.QuitGame, QuitGame);
            #endregion

            #region Game Screen
            GameScreenBackground = Content.Load<Texture2D>("HellDungeonDecoration");
            gameScreenTextures.Add(Texture.GameScreenBackground, GameScreenBackground);

            #endregion
            StartMenuScreen startMenuScreen = new StartMenuScreen(startMenuTextures, graphics);
            GameScreen gameScreen = new GameScreen(gameScreenTextures, graphics); 
            ScreenManager<Screenum>.Instance.AddScreen(Screenum.StartMenuScreen, startMenuScreen);
            ScreenManager<Screenum>.Instance.ActivateScreen(startMenuScreen);
            ScreenManager<Screenum>.Instance.AddScreen(Screenum.GameScreen, gameScreen);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Globals.Update(gameTime);
            ScreenManager<Screenum>.Instance.Update(graphics);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            //spriteBatch.Begin();
            ScreenManager<Screenum>.Instance.Draw(spriteBatch, graphics);
            //spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}