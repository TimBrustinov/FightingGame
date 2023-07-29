using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class GameScreen : Screen<Screenum>
    {
        public GraphicsDeviceManager Graphics;
        public Camera Camera;

        private Matrix Transform;
        public override Screenum ScreenType { get; protected set; }
        public override bool IsActive { get; set; }
        public override bool CanBeDrawnUnder { get; set; }

        Character Hashashin;
        Character SelectedCharacter;

        Dictionary<Keys, AnimationType> KeysToAnimation = new Dictionary<Keys, AnimationType>()
        {
            [Keys.W] = AnimationType.Run,
            [Keys.A] = AnimationType.Run,
            [Keys.D] = AnimationType.Run,
            [Keys.S] = AnimationType.Run,
            [Keys.I] = AnimationType.BasicAttack,
            [Keys.J] = AnimationType.Ability1,
            [Keys.K] = AnimationType.Ability2,
            [Keys.L] = AnimationType.Ability3,
            [Keys.R] = AnimationType.UltimateTransform,
            [Keys.F] = AnimationType.UndoTransform,
            [Keys.Space] = AnimationType.Dodge,
        };
        AnimationType currentAnimation = AnimationType.Stand;
        
        CharacterUIManager CharacterUIManager;
        EnemyManager EnemyManager;

        #region DrawableObjects
        DrawableObject Tilemap;
        #endregion

        public GameScreen(Dictionary<Texture, Texture2D> textures, GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
            Tilemap = new DrawableObject(textures[Texture.GameScreenBackground], new Vector2(0, 0), new Vector2(1920 * 1.8f, 1920 * 1.8f), Color.White);
            Hashashin = new Character(EntityName.Hashashin, ContentManager.Instance.EntitySpriteSheets[EntityName.Hashashin], 100, 4, 1.5f, ContentManager.Instance.EntityAbilites[EntityName.Hashashin]);
            Hashashin.SetBounds(new Rectangle(Tilemap.HitBox.X + 64, Tilemap.HitBox.Y + 64, Tilemap.HitBox.Width - 64, Tilemap.HitBox.Height - 64));
        }
        public override void PreferedScreenSize(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();

            //graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //graphics.IsFullScreen = true;
            //graphics.ApplyChanges();

        }
        public override void Initialize()
        {
            SelectedCharacter = Hashashin;
            Camera = new Camera(Graphics.GraphicsDevice.Viewport);
            EnemyManager = new EnemyManager(Tilemap);
            CharacterUIManager = new CharacterUIManager(SelectedCharacter, Camera);
        }
        public override Screenum Update(MouseState ms)
        {
            Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();
            InputManager.Update(Camera, SelectedCharacter);
            if (keysPressed.Length == 0)
            {
                currentAnimation = AnimationType.Stand;
            }
            else
            {    
                foreach (Keys key in keysPressed)
                {
                    if(KeysToAnimation.ContainsKey(key))
                    {
                        currentAnimation = KeysToAnimation[key];
                        if(InputManager.Moving && currentAnimation != AnimationType.Run)
                        {
                            break;
                        }
                    }
                }
            }
            if(SelectedCharacter.RemainingHealth <= 0)
            {
                currentAnimation = AnimationType.Death;
            }

            SelectedCharacter.Update(currentAnimation, InputManager.Direction);
            Camera.Update(SelectedCharacter.Position, Tilemap.HitBox);
            EnemyManager.Update(SelectedCharacter, Camera);
            return Screenum.GameScreen;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Transform = Camera.GetTransformMatrix();
            spriteBatch.Begin(transformMatrix: Transform);

            Tilemap.Draw(spriteBatch);
            SelectedCharacter.Draw();
            EnemyManager.Draw();
            CharacterUIManager.Draw(spriteBatch, Camera.Corner);

            spriteBatch.End();
        }
        public SideHit SideIntersected(Rectangle objectA, Rectangle objectB, out int offset)
        {
            int left = Math.Abs(objectA.Left - objectB.Right);
            int right = Math.Abs(objectA.Right - objectB.Left);
            int bottom = Math.Abs(objectA.Bottom - objectB.Top);
            int top = Math.Abs(objectA.Top - objectB.Bottom);

            int minVal = Min(left, right, top, bottom);
            if (left == minVal)
            {
                offset = left;
                return SideHit.Left;
            }
            else if (right == minVal)
            {
                offset = right;
                return SideHit.Right;
            }
            else if (top == minVal)
            {
                offset = top;
                return SideHit.Top;
            }
            else
            {
                offset = bottom;
                return SideHit.Bottom;
            }
        }
        private int Min(int a, int b, int c, int d)
        {
            int[] array = new int[] { a, b, c, d };
            int tempMin = a;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < tempMin)
                {
                    tempMin = array[i];
                }
            }
            return tempMin;
        }
    }
}
