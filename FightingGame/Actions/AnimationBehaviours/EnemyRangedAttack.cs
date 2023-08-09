using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FightingGame
{
   
    public class EnemyRangedAttack : AttackBehaviour
    {
        Projectile projectile;
        private Animation animation;
        Vector2 projectileAttachmentPoint;
        Rectangle projectileTriggerFrame;
        float projectileSpeed;

        public EnemyRangedAttack(AnimationType animationType, Animation projectileAnimation, Vector2 projectileAttachmentPoint, Rectangle projectileTriggerFrame, float projectileSpeed, int damage, int attackRange, int cooldown, bool canMove) : base(animationType, damage, attackRange, cooldown, canMove)
        {
            this.projectileAttachmentPoint = projectileAttachmentPoint;
            this.projectileTriggerFrame = projectileTriggerFrame;
            this.projectileSpeed = projectileSpeed;
            animation = projectileAnimation;
            IsRanged = true;
        }
        public override void OnStateEnter(Animator animator)
        {
            projectile = new Projectile(Damage, ContentManager.Instance.EntitySpriteSheets[animator.Entity.Name], animation.frameTime, animation.AnimationFrames);
            if (animator.Entity.CooldownManager.AnimationCooldown.ContainsKey(AnimationType))
            {
                animator.Entity.CooldownManager.AnimationCooldown[AnimationType] = Cooldown;
            }
            return;
        }
        public override void OnStateUpdate(Animator animator)
        {
            if(animator.CurrentAnimation.PreviousFrame.SourceRectangle == projectileTriggerFrame && !projectile.IsActive)
            {
                Vector2 relativePosition = Vector2.Zero;
                if(animator.Entity.IsFacingLeft)
                {
                    relativePosition.X = animator.Entity.TopRight.X - projectileTriggerFrame.X;
                }
                else
                {
                    relativePosition.X = animator.Entity.TopLeft.X - projectileTriggerFrame.X;
                }
                relativePosition.Y = animator.Entity.TopLeft.Y - projectileTriggerFrame.Y;
                Vector2 attachmentPointRelative = projectileAttachmentPoint + relativePosition;

                Console.WriteLine(GameObjects.Instance.SelectedCharacter.Position);
                Vector2 projectileDirection = Vector2.Normalize(GameObjects.Instance.SelectedCharacter.Position - attachmentPointRelative);
                GameObjects.Instance.ProjectileManager.AddProjectile(projectile);
                projectile.Activate(attachmentPointRelative, projectileDirection, projectileSpeed);
            }
        }
        public override void OnStateExit(Animator animator)
        {
            animator.SetAnimation(AnimationType.Stand);
        }
        public override AnimationBehaviour Clone()
        {
            return new EnemyRangedAttack(AnimationType, animation, projectileAttachmentPoint, projectileTriggerFrame, projectileSpeed, Damage, AttackRange, Cooldown, canMove);
        }
    }
}
