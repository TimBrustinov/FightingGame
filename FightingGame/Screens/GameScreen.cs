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
        public override Screenum ScreenType { get; protected set; }
        public override bool IsActive { get; set; }
        public override bool CanBeDrawnUnder { get; set; }

        private int heartOffsets = 10;
        private Vector2 healthStartingPosition = new Vector2(20, 25);

        Character Swordsman;
        Enemies.Enemy Skeleton;

        List<Keys> forbiddenDirections = new List<Keys>();
        Dictionary<Keys, AnimationType> KeysToAnimation = new Dictionary<Keys, AnimationType>()
        {
            [Keys.W] = AnimationType.RunUp,
            [Keys.A] = AnimationType.Run,
            [Keys.D] = AnimationType.Run,
            [Keys.S] = AnimationType.RunDown,
            [Keys.K] = AnimationType.Attack,
        };
        AnimationType currentAnimation = AnimationType.Stand;

        private Dictionary<CharacterName, Character> characterPool = new Dictionary<CharacterName, Character>();

        #region DrawableObjects
        DrawableObject GameScreenBackground;

        List<DrawableObject> tiles = new List<DrawableObject>();
        #endregion

        public GameScreen(Dictionary<Texture, Texture2D> textures, GraphicsDeviceManager graphics)
        {
            GameScreenBackground = new DrawableObject(textures[Texture.GameScreenBackground], new Vector2(0, 100), new Vector2(1374, 860), Color.White);

            Swordsman = new Swordsman(CharacterName.Swordsman, ContentManager.Instance.CharacterSpriteSheets[CharacterName.Swordsman]);
            Skeleton = new Enemies.Skeleton(EnemyName.Skeleton, ContentManager.Instance.EnemySpriteSheets[EnemyName.Skeleton]);
        }
        public override void PreferedScreenSize(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferWidth = 1374;
            graphics.PreferredBackBufferHeight = 960;
            graphics.ApplyChanges();
        }
        public override void Initialize()
        {

        }
        public override Screenum Update(MouseState ms)
        {
            Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();
            // Hitbox Detection, if side is hit, prevent keys from being pressed. 
            //CheckPlayerHitbox(Swordsman);
            //CheckEnemyHitBox(Skeleton);
            MouseState mouseState = Mouse.GetState();
            if (Swordsman.NumOfHits != 0 && Swordsman.WeaponHitBox.Intersects(Skeleton.HitBox))
            {
                Console.WriteLine(Swordsman.NumOfHits);
                Skeleton.Health--;
                Swordsman.NumOfHits--;
                
            }
            //updates input manager, if key pressed = a forbidden direction, the direction vector is unchanged aka (0,0)
            InputManager.Update(forbiddenDirections);
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
                        if (InputManager.MovingUp && currentAnimation == AnimationType.Attack)
                        {
                            currentAnimation = AnimationType.UpAttack;
                        }
                        if (InputManager.MovingDown && currentAnimation == AnimationType.Attack)
                        {
                            currentAnimation = AnimationType.DownAttack;
                        }
                        if (InputManager.Moving && currentAnimation == AnimationType.Attack)
                        {
                            currentAnimation = AnimationType.SideAttack;
                        }
                        if (InputManager.Moving == false && currentAnimation == AnimationType.Attack)
                        {
                            currentAnimation = AnimationType.SideAttack;
                        }
                    }
                }
            }

           

            if (CalculateDistance(Swordsman.Position, Skeleton.Position) <= 50f)
            {
                Skeleton.Update(AnimationType.SideAttack, Vector2.Normalize(Swordsman.Position - Skeleton.Position));
            }
            else
            {
                Skeleton.Update(AnimationType.Run, Vector2.Normalize(Swordsman.Position - Skeleton.Position));
            }

            Swordsman.Update(currentAnimation, InputManager.Direction);
            return Screenum.GameScreen;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            GameScreenBackground.Draw(spriteBatch);
            Swordsman.Draw();
            Skeleton.Draw();
            DrawHearts(spriteBatch);
        }
        private void DrawHearts(SpriteBatch spriteBatch)
        {
            int heartX;
            for (int i = 0; i < Swordsman.RemainingHealth; i++)
            {
                heartX = (int)(healthStartingPosition.X + (i * (25 + heartOffsets)));
                //spriteBatch.Draw(ContentManager.Instance.Heart, new Rectangle(heartX, (int)healthStartingPosition.Y, 25, 25), Color.White);

                //Drawing hearts with outline and background
                spriteBatch.Draw(ContentManager.Instance.HeartOutline, new Rectangle(heartX, (int)healthStartingPosition.Y, 25, 25), Color.White);
                spriteBatch.Draw(ContentManager.Instance.HeartBackground, new Rectangle(heartX, (int)healthStartingPosition.Y, 25, 25), Color.Red);
            }
            for (int i = Swordsman.RemainingHealth; i < Swordsman.TotalHealth; i++)
            {
                heartX = (int)(healthStartingPosition.X + (i * (25 + heartOffsets)));
                //spriteBatch.Draw(ContentManager.Instance.GrayHeart, new Rectangle(heartX, (int)healthStartingPosition.Y, 25, 25), Color.White);

                //Drawing hearts with outline and background
                spriteBatch.Draw(ContentManager.Instance.HeartOutline, new Rectangle(heartX, (int)healthStartingPosition.Y, 25, 25), Color.White);
                spriteBatch.Draw(ContentManager.Instance.HeartBackground, new Rectangle(heartX, (int)healthStartingPosition.Y, 25, 25), Color.Gray);
            }

            
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
        private void CheckPlayerHitbox(Character character)
        {
            bool isColliding = false;
            foreach (var tile in tiles)
            {
                if (character.HitBox.Intersects(tile.HitBox))
                {
                    SideHit side = SideIntersected(tile.HitBox, character.HitBox, out int offset);

                    if (side == SideHit.Top)
                    {
                        forbiddenDirections.Add(Keys.S);
                        character.Position = new Vector2(character.Position.X, character.Position.Y - offset + 1);
                        isColliding = true;
                    }
                    else if (side == SideHit.Bottom)
                    {
                        forbiddenDirections.Add(Keys.W);
                        character.Position = new Vector2(character.Position.X, character.Position.Y + offset);
                    }
                    else if (side == SideHit.Right)
                    {
                        character.Position = new Vector2(character.Position.X + offset, character.Position.Y);
                        forbiddenDirections.Add(Keys.D);
                    }
                    else if (side == SideHit.Left)
                    {
                        character.Position = new Vector2(character.Position.X - offset, character.Position.Y);
                        forbiddenDirections.Add(Keys.A);
                    }
                }
                else
                {
                    forbiddenDirections.Clear();
                }
            }
            if (!isColliding)
            {

            }
        }
        private void CheckEnemyHitBox(Enemies.Enemy enemy)
        {
            bool isColliding = false;
            foreach (var tile in tiles)
            {
                if (enemy.HitBox.Intersects(tile.HitBox))
                {
                    SideHit side = SideIntersected(tile.HitBox, enemy.HitBox, out int offset);

                    if (side == SideHit.Top)
                    {
                        enemy.Position = new Vector2(enemy.Position.X, enemy.Position.Y - offset + 1);
                        isColliding = true;
                    }
                    else if (side == SideHit.Bottom)
                    {
                        enemy.Position = new Vector2(enemy.Position.X, enemy.Position.Y + offset);

                    }
                    else if (side == SideHit.Right)
                    {
                        enemy.Position = new Vector2(enemy.Position.X + offset, enemy.Position.Y);
                    }
                    else if (side == SideHit.Left)
                    {
                        enemy.Position = new Vector2(enemy.Position.X - offset, enemy.Position.Y);
                    }
                }
                else
                {
                    forbiddenDirections.Clear();
                }
            }
            if (!isColliding)
            {

            }
        }
        private float CalculateDistance(Vector2 playerPosition, Vector2 enemyPosition)
        {
            float distanceX = playerPosition.X - enemyPosition.X;
            float distanceY = playerPosition.Y - enemyPosition.Y;

            float distance = (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
            return distance;
        }
    }
}
