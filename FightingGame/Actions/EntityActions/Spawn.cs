using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class Spawn : EntityAction
    {
        public Spawn(AnimationType animationType, List<FrameHelper> frames, bool canBeCanceled, float animationSpeed) : base(animationType, frames, canBeCanceled, animationSpeed)
        {
        }

        public override bool MetCondition(Entity entity)
        {
            return true;
        }

        public override void Update(Entity entity)
        {
            return;
        }
    }
}
