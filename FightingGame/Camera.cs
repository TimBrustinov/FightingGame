﻿using Microsoft.Xna.Framework;
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
        public Camera(Viewport viewport)
        {
            Viewport = viewport;
            viewportCenter = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
            CameraView = new Rectangle(0, 0, viewport.Width, viewport.Height);
        }

        public void Update(Vector2 targetPosition, Rectangle map, float zoom)
        {
            // Adjust the target position based on the zoom level
            Zoom = zoom;
            var adjustedTargetPosition = new Vector2(targetPosition.X * zoom, targetPosition.Y * zoom);

            // Clamp the target position to the map boundaries
            adjustedTargetPosition.X = MathHelper.Clamp(adjustedTargetPosition.X, map.Left + Viewport.Width / (2f * zoom), map.Right - Viewport.Width / (2f * zoom));
            adjustedTargetPosition.Y = MathHelper.Clamp(adjustedTargetPosition.Y, map.Top + Viewport.Height / (2f * zoom), map.Bottom - Viewport.Height / (2f * zoom));

            CameraView.X = (int)adjustedTargetPosition.X;
            CameraView.Y = (int)adjustedTargetPosition.Y;
            var translationMatrix = Matrix.CreateTranslation(viewportCenter.X - adjustedTargetPosition.X, viewportCenter.Y - adjustedTargetPosition.Y, 0f);
            var zoomMatrix = Matrix.CreateScale(zoom);

            transform = translationMatrix * zoomMatrix;
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
