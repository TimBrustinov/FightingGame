using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class Death : EntityAction
    {
        public Death(AnimationType animationType, List<FrameHelper> frames, bool canBeCanceled, float animationSpeed) : base(animationType, frames, canBeCanceled, animationSpeed)
        {
        }

        public override bool MetCondition(Entity entity)
        {
            return entity.RemainingHealth <= 0;
        }
        public override void Update(Entity entity)
        {
            return;
        }
    }
}
