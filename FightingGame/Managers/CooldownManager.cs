using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class CooldownManager
    {
        public Dictionary<AnimationType, float> AnimationCooldown;
        public Dictionary<AnimationType, float> MaxAnimationCooldown;
        public CooldownManager()
        {
            AnimationCooldown = new Dictionary<AnimationType, float>();
            MaxAnimationCooldown = new Dictionary<AnimationType, float>();
        }
        public void Update()
        {
            foreach (var animation in AnimationCooldown.Keys)
            {
                if(AnimationCooldown[animation] > 0)
                {
                    AnimationCooldown[animation] -= (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    AnimationCooldown[animation] = 0;
                }
            }
        }
    }
}
