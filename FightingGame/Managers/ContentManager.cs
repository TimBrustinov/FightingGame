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

        public Dictionary<EnemyName, Rectangle> EnemyTextures = new Dictionary<EnemyName, Rectangle>();
        public Dictionary<EnemyName, Texture2D> EnemySpriteSheets = new Dictionary<EnemyName, Texture2D>();
        public Dictionary<EnemyName, Dictionary<(AnimationType, bool), List<Rectangle>>> EnemyAnimations = new Dictionary<EnemyName, Dictionary<(AnimationType, bool), List<Rectangle>>>();

        private ContentManager()
        {

        }

        public static ContentManager Instance { get; } = new ContentManager();

        public void LoadContent(Content content)
        {
            bool CanBeCanceled = true;
            #region Swordsman 
            CharacterSpriteSheets.Add(CharacterName.Swordsman, content.Load<Texture2D>("Swordsman"));
            CharacterTextures.Add(CharacterName.Swordsman, new Rectangle(17, 70, 15, 21));

            Dictionary<(AnimationType, bool), List<Rectangle>> Swordsman = new Dictionary<(AnimationType, bool), List<Rectangle>>();
            List<Rectangle> SwordsmanStand = new List<Rectangle>();
            SwordsmanStand.Add(new Rectangle(17, 70, 15, 21));
            Swordsman.Add((AnimationType.Stand, CanBeCanceled), SwordsmanStand);

            List<Rectangle> SwordsmanRun = new List<Rectangle>();
            SwordsmanRun.Add(new Rectangle(17, 212, 15, 23));
            SwordsmanRun.Add(new Rectangle(65, 213, 15, 22));
            SwordsmanRun.Add(new Rectangle(113, 214, 15, 21));
            SwordsmanRun.Add(new Rectangle(161, 212, 15, 23));
            SwordsmanRun.Add(new Rectangle(209, 213, 15, 22));
            SwordsmanRun.Add(new Rectangle(257, 214, 15, 21));
            Swordsman.Add((AnimationType.Run, CanBeCanceled), SwordsmanRun);

            List<Rectangle> SwordsmanRunUp = new List<Rectangle>();
            SwordsmanRunUp.Add(new Rectangle(18, 261, 13, 22));
            SwordsmanRunUp.Add(new Rectangle(66, 262, 13, 21));
            SwordsmanRunUp.Add(new Rectangle(114, 263, 13, 20));
            SwordsmanRunUp.Add(new Rectangle(162, 261, 13, 22));
            SwordsmanRunUp.Add(new Rectangle(210, 262, 13, 21));
            SwordsmanRunUp.Add(new Rectangle(258, 263, 13, 20));
            Swordsman.Add((AnimationType.RunUp, CanBeCanceled), SwordsmanRunUp);

            List<Rectangle> SwordsmanRunDown = new List<Rectangle>();
            SwordsmanRunDown.Add(new Rectangle(18, 164, 13, 23));
            SwordsmanRunDown.Add(new Rectangle(66, 165, 13, 22));
            SwordsmanRunDown.Add(new Rectangle(114, 166, 13, 21));
            SwordsmanRunDown.Add(new Rectangle(162, 164, 13, 23));
            SwordsmanRunDown.Add(new Rectangle(210, 165, 13, 22));
            SwordsmanRunDown.Add(new Rectangle(258, 166, 13, 21));
            Swordsman.Add((AnimationType.RunDown, CanBeCanceled), SwordsmanRunDown);

            List<Rectangle> SwordsmanSideAttack = new List<Rectangle>();
            SwordsmanSideAttack.Add(new Rectangle(19, 359, 16, 20));
            SwordsmanSideAttack.Add(new Rectangle(56, 358, 34, 23));
            SwordsmanSideAttack.Add(new Rectangle(107, 358, 20, 21));
            SwordsmanSideAttack.Add(new Rectangle(161, 360, 15, 19));
            Swordsman.Add((AnimationType.SideAttack, !CanBeCanceled), SwordsmanSideAttack);

            List<Rectangle> SwordsmanUpAttack = new List<Rectangle>();
            SwordsmanUpAttack.Add(new Rectangle(18, 407, 17, 20));
            SwordsmanUpAttack.Add(new Rectangle(59, 406, 22, 21));
            SwordsmanUpAttack.Add(new Rectangle(108, 407, 19, 20));
            SwordsmanUpAttack.Add(new Rectangle(162, 408, 13, 19));
            Swordsman.Add((AnimationType.UpAttack, !CanBeCanceled), SwordsmanUpAttack);

            List<Rectangle> SwordsmanDownAttack = new List<Rectangle>();
            SwordsmanDownAttack.Add(new Rectangle(15, 311, 16, 20));
            SwordsmanDownAttack.Add(new Rectangle(65, 310, 20, 26));
            SwordsmanDownAttack.Add(new Rectangle(114, 311, 19, 21));
            SwordsmanDownAttack.Add(new Rectangle(162, 312, 13, 19));
            Swordsman.Add((AnimationType.DownAttack, !CanBeCanceled), SwordsmanDownAttack);

            Animations.Add(CharacterName.Swordsman, Swordsman);
            #endregion

            #region Skeleton
            EnemySpriteSheets.Add(EnemyName.Skeleton, content.Load<Texture2D>("Skeleton"));
            EnemyTextures.Add(EnemyName.Skeleton, new Rectangle(17, 70, 15, 21));

            Dictionary<(AnimationType, bool), List<Rectangle>> Skeleton = new Dictionary<(AnimationType, bool), List<Rectangle>>();

            List<Rectangle> SkeletonStand = new List<Rectangle>();
            SkeletonStand.Add(new Rectangle(9, 208, 33, 33));
            Skeleton.Add((AnimationType.Stand, CanBeCanceled), SkeletonStand);

            List<Rectangle> SkeletonRun = new List<Rectangle>();
            SkeletonRun.Add(new Rectangle(5, 145, 36, 32));
            SkeletonRun.Add(new Rectangle(70, 144, 35, 33));
            SkeletonRun.Add(new Rectangle(134, 144, 35, 33));
            SkeletonRun.Add(new Rectangle(198, 144, 35, 33));
            SkeletonRun.Add(new Rectangle(261, 145, 36, 32));
            SkeletonRun.Add(new Rectangle(325, 146, 36, 31));
            SkeletonRun.Add(new Rectangle(389, 145, 36, 32));
            SkeletonRun.Add(new Rectangle(452, 144, 38, 33));
            SkeletonRun.Add(new Rectangle(516, 144, 38, 33));
            SkeletonRun.Add(new Rectangle(580, 144, 38, 33));
            SkeletonRun.Add(new Rectangle(654, 145, 37, 32));
            Skeleton.Add((AnimationType.Run, CanBeCanceled), SkeletonRun);

            List<Rectangle> SkeletonSideAttack = new List<Rectangle>();
            SkeletonSideAttack.Add(new Rectangle(8, 16, 34, 32));
            SkeletonSideAttack.Add(new Rectangle(69, 16, 37, 32));
            SkeletonSideAttack.Add(new Rectangle(129, 16, 41, 32));
            SkeletonSideAttack.Add(new Rectangle(196, 16, 38, 32));
            SkeletonSideAttack.Add(new Rectangle(269, 8, 51, 40));
            SkeletonSideAttack.Add(new Rectangle(335, 9, 31, 39));
            SkeletonSideAttack.Add(new Rectangle(401, 15, 26, 33));
            SkeletonSideAttack.Add(new Rectangle(466, 16, 25, 33));
            SkeletonSideAttack.Add(new Rectangle(514, 16, 62, 32));
            SkeletonSideAttack.Add(new Rectangle(580, 15, 38, 31));
            SkeletonSideAttack.Add(new Rectangle(646, 12, 36, 36));
            SkeletonSideAttack.Add(new Rectangle(707, 16, 39, 32));
            SkeletonSideAttack.Add(new Rectangle(776, 16, 34, 32));
            Skeleton.Add((AnimationType.SideAttack, !CanBeCanceled), SkeletonSideAttack);

            EnemyAnimations.Add(EnemyName.Skeleton, Skeleton);

            #endregion
        }
    }
}
