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

        Character Hashashin;
        Character Samurai;
        Character SelectedCharacter;
        Enemies.Enemy Skeleton;

        List<Keys> forbiddenDirections = new List<Keys>();
        Dictionary<Keys, AnimationType> KeysToAnimation = new Dictionary<Keys, AnimationType>()
        {
            [Keys.W] = AnimationType.Run,
            [Keys.A] = AnimationType.Run,
            [Keys.D] = AnimationType.Run,
            [Keys.S] = AnimationType.Run,
            [Keys.K] = AnimationType.Attack,
            [Keys.Q] = AnimationType.Ability1,
            [Keys.E] = AnimationType.Ability2,
            [Keys.R] = AnimationType.Ability3,
            [Keys.Space] = AnimationType.Dodge,
        };
        AnimationType currentAnimation = AnimationType.Stand;

        private Dictionary<EntityName, Character> characterPool = new Dictionary<EntityName, Character>();

        #region DrawableObjects
        DrawableObject GameScreenBackground;

        List<DrawableObject> tiles = new List<DrawableObject>();
        #endregion

        public GameScreen(Dictionary<Texture, Texture2D> textures, GraphicsDeviceManager graphics)
        {
            GameScreenBackground = new DrawableObject(textures[Texture.GameScreenBackground], new Vector2(0, 100), new Vector2(1374, 860), Color.White);
            Hashashin = new Hashashin(EntityName.Hashashin, ContentManager.Instance.CharacterSpriteSheets[EntityName.Hashashin]);
            //Samurai = new Samurai(CharacterName.Samurai, ContentManager.Instance.CharacterSpriteSheets[CharacterName.Samurai]);
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
            SelectedCharacter = Hashashin;
        }
        public override Screenum Update(MouseState ms)
        {
            Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();
            //MouseState mouseState = Mouse.GetState();
            if (SelectedCharacter.NumOfHits != 0 && SelectedCharacter.WeaponHitBox.Intersects(Skeleton.HitBox))
            {
                Skeleton.RemainingHealth--;
                Skeleton.objectColor = new Color(255, 255, 255, 0);
                SelectedCharacter.NumOfHits--;
            }
            
            if (Skeleton.NumOfHits != 0 && Skeleton.WeaponHitBox.Intersects(SelectedCharacter.HitBox))
            {
                SelectedCharacter.RemainingHealth--;
                Skeleton.NumOfHits--;
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
                        if(InputManager.Moving && currentAnimation == AnimationType.Dodge)
                        {
                            break;
                        }
                        if (InputManager.Moving && currentAnimation == AnimationType.Attack)
                        {
                            currentAnimation = AnimationType.BasicAttack;
                        }
                        if (InputManager.Moving == false && currentAnimation == AnimationType.Attack)
                        {
                            currentAnimation = AnimationType.BasicAttack;
                        }
                    }
                }
            }


            if (Skeleton.RemainingHealth <= 0)
            {
                Skeleton.Update(AnimationType.Death, Vector2.Normalize(SelectedCharacter.Position - Skeleton.Position));
            }
            else if (CalculateDistance(SelectedCharacter.Position, Skeleton.Position) <= 50f)
            {
                Skeleton.Update(AnimationType.BasicAttack, Vector2.Normalize(SelectedCharacter.Position - Skeleton.Position));
            }
            else
            {
                Skeleton.Update(AnimationType.Run, Vector2.Normalize(SelectedCharacter.Position - Skeleton.Position));
            }

            if(SelectedCharacter.RemainingHealth <= 0)
            {
                currentAnimation = AnimationType.Death;
            }


            SelectedCharacter.UpdateCharacter(currentAnimation, InputManager.Direction);
            return Screenum.GameScreen;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            GameScreenBackground.Draw(spriteBatch);
            SelectedCharacter.Draw();
            if(!Skeleton.isDead)
            {
                Skeleton.Draw();
            }
            //DrawHearts(spriteBatch);
        }
        //private void DrawHearts(SpriteBatch spriteBatch)
        //{
        //    int heartX;
        //    for (int i = 0; i < Swordsman.RemainingHealth; i++)
        //    {
        //        heartX = (int)(healthStartingPosition.X + (i * (25 + heartOffsets)));
        //        //spriteBatch.Draw(ContentManager.Instance.Heart, new Rectangle(heartX, (int)healthStartingPosition.Y, 25, 25), Color.White);

        //        //Drawing hearts with outline and background
        //        spriteBatch.Draw(ContentManager.Instance.HeartOutline, new Rectangle(heartX, (int)healthStartingPosition.Y, 25, 25), Color.White);
        //        spriteBatch.Draw(ContentManager.Instance.HeartBackground, new Rectangle(heartX, (int)healthStartingPosition.Y, 25, 25), Color.Red);
        //    }
        //    for (int i = Swordsman.RemainingHealth; i < Swordsman.TotalHealth; i++)
        //    {
        //        heartX = (int)(healthStartingPosition.X + (i * (25 + heartOffsets)));
        //        //spriteBatch.Draw(ContentManager.Instance.GrayHeart, new Rectangle(heartX, (int)healthStartingPosition.Y, 25, 25), Color.White);

        //        //Drawing hearts with outline and background
        //        spriteBatch.Draw(ContentManager.Instance.HeartOutline, new Rectangle(heartX, (int)healthStartingPosition.Y, 25, 25), Color.White);
        //        spriteBatch.Draw(ContentManager.Instance.HeartBackground, new Rectangle(heartX, (int)healthStartingPosition.Y, 25, 25), Color.Gray);
        //    }

            
        //}
        
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
