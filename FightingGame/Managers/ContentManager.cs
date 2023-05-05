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
        public Dictionary<CharacterName, Texture2D> CharacterSprites = new Dictionary<CharacterName, Texture2D>();
        public Dictionary<CharacterName, Dictionary<AnimationType, List<Rectangle>>> Animations = new Dictionary<CharacterName, Dictionary<AnimationType, List<Rectangle>>>();
        private ContentManager()
        {

        }

        public static ContentManager Instance { get; } = new ContentManager();

        public void LoadContent(Content content)
        {
            #region Captain Falcon animations
            CharacterSprites.Add(CharacterName.CaptainFalcon, content.Load<Texture2D>("Captain Falcon"));
            
            Dictionary<AnimationType, List<Rectangle>> CaptainFalcon = new Dictionary<AnimationType, List<Rectangle>>();
            List<Rectangle> CaptainFalconRun = new List<Rectangle>();
            CaptainFalconRun.Add(new Rectangle(309, 118, 69, 50));
            CaptainFalconRun.Add(new Rectangle(386, 117, 70, 51));
            CaptainFalconRun.Add(new Rectangle(467, 114, 47, 54));
            CaptainFalcon.Add(AnimationType.Run, CaptainFalconRun);

            List<Rectangle> CaptainFalconStand = new List<Rectangle>();
            CaptainFalconStand.Add(new Rectangle(41, 24, 42, 57));
            CaptainFalcon.Add(AnimationType.Stand, CaptainFalconStand);

            Animations.Add(CharacterName.CaptainFalcon, CaptainFalcon);
            #endregion
        }
    }
}
