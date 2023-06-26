using FightingGame.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Content = Microsoft.Xna.Framework.Content.ContentManager;

namespace FightingGame
{
    public class ContentManager
    {
        public Texture2D Pixel;
        public Dictionary<CharacterName, Rectangle> CharacterTextures = new Dictionary<CharacterName, Rectangle>();
        public Dictionary<CharacterName, Texture2D> CharacterSpriteSheets = new Dictionary<CharacterName, Texture2D>();
        public Dictionary<CharacterName, Dictionary<(AnimationType, bool), List<Rectangle>>> Animations = new Dictionary<CharacterName, Dictionary<(AnimationType, bool), List<Rectangle>>>();
        private ContentManager()
        {

        }

        public static ContentManager Instance { get; } = new ContentManager();

        public void LoadContent(Content content)
        {
            bool CanBeCanceled = true;
            #region Captain Falcon 
            CharacterSpriteSheets.Add(CharacterName.CaptainFalcon, content.Load<Texture2D>("Captain Falcon"));
            CharacterTextures.Add(CharacterName.CaptainFalcon, new Rectangle(41, 24, 42, 57)); 
            
            Dictionary<(AnimationType, bool), List<Rectangle>> CaptainFalcon = new Dictionary<(AnimationType, bool), List<Rectangle>>();
            List<Rectangle> CaptainFalconRun = new List<Rectangle>();
            CaptainFalconRun.Add(new Rectangle(309, 118, 69, 50));
            CaptainFalconRun.Add(new Rectangle(386, 117, 70, 51));
            CaptainFalconRun.Add(new Rectangle(467, 114, 47, 54));
            CaptainFalcon.Add((AnimationType.Run, CanBeCanceled), CaptainFalconRun);

            List<Rectangle> CaptainFalconStand = new List<Rectangle>();
            CaptainFalconStand.Add(new Rectangle(41, 24, 42, 57));
            CaptainFalcon.Add((AnimationType.Stand, CanBeCanceled), CaptainFalconStand);

            List<Rectangle> CaptainFalconJump = new List<Rectangle>();
            CaptainFalconJump.Add(new Rectangle(269, 14, 39, 74));
            CaptainFalcon.Add((AnimationType.Jump, !CanBeCanceled), CaptainFalconJump);

            List<Rectangle> CaptainFalconNeutralAttack = new List<Rectangle>();
            CaptainFalconNeutralAttack.Add(new Rectangle(48, 114, 53, 51));
            CaptainFalconNeutralAttack.Add(new Rectangle(123, 113, 77, 52));
            CaptainFalconNeutralAttack.Add(new Rectangle(123, 113, 77, 52));
            CaptainFalcon.Add((AnimationType.NeutralAttack, !CanBeCanceled), CaptainFalconNeutralAttack);

            List<Rectangle> CaptainFalconDirectionalAttack = new List<Rectangle>();
            CaptainFalconDirectionalAttack.Add(new Rectangle(32, 319, 63, 48));
            CaptainFalconDirectionalAttack.Add(new Rectangle(102, 313, 90, 54));
            CaptainFalconDirectionalAttack.Add(new Rectangle(206, 297, 41, 70));
            CaptainFalconDirectionalAttack.Add(new Rectangle(206, 297, 41, 70));
            CaptainFalcon.Add((AnimationType.DirectionalAttack, !CanBeCanceled), CaptainFalconDirectionalAttack);



            Animations.Add(CharacterName.CaptainFalcon, CaptainFalcon);
            #endregion
        }
    }
}
