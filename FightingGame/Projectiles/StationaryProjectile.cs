using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace FightingGame
{
    public class StationaryProjectile : Projectile
    {
        private Vector2 TopLeft;
        public StationaryProjectile(ProjectileType projectileType, int damage, Texture2D projectileTexture, List<FrameHelper> animationFrames, float animationSpeed, float scale) : base(projectileType, damage, projectileTexture, animationFrames, animationSpeed, scale)
        {
            ProjectileAnimation = new Animation(ProjectileTexture, AnimationSpeed, animationFrames);
        }
        public override void Activate(Vector2 position, Vector2 direction, float speed, int damage)
        {
            Damage = damage;
            ProjectileAnimation.Start();
            IsActive = true;
            Position = position;
        }
        public override void Update()
        {
            ProjectileAnimation.Update();
            UpdateHitbox();
        }

        public override void Draw()
        {
            //Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, Hitbox, Color.Red);
            ProjectileAnimation.Draw(Position, false, Scale, Color.White);
            if(ProjectileAnimation.IsAnimationDone)
            {
                IsActive = false;
            }
        }

        public override void Reset()
        {
            HitEntities.Clear();
            ProjectileAnimation.Restart();
            IsActive = false;
            HasHit = false;
        }
        public override Projectile Clone()
        {
            return new StationaryProjectile(ProjectileType, Damage, ProjectileTexture, ProjectileAnimation.AnimationFrames, AnimationSpeed, Scale);
        }
        private void UpdateHitbox()
        {
            TopLeft = Position - new Vector2(ProjectileAnimation.PreviousFrame.SourceRectangle.Width / 2, ProjectileAnimation.PreviousFrame.SourceRectangle.Height / 2);
            Hitbox = new Rectangle((int)TopLeft.X, (int)Position.Y, ProjectileAnimation.PreviousFrame.SourceRectangle.Width, ProjectileAnimation.PreviousFrame.SourceRectangle.Height);
        }
    }
}
