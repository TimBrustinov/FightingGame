using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace FightingGame
{
    public class AssassinCultistAmbush : AttackBehaviour
    {
        Character selectedCharacter;
        Rectangle teleportFrame;
        public AssassinCultistAmbush(AnimationType animationType, float damage, int attackRange, int cooldown, bool canMove) : base(animationType, damage, attackRange, cooldown, canMove)
        {
            teleportFrame = new Rectangle(233, 238, 21, 20);
            IsRanged = true;
        }

        public override void OnStateEnter(Animator animator) 
        {
            selectedCharacter = GameObjects.Instance.SelectedCharacter;
            base.OnStateEnter(animator);
            if (animator.Entity.CooldownManager.AnimationCooldown.ContainsKey(AnimationType))
            {
                animator.Entity.CooldownManager.AnimationCooldown[AnimationType] = Cooldown;
            }
        }
        public override void OnStateUpdate(Animator animator)
        {
            if(animator.CurrentAnimation.CurrerntFrame.SourceRectangle == teleportFrame)
            {
                animator.CurrentAnimation.frameTime = 0.08f;
                if(selectedCharacter.IsFacingLeft)
                {
                    animator.Entity.Position = new Vector2(selectedCharacter.Position.X + 20, selectedCharacter.Position.Y);
                }
                else
                {
                    animator.Entity.Position = new Vector2(selectedCharacter.Position.X - 20, selectedCharacter.Position.Y);
                }
            }
        }
        public override void OnStateExit(Animator animator) 
        {
            animator.CurrentAnimation.frameTime = 0.1f;
            base.OnStateExit(animator);
            animator.SetAnimation(AnimationType.Stand);
        }

        public override AnimationBehaviour Clone()
        {
            return new AssassinCultistAmbush(AnimationType, DamageCoefficent, AttackRange, Cooldown, canMove);
        }
    }
}
