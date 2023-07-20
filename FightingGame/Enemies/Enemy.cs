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
        public Enemy(EntityName name, Texture2D texture, int health, int speed, float animationSpeed, float scale, Dictionary<AnimationType, Ability> abilites) : base(name, texture, abilites, animationSpeed)
        {
            Rectangle characterRectangle = ContentManager.Instance.EntityTextures[name];
            Scale = scale;
            Position = new Vector2(1000, 350);
            Dimentions = new Vector2(characterRectangle.Width, characterRectangle.Height) * Scale;

            Speed = speed;
            TotalHealth = health;
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
            Globals.SpriteBatch.Draw(ContentManager.Instance.Pixel, new Rectangle((int)TopLeft.X, (int)TopLeft.Y - 10, foregroundWidth, 3), Color.Green);
        }
    }
}