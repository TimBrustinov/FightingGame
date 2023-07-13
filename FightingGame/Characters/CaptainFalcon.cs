using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame.Characters
{
    public class CaptainFalcon : Character
    {
        public CaptainFalcon(CharacterName name, Texture2D texture) : base(name, texture)
        {

        }
        protected override void NeutralAttack()
        {
            savedAnimaton = AnimationType.NeutralAttack;
            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
            {
                savedAnimaton = AnimationType.None;
            }
        }

        protected override void DirectionalAttack()
        {
            savedAnimaton = AnimationType.DirectionalAttack;
            if(InputManager.IsMovingLeft)
            {
                Position = new Vector2(Position.X - 6, Position.Y);
            }
            else
            {
                Position = new Vector2(Position.X + 6, Position.Y);
            }

            if(animationManager.PreviousFrame == new Rectangle(206, 297, 41, 70))
            {
                Position = new Vector2(Position.X, Position.Y - 2);
            }

            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
            {
                savedAnimaton = AnimationType.None;
            }
        }

        protected override void DownAttack()
        {
            throw new NotImplementedException();
        }

        protected override void UpAttack()
        {
            savedAnimaton = AnimationType.DirectionalAttack;
            Position = new Vector2(Position.X, Position.Y - 4);
            if (animationManager.CurrentAnimation.IsAnimationDone && animationManager.lastAnimation == savedAnimaton)
            {
                savedAnimaton = AnimationType.None;
            }
        }

        protected override void Jump()
        {
            savedAnimaton = AnimationType.Jump;
            if(IsGrounded)
            {
                Velocity = defaultVelocity;
                savedAnimaton = AnimationType.None;
                return;
            }
            IsGrounded = false;

            if (Velocity == -defaultVelocity && IsGrounded == false)
            {
                Position += new Vector2(InputManager.Direction.X * 5, -1 * Velocity);
            }
            else
            {
                Position += new Vector2(InputManager.Direction.X * 5, -1 * Velocity--);
            }
        }
    }
}
