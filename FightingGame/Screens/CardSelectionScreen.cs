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
        List<Card> CommonCards;
        List<Card> RareCards;
        List<Card> LegendaryCards;
        List<Card> DisplayCards;

        private float CardScale = 0.5f;
        private float SizeIncreaseFactor; // 10% increase
        private float[] cardScales; // Initial scales for each card
        private int selectedCardIndex = 0;
        private bool isLeftKeyPressed = false;
        private bool isRightKeyPressed = false;
        private bool isEnterKeyPressed = false;
        private bool isSpaceKeyPressed = false;


        public CardSelectionScreen()
        {
            SizeIncreaseFactor = CardScale + 0.05f;
            cardScales = new float[] { CardScale, CardScale, CardScale };
            Position = new Vector2(500, 500);
            CommonCards = new List<Card>();
            RareCards = new List<Card>();
            LegendaryCards = new List<Card>();
            DisplayCards = new List<Card>();
            foreach (var card in ContentManager.Instance.PowerUpCards.Values)
            {
                if(card.Rarity == CardRarity.Common)
                {
                    CommonCards.Add(card);
                }
                else if(card.Rarity == CardRarity.Rare)
                {
                    RareCards.Add(card);
                }
                else
                {
                    LegendaryCards.Add(card);
                }
            }
        }
        public override void PreferedScreenSize(GraphicsDeviceManager graphics)
        {
            return;
        }
        public override void Initialize() 
        {
            DisplayCards.Clear();
            for (int i = 0; i < 3; i++)
            {
                DisplayCards.Add(chooseRandomCard());
            }
        }
        private Card chooseRandomCard()
        {
            double randomNumber = random.NextDouble();
            Card chosenCard = null;

            while (chosenCard == null)
            {
                if (randomNumber < 0.15)
                {
                    var num = random.Next(0, RareCards.Count);
                    chosenCard = RareCards[num];
                    // 15% chance for Rare cards
                }
                else if (randomNumber < 0.05)
                {
                    var num = random.Next(0, LegendaryCards.Count);
                    chosenCard = LegendaryCards[num];
                    // 5% chance for Legendary cards
                }
                else
                {
                    var num = random.Next(0, CommonCards.Count);
                    chosenCard = CommonCards[num];
                    // 80% chance for Common cards
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
            float spacing = 100 + DisplayCards[0].Dimentions.X;
            //spriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle(0, 0, 1920, 1080), Color.Gray);
            for (int i = 0; i < DisplayCards.Count; i++)
            {
                DisplayCards[i].Position = new Vector2(Position.X + i * spacing, Position.Y);
                DisplayCards[i].Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
