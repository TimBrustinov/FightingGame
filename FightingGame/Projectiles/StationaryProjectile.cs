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
        public StationaryProjectile(ProjectileType projectileType, int damage, Texture2D projectileTexture, List<FrameHelper> animationFrames, float animationSpeed, float scale) : base(projectileType, damage, projectileTexture, animationFrames, animationSpeed, scale)
        {

        }

        public void Activate(Vector2 position)
        {
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
            ProjectileAnimation.Draw(Position, false, Scale, Color.White);
            if(ProjectileAnimation.IsAnimationDone)
            {
                IsActive = false;
            }
        }

        public override void Reset()
        {
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
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, ProjectileAnimation.PreviousFrame.SourceRectangle.Width, ProjectileAnimation.PreviousFrame.SourceRectangle.Height);
        }
    }
}
