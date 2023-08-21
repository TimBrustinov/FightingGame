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

        Random random = new Random();
        List<Card> CommonCards;
        List<Card> RareCards;
        List<Card> LegendaryCards;
        List<Card> DisplayCards;
        public CardSelectionScreen()
        {
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

        public override void PreferedScreenSize(GraphicsDeviceManager graphics)
        {
            return;
        }

        public override Screenum Update(MouseState ms)
        {
            for (int i = 0; i < 3; i++)
            {
                if(DisplayCards[i].GetMouseAction(ms) == ClickResult.LeftClicked)
                {
                    PowerUps.Instance.AddPowerUpIcon(DisplayCards[i].Icon);
                    DisplayCards[i].PowerUp.Invoke();
                    ScreenManager<Screenum>.Instance.GoBack();
                    return Screenum.GameScreen;
                }
            }
            return Screenum.CardSelectionScreen;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            float spacing = 100 + DisplayCards[0].Dimentions.X;
            for (int i = 0; i < DisplayCards.Count; i++)
            {
                DisplayCards[i].Position = new Vector2(i * spacing, 200);
                DisplayCards[i].Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
