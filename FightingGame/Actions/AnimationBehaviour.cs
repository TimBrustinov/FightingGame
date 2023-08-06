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
    public abstract class AnimationBehaviour
    {
        public Animation Animation;
        public AnimationType AnimationType;
        public AnimationBehaviour(AnimationType animationType, Animation animation)
        {
            AnimationType = animationType;
            Animation = animation;
        }
        public abstract void OnStateEnter(Animator animator);
        public abstract void OnStateUpdate(Animator animator);
        public abstract void OnStateExit(Animator animator);

    }
}
