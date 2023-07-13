using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame.Enemies
{
    public class Zombie : Enemy
    {
        public Zombie(EnemyName name, Texture2D texture) : base(name, texture)
        {

        }
        protected override void NeutralAttack()
        {
            savedAnimaton = AnimationType.NeutralAttack;
            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
            {
                savedAnimaton = AnimationType.None;
            }
        }
        
    }
}
