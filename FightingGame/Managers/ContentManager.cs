using FightingGame.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Content = Microsoft.Xna.Framework.Content.ContentManager;

namespace FightingGame
{
    public class ContentManager
    {
        public Texture2D Pixel;
        public SpriteFont Font;
        public Texture2D Heart;
        public Texture2D GrayHeart;

        //Messing around
        public Texture2D HeartOutline;
        public Texture2D HeartBackground;
        //Messing around

        public Dictionary<CharacterName, Rectangle> CharacterTextures = new Dictionary<CharacterName, Rectangle>();
        public Dictionary<CharacterName, Texture2D> CharacterSpriteSheets = new Dictionary<CharacterName, Texture2D>();
        public Dictionary<CharacterName, Dictionary<(AnimationType, bool), List<FrameHelper>>> Animations = new Dictionary<CharacterName, Dictionary<(AnimationType, bool), List<FrameHelper>>>();

        public Dictionary<EnemyName, Rectangle> EnemyTextures = new Dictionary<EnemyName, Rectangle>();
        public Dictionary<EnemyName, Texture2D> EnemySpriteSheets = new Dictionary<EnemyName, Texture2D>();
        public Dictionary<EnemyName, Dictionary<(AnimationType, bool), List<FrameHelper>>> EnemyAnimations = new Dictionary<EnemyName, Dictionary<(AnimationType, bool), List<FrameHelper>>>();

        public Texture2D FontTexture;
        public Dictionary<char, Rectangle> PixelFont = new Dictionary<char, Rectangle>();

        private ContentManager()
        {

        }

        public static ContentManager Instance { get; } = new ContentManager();

