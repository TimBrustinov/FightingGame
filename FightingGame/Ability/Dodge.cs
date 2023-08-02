using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class Dodge : EntityAction
    {
        private int staminaDrain;
        public Dodge(AnimationType animationType, float cooldown, int staminaDrain) : base(animationType, cooldown)
        {
            this.staminaDrain = staminaDrain;
        }

        public override void Update(Entity entity)
        {
            if(entity.RemainingStamina - staminaDrain < 0)
            {
                //entity.canPerformAttack = false;
            }
            else
            {
                Move(entity);
                //entity.canPerformAttack = true;
            }
        }
    }
}
