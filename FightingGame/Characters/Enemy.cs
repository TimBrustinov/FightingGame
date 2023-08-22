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
    public class Enemy : Entity
    {
        public int WaveNum;
        public int XPAmmount = 5;
        public bool IsSpawning;
        public Color HealthBarColor = Color.Green;
        public bool IsBoss;
        private Vector2 direction;
        public bool leftFacingSprite;

        public Enemy(EntityName name, bool isBoss, float health, float speed, float scale, bool leftFacingSprite, int waveNum) : base(name)
        {
            Rectangle characterRectangle = ContentManager.Instance.EntityTextures[name];
            EntityScale = scale;
            Position = new Vector2(1000, 350);
            Dimentions = new Vector2(characterRectangle.Width, characterRectangle.Height) * EntityScale;

            Speed = speed;
            TotalHealth = health;
            RemainingHealth = TotalHealth;
            IsBoss = isBoss;
            this.leftFacingSprite = leftFacingSprite;
            WaveNum = waveNum;
        }
        public Enemy(Enemy enemy) : base(enemy.Name)
        {
            Rectangle characterRectangle = ContentManager.Instance.EntityTextures[enemy.Name];
            EntityScale = enemy.EntityScale;
            Position = new Vector2(1000, 350);
            Dimentions = new Vector2(characterRectangle.Width, characterRectangle.Height) * EntityScale;
            Speed = enemy.Speed;
            TotalHealth = enemy.TotalHealth;
            RemainingHealth = TotalHealth;
            this.leftFacingSprite = enemy.leftFacingSprite;
            WaveNum = enemy.WaveNum;
        }
        public void Update(Character character)
        {
            direction = Vector2.Normalize(character.Position - Position);
            if (character.WeaponHitBox.Intersects(HitBox))
            {
                if (character.HasFrameChanged)
                {
                    HasBeenHit = false;
                }
                if (character.Animator.CurrentAnimation != null && character.Animator.CurrentAnimation.CurrerntFrame.CanHit && !HasBeenHit)
                {
                    TakeDamage(character.CurrentAttackDamage, Color.White);
                }
            }
            else
            {
                HasBeenHit = false;
            }

            if (leftFacingSprite)
            {
                IsFacingLeft = direction.X > 0;
            }
            else
            {
                IsFacingLeft = direction.X < 0;
            }
            base.Update(GetWantedAnimation(character), direction);
            //if (CurrentAction.AnimationType == AnimationType.Death && Animator.CurrentAnimation.IsAnimationDone)
            //{
            //    IsDead = true;
            //}
            //animationManager.Update(currentAnimation, overrideAnimation);
        }
        public override void Draw()
        {
            base.Draw();
            float healthPercentage = (float)RemainingHealth / TotalHealth;
            int foregroundWidth = (int)(healthPercentage * 30);
            Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle((int)TopLeft.X, (int)TopLeft.Y - 10, foregroundWidth, 3), HealthBarColor);
        }
        public void SpawnWithAnimation(Vector2 position)
        {
            Position = position;
            IsSpawning = true;
        }
        public void Spawn(Vector2 position)
        {
            Position = position;
        }
        private float CalculateDistance(Vector2 playerPosition, Vector2 enemyPosition)
        {
            float distanceX = playerPosition.X - enemyPosition.X;
            float distanceY = playerPosition.Y - enemyPosition.Y;
            float distance = (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
            return distance;
        }
        private AnimationType GetWantedAnimation(Character character)
        {
            if (IsSpawning)
            {
                IsSpawning = false;
                return AnimationType.Spawn;
            }
            if (RemainingHealth <= 0)
            {
                return AnimationType.Death;
            }
            if (Attacks.Count > 0)
            {
                foreach (var attack in Attacks.Values)
                {
                    if (CalculateDistance(character.Position, Position) <= attack.AttackRange && CooldownManager.AnimationCooldown[attack.AnimationType] == 0)
                    {
                        if(attack.IsRanged || Math.Abs(character.Position.Y - Position.Y) <= 30)
                        {
                            return attack.AnimationType;
                        }
                    }
                }
            }
            if (direction != Vector2.Zero)
            {
                return AnimationType.Run;
            }
            else
            {
                return AnimationType.Stand;
            }
        }
        public void TakeDamage(float damage, Color damageColor)
        {
            //if(damage == 2)
            //{
            //    Console.WriteLine($"enemy {NUM} is bleeding");
            //}
            HasBeenHit = true;
            RemainingHealth -= damage;
            DamageNumberManager.Instance.AddDamageNumber(damage, Position - new Vector2(0, 30), damageColor);
        }
        public Enemy Clone()
        {
            return new Enemy(Name, IsBoss, TotalHealth, Speed, EntityScale, leftFacingSprite, WaveNum);
        }
    }
}