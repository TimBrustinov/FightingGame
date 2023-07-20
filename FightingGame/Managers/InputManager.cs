using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public static class InputManager
    {
        private static Vector2 direction;
        public static Vector2 Direction => direction;
        public static Vector2 targetPosition;
        public static bool Moving;
        public static bool MovingUp;
        public static bool MovingDown;

        public static bool IsMovingLeft = false;

        public static void Update()
        {
            direction = Vector2.Zero;
            var keyboardState = Keyboard.GetState();

            if (keyboardState.GetPressedKeyCount() > 0)
            {
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    direction.X--;
                    IsMovingLeft = true;
                }
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    direction.X++;
                    IsMovingLeft = false;
                }
                if (keyboardState.IsKeyDown(Keys.S))
                {
                    direction.Y++;
                }
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    direction.Y--;
                }
            }
            Moving = direction != Vector2.Zero;

            var mouseState = Mouse.GetState();
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                int mouseX = mouseState.X;
                int mouseY = mouseState.Y;
                targetPosition = new Vector2(mouseX, mouseY);
            }
        }
    }
}
