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
        public AnimationType AnimationType;
        public bool IsDone { get; private set; }
        public AnimationBehaviour(AnimationType animationType)
        {
            AnimationType = animationType;
        }
        public virtual void OnStateEnter(Animator animator)
        {
            IsDone = false;
        }
        public abstract void OnStateUpdate(Animator animator);
        public virtual void OnStateExit(Animator animator)
        {
            IsDone = true;
        }
        public abstract AnimationBehaviour Clone();

    }
}
