﻿using System;
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
        Vector2 projectileAttachmentPoint;
        Rectangle projectileTriggerFrame;
        float projectileSpeed;

        public EnemyRangedAttack(AnimationType animationType, Projectile projectile, Vector2 projectileAttachmentPoint, Rectangle projectileTriggerFrame, float projectileSpeed, int damage, int attackRange, int cooldown, bool canMove) : base(animationType, damage, attackRange, cooldown, canMove)
        {
            this.projectileAttachmentPoint = projectileAttachmentPoint;
            this.projectileTriggerFrame = projectileTriggerFrame;
            this.projectileSpeed = projectileSpeed;
            this.projectile = projectile;
            IsRanged = true;
        }
        public override void OnStateEnter(Animator animator)
        {
            GetProjectile();
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

                Vector2 projectileDirection = Vector2.Normalize(GameObjects.Instance.SelectedCharacter.Position - attachmentPointRelative);
                GameObjects.Instance.ProjectileManager.AddEnemyProjectile(projectile);
                projectile.Activate(attachmentPointRelative, projectileDirection, projectileSpeed);
            }
        }
        public override void OnStateExit(Animator animator)
        {
            animator.SetAnimation(AnimationType.Stand);
        }
        public override AnimationBehaviour Clone()
        {
            return new EnemyRangedAttack(AnimationType, projectile, projectileAttachmentPoint, projectileTriggerFrame, projectileSpeed, Damage, AttackRange, Cooldown, canMove);
        }


        public void GetProjectile()
        {
            if (GameObjects.Instance.ProjectileManager.ReserveEnemyProjectiles.Count > 0)
            {
                foreach (var projectile in GameObjects.Instance.ProjectileManager.ReserveEnemyProjectiles)
                {
                    if (projectile.ProjectileType == this.projectile.ProjectileType)
                    {
                        this.projectile = projectile;
                        this.projectile.Reset();
                        GameObjects.Instance.ProjectileManager.ReserveEnemyProjectiles.Remove(projectile);
                        return;
                    }
                }
            }
            projectile = new Projectile(projectile);
        }
    }
}