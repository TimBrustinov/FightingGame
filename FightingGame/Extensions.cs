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
    static class Extensions
    {
        public static (int, int) GetWeaponHitboxOffsets(this FrameHelper frame)
        {
            Rectangle characterHitbox = frame.CharacterHitbox;
            Rectangle weaponHitbox = frame.AttackHitbox;
            if(weaponHitbox.Width == 0 || weaponHitbox.Height == 0)
            {
                return (0, 0);
            }
            return (weaponHitbox.X - characterHitbox.X, weaponHitbox.Y - characterHitbox.Y);
        }
        //public static (int, int) GetCharacterHitboxOffsets(this FrameHelper frame)
        //{

        //} 
    }
}