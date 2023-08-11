using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FightingGame
{
    public class Projectile
    {
        public Texture2D ProjectileTexture;
        public Animation FlightAnimation;
        public Animation HitAnimation;
        public Rectangle Hitbox;
        public Vector2 Direction;
        public Vector2 Position;
        public Point Dimensions;
        public float Speed;
        
        public int Damage;
        public bool IsActive;
        public bool HasHit;

        public Projectile(int damage, Texture2D projectileTexture, float animationSpeed, List<FrameHelper> frames, List<FrameHelper> hitFrames)
        {
            Damage = damage;
            FlightAnimation = new Animation(projectileTexture, animationSpeed, frames);
            Dimensions = new Point(frames[0].SourceRectangle.Width, frames[0].SourceRectangle.Height);
            HitAnimation = new Animation(projectileTexture, animationSpeed, hitFrames);
            ProjectileTexture = projectileTexture;
            IsActive = false;
        }

        public Projectile(Projectile projectile)
        {
            Damage = projectile.Damage;
            FlightAnimation = new Animation(projectile.FlightAnimation.Texture, projectile.FlightAnimation.frameTime, projectile.FlightAnimation.AnimationFrames);
            HitAnimation = new Animation(projectile.HitAnimation.Texture, projectile.HitAnimation.frameTime, projectile.HitAnimation.AnimationFrames);
            Dimensions = projectile.Dimensions;
            IsActive = false;
        }

        public void Activate(Vector2 position, Vector2 direction, float speed)
        {
            Position = position;
            Direction = direction;
            Hitbox = new Rectangle((int)position.X, (int)position.Y, Dimensions.X, Dimensions.Y);
            Speed = speed;
            IsActive = true;
        }

        public void Update()
        {

            Hitbox.X = (int)Position.X;
            Hitbox.Y = (int)Position.Y;
            if (!HasHit)
            {
                Position += Direction * Speed;
                FlightAnimation.Update();
            }
            else
            {
                HitAnimation.Update();
            }
            
        }

        public void Draw()
        {
            float rotationAngle = (float)Math.Atan2(Direction.Y, Direction.X);
            if(HasHit)
            {
                HitAnimation.Draw(Position, true, 1f, rotationAngle, Color.White);
                if(HitAnimation.IsAnimationDone == true)
                {
                    IsActive = false;
                }
            }
            else
            {
                FlightAnimation.Draw(Position, true, 1f, rotationAngle, Color.White);
            }
        }
    }
}
