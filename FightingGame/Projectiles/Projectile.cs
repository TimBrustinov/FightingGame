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
        public ProjectileType ProjectileType;
        public Texture2D ProjectileTexture;
        public Animation FlightAnimation;
        public Animation HitAnimation;
        public Rectangle Hitbox;
        public Vector2 Direction;
        public Vector2 Position;
        private Vector2 startPosition;
        public float Speed;
        private float scale;
        
        public int Damage;
        public bool IsActive;
        public bool HasHit;

        public Projectile(ProjectileType projectileType, int damage, Texture2D projectileTexture, float animationSpeed, List<FrameHelper> frames, List<FrameHelper> hitFrames, float scale)
        {
            ProjectileType = projectileType;
            Damage = damage;
            FlightAnimation = new Animation(projectileTexture, animationSpeed, frames);
            HitAnimation = new Animation(projectileTexture, animationSpeed, hitFrames);
            ProjectileTexture = projectileTexture;
            IsActive = false;
            this.scale = scale;
        }

        public Projectile(Projectile projectile)
        {
            ProjectileType = projectile.ProjectileType;
            Damage = projectile.Damage;
            FlightAnimation = new Animation(projectile.FlightAnimation.Texture, projectile.FlightAnimation.frameTime, projectile.FlightAnimation.AnimationFrames);
            HitAnimation = new Animation(projectile.HitAnimation.Texture, projectile.HitAnimation.frameTime, projectile.HitAnimation.AnimationFrames);
            IsActive = false;
            scale = projectile.scale;
        }

        public void Activate(Vector2 position, Vector2 direction, float speed)
        {
            Position = position;
            startPosition = position;
            Direction = direction;
            Speed = speed;
            IsActive = true;
        }

        public void Update()
        {
            if(Vector2.Distance(Position, startPosition) > 400)
            {
                HasHit = true;
            }
            UpdateHitbox();
            if (!HasHit)
            {
                Position += Direction * Speed;
                FlightAnimation.Update();
            }
            else
            {
                HitAnimation.Update();
                Position += Direction * Speed / 2;
            }
            
        }

        public void Draw()
        {
            float rotationAngle = (float)Math.Atan2(Direction.Y, Direction.X);
            if(HasHit)
            {
                HitAnimation.Draw(Position, true, scale, rotationAngle, Color.White);
                if(HitAnimation.IsAnimationDone == true)
                {
                    IsActive = false;
                }
            }
            else
            {
                FlightAnimation.Draw(Position, true, scale, rotationAngle, Color.White);
            }
        }
        public void Reset()
        {
            IsActive = false;
            HasHit = false;
            Direction = Vector2.Zero;
            startPosition = Vector2.Zero;
            FlightAnimation.Restart();
            HitAnimation.Restart();
        }
        private void UpdateHitbox()
        {
            if(HasHit)
            {
                Hitbox = new Rectangle((int)Position.X, (int)Position.Y, HitAnimation.PreviousFrame.SourceRectangle.Width, HitAnimation.PreviousFrame.SourceRectangle.Height);
            }
            else
            {
                Hitbox = new Rectangle((int)Position.X, (int)Position.Y, FlightAnimation.PreviousFrame.SourceRectangle.Width, FlightAnimation.PreviousFrame.SourceRectangle.Height);
            }
        }
    }
}
