using FightingGame.Characters;
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
        private const int hitBoxMargin = 10;
        private const int characterHitBoxCompensation = 2;
        public override Screenum ScreenType { get; protected set; }
        public override bool IsActive { get; set; }
        public override bool CanBeDrawnUnder { get; set; }
        Character CaptainFalcon;

        List<Keys> forbiddenDirections = new List<Keys>();
        private Keys savedKey;
        Dictionary<Keys, AnimationType> KeysToAnimation = new Dictionary<Keys, AnimationType>()
        {
            [Keys.W] = AnimationType.Jump,
            [Keys.A] = AnimationType.Run,
            [Keys.D] = AnimationType.Run,
            [Keys.S] = AnimationType.Fall,
            [Keys.K] = AnimationType.NeutralAttack,
            [Keys.L] = AnimationType.Special,
        };
        AnimationType currentAnimation = AnimationType.Stand;
        private Dictionary<CharacterName, Character> characterPool = new Dictionary<CharacterName, Character>();
        #region DrawableObjects
        DrawableObject GameScreenBackground;
        DrawableObject StageTile;
        #endregion
        public GameScreen(Dictionary<Texture, Texture2D> textures, GraphicsDeviceManager graphics)
        {
            GameScreenBackground = new DrawableObject(textures[Texture.GameScreenBackground], new Vector2(0, 0), new Vector2(1500, 700), Color.White);
            StageTile = new DrawableObject(textures[Texture.StageTile], new Vector2(GameScreenBackground.Dimentions.X / 2 - 500 / 2, GameScreenBackground.Dimentions.Y / 2), new Vector2(500, 80), Color.White);
            Texture2D thing = ContentManager.Instance.CharacterSpriteSheets[CharacterName.CaptainFalcon];
            CaptainFalcon = new Character(CharacterName.CaptainFalcon, thing);
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
            // Hitbox Detection, if side is hit, prevent keys from being pressed. 
            if (CaptainFalcon.HitBox.Intersects(StageTile.HitBox))
            {
                // Calculates what side was hit
                SideHit side = SideIntersected(StageTile.HitBox, CaptainFalcon.HitBox, out int offset);
                
                if (side == SideHit.Top)
                {
                    forbiddenDirections.Add(Keys.S);
                    CaptainFalcon.Position = new Vector2(CaptainFalcon.Position.X, CaptainFalcon.Position.Y - offset + 1);
                    CaptainFalcon.IsGrounded = true;
                }
                else if (side == SideHit.Bottom)
                {
                    forbiddenDirections.Add(Keys.W);
                    CaptainFalcon.Position = new Vector2(CaptainFalcon.Position.X, CaptainFalcon.Position.Y + offset);
                    if(CaptainFalcon.Velocity > 0)
                    {
                        CaptainFalcon.Velocity = 0;
                    }
                }
                else if (side == SideHit.Right)
                {
                    CaptainFalcon.Position = new Vector2(CaptainFalcon.Position.X + offset, CaptainFalcon.Position.Y);
                    forbiddenDirections.Add(Keys.D);

                }
                else if (side == SideHit.Left)
                {
                    CaptainFalcon.Position = new Vector2(CaptainFalcon.Position.X - offset, CaptainFalcon.Position.Y);
                    forbiddenDirections.Add(Keys.A);
                }
            }
            else
            {
                // Captain falcon is in the air, gravity is applied
                CaptainFalcon.IsGrounded = false;
                forbiddenDirections = new List<Keys>();
            }

            //updates input manager, if key pressed = a forbidden direction, the direction vector is unchanged aka (0,0)
            InputManager.Update(forbiddenDirections);

            foreach(Keys key in keysPressed)
            {
                if(KeysToAnimation.ContainsKey(key) && forbiddenDirections.Contains(key) == false)
                {
                    currentAnimation = KeysToAnimation[key];
                }
            }

            //if (InputManager.Direction == Vector2.Zero)
            //{
            //    if (CaptainFalcon.animationManager.CurrentAnimation != null && !CaptainFalcon.animationManager.CurrentAnimation.CanBeCanceled && savedKey != Keys.None)
            //    {
            //        currentAnimation = KeysToAnimation[savedKey];
            //    }
            //    else
            //    {
            //        currentAnimation = AnimationType.Stand;
            //    }
            //}

            //// loops thru all keys pressed, if a key is forbidden that key isnt registered
            //foreach (Keys key in keysPressed)
            //{
            //    if (KeysToAnimation.ContainsKey(key) && !forbiddenDirections.Contains(key))
            //    {
            //        if(key == Keys.W && CaptainFalcon.JumpCount < 1)
            //        {
            //            CaptainFalcon.IsGrounded = false;
            //        }
            //        if(CaptainFalcon.animationManager.CurrentAnimation.CanBeCanceled)
            //        {
            //            currentAnimation = KeysToAnimation[key];
            //            savedKey = key;
            //        }
            //        else if(KeysToAnimation.ContainsKey(savedKey))
            //        {
            //            currentAnimation = KeysToAnimation[savedKey];
            //        }
            //    }
            //}
            CaptainFalcon.Update(currentAnimation, InputManager.Direction);

            return Screenum.GameScreen;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            GameScreenBackground.Draw(spriteBatch);
            StageTile.Draw(spriteBatch);
            CaptainFalcon.Draw();
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle(0, 350, 1500, 2), Color.Red);
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle(0, 350 + 80, 1500, 2), Color.Red);
            //spriteBatch.Draw(ContentManager.Instance.Pixel, StageTile.HitBox, Color.Blue);
            //spriteBatch.Draw(ContentManager.Instance.Pixel, CaptainFalcon.HitBox, Color.Green);

        }
        
        public SideHit SideIntersected(Rectangle objectA, Rectangle objectB,  out int offset)
        {
            int left = Math.Abs(objectA.Left - objectB.Right);
            int right = Math.Abs(objectA.Right - objectB.Left);
            int bottom = Math.Abs(objectA.Bottom - objectB.Top);
            int top = Math.Abs(objectA.Top - objectB.Bottom);

            int minVal = Min(left, right, top, bottom);
            if(left == minVal)
            {
                offset = left;
                return SideHit.Left;
            }
            else if(right == minVal)
            {
                offset = right;
                return SideHit.Right;
            }
            else if(top == minVal)
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
                if(array[i] < tempMin)
                {
                    tempMin = array[i];
                }
            }
            return tempMin;
        }
    }
}
