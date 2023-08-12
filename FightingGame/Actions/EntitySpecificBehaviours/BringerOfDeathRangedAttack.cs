using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace FightingGame
{
    public class BringerOfDeathRangedAttack : AttackBehaviour
    {
        StationaryProjectile projectile;
        Rectangle projectileTriggerFrame;
        float projectileSpeed;
        
        public BringerOfDeathRangedAttack(AnimationType animationType, StationaryProjectile projectile, Rectangle projectileTriggerFrame, float projectileSpeed, int damage, int attackRange, int cooldown, bool canMove) : base(animationType, damage, attackRange, cooldown, canMove)
        {
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
            base.OnStateEnter(animator);
        }

        public override void OnStateUpdate(Animator animator)
        {
            if (animator.CurrentAnimation.PreviousFrame.SourceRectangle == projectileTriggerFrame && !projectile.IsActive)
            {
                Vector2 projectileAttachmentPoint = GameObjects.Instance.SelectedCharacter.Position - new Vector2(0, 70);
                GameObjects.Instance.ProjectileManager.AddEnemyProjectile(projectile);
                projectile.Activate(projectileAttachmentPoint);
            }
        }

        public override void OnStateExit(Animator animator)
        {
            animator.SetAnimation(AnimationType.Stand);
        }
        public override AnimationBehaviour Clone()
        {
            return new BringerOfDeathRangedAttack(AnimationType, projectile, projectileTriggerFrame, projectileSpeed, Damage, AttackRange, Cooldown, canMove);
        }
        public void GetProjectile()
        {
            if (GameObjects.Instance.ProjectileManager.ReserveEnemyProjectiles.Count > 0)
            {
                foreach (var projectile in GameObjects.Instance.ProjectileManager.ReserveEnemyProjectiles)
                {
                    if (projectile.ProjectileType == this.projectile.ProjectileType && projectile.GetType() == typeof(StationaryProjectile))
                    {
                        this.projectile = (StationaryProjectile)projectile;
                        this.projectile.Reset();
                        GameObjects.Instance.ProjectileManager.ReserveEnemyProjectiles.Remove(projectile);
                        return;
                    }
                }
            }
            projectile = (StationaryProjectile)projectile.Clone();
        }
    }
}
