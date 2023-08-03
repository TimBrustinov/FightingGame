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
        public Dictionary<EntityName, Dictionary<AnimationType, EntityAction>> EntityActions;

        public Dictionary<EntityName, Dictionary<AnimationType, Rectangle>> CharacterAbilityIcons;
        public Dictionary<EntityName, Dictionary<CharacterPortrait, Texture2D>> CharacterPortraits;
        private ContentManager()
        {
            CharacterAbilityIcons = new Dictionary<EntityName, Dictionary<AnimationType, Rectangle>>();
            EntityTextures = new Dictionary<EntityName, Rectangle>();
            EntitySpriteSheets = new Dictionary<EntityName, Texture2D>();
            Animations = new Dictionary<EntityName, Dictionary<(AnimationType, bool, float), List<FrameHelper>>>();
            EntityActions = new Dictionary<EntityName, Dictionary<AnimationType, EntityAction>>();
            CharacterPortraits = new Dictionary<EntityName, Dictionary<CharacterPortrait, Texture2D>>();
        }

        public static ContentManager Instance { get; } = new ContentManager();

        public void LoadContent(Content content)
        {
            bool CanBeCanceled = true;
            bool canHit = true;
            Font = content.Load<SpriteFont>("Font");
            Shadow = content.Load<Texture2D>("SHADOW");

            #region Hashashin
            EntitySpriteSheets.Add(EntityName.Hashashin, content.Load<Texture2D>("HashashinFullSpritesheet"));
            EntityTextures.Add(EntityName.Hashashin, new Rectangle(132, 90, 34, 37));

           

            Dictionary<AnimationType, Rectangle> HashashinAbilityIcons = new Dictionary<AnimationType, Rectangle>()
            { 
                [AnimationType.Ability1] = new Rectangle(988, 597, 50, 50),
                [AnimationType.Ability2] = new Rectangle(5677, 1106, 50, 50),
                [AnimationType.Ability3] = new Rectangle(1300, 1236, 50, 50),
                [AnimationType.UltimateAbility1] = new Rectangle(690, 2361, 50, 50),
                [AnimationType.UltimateAbility2] = new Rectangle(4836, 2743, 50, 50),
                [AnimationType.UltimateAbility3] = new Rectangle(1601, 2884, 50, 50),
            };

            Dictionary<CharacterPortrait, Texture2D> HashashinPortraits = new Dictionary<CharacterPortrait, Texture2D>()
            { 
                [CharacterPortrait.HashashinBase] = content.Load<Texture2D>("wind_hashashin"),
                [CharacterPortrait.HashashinElemental] = content.Load<Texture2D>("wind_elemental"),
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
            HashashinAbility3.Add(new FrameHelper(new Rectangle(3279, 1227, 68, 52), new Rectangle(3270, 1221, 85, 42), new Rectangle(3300, 1249, 27, 32), canHit));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(3874, 1249, 66, 30)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(4240, 1243, 47, 29)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(4511, 1242, 50, 30)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(4782, 1242, 50, 30)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(4976, 1235, 91, 41), new Rectangle(4993, 1222, 105, 63), new Rectangle(4974, 1239, 39, 38), canHit));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(4976, 1235, 91, 41), new Rectangle(4993, 1222, 105, 63), new Rectangle(4974, 1239, 39, 38), canHit));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(5588, 1225, 114, 54), new Rectangle(5568, 1217, 105, 73), new Rectangle(5662, 1244, 29, 36), canHit));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(5588, 1225, 114, 54), new Rectangle(5568, 1217, 105, 73), new Rectangle(5662, 1244, 29, 36), canHit));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(6219, 1245, 59, 34)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(7072, 1245, 62, 34)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(7364, 1247, 47, 30)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(7938, 1244, 42, 34)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(8212, 1243, 39, 36)));
            HashashinAbility3.Add(new FrameHelper(new Rectangle(8478, 1243, 39, 36)));
            Hashashin.Add((AnimationType.Ability3, !CanBeCanceled, 0.12f), HashashinAbility3);

            //List<FrameHelper> HashashinAbility3 = new List<FrameHelper>();
            //HashashinAbility3.Add(new FrameHelper(new Rectangle(978, 986, 45, 37)));
            //HashashinAbility3.Add(new FrameHelper(new Rectangle(1266, 986, 47, 37)));
            //HashashinAbility3.Add(new FrameHelper(new Rectangle(1554, 986, 47, 37)));
            //HashashinAbility3.Add(new FrameHelper(new Rectangle(1842, 986, 51, 37)));
            //HashashinAbility3.Add(new FrameHelper(new Rectangle(2134, 974, 75, 49), new Rectangle(2166, 971, 49, 48), new Rectangle(2148, 988, 31, 36), canHit));
            //HashashinAbility3.Add(new FrameHelper(new Rectangle(2720, 977, 64, 46), new Rectangle(2746, 976, 38, 44), new Rectangle(2724, 989, 30, 34), canHit));
            //HashashinAbility3.Add(new FrameHelper(new Rectangle(3010, 978, 65, 45), new Rectangle(3035, 975, 42, 44), new Rectangle(3013, 986, 26, 37), canHit));
            //HashashinAbility3.Add(new FrameHelper(new Rectangle(3282, 978, 73, 45), new Rectangle(3282, 978, 78, 36), new Rectangle(3297, 986, 30, 37), canHit));
            //HashashinAbility3.Add(new FrameHelper(new Rectangle(3570, 986, 60, 37), new Rectangle(3586, 1002, 65, 14), new Rectangle(3585, 986, 31, 37), canHit));
            //HashashinAbility3.Add(new FrameHelper(new Rectangle(3871, 980, 34, 43)));
            //HashashinAbility3.Add(new FrameHelper(new Rectangle(4156, 986, 41, 37)));
            //HashashinAbility3.Add(new FrameHelper(new Rectangle(4452, 986, 29, 37)));
            //Hashashin.Add((AnimationType.Ability3, !CanBeCanceled, 0.16f), HashashinAbility3);

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

            List<FrameHelper> HashashinUltimateAbility2 = new List<FrameHelper>();
            HashashinUltimateAbility2.Add(new FrameHelper(new Rectangle(3604, 2764, 63, 52)));
            HashashinUltimateAbility2.Add(new FrameHelper(new Rectangle(3897, 2743, 70, 73), new Rectangle(3926, 2735, 48, 88), new Rectangle(3906, 2758, 33, 50), canHit));
            HashashinUltimateAbility2.Add(new FrameHelper(new Rectangle(4158, 2697, 118, 119), new Rectangle(4204, 2692, 90, 133), new Rectangle(4174, 2757, 47, 54), canHit));
            HashashinUltimateAbility2.Add(new FrameHelper(new Rectangle(4445, 2691, 128, 125), new Rectangle(4493, 2681, 95, 144), new Rectangle(4445, 2758, 39, 55), canHit));
            HashashinUltimateAbility2.Add(new FrameHelper(new Rectangle(4738, 2692, 141, 124), new Rectangle(4798, 2684, 91, 139), new Rectangle(4738, 2753, 50, 58), canHit));
            HashashinUltimateAbility2.Add(new FrameHelper(new Rectangle(5015, 2696, 160, 116), new Rectangle(5102, 2687, 84, 131), new Rectangle(5015, 2754, 63, 58), canHit));
            HashashinUltimateAbility2.Add(new FrameHelper(new Rectangle(5304, 2694, 167, 117), new Rectangle(5387, 2689, 97, 125), new Rectangle(5304, 2756, 62, 55), canHit));
            Hashashin.Add((AnimationType.UltimateAbility2, !CanBeCanceled, 0.2f), HashashinUltimateAbility2);

            List<FrameHelper> HashashinUndoTransform = new List<FrameHelper>();
            HashashinUndoTransform.Add(new FrameHelper(new Rectangle(120, 3268, 59, 56)));
            HashashinUndoTransform.Add(new FrameHelper(new Rectangle(400, 3267, 64, 52)));
            HashashinUndoTransform.Add(new FrameHelper(new Rectangle(665, 3265, 110, 55)));
            HashashinUndoTransform.Add(new FrameHelper(new Rectangle(953, 3269, 112, 51)));
            HashashinUndoTransform.Add(new FrameHelper(new Rectangle(1236, 3270, 121, 51)));
            HashashinUndoTransform.Add(new FrameHelper(new Rectangle(1520, 3270, 130, 57)));
            HashashinUndoTransform.Add(new FrameHelper(new Rectangle(1859, 3294, 26, 33)));
            HashashinUndoTransform.Add(new FrameHelper(new Rectangle(2147, 3295, 26, 33)));
            HashashinUndoTransform.Add(new FrameHelper(new Rectangle(2436, 3290, 26, 37)));
            Hashashin.Add((AnimationType.UndoTransform, !CanBeCanceled, 0.2f), HashashinUndoTransform);

            List<FrameHelper> HashashinUltimateAbility3 = new List<FrameHelper>();
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(122, 2886, 41, 53)));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(641, 2886, 48, 56)));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(907, 2859, 179, 81), new Rectangle(952, 2852, 151, 100), new Rectangle(907, 2872, 36, 67), canHit));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(1184, 2866, 193, 73), new Rectangle(1238, 2858, 149, 92), new Rectangle(1195, 2880, 35, 59), canHit));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(1477, 2866, 174, 74), new Rectangle(1523, 2860, 138, 90), new Rectangle(1491, 2891, 29, 50), canHit));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(1762, 2865, 180, 74), new Rectangle(1813, 2860, 138, 83), new Rectangle(1773, 2886, 38, 53), canHit));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(2048, 2859, 190, 81), new Rectangle(2098, 2853, 152, 95), new Rectangle(2067, 2891, 24, 49), canHit));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(2331, 2866, 184, 73), new Rectangle(2385, 2860, 141, 90), new Rectangle(2354, 2889, 34, 50), canHit));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(2629, 2865, 177, 75), new Rectangle(2679, 2859, 139, 90), new Rectangle(2645, 2889, 35, 51), canHit));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(2914, 2866, 191, 73), new Rectangle(2963, 2860, 150, 87), new Rectangle(2930, 2878, 35, 61), canHit));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(3204, 2866, 175, 74), new Rectangle(3249, 2861, 138, 87), new Rectangle(3216, 2896, 40, 44), canHit));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(3463, 2863, 57, 76)));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(3756, 2864, 51, 76)));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(4064, 2864, 47, 75)));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(4642, 2858, 157, 84), new Rectangle(4735, 2849, 76, 98), new Rectangle(4655, 2903, 50, 39), canHit));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(5218, 2833, 245, 109), new Rectangle(5376, 2826, 93, 113), new Rectangle(5231, 2904, 50, 38), canHit));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(5533, 2830, 225, 111), new Rectangle(5681, 2826, 83, 115), new Rectangle(5533, 2905, 46, 36), canHit));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(5874, 2829, 173, 102), new Rectangle(5967, 2826, 84, 109), new Rectangle(5874, 2886, 51, 35), canHit));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(5874, 2829, 173, 102), new Rectangle(5967, 2826, 84, 109), new Rectangle(5874, 2886, 51, 35), canHit));
            HashashinUltimateAbility3.Add(new FrameHelper(new Rectangle(6168, 2886, 50, 54)));
            Hashashin.Add((AnimationType.UltimateAbility3, !CanBeCanceled, 0.18f), HashashinUltimateAbility3);

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

            Dictionary<AnimationType, EntityAction> HashashinAbilites = new Dictionary<AnimationType, EntityAction>()
            {
                [AnimationType.BasicAttack] = new Ability(AnimationType.BasicAttack, HashashinBasicAttack, !CanBeCanceled, 0.07f, 0, default, 5, true),
                [AnimationType.Stand] = new Stand(AnimationType.Stand, HashashinStand, CanBeCanceled, 0.13f),
                [AnimationType.Dodge] = new Dodge(AnimationType.Dodge, HashashinDodge, !CanBeCanceled, 0.1f, 15, 5),
                [AnimationType.Ability1] = new Ability(AnimationType.Ability1, HashashinAbility1, !CanBeCanceled, 0.115f, 2, default, 7, true),
                [AnimationType.Ability2] = new Ability(AnimationType.Ability2, HashashinAbility2, !CanBeCanceled, 0.1f, 2, default, 5, true),
                [AnimationType.Ability3] = new Ability(AnimationType.Ability3, HashashinAbility3, !CanBeCanceled, 0.065f, 3, default, 8, false),
                [AnimationType.UltimateTransform] = new Ability(AnimationType.UltimateTransform, HashashinUltimateTransformation, !CanBeCanceled, 0.1f, 60, 0, 5, false),
                [AnimationType.UltimateStand] = new Stand(AnimationType.UltimateStand, HashashinUltimateStand, CanBeCanceled, 0.1f),
                [AnimationType.UltimateRun] = new Run(AnimationType.UltimateRun, HashashinUltimateRun, CanBeCanceled, 0.1f),
                [AnimationType.UltimateAbility1] = new Ability(AnimationType.UltimateAbility1, HashashinUltimateAbility1, !CanBeCanceled, 0.08f, 3, default, 8, true),
                [AnimationType.UltimateAbility2] = new Ability(AnimationType.UltimateAbility2, HashashinUltimateAbility2, !CanBeCanceled, 0.08f, 3, default, 8, true),
                [AnimationType.UltimateAbility3] = new Ability(AnimationType.UltimateAbility3, HashashinUltimateAbility3, !CanBeCanceled, 0.08f, 3, default, 8, true),
                [AnimationType.UltimateBasicAttack] = new Ability(AnimationType.UltimateBasicAttack, HashashinUltimateBasicAttack, !CanBeCanceled, 0.08f, 3, default, 8, true),
                [AnimationType.UltimateDodge] = new Dodge(AnimationType.UltimateDodge, HashashinUltimateDodge, !CanBeCanceled, 0.1f, 15, 5),
                [AnimationType.UndoTransform] = new Ability(AnimationType.UndoTransform, HashashinUndoTransform, !CanBeCanceled, 0.1f, 5, 0, 0, false),
                [AnimationType.Run] = new Run(AnimationType.Run, HashashinRun, CanBeCanceled, 0.13f),

                //[AnimationType.UltimateAbility2] = new Ability(AnimationType.UltimateAbility2, 3, default, 10, true),
                //[AnimationType.UltimateAbility3] = new Ability(AnimationType.UltimateAbility3, 3, default, 10, true),
                //[AnimationType.UndoTransform] = new HashashinUndoTransform(0),
                //[AnimationType.UltimateDodge] = new HashashinUltimateDodge(0),
                //[AnimationType.UltimateBasicAttack] = new HashashinUltimateBasicAttack(0),
            };

            Animations.Add(EntityName.Hashashin, Hashashin);
            EntityActions.Add(EntityName.Hashashin, HashashinAbilites);
            CharacterAbilityIcons.Add(EntityName.Hashashin, HashashinAbilityIcons);
            CharacterPortraits.Add(EntityName.Hashashin, HashashinPortraits);
            #endregion

            #region Skeleton
            EntitySpriteSheets.Add(EntityName.Skeleton, content.Load<Texture2D>("Skeleton"));
            EntityTextures.Add(EntityName.Skeleton, new Rectangle(17, 70, 15, 21));

            //Dictionary<AnimationType, Ability> SkeletonAbilites = new Dictionary<AnimationType, Ability>()
            //{
            //    [AnimationType.BasicAttack] = new SkeletonBasicAttack(0),
            //    [AnimationType.Death] = new SkeletonDeath(0),
            //    [AnimationType.Spawn] = new SkeletonSpawn(0),
            //};
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
            //EntityAbilites.Add(EntityName.Skeleton, SkeletonAbilites);
            #endregion

            #region SkeletonCat

            EntitySpriteSheets.Add(EntityName.SkeletonCat, content.Load<Texture2D>("SkeletonCat"));
            EntityTextures.Add(EntityName.SkeletonCat, new Rectangle(33, 27, 32, 26));

            //Dictionary<AnimationType, Ability> SkeletonCatAbilites = new Dictionary<AnimationType, Ability>()
            //{
            //    [AnimationType.BasicAttack] = new SkeletonCatBasicAttack(0, 70),
            //    [AnimationType.Death] = new SkeletonCatDeath(0),
            //    [AnimationType.Spawn] = new SkeletonCatSpawn(0),
            //};
            Dictionary<(AnimationType, bool, float), List<FrameHelper>> SkeletonCat = new Dictionary<(AnimationType, bool, float), List<FrameHelper>>();

            List<FrameHelper> skeletonCatBasicAttack = new List<FrameHelper>();
            skeletonCatBasicAttack.Add(new FrameHelper(new Rectangle(33, 27, 32, 26)));
            skeletonCatBasicAttack.Add(new FrameHelper(new Rectangle(127, 28, 33, 25)));
            skeletonCatBasicAttack.Add(new FrameHelper(new Rectangle(221, 30, 34, 23)));
            skeletonCatBasicAttack.Add(new FrameHelper(new Rectangle(312, 34, 39, 19)));
            skeletonCatBasicAttack.Add(new FrameHelper(new Rectangle(408, 34, 39, 19)));
            skeletonCatBasicAttack.Add(new FrameHelper(new Rectangle(504, 34, 39, 19)));
            skeletonCatBasicAttack.Add(new FrameHelper(new Rectangle(600, 34, 39, 19)));
            skeletonCatBasicAttack.Add(new FrameHelper(new Rectangle(696, 34, 39, 19)));
            skeletonCatBasicAttack.Add(new FrameHelper(new Rectangle(796, 34, 35, 19)));
            skeletonCatBasicAttack.Add(new FrameHelper(new Rectangle(901, 35, 41, 18), new Rectangle(914, 41, 34, 12), canHit));
            skeletonCatBasicAttack.Add(new FrameHelper(new Rectangle(997, 35, 43, 18), new Rectangle(1018, 40, 26, 13), canHit));
            skeletonCatBasicAttack.Add(new FrameHelper(new Rectangle(1091, 35, 46, 18), new Rectangle(114, 40, 27, 13), canHit));
            skeletonCatBasicAttack.Add(new FrameHelper(new Rectangle(1187, 35, 34, 18), new Rectangle(1211, 41, 13, 10), canHit));
            skeletonCatBasicAttack.Add(new FrameHelper(new Rectangle(1283, 35, 34, 18), new Rectangle(1308, 41, 13, 9), canHit));
            SkeletonCat.Add((AnimationType.BasicAttack, !CanBeCanceled, 0.13f), skeletonCatBasicAttack);

            List<FrameHelper> SkeletonCatDeath = new List<FrameHelper>();
            SkeletonCatDeath.Add(new FrameHelper(new Rectangle(33, 207, 32, 26)));
            SkeletonCatDeath.Add(new FrameHelper(new Rectangle(129, 207, 32, 26)));
            SkeletonCatDeath.Add(new FrameHelper(new Rectangle(225, 208, 32, 25)));
            SkeletonCatDeath.Add(new FrameHelper(new Rectangle(321, 210, 32, 23)));
            SkeletonCatDeath.Add(new FrameHelper(new Rectangle(418, 212, 32, 21)));
            SkeletonCatDeath.Add(new FrameHelper(new Rectangle(515, 219, 30, 14)));
            SkeletonCatDeath.Add(new FrameHelper(new Rectangle(611, 221, 30, 12)));
            SkeletonCatDeath.Add(new FrameHelper(new Rectangle(705, 222, 32, 11)));
            SkeletonCatDeath.Add(new FrameHelper(new Rectangle(803, 222, 30, 11)));
            SkeletonCatDeath.Add(new FrameHelper(new Rectangle(705, 222, 32, 11)));
            SkeletonCatDeath.Add(new FrameHelper(new Rectangle(803, 222, 30, 11)));
            SkeletonCatDeath.Add(new FrameHelper(new Rectangle(899, 223, 30, 10)));
            SkeletonCatDeath.Add(new FrameHelper(new Rectangle(995, 225, 30, 8)));
            SkeletonCat.Add((AnimationType.Death, !CanBeCanceled, 0.2f), SkeletonCatDeath);

            List<FrameHelper> SkeletonCatRun = new List<FrameHelper>();
            SkeletonCatRun.Add(new FrameHelper(new Rectangle(19, 138, 47, 13)));
            SkeletonCatRun.Add(new FrameHelper(new Rectangle(114, 135, 47, 16)));
            SkeletonCatRun.Add(new FrameHelper(new Rectangle(211, 134, 45, 17)));
            SkeletonCatRun.Add(new FrameHelper(new Rectangle(309, 133, 44, 18)));
            SkeletonCatRun.Add(new FrameHelper(new Rectangle(404, 136, 49, 15)));
            SkeletonCatRun.Add(new FrameHelper(new Rectangle(498, 137, 48, 14)));
            SkeletonCatRun.Add(new FrameHelper(new Rectangle(594, 137, 46, 14)));
            SkeletonCatRun.Add(new FrameHelper(new Rectangle(691, 137, 46, 14)));
            SkeletonCat.Add((AnimationType.Run, CanBeCanceled, 0.15f), SkeletonCatRun);

            List<FrameHelper> SkeletonCatSpawn = new List<FrameHelper>();
            SkeletonCatSpawn.Add(new FrameHelper(new Rectangle(514, 308, 32, 26)));
            SkeletonCatSpawn.Add(new FrameHelper(new Rectangle(706, 308, 32, 26)));
            SkeletonCatSpawn.Add(new FrameHelper(new Rectangle(802, 308, 32, 26)));
            SkeletonCat.Add((AnimationType.Spawn, !CanBeCanceled, 0.2f), SkeletonCatSpawn);

            Animations.Add(EntityName.SkeletonCat, SkeletonCat);
            //EntityAbilites.Add(EntityName.SkeletonCat, SkeletonCatAbilites);

            #endregion

            #region Ghost Warrior
            EntitySpriteSheets.Add(EntityName.GhostWarrior, content.Load<Texture2D>("GhostWarrior"));
            EntityTextures.Add(EntityName.GhostWarrior, new Rectangle(49, 130, 77, 56));

            //Dictionary<AnimationType, Ability> GhostWarriorAbilites = new Dictionary<AnimationType, Ability>()
            //{
            //    [AnimationType.Death] = new GhostWarriorDeath(0),
            //    [AnimationType.BasicAttack] = new GhostWarriorBasicAttack(0, 90),
            //};
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

            List<FrameHelper> GhostWarriorDeath = new List<FrameHelper>();
            GhostWarriorDeath.Add(new FrameHelper(new Rectangle(47, 339, 64, 58)));
            GhostWarriorDeath.Add(new FrameHelper(new Rectangle(177, 340, 66, 57)));
            GhostWarriorDeath.Add(new FrameHelper(new Rectangle(307, 341, 63, 56)));
            GhostWarriorDeath.Add(new FrameHelper(new Rectangle(437, 337, 63, 60)));
            GhostWarriorDeath.Add(new FrameHelper(new Rectangle(567, 339, 63, 58)));
            GhostWarriorDeath.Add(new FrameHelper(new Rectangle(697, 333, 63, 64)));
            GhostWarriorDeath.Add(new FrameHelper(new Rectangle(0, 0, 0, 0)));
            GhostWarrior.Add((AnimationType.Death, !CanBeCanceled, 0.3f), GhostWarriorDeath);

            List<FrameHelper> GhostWarriorBasicAttack = new List<FrameHelper>();
            GhostWarriorBasicAttack.Add(new FrameHelper(new Rectangle(49, 39, 77, 56), new Rectangle(49, 39, 36, 56)));
            GhostWarriorBasicAttack.Add(new FrameHelper(new Rectangle(179, 40, 74, 55), new Rectangle(179, 40, 32, 55)));
            GhostWarriorBasicAttack.Add(new FrameHelper(new Rectangle(309, 38, 79, 57), new Rectangle(339, 45, 49, 36), new Rectangle(309, 38, 32, 57), canHit));
            GhostWarriorBasicAttack.Add(new FrameHelper(new Rectangle(416, 7, 78, 88), new Rectangle(412, 3, 57, 85), new Rectangle(439, 44, 30, 51), canHit));
            GhostWarriorBasicAttack.Add(new FrameHelper(new Rectangle(546, 14, 54, 81), new Rectangle(546, 14, 38, 75), new Rectangle(569, 46, 31, 49), canHit));
            GhostWarriorBasicAttack.Add(new FrameHelper(new Rectangle(675, 35, 56, 60), new Rectangle(675, 46, 37, 45), new Rectangle(699, 48, 32, 47), canHit));
            GhostWarriorBasicAttack.Add(new FrameHelper(new Rectangle(811, 3, 96, 92), new Rectangle(833, 3, 74, 92), new Rectangle(829, 58, 29, 36), canHit));
            GhostWarriorBasicAttack.Add(new FrameHelper(new Rectangle(1088, 55, 72, 40), new Rectangle(1105, 80, 55, 15), new Rectangle(1088, 55, 35, 40), canHit));
            GhostWarriorBasicAttack.Add(new FrameHelper(new Rectangle(1219, 52, 78, 43)));
            GhostWarrior.Add((AnimationType.BasicAttack, !CanBeCanceled, 0.2f), GhostWarriorBasicAttack);

            List<FrameHelper> GhostWarriorRun = new List<FrameHelper>();
            GhostWarriorRun.Add(new FrameHelper(new Rectangle(43, 232, 70, 58), new Rectangle(43, 232, 35, 58)));
            GhostWarriorRun.Add(new FrameHelper(new Rectangle(176, 228, 67, 62), new Rectangle(176, 228, 30, 62)));
            GhostWarriorRun.Add(new FrameHelper(new Rectangle(307, 227, 66, 63), new Rectangle(307, 227, 29, 63)));
            GhostWarriorRun.Add(new FrameHelper(new Rectangle(437, 231, 66, 59), new Rectangle(437, 231, 30, 59)));
            GhostWarriorRun.Add(new FrameHelper(new Rectangle(568, 233, 65, 57), new Rectangle(568, 233, 30, 57)));
            GhostWarriorRun.Add(new FrameHelper(new Rectangle(700, 229, 63, 61), new Rectangle(700, 229, 26, 61)));
            GhostWarriorRun.Add(new FrameHelper(new Rectangle(830, 233, 63, 57), new Rectangle(830, 233, 26, 57)));
            GhostWarriorRun.Add(new FrameHelper(new Rectangle(956, 231, 67, 59), new Rectangle(956, 231, 31, 59)));
            GhostWarrior.Add((AnimationType.Run, CanBeCanceled, 0.4f), GhostWarriorRun);

            Animations.Add(EntityName.GhostWarrior, GhostWarrior);
            //EntityAbilites.Add(EntityName.GhostWarrior, GhostWarriorAbilites);
            #endregion

            #region Ghost Warrior 2
            EntitySpriteSheets.Add(EntityName.GhostWarrior2, content.Load<Texture2D>("GhostWarrior2"));
            EntityTextures.Add(EntityName.GhostWarrior2, new Rectangle(49, 130, 77, 56));

            //Dictionary<AnimationType, Ability> GhostWarrior2Abilites = new Dictionary<AnimationType, Ability>()
            //{
            //    [AnimationType.BasicAttack] = new GhostWarrior2BasicAttack(0, 90),
            //    [AnimationType.Death] = new GhostWarrior2Death(0),
            //};
            Dictionary<(AnimationType, bool, float), List<FrameHelper>> GhostWarrior2 = new Dictionary<(AnimationType, bool, float), List<FrameHelper>>();

            List<FrameHelper> GhostWarrior2Run = new List<FrameHelper>();
            GhostWarrior2Run.Add(new FrameHelper(new Rectangle(52, 477, 52, 53)));
            GhostWarrior2Run.Add(new FrameHelper(new Rectangle(213, 480, 51, 51)));
            GhostWarrior2Run.Add(new FrameHelper(new Rectangle(373, 476, 51, 56)));
            GhostWarrior2Run.Add(new FrameHelper(new Rectangle(533, 476, 51, 57)));
            GhostWarrior2Run.Add(new FrameHelper(new Rectangle(692, 479, 52, 54)));
            GhostWarrior2Run.Add(new FrameHelper(new Rectangle(853, 481, 51, 54)));
            GhostWarrior2Run.Add(new FrameHelper(new Rectangle(1013, 475, 51, 58)));
            GhostWarrior2Run.Add(new FrameHelper(new Rectangle(1173, 474, 51, 56)));
            GhostWarrior2.Add((AnimationType.Run, CanBeCanceled, 0.2f), GhostWarrior2Run);

            List<FrameHelper> GhostWarrior2BasicAttack = new List<FrameHelper>();
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(45, 51, 44, 56)));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(205, 46, 43, 60)));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(352, 41, 57, 66)));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(514, 30, 52, 77), new Rectangle(514, 30, 52, 30), new Rectangle(548, 48, 18, 59), canHit));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(702, 22, 83, 90), new Rectangle(702, 22, 92, 90), new Rectangle(711, 63, 29, 49), canHit));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(871, 23, 72, 91), new Rectangle(871, 23, 72, 91), new Rectangle(871, 63, 29, 47), canHit));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(1031, 34, 73, 80), new Rectangle(1031, 34, 73, 80), new Rectangle(1031, 61, 30, 53), canHit));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(1189, 52, 69, 67), new Rectangle(1189, 52, 69, 67), new Rectangle(1189, 56, 31, 58), canHit));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(1345, 54, 65, 66), new Rectangle(1345, 54, 65, 66), new Rectangle(1345, 54, 34, 59), canHit));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(1505, 52, 35, 61)));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(65, 176, 35, 61)));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(231, 174, 30, 66)));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(391, 169, 28, 70)));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(551, 176, 30, 61)));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(676, 156, 103, 74), new Rectangle(676, 156, 103, 74), new Rectangle(699, 176, 37, 54), canHit));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(839, 156, 98, 73), new Rectangle(839, 156, 98, 73), new Rectangle(857, 174, 34, 55), canHit));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(997, 158, 97, 71), new Rectangle(997, 158, 97, 71), new Rectangle(1020, 175, 33, 54), canHit));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(1157, 160, 82, 69), new Rectangle(1157, 160, 82, 69), new Rectangle(1181, 175, 31, 54), canHit));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(1322, 158, 65, 71), new Rectangle(1322, 158, 65, 71), new Rectangle(1336, 175, 36, 54), canHit));
            GhostWarrior2BasicAttack.Add(new FrameHelper(new Rectangle(1479, 176, 53, 55), new Rectangle(1499, 176, 33, 55)));
            GhostWarrior2.Add((AnimationType.BasicAttack, !CanBeCanceled, 0.2f), GhostWarrior2BasicAttack);

            List<FrameHelper> GhostWarrior2Death = new List<FrameHelper>();
            GhostWarrior2Death.Add(new FrameHelper(new Rectangle(59, 676, 48, 69)));
            GhostWarrior2Death.Add(new FrameHelper(new Rectangle(215, 674, 56, 70)));
            GhostWarrior2Death.Add(new FrameHelper(new Rectangle(377, 663, 59, 82)));
            GhostWarrior2Death.Add(new FrameHelper(new Rectangle(538, 657, 57, 88)));
            GhostWarrior2Death.Add(new FrameHelper(new Rectangle(676, 643, 79, 102)));
            GhostWarrior2Death.Add(new FrameHelper(new Rectangle(831, 645, 78, 100)));
            GhostWarrior2Death.Add(new FrameHelper(new Rectangle(992, 651, 77, 93)));
            GhostWarrior2Death.Add(new FrameHelper(new Rectangle(1156, 655, 81, 99)));
            GhostWarrior2Death.Add(new FrameHelper(new Rectangle(1318, 650, 79, 98)));
            GhostWarrior2Death.Add(new FrameHelper(new Rectangle(1476, 642, 82, 96)));
            GhostWarrior2Death.Add(new FrameHelper(new Rectangle(1636, 646, 83, 89)));
            GhostWarrior2Death.Add(new FrameHelper(new Rectangle(1796, 644, 82, 85)));
            GhostWarrior2Death.Add(new FrameHelper(new Rectangle(1956, 642, 82, 85)));
            GhostWarrior2Death.Add(new FrameHelper(new Rectangle(2118, 649, 79, 73)));
            GhostWarrior2.Add((AnimationType.Death, !CanBeCanceled, 0.2f), GhostWarrior2Death);

            List<FrameHelper> GhostWarrior2Stand = new List<FrameHelper>();
            GhostWarrior2Stand.Add(new FrameHelper(new Rectangle(68, 283, 35, 82)));
            GhostWarrior2Stand.Add(new FrameHelper(new Rectangle(228, 290, 46, 74)));
            GhostWarrior2Stand.Add(new FrameHelper(new Rectangle(388, 289, 38, 75)));
            GhostWarrior2Stand.Add(new FrameHelper(new Rectangle(548, 280, 40, 84)));
            GhostWarrior2Stand.Add(new FrameHelper(new Rectangle(708, 279, 43, 83)));
            GhostWarrior2.Add((AnimationType.Stand, CanBeCanceled, 0.2f), GhostWarrior2Stand);

            Animations.Add(EntityName.GhostWarrior2, GhostWarrior2);
            //EntityAbilites.Add(EntityName.GhostWarrior2, GhostWarrior2Abilites);
            #endregion
        }
    }
}
