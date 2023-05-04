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
        Dictionary<Keys, AnimationType> KeysToAnimation = new Dictionary<Keys, AnimationType>()
        {
            [Keys.Space] = AnimationType.Jump, 
            [Keys.A] = AnimationType.Run,
            [Keys.D] = AnimationType.Run,
        };

        private Dictionary<CharacterName, Character> characterPool = new Dictionary<CharacterName, Character>();
        #region DrawableObjects
        DrawableObject GameScreenBackground;
        DrawableObject StageTile;
        #endregion
        public GameScreen(Dictionary<Texture, Texture2D> textures, GraphicsDeviceManager graphics)
        {
            GameScreenBackground = new DrawableObject(textures[Texture.GameScreenBackground], new Vector2(0, 0), new Vector2(1500, 700), Color.White);
            StageTile = new DrawableObject(textures[Texture.StageTile], new Vector2(GameScreenBackground.Dimentions.X / 2 - 500 /2 , GameScreenBackground.Dimentions.Y / 2 ), new Vector2(500, 80), Color.White);
            Texture2D thing = ContentManager.Instance.CharacterSprites[CharacterName.CaptainFalcon];
            Character CaptainFalcon = new Character(CharacterName.CaptainFalcon, thing);
            characterPool.Add(CharacterName.CaptainFalcon, CaptainFalcon);
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
            Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();

            //foreach (Keys key in keysPressed)
            //{
            //    if (KeysToDirection.ContainsKey(key))
            //    {
            //        currentDirection = KeysToDirection[key];
            //        pacmanRotation = DirectionToRotation[currentDirection];
            //    }
            //}

            return Screenum.GameScreen;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            GameScreenBackground.Draw(spriteBatch);
            StageTile.Draw(spriteBatch);
        }

    }
}
