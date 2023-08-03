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

        public EntityAction(AnimationType animationType, List<FrameHelper> frames, bool canBeCanceled, float animationSpeed)
        {
            AnimationType = animationType;
            AnimationFrames = frames;
            CanBeCanceled = canBeCanceled;
            AnimationSpeed = animationSpeed;
        }

        public abstract bool MetCondition(Entity entity);
        public virtual AnimationType TransitionBack()
        {
            return AnimationType.Stand;
        }
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
