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
        Card[] Cards = new Card[3];
        public override Screenum ScreenType { get; protected set; } = Screenum.CardSelectionScreen;
        public override bool IsActive { get; set; }
        public override bool CanBeDrawnUnder { get; set; } = true;
        
        public override void Initialize() 
        {
            Cards[0] = ContentManager.Instance.PowerUpCards[PowerUpType.HealthRegenAmmountIncrease];
            Cards[0].Position = new Vector2(300, 200);
            Cards[1] = ContentManager.Instance.PowerUpCards[PowerUpType.HealthRegenRateIncrease];
            Cards[1].Position = new Vector2(800, 200);
            Cards[2] = ContentManager.Instance.PowerUpCards[PowerUpType.Overshield];
            Cards[2].Position = new Vector2(1300, 200);
            //for (int i = 0; i < Cards.Length; i++)
            //{
            //    Cards[i] = ContentManager.Instance.PowerUpCards[PowerUpType.HealthRegenAmmountIncrease];
            //    Cards[i].Position = new Vector2(i * 200, 200);
            //}
        }

        public override void PreferedScreenSize(GraphicsDeviceManager graphics)
        {
            return;
        }

        public override Screenum Update(MouseState ms)
        {
            for (int i = 0; i < Cards.Length; i++)
            {
                if(Cards[i].GetMouseAction(ms) == ClickResult.LeftClicked)
                {
                    Cards[i].PowerUp.Invoke();
                    ScreenManager<Screenum>.Instance.GoBack();
                    return Screenum.GameScreen;
                }
            }
            return Screenum.CardSelectionScreen;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var card in Cards)
            {
                card.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
