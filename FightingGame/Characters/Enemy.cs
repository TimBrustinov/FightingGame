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

        public Enemy(EntityName name, bool isBoss, Texture2D texture, float health, float speed, float scale, Dictionary<AnimationType, Action> abilites) : base(name, texture, abilites)
        {
            Rectangle characterRectangle = ContentManager.Instance.EntityTextures[name];
            Scale = scale;
            Position = new Vector2(1000, 350);
            Dimentions = new Vector2(characterRectangle.Width, characterRectangle.Height) * Scale;

            Speed = speed;
            TotalHealth = health;
            RemainingHealth = TotalHealth;
            IsBoss = isBoss;
        }
        public Enemy(Enemy enemy) : base(enemy.Name, ContentManager.Instance.EntitySpriteSheets[enemy.Name], ContentManager.Instance.EntityAbilites[enemy.Name])
        {
            Rectangle characterRectangle = ContentManager.Instance.EntityTextures[enemy.Name];
            Scale = enemy.Scale;
            Position = new Vector2(1000, 350);
            Dimentions = new Vector2(characterRectangle.Width, characterRectangle.Height) * Scale;

            Speed = enemy.Speed;
            TotalHealth = enemy.TotalHealth;
            RemainingHealth = TotalHealth;
        }
        public override void Update(AnimationType animation, Vector2 direction)
        {
            IsFacingLeft = direction.X < 0;
            base.Update(animation, direction);
            animationManager.Update(currentAnimation, overrideAnimation);
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

    }
}