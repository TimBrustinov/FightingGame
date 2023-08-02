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
        public List<FrameHelper> AnimationFrames;
        public bool CanBeCanceled;
        public float AnimationSpeed;
        //public Rectangle CurrentFrame;
        //public int AbilityDamage;
        //public int StaminaDrain;
        //public bool CanHit;
        //public bool IsDead = false;
        //public int AttackReach = 50;

        public EntityAction(AnimationType animationType, List<FrameHelper> frames, bool canBeCanceled, float animationSpeed)
        {
            AnimationType = animationType;
            AnimationFrames = frames;
            CanBeCanceled = canBeCanceled;
            AnimationSpeed = animationSpeed;
        }

        public abstract bool MetCondition(Entity entity);
        public abstract void Update(Entity entity);

        protected virtual void Move(Entity entity)
        {
            if (entity.Direction != Vector2.Zero)
            {
                entity.Position += Vector2.Normalize(entity.Direction) * entity.Speed;
            }
        }
    }
}
