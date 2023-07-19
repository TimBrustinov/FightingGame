using FightingGame.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class Camera
    {
        private readonly Vector2 viewportCenter;
        //private readonly float zoom;
        private Matrix transform;

        public Camera(Viewport viewport)
        {
            viewportCenter = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
        }

        public void Update(Vector2 targetPosition, float zoom)
        {
            // Adjust the target position based on the zoom level
            var adjustedTargetPosition = new Vector2(targetPosition.X * zoom, targetPosition.Y * zoom);

            var translationMatrix = Matrix.CreateTranslation(viewportCenter.X - adjustedTargetPosition.X, viewportCenter.Y - adjustedTargetPosition.Y, 0f);
            var zoomMatrix = Matrix.CreateScale(zoom);

            transform = translationMatrix * zoomMatrix;
        }


        public Matrix GetTransformMatrix()
        {
            return transform;
        }
    }

}
