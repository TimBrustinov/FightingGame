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
    public class CardSelectionScreen : Screen<Screenum>
    {
        public override Screenum ScreenType { get; protected set; } = Screenum.CardSelectionScreen;
        public override bool IsActive { get; set; }
        public override bool CanBeDrawnUnder { get; set; } = true;
        Vector2 Position;
        Random random = new Random();
        Dictionary<Rarity, List<Card>> Cards;
       
        
        List<Card> DisplayCards;

        private float CardScale = 0.5f;
        private Point CardDimentions;


        private float SizeIncreaseFactor; // 10% increase
        private float[] cardScales; // Initial scales for each card
        private int selectedCardIndex = 0;
        private bool isLeftKeyPressed = false;
        private bool isRightKeyPressed = false;
        private bool isEnterKeyPressed = false;
        private bool isSpaceKeyPressed = false;


        public CardSelectionScreen()
        {
            CardDimentions = new Point((int)(ContentManager.Instance.PowerUpCards[PowerUpType.CriticalChanceIncrease].Dimentions.X * CardScale), (int)(ContentManager.Instance.PowerUpCards[PowerUpType.CriticalChanceIncrease].Dimentions.Y * CardScale));
            SizeIncreaseFactor = CardScale + 0.05f;
            
            cardScales = new float[] { CardScale, CardScale, CardScale };
            Position = new Vector2(500, 500);
            DisplayCards = new List<Card>();
            Cards = new Dictionary<Rarity, List<Card>>();

            List<Card> CommonCards = new List<Card>();
            List<Card> RareCards = new List<Card>();
            List<Card> LegendaryCards = new List<Card>();
            foreach (var card in ContentManager.Instance.PowerUpCards.Values)
            {
                if(card.Rarity == Rarity.Common)
                {
                    CommonCards.Add(card);
                }
                else if(card.Rarity == Rarity.Rare)
                {
                    RareCards.Add(card);
                }
                else
                {
                    LegendaryCards.Add(card);
                }
            }
            Cards.Add(Rarity.Common, CommonCards);
            Cards.Add(Rarity.Rare, RareCards);
            Cards.Add(Rarity.Legendary, LegendaryCards);
        }
        public override void PreferedScreenSize(GraphicsDeviceManager graphics)
        {
            return;
        }
        public override void Initialize() 
        {
            DisplayCards.Clear();
            if(GameObjects.Instance.DropManager.SelectedRarity != Rarity.None)
            {
                Rarity selectedRarity = GameObjects.Instance.DropManager.SelectedRarity;
                Card chosenCard = null;
                while(DisplayCards.Count < 3)
                {
                    var num = random.Next(0, Cards[selectedRarity].Count);
                    chosenCard = Cards[selectedRarity][num];

                    if(!DisplayCards.Contains(chosenCard))
                    {
                        DisplayCards.Add(chosenCard);
                    }
                }
                GameObjects.Instance.DropManager.SelectedRarity = Rarity.None;
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    DisplayCards.Add(chooseRandomCard());
                }
            }
            Vector2 screenCenter = new Vector2(Globals.GraphicsDevice.Viewport.Width / 2, Globals.GraphicsDevice.Viewport.Height / 2);

            // Set the position of the center card
            DisplayCards[1].Position = screenCenter;

            // Calculate the horizontal offset for the side cards
            float cardSpacing = 300; // You can adjust this value as needed
            float totalWidth = DisplayCards[1].Dimentions.X + 2 * cardSpacing;

            // Set the positions of the left and right cards relative to the center card
            DisplayCards[0].Position = new Vector2(screenCenter.X - totalWidth / 2, screenCenter.Y);
            DisplayCards[2].Position = new Vector2(screenCenter.X + totalWidth / 2, screenCenter.Y);
        }
        private Card chooseRandomCard()
        {
            double randomNumber = random.NextDouble();
            Card chosenCard = null;

            while (chosenCard == null)
            {
                if (randomNumber < 0.04)
                {
                    var num = random.Next(0, Cards[Rarity.Legendary].Count);
                    chosenCard = Cards[Rarity.Legendary][num];
                }
                else if (randomNumber < 0.15)
                {
                    var num = random.Next(0, Cards[Rarity.Rare].Count);
                    chosenCard = Cards[Rarity.Rare][num];
                }
                else
                {
                    var num = random.Next(0, Cards[Rarity.Common].Count);
                    chosenCard = Cards[Rarity.Common][num];
                }

                // Check if the chosen card is already in DisplayCards
                if (DisplayCards.Contains(chosenCard))
                {
                    chosenCard = null; // Retry with a different card
                }
            }

            return chosenCard;
        }
        public override Screenum Update(MouseState ms)
        {
            KeyboardState ks = Keyboard.GetState();

            // Check for left arrow key press ('a') only once when it's initially pressed
            if (ks.IsKeyDown(Keys.A) && !isLeftKeyPressed)
            {
                isLeftKeyPressed = true;
                selectedCardIndex = (selectedCardIndex - 1 + 3) % 3;
            }
            else if (ks.IsKeyUp(Keys.A))
            {
                isLeftKeyPressed = false;
            }

            // Check for right arrow key press ('d') only once when it's initially pressed
            if (ks.IsKeyDown(Keys.D) && !isRightKeyPressed)
            {
                isRightKeyPressed = true;
                selectedCardIndex = (selectedCardIndex + 1) % 3;
            }
            else if (ks.IsKeyUp(Keys.D))
            {
                isRightKeyPressed = false;
            }

            // Check for Enter key press only once when it's initially pressed
            if ((ks.IsKeyDown(Keys.Enter) && !isEnterKeyPressed) || (ks.IsKeyDown(Keys.Space) && !isSpaceKeyPressed))
            {
                isSpaceKeyPressed = true;
                isEnterKeyPressed = true;
                PowerUps.Instance.AddPowerUpIcon(DisplayCards[selectedCardIndex].Icon);
                DisplayCards[selectedCardIndex].PowerUp.Invoke();
                ScreenManager<Screenum>.Instance.GoBack();
                return Screenum.GameScreen;
            }
            else if (ks.IsKeyUp(Keys.Enter) || ks.IsKeyUp(Keys.Space))
            {
                isEnterKeyPressed = false;
                isSpaceKeyPressed = false;
            }

            for (int i = 0; i < DisplayCards.Count; i++)
            {
                if (i == selectedCardIndex)
                {
                    // Increase the size of the selected card gradually
                    cardScales[i] = MathHelper.Lerp(cardScales[i], SizeIncreaseFactor, 0.1f); // Adjust the lerp speed if needed
                }
                else
                {
                    // Return unselected cards to their original size gradually
                    cardScales[i] = MathHelper.Lerp(cardScales[i], CardScale, 0.1f); // Adjust the lerp speed if needed
                }

                // Apply the scale to the card
                DisplayCards[i].Scale = cardScales[i];
            }

            return Screenum.CardSelectionScreen;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle(0, 0, Globals.GraphicsDevice.Viewport.Width, Globals.GraphicsDevice.Viewport.Height), new Color(30, 30, 30, 160));

           // float spacing = 100 + DisplayCards[0].Dimentions.X;
            //spriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle(0, 0, 1920, 1080), Color.Gray);
            for (int i = 0; i < DisplayCards.Count; i++)
            {
                //DisplayCards[i].Position = new Vector2(Position.X + i * spacing, Position.Y);
                DisplayCards[i].Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
