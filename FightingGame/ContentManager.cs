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
        public Dictionary<AnimationType, Rectangle> Animations = new Dictionary<AnimationType, Rectangle>();
        private ContentManager()
        {

        }

        public static ContentManager Instance { get; } = new ContentManager();

        public void LoadContent(Content content)
        {
            CharacterSprites.Add(CharacterName.CaptainFalcon, content.Load<Texture2D>("Captain Falcon"));
        }
    }
}
