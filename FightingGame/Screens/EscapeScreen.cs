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
    class EscapeScreen : Screen<Screenum>
    {
        public override Screenum ScreenType { get; protected set; }
        public override bool IsActive { get; set; }
        public override bool CanBeDrawnUnder { get; set; } = true;

        //private Vector2 

        public override void PreferedScreenSize(GraphicsDeviceManager graphics)
        {
            throw new NotImplementedException();
        }
        public override void Initialize()
        {
            
        }
        public override Screenum Update(MouseState ms)
        {
            throw new NotImplementedException();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
