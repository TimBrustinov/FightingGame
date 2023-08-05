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
        public int XPAmmount = 5;
        public bool IsSpawning;
        public Color HealthBarColor = Color.Green;
        public bool IsBoss;
        private AnimationType wantedAnimation;
        private Vector2 direction;
        public Enemy(EntityName name, bool isBoss, Texture2D texture, float health, float speed, float scale, Dictionary<AnimationType, EntityAction> abilites) : base(name, texture, abilites)
        {
            Rectangle characterRectangle = ContentManager.Instance.EntityTextures[name];
            EntityScale = scale;
            Position = new Vector2(1000, 350);
            Dimentions = new Vector2(characterRectangle.Width, characterRectangle.Height) * EntityScale;

            Speed = speed;
            TotalHealth = health;
            RemainingHealth = TotalHealth;
            IsBoss = isBoss;
        }
        public Enemy(Enemy enemy) : base(enemy.Name, ContentManager.Instance.EntitySpriteSheets[enemy.Name], ContentManager.Instance.EntityActions[enemy.Name])
        {
            Rectangle characterRectangle = ContentManager.Instance.EntityTextures[enemy.Name];
            EntityScale = enemy.EntityScale;
            Position = new Vector2(1000, 350);
            Dimentions = new Vector2(characterRectangle.Width, characterRectangle.Height) * EntityScale;

            Speed = enemy.Speed;
            TotalHealth = enemy.TotalHealth;
            RemainingHealth = TotalHealth;
        }
        public void Update(Character character)
        {
            direction = Vector2.Normalize(character.Position - Position);

            if(IsDead)
            {
                character.XP += XPAmmount;
            }

            if (character.WeaponHitBox.Intersects(HitBox))
            {
                if (character.HasFrameChanged)
                {
                    HasBeenHit = false;
                }
                if (character.Animator.CurrentAnimation != null && character.Animator.CurrentAnimation.CurrerntFrame.CanHit && !HasBeenHit)
                {
                    TakeDamage(character.CurrentAttack.AttackDamage);
                    HasBeenHit = true;
                }
            }
            else
            {
                HasBeenHit = false;
            }
            
            wantedAnimation = GetWantedAnimation(character);
            IsFacingLeft = direction.X < 0;
            base.Update(wantedAnimation, direction);
            if (CurrentAction.AnimationType == AnimationType.Death && Animator.CurrentAnimation.IsAnimationDone)
            {
                IsDead = true;
            }
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
            if(IsSpawning)
            {
                IsSpawning = false;
                return AnimationType.Spawn;

            }
            if (RemainingHealth <= 0)
            {
                return AnimationType.Death;
            }
            if(Attacks.Count > 0)
            {
                foreach (var attack in Attacks.Values)
                {
                    if (CalculateDistance(character.Position, Position) <= attack.AttackRange && Math.Abs(character.Position.Y - Position.Y) <= 30)
                    {
                        return attack.AnimationType;
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
    }
}