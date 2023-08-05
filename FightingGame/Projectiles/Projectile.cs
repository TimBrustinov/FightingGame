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

        public Projectile(int damage, List<FrameHelper> frames, Texture2D projectileTexture, float animationSpeed)
        {
            Damage = damage;
            Animation = new Animation(projectileTexture, true, animationSpeed, frames);
            IsActive = false;
        }

        public void Activate(Vector2 position, Vector2 direction)
        {
            Position = position;
            Direction = direction;
            IsActive = true;
        }

        public void Update()
        {
            Position += Direction * Speed;
        }
    }
}
