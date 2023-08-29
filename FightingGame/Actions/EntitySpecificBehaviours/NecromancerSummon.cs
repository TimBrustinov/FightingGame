using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace FightingGame
{
    public class NecromancerSummon : AttackBehaviour
    {
        Rectangle summonFrame;
        bool hasSpawned = false;
        public NecromancerSummon(AnimationType animationType, float damage, int attackRange, int cooldown, bool canMove) : base(animationType, damage, attackRange, cooldown, canMove)
        {
            summonFrame = new Rectangle(1425, 288, 30, 47);
            IsRanged = true;
        }


        public override void OnStateEnter(Animator animator)
        {
            hasSpawned = false;
            if (animator.Entity.CooldownManager.AnimationCooldown.ContainsKey(AnimationType))
            {
                animator.Entity.CooldownManager.AnimationCooldown[AnimationType] = Cooldown;
            }
            base.OnStateEnter(animator);
        }

        public override void OnStateUpdate(Animator animator)
        {
            if(animator.CurrentAnimation.PreviousFrame.SourceRectangle == summonFrame && !hasSpawned)
            {
                Vector2 Position1 = new Vector2(animator.Entity.Position.X - 50, animator.Entity.Position.Y);
                Vector2 Position2 = new Vector2(animator.Entity.Position.X + 50, animator.Entity.Position.Y);
                SpawnEnemy(Position1);
                SpawnEnemy(Position2);
                hasSpawned = true;
            }
        }

        public override void OnStateExit(Animator animator)
        {
            animator.SetAnimation(AnimationType.Stand);
            hasSpawned = false;
            base.OnStateExit(animator);
        }

        public override AnimationBehaviour Clone()
        {
            return new NecromancerSummon(AnimationType, DamageCoefficent, AttackRange, Cooldown, canMove);
        }

        private void SpawnEnemy(Vector2 position)
        {
            var enemyFromReserve = GameObjects.Instance.EnemyManager.GetEnemyFromReserve(EntityName.Skeleton);
            if (enemyFromReserve != null)
            {
                enemyFromReserve.Reset();
                enemyFromReserve.Spawn(position);
                GameObjects.Instance.EnemyManager.EnemyPool.Add(enemyFromReserve);
                GameObjects.Instance.EnemyManager.ReservePool.Remove(enemyFromReserve);
            }
            else
            {
                var newEnemy = GameObjects.Instance.EnemyManager.SkeletonPreset.Clone();
                newEnemy.SetBounds(Globals.Tilemap);
                newEnemy.Spawn(position);
                GameObjects.Instance.EnemyManager.EnemyPool.Add(newEnemy);
            }
            GameObjects.Instance.EnemyManager.enemyPoolIndex += 1;
        }
    }
}
