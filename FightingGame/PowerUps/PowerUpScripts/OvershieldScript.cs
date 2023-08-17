using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame 
{
    public class OvershieldScript : PowerUpScript
    {
        private float overshieldRechargeTimer;
        public OvershieldScript(PowerUpType type) : base(type)
        {

        }

        public override void Update()
        {
            var selectedCharacter = GameObjects.Instance.SelectedCharacter;
            overshieldRechargeTimer += (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            if(overshieldRechargeTimer >= 5 && selectedCharacter.Overshield < selectedCharacter.MaxOvershield)
            {
                selectedCharacter.Overshield++;
            }

            if(selectedCharacter.Overshield >= selectedCharacter.MaxOvershield || selectedCharacter.HasBeenHit)
            {
                overshieldRechargeTimer = 0;
            }
        }
        
    }
}
