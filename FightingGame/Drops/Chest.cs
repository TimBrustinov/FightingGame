using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace FightingGame
{
    public class Chest
    {
        public Icon Icon;
        private Animation openAnimation;
        public Vector2 Position;
        public Rectangle Hitbox;
        public Vector2 topLeftCorner { get; private set; }
        public int CoinsToOpen;
        private bool isKeyPressed;
        public bool IsOpen { get; private set; }

        public Chest(Icon icon, Animation animation)
        {
            Icon = icon;
            openAnimation = new Animation(animation.Texture, animation.frameTime, animation.AnimationFrames);
        }
        public void Activate(Vector2 position, int coinsToOpen)
        {
            Position = position;
            CoinsToOpen = coinsToOpen;
            topLeftCorner = Position - Icon.Dimensions / 2;
            Hitbox = new Rectangle((int)topLeftCorner.X - 20, (int)topLeftCorner.Y - 20, (int)Icon.Dimensions.X + 40, (int)Icon.Dimensions.Y + 40);
        }

        private bool isDisplayingCoinsPopup = false;
        private Vector2 popupPositionOffset = new Vector2(-10, -40); // Offset from the chest position

        public void Update()
        {
            KeyboardState ks = Keyboard.GetState();
            if (GameObjects.Instance.SelectedCharacter.HitBox.Intersects(Hitbox))
            {
                // Display the coins popup only when the player is near the chest
                if (!isDisplayingCoinsPopup)
                {
                    isDisplayingCoinsPopup = true;
                }

                if (ks.IsKeyDown(Keys.E) && !isKeyPressed && GameObjects.Instance.SelectedCharacter.Coins >= CoinsToOpen)
                {
                    GameObjects.Instance.SelectedCharacter.Coins -= CoinsToOpen;
                    openAnimation.Start();
                    isKeyPressed = true;
                }
                else if (ks.IsKeyUp(Keys.E))
                {
                    isKeyPressed = false;
                }
            }
            else
            {
                // Hide the coins popup when the player is not near the chest
                isDisplayingCoinsPopup = false;
            }
            openAnimation.Update();
        }

        public void Draw()
        {
            //Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, Hitbox, Color.Red);
            //Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, topLeftCorner, new Rectangle(0, 0, 5, 5), Color.Blue);

            if (openAnimation.active)
            {
                openAnimation.Draw(Position, false, Icon.Scale.X, Color.White);
                if (openAnimation.IsAnimationDone)
                {
                    IsOpen = true;
                }
            }
            else
            {
                if (isDisplayingCoinsPopup)
                {
                    DrawCoinsPopup();
                }
                Globals.SpriteBatch.Draw(Icon.Texture, Position, Icon.SourceRectangle, Color.White, 0, new Vector2(Icon.SourceRectangle.Width / 2, Icon.SourceRectangle.Height / 2), Icon.Scale, SpriteEffects.None, 0);
            }
        }

        private void DrawCoinsPopup()
        {
            Vector2 coinPosition = topLeftCorner - new Vector2(-5, 20);
            var coin = ContentManager.Instance.EnemyDrops[IconType.Coin].Icon;
            Globals.SpriteBatch.Draw(coin.Texture, topLeftCorner - new Vector2(0, 20), coin.SourceRectangle, Color.White, 0, Vector2.Zero, coin.Scale, SpriteEffects.None, 0);
            drawText(coinPosition + new Vector2(coin.Texture.Width, 0), $"{CoinsToOpen}");
        }

        /*
         * private void money()
        {
            var coin = ContentManager.Instance.EnemyDrops[IconType.Coin];
            SpriteBatch.Draw(ContentManager.Instance.Pixel, moneyBackgroundPosition - Vector2.One, new Rectangle(0, 0, moneyBackgroundDimensions.X + 2, moneyBackgroundDimensions.Y + 2), Color.White);
            SpriteBatch.Draw(ContentManager.Instance.Pixel, moneyBackgroundPosition, new Rectangle(0, 0, moneyBackgroundDimensions.X, moneyBackgroundDimensions.Y), new Color(30, 30, 30, 255));
            SpriteBatch.Draw(coin.Icon.Texture, moneyBackgroundPosition, coin.Icon.SourceRectangle, Color.White, 0, Vector2.Zero, coin.Icon.Scale, SpriteEffects.None, 0);
            string coinsText = $"{GameObjects.Instance.SelectedCharacter.Coins}";
            Vector2 textDimensions = ContentManager.Instance.Font.MeasureString(coinsText);
            Vector2 adjustedPosition = moneyBackgroundPosition + new Vector2(90 - textDimensions.X, 2);
            drawText(adjustedPosition, coinsText);
        }
         */


        private void drawText(Vector2 position, string text)
        {
            Globals.SpriteBatch.DrawString(ContentManager.Instance.Font, text, position, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }

        public Chest Clone()
        {
            return new Chest(Icon, openAnimation);
        }
    }

    public class ChestManager
    {
        private List<Chest> chests = new List<Chest>();
        private List<Chest> deleteChests = new List<Chest>();
        private Random random = new Random();
        private float spawnTimer = 5f; // Time in seconds between chest spawns
        private float currentSpawnTime = 0.0f;
        public int maxChests = 25;

        private int commonChestPrice;
        private int rareChestPrice;
        private int legendaryChestPrice;

        Dictionary<IconType, Chest> chestPresets;
        Dictionary<IconType, IconType> chestToDropType = new Dictionary<IconType, IconType>()
        {
            [IconType.LegendaryChest] = IconType.LegendaryScroll,
            [IconType.RareChest] = IconType.RareScroll,
            [IconType.NormalChest] = IconType.CommonScroll,
        };

        public ChestManager()
        {
            chestPresets = ContentManager.Instance.Chests;
            commonChestPrice = 5;
            rareChestPrice = 10;
            legendaryChestPrice = 40;
        }
        public void Update()
        {
            float deltaTime = (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            currentSpawnTime += deltaTime;

            // Spawn a new chest if the timer has reached the spawn time
            if (currentSpawnTime >= spawnTimer && chests.Count < maxChests)
            {
                SpawnChest();
                currentSpawnTime = 0.0f;
            }

            foreach (Chest chest in chests)
            {
                chest.Update();
                if (chest.IsOpen)
                {
                    deleteChests.Add(chest);
                    GameObjects.Instance.DropManager.AddDrop(chestToDropType[chest.Icon.Type], chest.Position, false);
                }
            }

            for (int i = 0; i < deleteChests.Count; i++)
            {
                chests.Remove(deleteChests[i]);

            }
            deleteChests.Clear();
        }

        public void Draw()
        {
            foreach (Chest chest in chests)
            {
                chest.Draw();
            }
        }

        public void SpawnChest()
        {
            Vector2 spawnPosition = new Vector2(random.Next(Globals.Tilemap.Width), random.Next(Globals.Tilemap.Height));
            Rectangle mapSize = new Rectangle(Globals.Tilemap.X + 64, Globals.Tilemap.Y + 64, Globals.Tilemap.Width - 64, Globals.Tilemap.Height - 64);
            Vector2.Clamp(spawnPosition, new Vector2(mapSize.X, mapSize.Y), new Vector2(mapSize.Width, mapSize.Height));
            int roll = random.Next(100);
            int chestPrice;
            IconType chestType;
            if (roll < 10)
            {
                chestType = IconType.LegendaryChest;
                chestPrice = legendaryChestPrice;
            }
            else if (roll < 40)
            {
                chestType = IconType.RareChest;
                chestPrice = rareChestPrice;
            }
            else
            {
                chestType = IconType.NormalChest;
                chestPrice = commonChestPrice;
            }
            var chest = chestPresets[chestType].Clone();
            chest.Activate(spawnPosition, chestPrice);
            chests.Add(chest);
        }

    }

}