        public void LoadContent(Content content)
        {
            bool CanBeCanceled = true;
            Heart = content.Load<Texture2D>("Heart");
            Font = content.Load<SpriteFont>("Font");
            GrayHeart = content.Load<Texture2D>("GrayHeart");
            HeartOutline = content.Load<Texture2D>("HeartOutline");

            #region Swordsman 

            //CharacterSpriteSheets.Add(CharacterName.Swordsman, content.Load<Texture2D>("Swordsman"));
            //CharacterTextures.Add(CharacterName.Swordsman, new Rectangle(17, 70, 15, 21));

            //Dictionary<(AnimationType, bool), List<FrameHelper>> Swordsman = new Dictionary<(AnimationType, bool), List<FrameHelper>>();
            //List<FrameHelper> SwordsmanStand = new List<FrameHelper>();
            //SwordsmanStand.Add(new FrameHelper(17, 70, 15, 21));
            //Swordsman.Add((AnimationType.Stand, CanBeCanceled), SwordsmanStand);

            //List<FrameHelper> SwordsmanRun = new List<FrameHelper>();
            //SwordsmanRun.Add(new FrameHelper(17, 212, 15, 23));
            //SwordsmanRun.Add(new FrameHelper(65, 213, 15, 22));
            //SwordsmanRun.Add(new FrameHelper(113, 214, 15, 21));
            //SwordsmanRun.Add(new FrameHelper(161, 212, 15, 23));
            //SwordsmanRun.Add(new FrameHelper(209, 213, 15, 22));
            //SwordsmanRun.Add(new FrameHelper(257, 214, 15, 21));
            //Swordsman.Add((AnimationType.Run, CanBeCanceled), SwordsmanRun);

            //List<FrameHelper> SwordsmanRunUp = new List<FrameHelper>();
            //SwordsmanRunUp.Add(new FrameHelper(18, 261, 13, 22));
            //SwordsmanRunUp.Add(new FrameHelper(66, 262, 13, 21));
            //SwordsmanRunUp.Add(new FrameHelper(114, 263, 13, 20));
            //SwordsmanRunUp.Add(new FrameHelper(162, 261, 13, 22));
            //SwordsmanRunUp.Add(new FrameHelper(210, 262, 13, 21));
            //SwordsmanRunUp.Add(new FrameHelper(258, 263, 13, 20));
            //Swordsman.Add((AnimationType.RunUp, CanBeCanceled), SwordsmanRunUp);

            //List<FrameHelper> SwordsmanRunDown = new List<FrameHelper>();

            //SwordsmanRunDown.Add(new FrameHelper(18, 164, 13, 23));
            //SwordsmanRunDown.Add(new FrameHelper(66, 165, 13, 22));
            //SwordsmanRunDown.Add(new FrameHelper(114, 166, 13, 21));
            //SwordsmanRunDown.Add(new FrameHelper(162, 164, 13, 23));
            //SwordsmanRunDown.Add(new FrameHelper(210, 165, 13, 22));
            //SwordsmanRunDown.Add(new FrameHelper(258, 166, 13, 21));
            //Swordsman.Add((AnimationType.RunDown, CanBeCanceled), SwordsmanRunDown);


            //List<FrameHelper> SwordsmanSideAttack = new List<FrameHelper>();
            //SwordsmanSideAttack.Add(new FrameHelper(19, 359, 16, 20));
            //SwordsmanSideAttack.Add(new FrameHelper(56, 358, 34, 23, new Rectangle(56, 364, 34, 17)));
            //SwordsmanSideAttack.Add(new FrameHelper(107, 358, 20, 21, new Rectangle(106, 370, 10, 8)));
            //SwordsmanSideAttack.Add(new FrameHelper(161, 360, 15, 19));
            //Swordsman.Add((AnimationType.SideAttack, !CanBeCanceled), SwordsmanSideAttack);

            //List<FrameHelper> SwordsmanUpAttack = new List<FrameHelper>();
            //SwordsmanUpAttack.Add(new FrameHelper(18, 407, 17, 20));
            //SwordsmanUpAttack.Add(new FrameHelper(59, 406, 22, 21));
            //SwordsmanUpAttack.Add(new FrameHelper(108, 407, 19, 20));
            //SwordsmanUpAttack.Add(new FrameHelper(162, 408, 13, 19));
            //Swordsman.Add((AnimationType.UpAttack, !CanBeCanceled), SwordsmanUpAttack);

            //List<FrameHelper> SwordsmanDownAttack = new List<FrameHelper>();
            //SwordsmanDownAttack.Add(new FrameHelper(15, 311, 16, 20));
            //SwordsmanDownAttack.Add(new FrameHelper(65, 310, 20, 26, new Rectangle(63, 319, 23, 19)));
            //SwordsmanDownAttack.Add(new FrameHelper(114, 311, 19, 21, new Rectangle(121, 319, 15, 14)));
            //SwordsmanDownAttack.Add(new FrameHelper(162, 312, 13, 19));
            //Swordsman.Add((AnimationType.DownAttack, !CanBeCanceled), SwordsmanDownAttack);

            //List<FrameHelper> SwordsmanDeath = new List<FrameHelper>();
            //SwordsmanDeath.Add(new FrameHelper(18, 457, 16, 18));
            //SwordsmanDeath.Add(new FrameHelper(18, 457, 16, 18));
            //SwordsmanDeath.Add(new FrameHelper(66, 459, 17, 16));
            //SwordsmanDeath.Add(new FrameHelper(66, 459, 17, 16));
            //SwordsmanDeath.Add(new FrameHelper(66, 459, 17, 16));
            //SwordsmanDeath.Add(new FrameHelper(113, 462, 21, 13));
            //SwordsmanDeath.Add(new FrameHelper(113, 462, 21, 13));
            //SwordsmanDeath.Add(new FrameHelper(113, 462, 21, 13));
            //Swordsman.Add((AnimationType.Death, !CanBeCanceled), SwordsmanDeath);

            //Animations.Add(CharacterName.Swordsman, Swordsman);

            #endregion

            #region Hashashin
            CharacterSpriteSheets.Add(CharacterName.Hashashin, content.Load<Texture2D>("HashashinSpritesheet"));
            CharacterTextures.Add(CharacterName.Hashashin, new Rectangle(132, 90, 34, 37));

            Dictionary<(AnimationType, bool), List<FrameHelper>> Hashashin = new Dictionary<(AnimationType, bool), List<FrameHelper>>();

            List<FrameHelper> HashashinStand = new List<FrameHelper>();
            HashashinStand.Add(new FrameHelper(132, 90, 34, 37));
            HashashinStand.Add(new FrameHelper(419, 91, 33, 36));
            HashashinStand.Add(new FrameHelper(705, 92, 31, 35));
            HashashinStand.Add(new FrameHelper(991, 92, 32, 35));
            HashashinStand.Add(new FrameHelper(1278, 92, 35, 35));
            HashashinStand.Add(new FrameHelper(1568, 92, 33, 35));
            HashashinStand.Add(new FrameHelper(1858, 92, 35, 35));
            HashashinStand.Add(new FrameHelper(2147, 91, 34, 36));
            Hashashin.Add((AnimationType.Stand, CanBeCanceled), HashashinStand);

            List<FrameHelper> HashashinRun = new List<FrameHelper>();
            HashashinRun.Add(new FrameHelper(123, 219, 39, 36));
            HashashinRun.Add(new FrameHelper(413, 218, 40, 37));
            HashashinRun.Add(new FrameHelper(698, 216, 44, 30));
            HashashinRun.Add(new FrameHelper(987, 218, 40, 37));
            HashashinRun.Add(new FrameHelper(1275, 219, 39, 36));
            HashashinRun.Add(new FrameHelper(1567, 218, 38, 37));
            HashashinRun.Add(new FrameHelper(1849, 216, 44, 30));
            HashashinRun.Add(new FrameHelper(2140, 218, 39, 37));
            Hashashin.Add((AnimationType.Run, CanBeCanceled), HashashinRun);

            List<FrameHelper> HashashinBasicAttack = new List<FrameHelper>();
            HashashinBasicAttack.Add(new FrameHelper(135, 858, 48, 37, new Rectangle(145, 858, 38, 25)));
            HashashinBasicAttack.Add(new FrameHelper(424, 858, 28, 37,  new Rectangle(435, 858, 23, 19)));
            HashashinBasicAttack.Add(new FrameHelper(691, 858, 58, 37, new Rectangle(690, 859, 61, 30)));
            HashashinBasicAttack.Add(new FrameHelper(978, 858, 45, 37, new Rectangle(970, 867, 35, 20)));
            HashashinBasicAttack.Add(new FrameHelper(1273, 858, 40, 37, new Rectangle(1270, 863, 21, 19)));
            HashashinBasicAttack.Add(new FrameHelper(1562, 856, 39, 39));
            HashashinBasicAttack.Add(new FrameHelper(1854, 858, 39, 37));
            HashashinBasicAttack.Add(new FrameHelper(2148, 858, 33, 37));
            Hashashin.Add((AnimationType.BasicAttack, !CanBeCanceled), HashashinBasicAttack);

            List<FrameHelper> HashashinDodge = new List<FrameHelper>();
            HashashinDodge.Add(new FrameHelper(127, 731, 39, 36));
            HashashinDodge.Add(new FrameHelper(412, 737, 40, 30));
            HashashinDodge.Add(new FrameHelper(699, 733, 34, 34));
            HashashinDodge.Add(new FrameHelper(992, 736, 34, 31));
            HashashinDodge.Add(new FrameHelper(1280, 736, 34, 31));
            HashashinDodge.Add(new FrameHelper(1572, 730, 26, 37));
            Hashashin.Add((AnimationType.Dodge, !CanBeCanceled), HashashinDodge);

            List<FrameHelper> HashashinAbility1 = new List<FrameHelper>();
            HashashinAbility1.Add(new FrameHelper(134, 601, 24, 36));
            HashashinAbility1.Add(new FrameHelper(new Rectangle(414, 571, 53, 66), new Rectangle(427, 567, 44, 71), new Rectangle(424, 604, 25, 30)));
            HashashinAbility1.Add(new FrameHelper(new Rectangle(702, 578, 70, 59), new Rectangle(731, 576, 49, 65), new Rectangle(712, 603, 27, 31)));
            HashashinAbility1.Add(new FrameHelper(new Rectangle(990, 578, 73, 59), new Rectangle(1017, 574, 48, 66), new Rectangle(1000, 603, 26, 32)));
            HashashinAbility1.Add(new FrameHelper(new Rectangle(1269, 575, 84, 61), new Rectangle(1305, 573, 51, 64), new Rectangle(1280, 599, 32, 37)));
            HashashinAbility1.Add(new FrameHelper(new Rectangle(1560, 571, 84, 67), new Rectangle(1601, 569, 49, 70), new Rectangle(1568, 588, 34, 38)));
            HashashinAbility1.Add(new FrameHelper(new Rectangle(1853, 572, 80, 67), new Rectangle(1895, 569, 43, 72), new Rectangle(1859, 600, 26, 38)));
            Hashashin.Add((AnimationType.Ability1, !CanBeCanceled), HashashinAbility1);

            Animations.Add(CharacterName.Hashashin, Hashashin);

            #endregion
            #region Skeleton
            EnemySpriteSheets.Add(EnemyName.Skeleton, content.Load<Texture2D>("Skeleton"));
            EnemyTextures.Add(EnemyName.Skeleton, new Rectangle(17, 70, 15, 21));

            Dictionary<(AnimationType, bool), List<FrameHelper>> Skeleton = new Dictionary<(AnimationType, bool), List<FrameHelper>>();

            List<FrameHelper> SkeletonStand = new List<FrameHelper>();
            SkeletonStand.Add(new FrameHelper(9, 208, 33, 33));
            Skeleton.Add((AnimationType.Stand, CanBeCanceled), SkeletonStand);

            List<FrameHelper> SkeletonRun = new List<FrameHelper>();
            SkeletonRun.Add(new FrameHelper(5, 145, 36, 32));
            SkeletonRun.Add(new FrameHelper(70, 144, 35, 33));
            SkeletonRun.Add(new FrameHelper(134, 144, 35, 33));
            SkeletonRun.Add(new FrameHelper(198, 144, 35, 33));
            SkeletonRun.Add(new FrameHelper(261, 145, 36, 32));
            SkeletonRun.Add(new FrameHelper(325, 146, 36, 31));
            SkeletonRun.Add(new FrameHelper(389, 145, 36, 32));
            SkeletonRun.Add(new FrameHelper(452, 144, 38, 33));
            SkeletonRun.Add(new FrameHelper(516, 144, 38, 33));
            SkeletonRun.Add(new FrameHelper(580, 144, 38, 33));
            SkeletonRun.Add(new FrameHelper(654, 145, 37, 32));
            Skeleton.Add((AnimationType.Run, CanBeCanceled), SkeletonRun);

            List<FrameHelper> SkeletonSideAttack = new List<FrameHelper>();
            SkeletonSideAttack.Add(new FrameHelper(8, 16, 34, 32));
            SkeletonSideAttack.Add(new FrameHelper(69, 16, 37, 32));
            SkeletonSideAttack.Add(new FrameHelper(129, 16, 41, 32));
            SkeletonSideAttack.Add(new FrameHelper(196, 16, 38, 32));
            SkeletonSideAttack.Add(new FrameHelper(269, 8, 51, 40, new Rectangle(268, 7, 53, 30)));
            SkeletonSideAttack.Add(new FrameHelper(335, 9, 31, 39, new Rectangle(331, 8, 41, 20)));
            SkeletonSideAttack.Add(new FrameHelper(401, 15, 26, 33));
            SkeletonSideAttack.Add(new FrameHelper(466, 16, 25, 33));
            SkeletonSideAttack.Add(new FrameHelper(514, 16, 62, 32, new Rectangle(512, 14, 65, 29)));
            SkeletonSideAttack.Add(new FrameHelper(580, 15, 38, 31, new Rectangle(577, 13, 34, 24)));
            SkeletonSideAttack.Add(new FrameHelper(646, 12, 36, 36));
            SkeletonSideAttack.Add(new FrameHelper(707, 16, 39, 32));
            SkeletonSideAttack.Add(new FrameHelper(776, 16, 34, 32));
            Skeleton.Add((AnimationType.BasicAttack, !CanBeCanceled), SkeletonSideAttack);

            List<FrameHelper> SkeletonDeath = new List<FrameHelper>();
            SkeletonDeath.Add(new FrameHelper(137, 81, 31, 32));
            SkeletonDeath.Add(new FrameHelper(265, 83, 37, 30));
            SkeletonDeath.Add(new FrameHelper(329, 82, 38, 31));
            SkeletonDeath.Add(new FrameHelper(392, 80, 42, 33));
            SkeletonDeath.Add(new FrameHelper(457, 79, 42, 34));
            SkeletonDeath.Add(new FrameHelper(521, 79, 42, 34));
            SkeletonDeath.Add(new FrameHelper(585, 79, 42, 34));
            SkeletonDeath.Add(new FrameHelper(649, 80, 42, 33));
            Skeleton.Add((AnimationType.Death, !CanBeCanceled), SkeletonDeath);

            EnemyAnimations.Add(EnemyName.Skeleton, Skeleton);

            #endregion

        }
        private List<Rectangle> GenerateFrameRectangles(Texture2D spriteSheet, Rectangle animationRowRectangle, int frameCount)
        {
            List<Rectangle> frameRectangles = new List<Rectangle>();

            // Calculate the width and height of each frame
            int frameWidth = animationRowRectangle.Width / frameCount;
            int frameHeight = animationRowRectangle.Height;

            // Generate rectangles for each frame in the row
            for (int i = 0; i < frameCount; i++)
            {
                Rectangle frameRectangle = new Rectangle(
                    animationRowRectangle.X + (i * frameWidth),
                    animationRowRectangle.Y,
                    frameWidth,
                    frameHeight);

                frameRectangles.Add(frameRectangle);
            }
            return frameRectangles;
        }
    }
}
