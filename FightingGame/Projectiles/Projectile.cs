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
        public Animation Animation;
        public Vector2 Direction;
        public Vector2 Position;
        public float Speed;
        
        public int Damage;
        public bool IsActive;
        private bool movingLeft;

        public Projectile(int damage, Texture2D projectileTexture, float animationSpeed, List<FrameHelper> frames)
        {
            Damage = damage;
            Animation = new Animation(projectileTexture, animationSpeed, frames);
            IsActive = false;
        }

        public Projectile(Projectile projectile)
        {
            Damage = projectile.Damage;
            Animation = projectile.Animation;
            IsActive = false;
        }

        public void Activate(Vector2 position, Vector2 direction, float speed)
        {
            Position = position;
            Direction = direction;
            Speed = speed;
            IsActive = true;
        }

        public void Update()
        {
            Position += Direction * Speed;
            Animation.Update();
        }

        public void Draw()
        {
            movingLeft = Direction.X < 0;
            Animation.Draw(Position, movingLeft, 1f, Color.White);
        }
    }
}
