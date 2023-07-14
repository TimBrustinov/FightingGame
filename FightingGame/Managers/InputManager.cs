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
        public static bool Moving;
        public static bool MovingUp;
        public static bool MovingDown;

        public static bool IsMovingLeft = false;

        public static void Update(List<Keys> forbiddenDirections)
        {
            direction = Vector2.Zero;
            var keyboardState = Keyboard.GetState();

            if (keyboardState.GetPressedKeyCount() > 0)
            {
                if (keyboardState.IsKeyDown(Keys.A) && !forbiddenDirections.Contains(Keys.A))
                {
                    direction.X--;
                    IsMovingLeft = true;
                }
                if (keyboardState.IsKeyDown(Keys.D) && !forbiddenDirections.Contains(Keys.D))
                {
                    direction.X++;
                    IsMovingLeft = false;
                }
                if (keyboardState.IsKeyDown(Keys.Space) && !forbiddenDirections.Contains(Keys.Space))
                {
                    direction.Y--;
                }
                if (keyboardState.IsKeyDown(Keys.S) && !forbiddenDirections.Contains(Keys.S))
                {
                    direction.Y++;
                }
                if (keyboardState.IsKeyDown(Keys.W) && !forbiddenDirections.Contains(Keys.W))
                {
                    direction.Y--;
                }
            }

            if (direction.Y > 0)
            {
                MovingDown = true;
                MovingUp = false;
            }
            else if(direction.Y < 0)
            {
                MovingUp = true;
                MovingDown = false;
            }
            else
            {
                MovingUp = false;
                MovingDown = false;
                Moving = direction != Vector2.Zero;
            }
        }
    }
}
