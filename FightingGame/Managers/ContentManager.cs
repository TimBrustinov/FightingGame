﻿using FightingGame.Screens;
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
        public Texture2D Shadow;
        public SpriteFont Font;
        public Texture2D Heart;

        public Dictionary<EntityName, Rectangle> EntityTextures;
        public Dictionary<EntityName, Texture2D> EntitySpriteSheets;
        public Dictionary<EntityName, Dictionary<(AnimationType, bool, float), List<FrameHelper>>> Animations;
        public Dictionary<EntityName, Dictionary<AnimationType, Ability>> EntityAbilites;

        public Dictionary<EntityName, Dictionary<AnimationType, Rectangle>> CharacterAbilityIcons;

        private ContentManager()
        {
            CharacterAbilityIcons = new Dictionary<EntityName, Dictionary<AnimationType, Rectangle>>();
            EntityTextures = new Dictionary<EntityName, Rectangle>();
            EntitySpriteSheets = new Dictionary<EntityName, Texture2D>();
            Animations = new Dictionary<EntityName, Dictionary<(AnimationType, bool, float), List<FrameHelper>>>();
            EntityAbilites = new Dictionary<EntityName, Dictionary<AnimationType, Ability>>();
        }

        public static ContentManager Instance { get; } = new ContentManager();

        public void LoadContent(Content content)
        {
            bool CanBeCanceled = true;
            bool canHit = true;
            Font = content.Load<SpriteFont>("Font");
            Shadow = content.Load<Texture2D>("SHADOW");
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
            EntitySpriteSheets.Add(EntityName.Hashashin, content.Load<Texture2D>("HashashinFullSpritesheet"));
            EntityTextures.Add(EntityName.Hashashin, new Rectangle(132, 90, 34, 37));

            Dictionary<AnimationType, Ability> HashashinAbilites = new Dictionary<AnimationType, Ability>()
            {
                [AnimationType.Dodge] = new HashashinDodge(0),
                [AnimationType.BasicAttack] = new HashashinBasicAttack(0),
                [AnimationType.Ability1] = new HashashinAbility1(2),
                [AnimationType.Ability2] = new HashashinAbility2(3),
                [AnimationType.Ability3] = new HashashinAbility3(3),
                [AnimationType.UltimateAbility1] = new HashashinUltimateAbility1(3),
                [AnimationType.UltimateTransform] = new HashashinUltimateTransform(0),
                [AnimationType.UltimateDodge] = new HashashinUltimateDodge(0),
                [AnimationType.UltimateBasicAttack] = new HashashinUltimateBasicAttack(0),
            };

            Dictionary<AnimationType, Rectangle> HashashinAbilityIcons = new Dictionary<AnimationType, Rectangle>()
            { 
                [AnimationType.Ability1] = new Rectangle(988, 597, 50, 50),
                [AnimationType.Ability2] = new Rectangle(5677, 1106, 50, 50),
                [AnimationType.Ability3] = new Rectangle(1300, 1236, 50, 50),
            };

            Dictionary<(AnimationType, bool, float), List<FrameHelper>> Hashashin = new Dictionary<(AnimationType, bool, float), List<FrameHelper>>();
            List<FrameHelper> HashashinRun = new List<FrameHelper>();
            HashashinRun.Add(new FrameHelper(new Rectangle(123, 219, 39, 36)));
            HashashinRun.Add(new FrameHelper(new Rectangle(413, 218, 40, 37)));
            HashashinRun.Add(new FrameHelper(new Rectangle(698, 216, 44, 30)));
            HashashinRun.Add(new FrameHelper(new Rectangle(987, 218, 40, 37)));
            HashashinRun.Add(new FrameHelper(new Rectangle(1275, 219, 39, 36)));
            HashashinRun.Add(new FrameHelper(new Rectangle(1567, 218, 38, 37)));
            HashashinRun.Add(new FrameHelper(new Rectangle(1849, 216, 44, 30)));
            HashashinRun.Add(new FrameHelper(new Rectangle(2140, 218, 39, 37)));
            Hashashin.Add((AnimationType.Run, CanBeCanceled, 0.18f), HashashinRun);

            List<FrameHelper> HashashinBasicAttack = new List<FrameHelper>();
            HashashinBasicAttack.Add(new FrameHelper(new Rectangle(135, 858, 48, 37), new Rectangle(145, 858, 38, 25), canHit));
            HashashinBasicAttack.Add(new FrameHelper(new Rectangle(424, 858, 28, 37),  new Rectangle(435, 858, 23, 19), canHit));
            HashashinBasicAttack.Add(new FrameHelper(new Rectangle(691, 858, 58, 37), new Rectangle(690, 859, 61, 30), canHit));
            HashashinBasicAttack.Add(new FrameHelper(new Rectangle(978, 858, 45, 37), new Rectangle(970, 867, 35, 20), canHit));
            HashashinBasicAttack.Add(new FrameHelper(new Rectangle(1273, 858, 40, 37), new Rectangle(1270, 863, 21, 19), canHit));
            HashashinBasicAttack.Add(new FrameHelper(new Rectangle(1562, 856, 39, 39)));
            HashashinBasicAttack.Add(new FrameHelper(new Rectangle(1854, 858, 39, 37)));
            HashashinBasicAttack.Add(new FrameHelper(new Rectangle(2148, 858, 33, 37)));
            Hashashin.Add((AnimationType.BasicAttack, !CanBeCanceled, 0.15f), HashashinBasicAttack);

            List<FrameHelper> HashashinDodge = new List<FrameHelper>();
            HashashinDodge.Add(new FrameHelper(new Rectangle(127, 731, 39, 36)));
            HashashinDodge.Add(new FrameHelper(new Rectangle(412, 737, 40, 30)));
            HashashinDodge.Add(new FrameHelper(new Rectangle(699, 733, 34, 34)));
            HashashinDodge.Add(new FrameHelper(new Rectangle(992, 736, 34, 31)));
            HashashinDodge.Add(new FrameHelper(new Rectangle(1280, 736, 34, 31)));
            HashashinDodge.Add(new FrameHelper(new Rectangle(1572, 730, 26, 37)));
            Hashashin.Add((AnimationType.Dodge, !CanBeCanceled, 0.18f), HashashinDodge);

            List<FrameHelper> HashashinAbility1 = new List<FrameHelper>();
            HashashinAbility1.Add(new FrameHelper(new Rectangle(134, 601, 24, 36)));
            HashashinAbility1.Add(new FrameHelper(new Rectangle(414, 571, 53, 66), new Rectangle(427, 567, 44, 71), new Rectangle(424, 604, 25, 30), canHit));
            HashashinAbility1.Add(new FrameHelper(new Rectangle(702, 578, 70, 59), new Rectangle(731, 576, 49, 65), new Rectangle(712, 603, 27, 31), canHit));
            HashashinAbility1.Add(new FrameHelper(new Rectangle(990, 578, 73, 59), new Rectangle(1017, 574, 48, 66), new Rectangle(1000, 603, 26, 32), canHit));
            HashashinAbility1.Add(new FrameHelper(new Rectangle(1269, 575, 84, 61), new Rectangle(1305, 573, 51, 64), new Rectangle(1280, 599, 32, 37), canHit));
            HashashinAbility1.Add(new FrameHelper(new Rectangle(1560, 571, 84, 67), new Rectangle(1601, 569, 49, 70), new Rectangle(1568, 588, 34, 38), canHit));
            HashashinAbility1.Add(new FrameHelper(new Rectangle(1853, 572, 80, 67), new Rectangle(1895, 569, 43, 72), new Rectangle(1859, 600, 26, 38), canHit));
            Hashashin.Add((AnimationType.Ability1, !CanBeCanceled, 0.18f), HashashinAbility1);

            //List<FrameHelper> HashashinAbility2 = new List<FrameHelper>();
            //HashashinAbility2.Add(new FrameHelper(new Rectangle(978, 986, 45, 37)));
            //HashashinAbility2.Add(new FrameHelper(new Rectangle(1266, 986, 47, 37)));
            //HashashinAbility2.Add(new FrameHelper(new Rectangle(1554, 986, 47, 37)));
            //HashashinAbility2.Add(new FrameHelper(new Rectangle(1842, 986, 51, 37)));
            //HashashinAbility2.Add(new FrameHelper(new Rectangle(2134, 974, 75, 49), new Rectangle(2166, 971, 49, 48), new Rectangle(2148, 988, 31, 36), canHit));
            //HashashinAbility2.Add(new FrameHelper(new Rectangle(2720, 977, 64, 46), new Rectangle(2746, 976, 38, 44), new Rectangle(2724, 989, 30, 34), canHit));
            //HashashinAbility2.Add(new FrameHelper(new Rectangle(3010, 978, 65, 45), new Rectangle(3035, 975, 42, 44), new Rectangle(3013, 986, 26, 37), canHit));
            //HashashinAbility2.Add(new FrameHelper(new Rectangle(3282, 978, 73, 45), new Rectangle(3282, 978, 78, 36), new Rectangle(3297, 986, 30, 37), canHit));
            //HashashinAbility2.Add(new FrameHelper(new Rectangle(3570, 986, 60, 37), new Rectangle(3586, 1002, 65, 14), new Rectangle(3585, 986, 31, 37), canHit));
            //HashashinAbility2.Add(new FrameHelper(new Rectangle(3871, 980, 34, 43)));
            //HashashinAbility2.Add(new FrameHelper(new Rectangle(4156, 986, 41, 37)));
            //HashashinAbility2.Add(new FrameHelper(new Rectangle(4452, 986, 29, 37)));

            List<FrameHelper> HashashinAbility2 = new List<FrameHelper>();
            HashashinAbility2.Add(new FrameHelper(new Rectangle(3570, 1115, 46, 36)));
            HashashinAbility2.Add(new FrameHelper(new Rectangle(3856, 1116, 47, 35)));
            HashashinAbility2.Add(new FrameHelper(new Rectangle(4144, 1116, 49, 35)));
            HashashinAbility2.Add(new FrameHelper(new Rectangle(4451, 1100, 42, 51)));
            HashashinAbility2.Add(new FrameHelper(new Rectangle(4730, 1086, 71, 65), new Rectangle(4760, 1096, 56, 61), new Rectangle(4735, 1094, 32, 45), canHit));
            HashashinAbility2.Add(new FrameHelper(new Rectangle(5016, 1094, 90, 57), new Rectangle(5068, 1099, 57, 60), new Rectangle(5027, 1096, 23, 42), canHit));
            HashashinAbility2.Add(new FrameHelper(new Rectangle(5301, 1091, 122, 60), new Rectangle(5380, 1099, 65, 58), new Rectangle(5314, 1094, 31, 37), canHit));
            HashashinAbility2.Add(new FrameHelper(new Rectangle(5591, 1089, 122, 62), new Rectangle(5671, 1101, 66, 61), new Rectangle(5603, 1098, 27, 40), canHit));
            HashashinAbility2.Add(new FrameHelper(new Rectangle(5885, 1093, 126, 54), new Rectangle(5953, 1093, 69, 61), new Rectangle(5890, 1099, 28, 40), canHit));
            HashashinAbility2.Add(new FrameHelper(new Rectangle(6179, 1100, 122, 48), new Rectangle(6242, 1093, 72, 64), new Rectangle(6179, 1105, 26, 38), canHit));
            HashashinAbility2.Add(new FrameHelper(new Rectangle(6467, 1113, 26, 38)));
            HashashinAbility2.Add(new FrameHelper(new Rectangle(7332, 1114, 26, 37)));
            Hashashin.Add((AnimationType.Ability2, !CanBeCanceled, 0.2f), HashashinAbility2);

            List<FrameHelper> HashashinAbility3 = new List<FrameHelper>();
            HashashinAbility3.Add(new FrameHelper(new Rectangle(132, 1242, 32, 37)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(402, 1242, 60, 37)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(1300, 1244, 49, 35)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(1619, 1244, 45, 35)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(1920, 1247, 46, 32)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(2471, 1168, 53, 46)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(2700, 1172, 52, 47)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(2988, 1185, 52, 44)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(3279, 1227, 68, 52), new Rectangle(3270, 1221, 85, 42), new Rectangle(3300, 1249, 27, 32), canHit));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(3562, 1249, 66, 30), new Rectangle(3270, 1221, 85, 42), new Rectangle(3300, 1249, 27, 32), canHit));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(3874, 1249, 66, 30)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(4240, 1243, 47, 29)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(4511, 1242, 50, 30)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(4782, 1242, 50, 30)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(4976, 1235, 91, 41), new Rectangle(4993, 1222, 105, 63), new Rectangle(4974, 1239, 39, 38), canHit));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(5230, 1247, 63, 32), new Rectangle(4993, 1222, 105, 63), new Rectangle(4974, 1239, 39, 38), canHit));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(5588, 1225, 114, 54), new Rectangle(5568, 1217, 105, 73), new Rectangle(5662, 1244, 29, 36), canHit));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(5931, 1245, 59, 34), new Rectangle(5568, 1217, 105, 73), new Rectangle(5662, 1244, 29, 36), canHit));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(6219, 1245, 59, 34)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(7072, 1245, 62, 34)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(7364, 1247, 47, 30)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(7938, 1244, 42, 34)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(8212, 1243, 39, 36)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(8478, 1243, 39, 36)));
            Hashashin.Add((AnimationType.Ability3, !CanBeCanceled, 0.13f), HashashinAbility3);

            List<FrameHelper> HashashinUltimateTransformation = new List<FrameHelper>();
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(132, 1754, 34, 37)));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(420, 1754, 32, 37)));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(708, 1754, 28, 37)));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(987, 1754, 41, 37)));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(1277, 1748, 36, 43)));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(1552, 1753, 53, 38)));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(1831, 1756, 66, 35)));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(2116, 1757, 60, 34)));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(2404, 1757, 60, 34)));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(2692, 1757, 60, 34)));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(2961, 1752, 88, 40)));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(3254, 1749, 102, 43), new Rectangle(3254, 1749, 102, 43), canHit));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(3550, 1749, 96, 43), new Rectangle(3550, 1749, 96, 43), canHit));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(3843, 1728, 91, 64), new Rectangle(3843, 1728, 91, 64), canHit));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(4130, 1726, 91, 66), new Rectangle(4130, 1726, 91, 66), canHit));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(4418, 1727, 91, 65), new Rectangle(4418, 1727, 91, 65), canHit));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(4658, 1681, 191, 111), new Rectangle(4658, 1681, 191, 111), canHit));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(4944, 1682, 193, 110), new Rectangle(4944, 1682, 193, 110), canHit));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(5231, 1683, 196, 109), new Rectangle(5231, 1683, 196, 109), canHit));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(5519, 1684, 196, 108), new Rectangle(5519, 1648, 196, 108), canHit));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(5805, 1689, 200, 100), new Rectangle(5805, 1689, 200, 100), canHit));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(6084, 1686, 217, 103), new Rectangle(6084, 1686, 217, 103), canHit));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(6364, 1684, 233, 108), new Rectangle(6364, 1684, 233, 108), canHit));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(6648, 1682, 241, 110)));
            HashashinUltimateTransformation.Add(new FrameHelper(new Rectangle(7032, 1732, 62, 55)));
            Hashashin.Add((AnimationType.UltimateTransform, !CanBeCanceled, 0.18f), HashashinUltimateTransformation);

            List<FrameHelper> HashashinUltimateStand = new List<FrameHelper>();
            HashashinUltimateStand.Add(new FrameHelper(new Rectangle(120, 1860, 59, 56)));
            HashashinUltimateStand.Add(new FrameHelper(new Rectangle(408, 1859, 59, 56)));
            HashashinUltimateStand.Add(new FrameHelper(new Rectangle(696, 1857, 62, 59)));
            HashashinUltimateStand.Add(new FrameHelper(new Rectangle(984, 1857, 62, 58)));
            HashashinUltimateStand.Add(new FrameHelper(new Rectangle(1272, 1859, 62, 57)));
            HashashinUltimateStand.Add(new FrameHelper(new Rectangle(1560, 1861, 63, 54)));
            HashashinUltimateStand.Add(new FrameHelper(new Rectangle(1848, 1858, 63, 58)));
            HashashinUltimateStand.Add(new FrameHelper(new Rectangle(2136, 1860, 62, 55)));
            Hashashin.Add((AnimationType.UltimateStand, CanBeCanceled, 0.15f), HashashinUltimateStand);

            List<FrameHelper> HashashinUltimateRun = new List<FrameHelper>();
            HashashinUltimateRun.Add(new FrameHelper(new Rectangle(116, 1993, 57, 52)));
            HashashinUltimateRun.Add(new FrameHelper(new Rectangle(403, 1995, 58, 49)));
            HashashinUltimateRun.Add(new FrameHelper(new Rectangle(692, 1994, 57, 50)));
            HashashinUltimateRun.Add(new FrameHelper(new Rectangle(979, 1996, 58, 51)));
            HashashinUltimateRun.Add(new FrameHelper(new Rectangle(1270, 1995, 55, 49)));
            HashashinUltimateRun.Add(new FrameHelper(new Rectangle(1554, 1994, 59, 50)));
            Hashashin.Add((AnimationType.UltimateRun, CanBeCanceled, 0.14f), HashashinUltimateRun);

            List<FrameHelper> HashashinUltimateDodge = new List<FrameHelper>();
            HashashinUltimateDodge.Add(new FrameHelper(new Rectangle(122, 3014, 41, 53)));
            HashashinUltimateDodge.Add(new FrameHelper(new Rectangle(652, 3014, 48, 56)));
            HashashinUltimateDodge.Add(new FrameHelper(new Rectangle(1243, 3016, 40, 52)));
            HashashinUltimateDodge.Add(new FrameHelper(new Rectangle(1556, 3014, 59, 48)));
            HashashinUltimateDodge.Add(new FrameHelper(new Rectangle(1846, 3014, 55, 54)));
            HashashinUltimateDodge.Add(new FrameHelper(new Rectangle(2122, 3014, 64, 53)));
            Hashashin.Add((AnimationType.UltimateDodge, !CanBeCanceled, 0.14f), HashashinUltimateDodge);

            List<FrameHelper> HashashinUltimateBasicAttack = new List<FrameHelper>();
            HashashinUltimateBasicAttack.Add(new FrameHelper(new Rectangle(139, 2503, 59, 53)));
            HashashinUltimateBasicAttack.Add(new FrameHelper(new Rectangle(372, 2501, 133, 54), new Rectangle(449, 2500, 72, 57), new Rectangle(404, 2505, 43, 50), canHit));
            HashashinUltimateBasicAttack.Add(new FrameHelper(new Rectangle(659, 2500, 203, 56), new Rectangle(770, 2497, 122, 57), new Rectangle(690, 2505, 45, 45), canHit));
            HashashinUltimateBasicAttack.Add(new FrameHelper(new Rectangle(944, 2457, 198, 98), new Rectangle(1059, 2437, 102, 70), new Rectangle(979, 2502, 40, 47), canHit));
            HashashinUltimateBasicAttack.Add(new FrameHelper(new Rectangle(1232, 2473, 145, 83), new Rectangle(1311, 2458, 93, 59), new Rectangle(1264, 2497, 39, 59), canHit));
            HashashinUltimateBasicAttack.Add(new FrameHelper(new Rectangle(1232, 2473, 145, 83), new Rectangle(1311, 2458, 93, 59)));
            HashashinUltimateBasicAttack.Add(new FrameHelper(new Rectangle(1518, 2491, 95, 64)));
            HashashinUltimateBasicAttack.Add(new FrameHelper(new Rectangle(2142, 2500, 49, 55)));
            Hashashin.Add((AnimationType.UltimateBasicAttack, !CanBeCanceled, 0.17f), HashashinUltimateBasicAttack);

            List<FrameHelper> HashashinUltimateAbility1 = new List<FrameHelper>();
            HashashinUltimateAbility1.Add(new FrameHelper(new Rectangle(99, 2374, 52, 54)));
            HashashinUltimateAbility1.Add(new FrameHelper(new Rectangle(383, 2364, 80, 64), new Rectangle(359, 2360, 128, 52), new Rectangle(412, 2370, 30, 58), canHit));
            HashashinUltimateAbility1.Add(new FrameHelper(new Rectangle(671, 2366, 97, 61), new Rectangle(655, 2363, 124, 51), new Rectangle(707, 2374, 30, 51), canHit));
            HashashinUltimateAbility1.Add(new FrameHelper(new Rectangle(956, 2362, 103, 65), new Rectangle(931, 2353, 157, 69), new Rectangle(991, 2372, 30, 55), canHit));
            HashashinUltimateAbility1.Add(new FrameHelper(new Rectangle(1239, 2357, 115, 69), new Rectangle(1230, 2353, 131, 74), new Rectangle(1278, 2367, 30, 58), canHit));
            HashashinUltimateAbility1.Add(new FrameHelper(new Rectangle(1522, 2355, 121, 72), new Rectangle(1514, 2351, 138, 80), new Rectangle(1563, 2367, 43, 54), canHit));
            HashashinUltimateAbility1.Add(new FrameHelper(new Rectangle(1808, 2354, 128, 72), new Rectangle(1803, 2350, 138, 78), new Rectangle(1854, 2372, 36, 52), canHit));
            HashashinUltimateAbility1.Add(new FrameHelper(new Rectangle(2125, 2353, 71, 75)));
            Hashashin.Add((AnimationType.UltimateAbility1, !CanBeCanceled, 0.15f), HashashinUltimateAbility1);

            //List<FrameHelper> UndoTransform = new List<FrameHelper>();
            //UndoTransform.Add(new FrameHelper(new Rectangle(120, 3268, 59, 56)));
            //UndoTransform.Add(new FrameHelper(new Rectangle(400, 3267, 64, 52), new Rectangle(400, 3267, 64, 52), new Rectangle(419, 3270, 25, 38), canHit));
            //UndoTransform.Add(new FrameHelper(new Rectangle(665, 3265, 110, 55), new Rectangle(665, 3265, 110, 55), new Rectangle(709, 3274, 24, 38), canHit));
            //UndoTransform.Add(new FrameHelper(new Rectangle(665, 3265, 110, 55), new Rectangle(665, 3265, 110, 55)));
            //UndoTransform.Add(new FrameHelper(new Rectangle(953, 3269, 112, 51), new Rectangle(953, 3269, 112, 51), new Rectangle(994, 3278, 28, 35), canHit));
            //UndoTransform.Add(new FrameHelper(new Rectangle(953, 3269, 112, 51), new Rectangle(953, 3269, 112, 51)));
            //UndoTransform.Add(new FrameHelper(new Rectangle(1236, 3270, 121, 51), new Rectangle(1236, 3270, 121, 51), new Rectangle(1283, 3284, 25, 38), canHit));
            //UndoTransform.Add(new FrameHelper(new Rectangle(1236, 3270, 121, 51), new Rectangle(1236, 3270, 121, 51)));
            //UndoTransform.Add(new FrameHelper(new Rectangle(1520, 3270, 130, 57), new Rectangle(1520, 3270, 130, 57), new Rectangle(1571, 3295, 26, 32), canHit));
            //UndoTransform.Add(new FrameHelper(new Rectangle(1520, 3270, 130, 57), new Rectangle(1520, 3270, 130, 57)));
            //UndoTransform.Add(new FrameHelper(new Rectangle(1859, 3294, 26, 33)));
            //UndoTransform.Add(new FrameHelper(new Rectangle(2436, 3290, 26, 37)));
            //Hashashin.Add((AnimationType.UndoTransform, !CanBeCanceled, 0.17f), UndoTransform);

            List<FrameHelper> HashashinStand = new List<FrameHelper>();
            HashashinStand.Add(new FrameHelper(new Rectangle(132, 90, 34, 37)));
            HashashinStand.Add(new FrameHelper(new Rectangle(419, 91, 33, 36)));
            HashashinStand.Add(new FrameHelper(new Rectangle(705, 92, 31, 35)));
            HashashinStand.Add(new FrameHelper(new Rectangle(991, 92, 32, 35)));
            HashashinStand.Add(new FrameHelper(new Rectangle(1278, 92, 35, 35)));
            HashashinStand.Add(new FrameHelper(new Rectangle(1568, 92, 33, 35)));
            HashashinStand.Add(new FrameHelper(new Rectangle(1858, 92, 35, 35)));
            HashashinStand.Add(new FrameHelper(new Rectangle(2147, 91, 34, 36)));
            Hashashin.Add((AnimationType.Stand, CanBeCanceled, 0.28f), HashashinStand);

            Animations.Add(EntityName.Hashashin, Hashashin);
            EntityAbilites.Add(EntityName.Hashashin, HashashinAbilites);
            CharacterAbilityIcons.Add(EntityName.Hashashin, HashashinAbilityIcons);
            #endregion

            #region Skeleton
            EntitySpriteSheets.Add(EntityName.Skeleton, content.Load<Texture2D>("Skeleton"));
            EntityTextures.Add(EntityName.Skeleton, new Rectangle(17, 70, 15, 21));

            Dictionary<AnimationType, Ability> SkeletonAbilites = new Dictionary<AnimationType, Ability>()
            {
                [AnimationType.BasicAttack] = new SkeletonBasicAttack(0),
                [AnimationType.Death] = new SkeletonDeath(0),
                [AnimationType.Spawn] = new SkeletonSpawn(0),
            };
            Dictionary<(AnimationType, bool, float), List<FrameHelper>> Skeleton = new Dictionary<(AnimationType, bool, float), List<FrameHelper>>();

            List<FrameHelper> SkeletonRun = new List<FrameHelper>();
            SkeletonRun.Add(new FrameHelper(new Rectangle(5, 145, 36, 32)));
            SkeletonRun.Add(new FrameHelper(new Rectangle(70, 144, 35, 33)));
            SkeletonRun.Add(new FrameHelper(new Rectangle(134, 144, 35, 33)));
            SkeletonRun.Add(new FrameHelper(new Rectangle(198, 144, 35, 33)));
            SkeletonRun.Add(new FrameHelper(new Rectangle(261, 145, 36, 32)));
            SkeletonRun.Add(new FrameHelper(new Rectangle(325, 146, 36, 31)));
            SkeletonRun.Add(new FrameHelper(new Rectangle(389, 145, 36, 32)));
            SkeletonRun.Add(new FrameHelper(new Rectangle(452, 144, 38, 33)));
            SkeletonRun.Add(new FrameHelper(new Rectangle(516, 144, 38, 33)));
            SkeletonRun.Add(new FrameHelper(new Rectangle(580, 144, 38, 33)));
            //SkeletonRun.Add(new FrameHelper(new Rectangle(654, 145, 37, 32)));
            Skeleton.Add((AnimationType.Run, CanBeCanceled, 0.3f), SkeletonRun);

            List<FrameHelper> SkeletonBasicAttack = new List<FrameHelper>();
            SkeletonBasicAttack.Add(new FrameHelper(new Rectangle(8, 16, 34, 32)));
            SkeletonBasicAttack.Add(new FrameHelper(new Rectangle(69, 16, 37, 32)));
            SkeletonBasicAttack.Add(new FrameHelper(new Rectangle(129, 16, 41, 32)));
            SkeletonBasicAttack.Add(new FrameHelper(new Rectangle(196, 16, 38, 32)));
            SkeletonBasicAttack.Add(new FrameHelper(new Rectangle(269, 8, 51, 40), new Rectangle(268, 7, 53, 30), canHit));
            SkeletonBasicAttack.Add(new FrameHelper(new Rectangle(335, 9, 31, 39), new Rectangle(331, 8, 41, 20), canHit));
            SkeletonBasicAttack.Add(new FrameHelper(new Rectangle(401, 15, 26, 33)));
            SkeletonBasicAttack.Add(new FrameHelper(new Rectangle(466, 16, 25, 33)));
            SkeletonBasicAttack.Add(new FrameHelper(new Rectangle(514, 16, 62, 32), new Rectangle(512, 14, 65, 29), canHit));
            SkeletonBasicAttack.Add(new FrameHelper(new Rectangle(579, 15, 39, 33), new Rectangle(577, 13, 34, 24), canHit));
            SkeletonBasicAttack.Add(new FrameHelper(new Rectangle(646, 12, 36, 36)));
            SkeletonBasicAttack.Add(new FrameHelper(new Rectangle(707, 16, 39, 32)));
            SkeletonBasicAttack.Add(new FrameHelper(new Rectangle(776, 16, 34, 32)));
            Skeleton.Add((AnimationType.BasicAttack, !CanBeCanceled, 0.2f), SkeletonBasicAttack);

            List<FrameHelper> SkeletonDeath = new List<FrameHelper>();
            SkeletonDeath.Add(new FrameHelper(new Rectangle(137, 81, 31, 32)));
            SkeletonDeath.Add(new FrameHelper(new Rectangle(265, 83, 37, 30)));
            SkeletonDeath.Add(new FrameHelper(new Rectangle(329, 82, 38, 31)));
            SkeletonDeath.Add(new FrameHelper(new Rectangle(392, 80, 42, 33)));
            SkeletonDeath.Add(new FrameHelper(new Rectangle(457, 79, 42, 34)));
            SkeletonDeath.Add(new FrameHelper(new Rectangle(521, 79, 42, 34)));
            SkeletonDeath.Add(new FrameHelper(new Rectangle(585, 79, 42, 34)));
            SkeletonDeath.Add(new FrameHelper(new Rectangle(649, 80, 42, 33)));
            Skeleton.Add((AnimationType.Death, !CanBeCanceled, 0.2f), SkeletonDeath);

            List<FrameHelper> SkeletonStand = new List<FrameHelper>();
            SkeletonStand.Add(new FrameHelper(new Rectangle(9, 208, 33, 33)));
            Skeleton.Add((AnimationType.Stand, CanBeCanceled, 0.3f), SkeletonStand);

            List<FrameHelper> SkeletonSpawn = new List<FrameHelper>();
            SkeletonSpawn.Add(new FrameHelper(new Rectangle(9, 271, 32, 34)));
            SkeletonSpawn.Add(new FrameHelper(new Rectangle(73, 272, 32, 33)));
            SkeletonSpawn.Add(new FrameHelper(new Rectangle(137, 272, 32, 33)));
            Skeleton.Add((AnimationType.Spawn, !CanBeCanceled, 0.2f), SkeletonSpawn);

            Animations.Add(EntityName.Skeleton, Skeleton);
            EntityAbilites.Add(EntityName.Skeleton, SkeletonAbilites);
            #endregion

            #region Ghost Warrior
            EntitySpriteSheets.Add(EntityName.GhostWarrior, content.Load<Texture2D>("GhostWarrior"));
            EntityTextures.Add(EntityName.GhostWarrior, new Rectangle(49, 130, 77, 56));

            Dictionary<AnimationType, Ability> GhostWarriorAbilites = new Dictionary<AnimationType, Ability>()
            {
               
            };
            Dictionary<(AnimationType, bool, float), List<FrameHelper>> GhostWarrior = new Dictionary<(AnimationType, bool, float), List<FrameHelper>>();

            List<FrameHelper> GhostWarriorStand = new List<FrameHelper>();
            GhostWarriorStand.Add(new FrameHelper(new Rectangle(49, 130, 77, 56)));
            GhostWarriorStand.Add(new FrameHelper(new Rectangle(179, 129, 77, 57)));
            GhostWarriorStand.Add(new FrameHelper(new Rectangle(309, 127, 77, 59)));
            GhostWarriorStand.Add(new FrameHelper(new Rectangle(439, 126, 77, 60)));
            GhostWarriorStand.Add(new FrameHelper(new Rectangle(569, 129, 77, 57)));
            GhostWarriorStand.Add(new FrameHelper(new Rectangle(699, 129, 77, 57)));
            GhostWarriorStand.Add(new FrameHelper(new Rectangle(829, 127, 77, 59)));
            GhostWarriorStand.Add(new FrameHelper(new Rectangle(959, 129, 77, 57)));
            GhostWarrior.Add((AnimationType.Stand, CanBeCanceled, 0.1f), GhostWarriorStand);

            List<FrameHelper> GhostWarriorRun = new List<FrameHelper>();
            GhostWarriorRun.Add(new FrameHelper(new Rectangle(43, 232, 70, 58)));
            GhostWarriorRun.Add(new FrameHelper(new Rectangle(176, 228, 67, 62)));
            GhostWarriorRun.Add(new FrameHelper(new Rectangle(307, 227, 66, 63)));
            GhostWarriorRun.Add(new FrameHelper(new Rectangle(437, 231, 66, 59)));
            GhostWarriorRun.Add(new FrameHelper(new Rectangle(568, 233, 65, 57)));
            GhostWarriorRun.Add(new FrameHelper(new Rectangle(700, 229, 63, 61)));
            GhostWarriorRun.Add(new FrameHelper(new Rectangle(830, 233, 63, 57)));
            GhostWarriorRun.Add(new FrameHelper(new Rectangle(956, 231, 67, 59)));
            GhostWarrior.Add((AnimationType.Run, CanBeCanceled, 0.3f), GhostWarriorRun);

            Animations.Add(EntityName.GhostWarrior, GhostWarrior);
            EntityAbilites.Add(EntityName.GhostWarrior, GhostWarriorAbilites);
            #endregion

        }
    }
}
