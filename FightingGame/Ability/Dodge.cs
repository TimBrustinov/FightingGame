using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FightingGame
{
    public class Dodge : EntityAction
    {
        private int staminaDrain;
        private int dodgeSpeed;
        public Dodge(AnimationType animationType, List<FrameHelper> frames, bool canBeCanceled, float animationSpeed, int staminaDrain, int dodgeSpeed)
            : base(animationType, frames, canBeCanceled, animationSpeed)
        {
            this.staminaDrain = staminaDrain;
            this.dodgeSpeed = dodgeSpeed;
        }

        public override bool MetCondition(Entity entity)
        {
            if(entity.RemainingStamina - staminaDrain > 0)
            {
                if (entity.Animator.CurrentAction != this)
                {
                    entity.RemainingStamina -= staminaDrain;
                    entity.staminaTimer = 0;
                }
                return true;
            }
            return false;
            
        }
        public override void Update(Entity entity)
        {
            if (entity.Direction != Vector2.Zero)
            {
                entity.Position += Vector2.Normalize(entity.Direction) * (entity.Speed + dodgeSpeed);
            }
        }

    }
}
