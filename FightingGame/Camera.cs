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
        public Vector2 viewportCenter;
        public Viewport Viewport;
        public Rectangle CameraView;
        public float Zoom;
        private Matrix transform;
        public Vector2 Corner;

        public Camera(Viewport viewport)
        {
            Viewport = viewport;
            viewportCenter = new Vector2(viewport.Width / 2, viewport.Height / 2);
            CameraView = new Rectangle(0, 0, viewport.Width, viewport.Height);
            Corner = Vector2.Zero;
        }

        public void Update(Vector2 targetPosition, Rectangle map)
        {

            // Clamp the target position to the map boundaries
            targetPosition.X = MathHelper.Clamp(targetPosition.X, map.Left + Viewport.Width / 2, map.Right - Viewport.Width / 2);
            targetPosition.Y = MathHelper.Clamp(targetPosition.Y, map.Top + Viewport.Height / 2, map.Bottom - Viewport.Height / 2);

            Corner.X = targetPosition.X - CameraView.Width/2;
            Corner.Y = targetPosition.Y - CameraView.Height/2;

            CameraView.X = (int)targetPosition.X;
            CameraView.Y = (int)targetPosition.Y;
            var translationMatrix = Matrix.CreateTranslation(viewportCenter.X - targetPosition.X, viewportCenter.Y - targetPosition.Y, 0f);

            transform = translationMatrix;
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition, float zoom)
        {
            var invertedTransform = Matrix.Invert(GetTransformMatrix());
            return Vector2.Transform(screenPosition, invertedTransform) / zoom;
        }

        public Matrix GetTransformMatrix()
        {
            return transform;
        }
    }

}
