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
        private int updateTicks = 0;
        Character Swordsman;
        Enemies.Enemy Slime;
        Enemies.Enemy Skeleton;

        List<Keys> forbiddenDirections = new List<Keys>();
        private Keys savedKey;
        Dictionary<Keys, AnimationType> KeysToAnimation = new Dictionary<Keys, AnimationType>()
        {
            [Keys.W] = AnimationType.RunUp,
            [Keys.A] = AnimationType.Run,
            [Keys.D] = AnimationType.Run,
            [Keys.S] = AnimationType.RunDown,
            [Keys.K] = AnimationType.Attack,
        };
        AnimationType currentAnimation = AnimationType.Stand;
        AnimationType prevAnimation;

        private Dictionary<CharacterName, Character> characterPool = new Dictionary<CharacterName, Character>();
        #region DrawableObjects
        DrawableObject GameScreenBackground;
        DrawableObject StageTile;
        DrawableObject StageTile2;

        List<DrawableObject> tiles = new List<DrawableObject>();
        #endregion
        public GameScreen(Dictionary<Texture, Texture2D> textures, GraphicsDeviceManager graphics)
        {
            GameScreenBackground = new DrawableObject(textures[Texture.GameScreenBackground], new Vector2(0, 0), new Vector2(1920, 1080), Color.White);
            StageTile = new DrawableObject(textures[Texture.StageTile], new Vector2(GameScreenBackground.Dimentions.X / 2 - 500 / 2, GameScreenBackground.Dimentions.Y / 2), new Vector2(500, 80), Color.White);
            StageTile2 = new DrawableObject(textures[Texture.StageTile], new Vector2(GameScreenBackground.Dimentions.X / 2 - 150 / 2, GameScreenBackground.Dimentions.Y - 500), new Vector2(150, 20), Color.White);
            tiles.Add(StageTile);
            tiles.Add(StageTile2);

            Swordsman = new Swordsman(CharacterName.Swordsman, ContentManager.Instance.CharacterSpriteSheets[CharacterName.Swordsman]);
            Skeleton = new Enemies.Skeleton(EnemyName.Skeleton, ContentManager.Instance.EnemySpriteSheets[EnemyName.Skeleton]);
        }
        public override void PreferedScreenSize(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
        }
        public override void Initialize()
        {

        }
        public override Screenum Update(MouseState ms)
        {
            Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();
            // Hitbox Detection, if side is hit, prevent keys from being pressed. 
            CheckPlayerHitbox(Swordsman);
            CheckEnemyHitBox(Skeleton);

            //updates input manager, if key pressed = a forbidden direction, the direction vector is unchanged aka (0,0)
            InputManager.Update(forbiddenDirections);
            if(keysPressed.Length == 0)
            {
               currentAnimation = AnimationType.Stand;
            }
            else
            {
                foreach (Keys key in keysPressed)
                {
                    if (KeysToAnimation.ContainsKey(key) && forbiddenDirections.Contains(key) == false)
                    {
                        currentAnimation = KeysToAnimation[key];
                        if(currentAnimation == AnimationType.Attack)
                        {

                        }

                        if (InputManager.MovingUp && currentAnimation == AnimationType.Attack)
                        {
                            currentAnimation = AnimationType.UpAttack;
                            break;
                        }
                        if (InputManager.MovingDown && currentAnimation == AnimationType.Attack)
                        {
                            currentAnimation = AnimationType.DownAttack;
                            break;
                        }
                        if (InputManager.Moving && currentAnimation == AnimationType.Attack)
                        {
                            currentAnimation = AnimationType.SideAttack;
                        }
                        if(InputManager.Moving == false && currentAnimation == AnimationType.Attack)
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
            StageTile.Draw(spriteBatch);
            StageTile2.Draw(spriteBatch);
            Swordsman.Draw();
            Skeleton.Draw();
            //spriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle(0, 350, 1500, 2), Color.Red);
            //spriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle(0, 350 + 80, 1500, 2), Color.Red);
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
                        character.IsGrounded = true;
                        isColliding = true;
                    }
                    else if (side == SideHit.Bottom)
                    {
                        forbiddenDirections.Add(Keys.W);
                        character.Position = new Vector2(character.Position.X, character.Position.Y + offset);
                        if (character.Velocity > 0)
                        {
                            character.Velocity = 0;
                        }
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
                character.IsGrounded = false;
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
                        enemy.IsGrounded = true;
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
                enemy.IsGrounded = false;
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
