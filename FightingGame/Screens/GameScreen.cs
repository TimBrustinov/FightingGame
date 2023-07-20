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
        public GraphicsDeviceManager Graphics;
        public Camera Camera;

        private Matrix Transform;
        public override Screenum ScreenType { get; protected set; }
        public override bool IsActive { get; set; }
        public override bool CanBeDrawnUnder { get; set; }
        private int heartOffsets = 10;
        private Vector2 healthStartingPosition = new Vector2(20, 25);

        Character Hashashin;
        Character Samurai;
        Character SelectedCharacter;
        Enemy Skeleton;

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


        private int enemyPoolIndex;
        private int enemySpawnRate = 1000;
        private int deadEnemies;
        private List<Enemy> enemyPool;
        private List<Enemy> reservePool;
        private double spawnTimer;

        #region DrawableObjects
        DrawableObject GameScreenBackground;
        DrawableObject UIBackground;
        List<DrawableObject> tiles = new List<DrawableObject>();
        #endregion

        public GameScreen(Dictionary<Texture, Texture2D> textures, GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
            GameScreenBackground = new DrawableObject(textures[Texture.GameScreenBackground], new Vector2(0, 100), new Vector2(1374 + 500, 860 + 500), Color.White);
            Hashashin = new Character(EntityName.Hashashin, ContentManager.Instance.EntitySpriteSheets[EntityName.Hashashin], 100, 5, 0.18f, 1.5f, ContentManager.Instance.EntityAbilites[EntityName.Hashashin]);
            //Samurai = new Samurai(CharacterName.Samurai, ContentManager.Instance.CharacterSpriteSheets[CharacterName.Samurai]);
            Skeleton = new Enemy(EntityName.Skeleton, ContentManager.Instance.EntitySpriteSheets[EntityName.Skeleton], 10, 1, 0.3f, 1.2f, ContentManager.Instance.EntityAbilites[EntityName.Skeleton]);
        }
        public override void PreferedScreenSize(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();
        }
        public override void Initialize()
        {
            SelectedCharacter = Hashashin;
            Camera = new Camera(Graphics.GraphicsDevice.Viewport);
            UIBackground = new DrawableObject(ContentManager.Instance.Pixel, new Vector2(0, 0), new Vector2(Camera.Viewport.Width, 10), Color.Gray);
            enemyPool = new List<Enemy>();
            reservePool = new List<Enemy>();
            enemyPoolIndex = 0;
        }
        public override Screenum Update(MouseState ms)
        {
            Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();
            //MouseState mouseState = Mouse.GetState();
            spawnTimer += Globals.CurrentTime.ElapsedGameTime.TotalMilliseconds;
            if(spawnTimer >= enemySpawnRate)
            {
                SpawnEnemy();
                spawnTimer = 0;
            }
            //updates input manager, if key pressed = a forbidden direction, the direction vector is unchanged aka (0,0)
            InputManager.Update();
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
            for (int i = 0; i < enemyPoolIndex - deadEnemies; i++)
            {
                if(enemyPool[i].IsDead)
                {
                    reservePool.Add(enemyPool[i]);
                    enemyPool.RemoveAt(i);
                    deadEnemies++;
                    continue;
                }

                if (SelectedCharacter.WeaponHitBox.Intersects(enemyPool[i].HitBox))
                {
                    if (SelectedCharacter.HasFrameChanged)
                    {
                        enemyPool[i].HasBeenHit = false;
                    }
                    if (SelectedCharacter.CurrentAbility != null && SelectedCharacter.CurrentAbility.CanHit && !enemyPool[i].HasBeenHit)
                    {
                        enemyPool[i].TakeDamage(SelectedCharacter.AbilityDamage);
                        enemyPool[i].HasBeenHit = true;
                    }
                }
                else
                {
                    enemyPool[i].HasBeenHit = false;
                }


                if (enemyPool[i].RemainingHealth <= 0)
                {
                    enemyPool[i].Update(AnimationType.Death, Vector2.Normalize(SelectedCharacter.Position - enemyPool[i].Position));
                }
                else if (CalculateDistance(SelectedCharacter.Position, enemyPool[i].Position) <= 50f && Math.Abs(SelectedCharacter.Position.Y - enemyPool[i].Position.Y) <= 20)
                {
                    enemyPool[i].Update(AnimationType.BasicAttack, Vector2.Normalize(SelectedCharacter.Position - enemyPool[i].Position));
                }
                else
                {
                    enemyPool[i].Update(AnimationType.Run, Vector2.Normalize(SelectedCharacter.Position - enemyPool[i].Position));
                }
            }
            
            

            if(SelectedCharacter.RemainingHealth <= 0)
            {
                currentAnimation = AnimationType.Death;
            }
            SelectedCharacter.Update(currentAnimation, InputManager.Direction);
            Camera.Update(SelectedCharacter.Position, 1f);
            UIBackground.Position = new Vector2(SelectedCharacter.Position.X - Camera.Viewport.Width / 2, SelectedCharacter.Position.Y - Camera.Viewport.Height / 2);
            return Screenum.GameScreen;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Transform = Camera.GetTransformMatrix();
            spriteBatch.Begin(transformMatrix: Transform);
            GameScreenBackground.Draw(spriteBatch);
            SelectedCharacter.Draw();
            for (int i = 0; i < enemyPoolIndex - deadEnemies; i++)
            {
                if (!enemyPool[i].IsDead)
                {
                    enemyPool[i].Draw();
                }
            }

            UIBackground.Draw(spriteBatch);
            spriteBatch.End();
        }
        
        private void SpawnEnemy()
        {
            int increaseEnemyPoolAmmount = 1;
            if(reservePool.Count > 0)
            {
                deadEnemies = 0;
                foreach (var enemy in reservePool)
                {
                    enemy.Reset();
                    enemyPool.Add(enemy);
                    enemy.Position = GetSpawnLocation();
                }
                reservePool.Clear();
            }

            for (int i = 0; i < increaseEnemyPoolAmmount; i++)
            {
                Enemy enemy = new Enemy(EntityName.Skeleton, ContentManager.Instance.EntitySpriteSheets[EntityName.Skeleton], 10, 1, 0.3f, 1.5f, ContentManager.Instance.EntityAbilites[EntityName.Skeleton]);
                enemyPool.Add(enemy);
                enemy.Position = GetSpawnLocation();
            }
            enemyPoolIndex += increaseEnemyPoolAmmount;

        }
        private Vector2 GetSpawnLocation()
        {
            int spawnAreaOffset = 50;
            int minSpawnX = Camera.CameraView.X - Camera.CameraView.Width / 2 + spawnAreaOffset;
            int maxSpawnX = Camera.CameraView.X + Camera.CameraView.Width / 2 - spawnAreaOffset;
            int minSpawnY = Camera.CameraView.Y - Camera.CameraView.Height / 2 + spawnAreaOffset;
            int maxSpawnY = Camera.CameraView.Y + Camera.CameraView.Height / 2 - spawnAreaOffset;

            int randomSpawnX = new Random().Next(minSpawnX, maxSpawnX);
            int randomSpawnY = new Random().Next(minSpawnY, maxSpawnY);
            return new Vector2(randomSpawnX,randomSpawnY);
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
        //private void CheckEnemyHitBox(Enemies.Enemy enemy)
        //{
        //    bool isColliding = false;
        //    foreach (var tile in tiles)
        //    {
        //        if (enemy.HitBox.Intersects(tile.HitBox))
        //        {
        //            SideHit side = SideIntersected(tile.HitBox, enemy.HitBox, out int offset);

        //            if (side == SideHit.Top)
        //            {
        //                enemy.Position = new Vector2(enemy.Position.X, enemy.Position.Y - offset + 1);
        //                isColliding = true;
        //            }
        //            else if (side == SideHit.Bottom)
        //            {
        //                enemy.Position = new Vector2(enemy.Position.X, enemy.Position.Y + offset);

        //            }
        //            else if (side == SideHit.Right)
        //            {
        //                enemy.Position = new Vector2(enemy.Position.X + offset, enemy.Position.Y);
        //            }
        //            else if (side == SideHit.Left)
        //            {
        //                enemy.Position = new Vector2(enemy.Position.X - offset, enemy.Position.Y);
        //            }
        //        }
        //        else
        //        {
        //            forbiddenDirections.Clear();
        //        }
        //    }
        //    if (!isColliding)
        //    {

        //    }
        //}
        private float CalculateDistance(Vector2 playerPosition, Vector2 enemyPosition)
        {
            float distanceX = playerPosition.X - enemyPosition.X;
            float distanceY = playerPosition.Y - enemyPosition.Y;

            float distance = (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
            return distance;
        }
        private void CalculateTranslation()
        {
            var dx = (GameScreenBackground.Dimentions.X / 2) - SelectedCharacter.Position.X;
            var dy = (GameScreenBackground.Dimentions.Y / 2) - SelectedCharacter.Position.Y;
            Transform = Matrix.CreateTranslation(dx, dy, 0f);
        }
    }
}
