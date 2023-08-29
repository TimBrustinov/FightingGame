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
        ProjectileType projectileType;
        Vector2 projectileAttachmentPoint;
        Rectangle projectileTriggerFrame;
        float projectileSpeed;

        public EnemyRangedAttack(AnimationType animationType, ProjectileType projectileType, Vector2 projectileAttachmentPoint, Rectangle projectileTriggerFrame, float projectileSpeed, float damage, int attackRange, int cooldown, bool canMove) : base(animationType, damage, attackRange, cooldown, canMove)
        {
            this.projectileType = projectileType;
            this.projectileAttachmentPoint = projectileAttachmentPoint;
            this.projectileTriggerFrame = projectileTriggerFrame;
            this.projectileSpeed = projectileSpeed;
            IsRanged = true;
        }
        public override void OnStateEnter(Animator animator)
        {
            animator.Entity.IsAttacking = true;
            if (animator.Entity.CooldownManager.AnimationCooldown.ContainsKey(AnimationType))
            {
                animator.Entity.CooldownManager.AnimationCooldown[AnimationType] = Cooldown;
            }
            base.OnStateEnter(animator);
            return;
        }
        public override void OnStateUpdate(Animator animator)
        {
            if(animator.CurrentAnimation.PreviousFrame.SourceRectangle == projectileTriggerFrame)
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

                Vector2 projectileDirection = Vector2.Normalize(GameObjects.Instance.SelectedCharacter.Position - attachmentPointRelative);
                GameObjects.Instance.ProjectileManager.AddEnemyProjectile(projectileType, attachmentPointRelative, projectileDirection, projectileSpeed, (int)Damage);
            }
        }
        public override void OnStateExit(Animator animator)
        {
            animator.Entity.IsAttacking = false;
            animator.SetAnimation(AnimationType.Stand);
            base.OnStateExit(animator);
        }
        public override AnimationBehaviour Clone()
        {
            return new EnemyRangedAttack(AnimationType, projectileType, projectileAttachmentPoint, projectileTriggerFrame, projectileSpeed, DamageCoefficent, AttackRange, Cooldown, canMove);
        }
        //public void GetProjectile()
        //{
        //    if (GameObjects.Instance.ProjectileManager.ReserveProjectiles.Count > 0)
        //    {
        //        foreach (var projectile in GameObjects.Instance.ProjectileManager.ReserveProjectiles)
        //        {
        //            if (projectile.ProjectileType == this.projectile.ProjectileType && projectile.GetType() == typeof(MovingProjectile))
        //            {
        //                this.projectile = (MovingProjectile)projectile;
        //                this.projectile.Reset();
        //                GameObjects.Instance.ProjectileManager.ReserveProjectiles.Remove(projectile);
        //                return;
        //            }
        //        }
        //    }
        //    projectile = (MovingProjectile)projectile.Clone();
        //}
    }
}
