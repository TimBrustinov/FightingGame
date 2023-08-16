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
        
        public override void Initialize()
        {
            return;
        }

        public override void PreferedScreenSize(GraphicsDeviceManager graphics)
        {
            return;
        }

        public override Screenum Update(MouseState ms)
        {
            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                ScreenManager<Screenum>.Instance.GoBack();
                return Screenum.GameScreen;
            }
            return Screenum.CardSelectionScreen;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            return;
        }

        

        
    }
}
