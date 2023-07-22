using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FightingGame
{
    public class AbilityManager
    {
        public bool CanUseAbility;

        public List<AnimationType> AvailableAnimations = new List<AnimationType>();
        private Dictionary<AnimationType, Ability> abilities = new Dictionary<AnimationType, Ability>();
        public Dictionary<AnimationType, float> cooldowns = new Dictionary<AnimationType, float>();

        public void RegisterAbility(AnimationType type, Ability ability)
        {
            abilities.Add(type, ability);
            cooldowns.Add(type, ability.Cooldown);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var kvp in abilities)
            {
                AnimationType abilityType = kvp.Key;
                Ability ability = kvp.Value;
                float cooldown = cooldowns[abilityType];

                if (cooldown > 0f)
                {
                    cooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    cooldowns[abilityType] = Math.Max(cooldown, 0f);
                }
                else
                {
                    CanUseAbility = true;
                    cooldowns[abilityType] = ability.Cooldown;
                }
            }
        }
    }

}
