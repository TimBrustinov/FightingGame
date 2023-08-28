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
        ProjectileType projectileType;
        Rectangle projectileTriggerFrame;
        float projectileSpeed;
        
        public BringerOfDeathRangedAttack(AnimationType animationType, ProjectileType projectileType, Rectangle projectileTriggerFrame, float projectileSpeed, float damage, int attackRange, int cooldown, bool canMove) : base(animationType, damage, attackRange, cooldown, canMove)
        {
            this.projectileType = projectileType;
            this.projectileTriggerFrame = projectileTriggerFrame;
            this.projectileSpeed = projectileSpeed;
            IsRanged = true;
        }

        public override void OnStateEnter(Animator animator)
        {
            if (animator.Entity.CooldownManager.AnimationCooldown.ContainsKey(AnimationType))
            {
                animator.Entity.CooldownManager.AnimationCooldown[AnimationType] = Cooldown;
            }
            base.OnStateEnter(animator);
        }

        public override void OnStateUpdate(Animator animator)
        {
            if (animator.CurrentAnimation.PreviousFrame.SourceRectangle == projectileTriggerFrame)
            {
                Vector2 projectileAttachmentPoint = GameObjects.Instance.SelectedCharacter.Position - new Vector2(0, 50);
                GameObjects.Instance.ProjectileManager.AddEnemyProjectile(projectileType, projectileAttachmentPoint, Vector2.Zero, 0, (int)Damage);
            }
        }

        public override void OnStateExit(Animator animator)
        {
            animator.SetAnimation(AnimationType.Stand);
            base.OnStateExit(animator);
        }
        public override AnimationBehaviour Clone()
        {
            return new BringerOfDeathRangedAttack(AnimationType, projectileType, projectileTriggerFrame, projectileSpeed, Damage, AttackRange, Cooldown, canMove);
        }
    }
}
