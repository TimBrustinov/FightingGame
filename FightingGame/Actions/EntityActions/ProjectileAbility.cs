using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FightingGame.Actions.EntityActions
{
    public class ProjectileAbility : Attack
    {
        Vector2 AttachmentPoint;
        Rectangle LaunchFrame;
        Projectile Projectile;

        public ProjectileAbility(AnimationType animationType, List<FrameHelper> frames, bool canBeCanceled, float animationSpeed, int cooldown, float attackRange, int attackDamage, bool canMove, Projectile projectile, Vector2 attachmentPoint, Rectangle launchFrame) : base(animationType, frames, canBeCanceled, animationSpeed, cooldown, attackRange, attackDamage, canMove)
        {
            AttachmentPoint = attachmentPoint;
            LaunchFrame = launchFrame;
            Projectile = projectile;
        }

        public override bool MetCondition(Entity entity)
        {
            return true;
        }

        public override void Update(Entity entity)
        {
            if(entity.Animator.CurrentAnimation.CurrerntFrame.SourceRectangle == LaunchFrame)
            {

            }
        }
    }
}
