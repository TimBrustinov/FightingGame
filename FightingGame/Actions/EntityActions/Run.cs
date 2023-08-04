using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FightingGame
{
    public class Run : EntityAction
    {
        public Run(AnimationType animationType, List<FrameHelper> frames, bool canBeCanceled, float animationSpeed) : base(animationType, frames, canBeCanceled, animationSpeed)
        {
        }

        public override bool MetCondition(Entity entity)
        {
            if (entity.Direction != Vector2.Zero)
            {
                return true;
            }
            return false;
        }

        public override void Update(Entity entity)
        {
            if (entity.Direction != Vector2.Zero)
            {
                entity.Position += Vector2.Normalize(entity.Direction) * entity.Speed;
            }
        }
    }
}
