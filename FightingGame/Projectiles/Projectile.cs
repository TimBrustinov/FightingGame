using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FightingGame
{
    public abstract class Projectile 
    {
        public Animation ProjectileAnimation;
        public ProjectileType ProjectileType;
        public Texture2D ProjectileTexture;
        public Rectangle Hitbox;
        public Vector2 Direction;
        public Vector2 Position;
        public float Speed;
        public float Scale;
        public float AnimationSpeed;
        
        public int Damage;
        public bool IsActive;
        public bool HasHit;

        public Projectile(ProjectileType projectileType, int damage, Texture2D projectileTexture, List<FrameHelper> animationFrames, float animationSpeed, float scale)
        {
            ProjectileAnimation = new Animation(projectileTexture, animationSpeed, animationFrames);
            ProjectileType = projectileType;
            Damage = damage;
            ProjectileTexture = projectileTexture;
            AnimationSpeed = animationSpeed;
            this.Scale = scale;
            IsActive = false;
        }
        public abstract void Update();
        public abstract void Draw();
        public abstract void Reset();
        public abstract Projectile Clone();
    }
}
