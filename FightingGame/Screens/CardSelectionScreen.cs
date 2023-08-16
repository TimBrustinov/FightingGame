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
            for (int i = 0; i < Cards.Length; i++)
            {
                Texture2D cardTexture = ContentManager.Instance.Pixel;
                Cards[i] = new Card(cardTexture, new Vector2(100 + i * 100, 100), new Vector2(100, 100), Color.White, PowerUps.Instance.MaxHealthIncrease);
            }
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
