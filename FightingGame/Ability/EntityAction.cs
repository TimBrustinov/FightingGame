using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FightingGame
{
    public abstract class EntityAction
    {
        public AnimationType AnimationType;
        public float Cooldown;
        //public Rectangle CurrentFrame;
        //public int AbilityDamage;
        //public int StaminaDrain;
        //public bool CanHit;
        //public bool IsDead = false;
        //public int AttackReach = 50;

        public EntityAction(AnimationType animationType, float cooldown)
        {
            AnimationType = animationType;
            Cooldown = cooldown;
        }
        public abstract void Update(Entity entity);

        protected void Move(Entity entity)
        {
            if (entity.Direction != Vector2.Zero)
            {
                entity.Position += Vector2.Normalize(entity.Direction) * entity.Speed;
            }
        }
    }
}
