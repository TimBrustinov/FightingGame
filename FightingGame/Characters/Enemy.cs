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
        public Color HealthBarColor = Color.Green;
        public bool IsBoss;
        private AnimationType wantedAnimation;
        private List<Attack> attacks;
        public Enemy(EntityName name, bool isBoss, Texture2D texture, float health, float speed, float scale, Dictionary<AnimationType, EntityAction> abilites) : base(name, texture, abilites)
        {
            Rectangle characterRectangle = ContentManager.Instance.EntityTextures[name];
            Scale = scale;
            Position = new Vector2(1000, 350);
            Dimentions = new Vector2(characterRectangle.Width, characterRectangle.Height) * Scale;

            Speed = speed;
            TotalHealth = health;
            RemainingHealth = TotalHealth;
            IsBoss = isBoss;

            foreach (var item in Animator.AnimationToAction)
            {
                if(item.Value.GetType() == typeof(Attack))
                {
                    attacks.Add((Attack)item.Value);
                }
            }
        }
        public Enemy(Enemy enemy) : base(enemy.Name, ContentManager.Instance.EntitySpriteSheets[enemy.Name], ContentManager.Instance.EntityActions[enemy.Name])
        {
            Rectangle characterRectangle = ContentManager.Instance.EntityTextures[enemy.Name];
            Scale = enemy.Scale;
            Position = new Vector2(1000, 350);
            Dimentions = new Vector2(characterRectangle.Width, characterRectangle.Height) * Scale;

            Speed = enemy.Speed;
            TotalHealth = enemy.TotalHealth;
            RemainingHealth = TotalHealth;
        }
        public void Update(Character character, Vector2 direction)
        {
            Direction = direction;

            if (character.WeaponHitBox.Intersects(HitBox))
            {
                if (character.HasFrameChanged)
                {
                    HasBeenHit = false;
                }
                if(character.Animator.CurrentAnimation != null && character.Animator.CurrentAnimation.CurrerntFrame.CanHit && !HasBeenHit)
                {
                }
                //if (character.CurrentAction != null && character.Animator && !enemHasBeenHit)
                //{
                //    enemy.TakeDamage(selectedCharacter.AbilityDamage);
                //    enemy.HasBeenHit = true;
                //}
            }
            else
            {
                HasBeenHit = false;
            }

            if (IsDead)
            {
                character.XP += XPAmmount;
            }

            if(RemainingHealth <= 0)
            {
                wantedAnimation = AnimationType.Death;
            }
            else
            {
                foreach (var attack in attacks)
                {
                    if(CalculateDistance(character.Position, Position) <= attack.AttackRange)
                    {
                        wantedAnimation = attack.AnimationType;
                    }
                    else
                    {
                        wantedAnimation = AnimationType.Run;
                    }
                }
            }
            IsFacingLeft = direction.X < 0;
            base.Update(wantedAnimation, direction);
            //animationManager.Update(currentAnimation, overrideAnimation);
        }
        public override void Draw()
        {
            base.Draw();
            float healthPercentage = (float)RemainingHealth / TotalHealth;
            int foregroundWidth = (int)(healthPercentage * 30);
            Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle((int)TopLeft.X, (int)TopLeft.Y - 10, foregroundWidth, 3), HealthBarColor);
        }
        public void Spawn(Vector2 position, AnimationType animation)
        {
            Position = position;
            savedAnimaton = animation;
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
    }
}