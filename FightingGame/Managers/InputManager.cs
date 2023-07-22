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
        public static Vector2 MousePosition;
        public static Vector2 Direction => direction;
        public static bool Moving;
        public static bool IsMovingLeft = false;
        public static bool IsMovingUp = false;

        public static void Update(Camera camera, Entity entity)
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
                    IsMovingUp = false;
                }
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    direction.Y--;
                    IsMovingUp = true;
                }
            }
            // Use mouse input for facing direction
            MouseState ms = Mouse.GetState();
            Vector2 msPosition = new Vector2(ms.X, ms.Y);
            MousePosition = camera.ScreenToWorld(msPosition, camera.Zoom);

            // Determine facing direction based on the character's position and the mouse position
            //IsMovingLeft = MousePosition.X < entity.Position.X;
            Moving = direction != Vector2.Zero;
        }
    }
}
