using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FightingGame
{
    public class MovingProjectile : Projectile
    {
        public Animation ImpactAnimation;
        private Vector2 startPosition;
        private float scale;

        public MovingProjectile(ProjectileType projectileType, int damage, Texture2D projectileTexture, List<FrameHelper> animationFrames, List<FrameHelper> impactFrames, float animationSpeed, float scale) : base(projectileType, damage, projectileTexture, animationFrames, animationSpeed, scale)
        {
            ProjectileType = projectileType;
            Damage = damage;
            ImpactAnimation = new Animation(projectileTexture, animationSpeed, impactFrames);
            ProjectileTexture = projectileTexture;
            IsActive = false;
            this.scale = scale;
        }
       
        public void Activate(Vector2 position, Vector2 direction, float speed)
        {
            Position = position;
            startPosition = position;
            Direction = direction;
            Speed = speed;
            ProjectileAnimation.Start();
            IsActive = true;
        }
       

        public override void Update()
        {
            if (Vector2.Distance(Position, startPosition) > 400)
            {
                HasHit = true;
            }
            UpdateHitbox();
            if (!HasHit)
            {
                Position += Direction * Speed;
                ProjectileAnimation.Update();
            }
            else
            {
                ImpactAnimation.Start();
                ImpactAnimation.Update();
                Position += Direction * Speed / 2;
            }
        }

        public override void Draw()
        {
            float rotationAngle = (float)Math.Atan2(Direction.Y, Direction.X);
            if (HasHit)
            {
                ImpactAnimation.Draw(Position, true, scale, rotationAngle, Color.White);
                if (ImpactAnimation.IsAnimationDone == true)
                {
                    IsActive = false;
                }
            }
            else
            {
                ProjectileAnimation.Draw(Position, true, scale, rotationAngle, Color.White);
            }
        }

        public override void Reset()
        {
            IsActive = false;
            HasHit = false;
            Direction = Vector2.Zero;
            startPosition = Vector2.Zero;
            ProjectileAnimation.Restart();
            ImpactAnimation.Restart();
        }
        private void UpdateHitbox()
        {
            if (HasHit)
            {
                Hitbox = new Rectangle((int)Position.X, (int)Position.Y, ImpactAnimation.PreviousFrame.SourceRectangle.Width, ImpactAnimation.PreviousFrame.SourceRectangle.Height);
            }
            else
            {
                Hitbox = new Rectangle((int)Position.X, (int)Position.Y, ProjectileAnimation.PreviousFrame.SourceRectangle.Width, ProjectileAnimation.PreviousFrame.SourceRectangle.Height);
            }
        }

        public override Projectile Clone()
        {
            return new MovingProjectile(ProjectileType, Damage, ProjectileTexture, ProjectileAnimation.AnimationFrames, ImpactAnimation.AnimationFrames, AnimationSpeed, Scale);
        }
    }
}
