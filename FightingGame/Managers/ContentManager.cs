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
        public Texture2D StartMenuBackground;
        public Texture2D CharacterSelectBackground;
        public Texture2D Shadow;
        public SpriteFont Font;

        public Dictionary<EntityName, Character> Characters;
        public Dictionary<EntityName, Rectangle> EntityTextures;
        public Dictionary<EntityName, Texture2D> EntitySpriteSheets;
        public Dictionary<EntityName, Dictionary<AnimationType, AnimationBehaviour>> EntityAnimationBehaviours;
        public Dictionary<EntityName, Dictionary<AnimationType, Animation>> EntityAnimations;

        public Dictionary<EntityName, Dictionary<AnimationType, Icon>> CharacterAbilityIcons;
        public Dictionary<EntityName, Dictionary<CharacterPortrait, Texture2D>> CharacterPortraits;

        public Dictionary<ProjectileType, Projectile> Projectiles;
        public Dictionary<PowerUpType, Card> PowerUpCards;
        public Dictionary<IconType, Drop> EnemyDrops;
        public Dictionary<IconType, Chest> Chests; 
        private ContentManager()
        {
            Characters = new Dictionary<EntityName, Character>();
            CharacterAbilityIcons = new Dictionary<EntityName, Dictionary<AnimationType, Icon>>();
            EntityTextures = new Dictionary<EntityName, Rectangle>();
            EntitySpriteSheets = new Dictionary<EntityName, Texture2D>();
            EntityAnimationBehaviours = new Dictionary<EntityName, Dictionary<AnimationType, AnimationBehaviour>>();
            CharacterPortraits = new Dictionary<EntityName, Dictionary<CharacterPortrait, Texture2D>>();
            EntityAnimations = new Dictionary<EntityName, Dictionary<AnimationType, Animation>>();
            PowerUpCards = new Dictionary<PowerUpType, Card>();
            EnemyDrops = new Dictionary<IconType, Drop>();
            Chests = new Dictionary<IconType, Chest>();
            Projectiles = new Dictionary<ProjectileType, Projectile>();
        }

        public static ContentManager Instance { get; } = new ContentManager();

        public void LoadContent(Content content)
        {
            bool canHit = true;
            Font = content.Load<SpriteFont>("Font");
            Shadow = content.Load<Texture2D>("SHADOW");
            StartMenuBackground = content.Load<Texture2D>("DungeonBackground");
            CharacterSelectBackground = content.Load<Texture2D>("CharacterSelectBackground");

            #region Drops
            EnemyDrops.Add(IconType.CommonScroll, new Drop(Rarity.Common, new Icon(IconType.CommonScroll, content.Load<Texture2D>("Drops/quest_04"), 1.3f)));
            EnemyDrops.Add(IconType.RareScroll, new Drop(Rarity.Rare, new Icon(IconType.RareScroll, content.Load<Texture2D>("Drops/quest_06"), 1.3f)));
            EnemyDrops.Add(IconType.LegendaryScroll, new Drop(Rarity.Legendary, new Icon(IconType.LegendaryScroll, content.Load<Texture2D>("Drops/quest_03"), 1.3f)));
            EnemyDrops.Add(IconType.Coin, new Drop(Rarity.None, new Icon(IconType.Coin, content.Load<Texture2D>("Drops/material_125"), 1.3f)));
            #endregion

            #region Chests
            var legendaryChestTexture = content.Load<Texture2D>("Chests/Chest02");
            List<FrameHelper> legendaryChestOpen = new List<FrameHelper>();
            legendaryChestOpen.Add(new FrameHelper(new Rectangle(12, 35, 26, 24)));
            legendaryChestOpen.Add(new FrameHelper(new Rectangle(55, 34, 44, 25), new Rectangle(62, 35, 26, 24)));
            legendaryChestOpen.Add(new FrameHelper(new Rectangle(112, 24, 26, 35), new Rectangle(112, 35, 26, 24)));
            legendaryChestOpen.Add(new FrameHelper(new Rectangle(162, 5, 26, 54), new Rectangle(162, 35, 26, 24)));
            legendaryChestOpen.Add(new FrameHelper(new Rectangle(212, 4, 26, 55), new Rectangle(212, 35, 26, 24)));
            legendaryChestOpen.Add(new FrameHelper(new Rectangle(262, 25, 26, 34), new Rectangle(262, 35, 26, 24)));
            Chest legendaryChest = new Chest(new Icon(IconType.LegendaryChest, legendaryChestTexture, new Rectangle(12, 35, 26, 25), 1.6f), new Animation(legendaryChestTexture, 0.2f, legendaryChestOpen));
            Chests.Add(IconType.LegendaryChest, legendaryChest);


            var commonChestTexture = content.Load<Texture2D>("Chests/Chest07");
            List<FrameHelper> commonChestOpen = new List<FrameHelper>();
            commonChestOpen.Add(new FrameHelper(new Rectangle(12, 37, 26, 24)));
            commonChestOpen.Add(new FrameHelper(new Rectangle(51, 37, 48, 24), new Rectangle(62, 37, 26, 24)));
            commonChestOpen.Add(new FrameHelper(new Rectangle(112, 26, 27, 35), new Rectangle(112, 37, 26, 24)));
            commonChestOpen.Add(new FrameHelper(new Rectangle(162, 2, 26, 59), new Rectangle(162, 37, 26, 24)));
            commonChestOpen.Add(new FrameHelper(new Rectangle(212, 2, 26, 59), new Rectangle(212, 37, 26, 24)));
            commonChestOpen.Add(new FrameHelper(new Rectangle(262, 27, 26, 34), new Rectangle(262, 37, 26, 24)));
            Chest normalChest = new Chest(new Icon(IconType.NormalChest, commonChestTexture, new Rectangle(12, 37, 26, 24), 1.6f), new Animation(commonChestTexture, 0.2f, commonChestOpen));
            Chests.Add(IconType.NormalChest, normalChest);

            var rareChestTexture = content.Load<Texture2D>("Chests/Chest09");
            List<FrameHelper> rareChestOpen = new List<FrameHelper>();
            rareChestOpen.Add(new FrameHelper(new Rectangle(11, 34, 25, 27)));
            rareChestOpen.Add(new FrameHelper(new Rectangle(51, 33, 47, 28), new Rectangle(61, 34, 25, 27)));
            rareChestOpen.Add(new FrameHelper(new Rectangle(111, 26, 25, 35), new Rectangle(111, 34, 25, 27)));
            rareChestOpen.Add(new FrameHelper(new Rectangle(161, 3, 25, 58), new Rectangle(161, 34, 25, 27)));
            rareChestOpen.Add(new FrameHelper(new Rectangle(211, 5, 25, 56), new Rectangle(211, 34, 25, 27)));
            rareChestOpen.Add(new FrameHelper(new Rectangle(261, 27, 25, 34), new Rectangle(261, 34, 25, 27)));
            Chest rareChest = new Chest(new Icon(IconType.RareChest, rareChestTexture, new Rectangle(11, 34, 25, 27), 1.6f), new Animation(rareChestTexture, 0.2f, rareChestOpen));
            Chests.Add(IconType.RareChest, rareChest);

            //Chests.Add(IconType.NormalChest, new Chest(new Icon(IconType.NormalChest, content.Load<Texture2D>("Chests/Chest02"), new Rectangle( ), 1f), new Animation ))
            #endregion

            #region Power Up Cards
            PowerUpCards.Add(PowerUpType.HealthRegenAmmountIncrease, new Card(content.Load<Texture2D>("Cards/Elixir_of_Eternal_Renewal_Card"), Rarity.Common, Color.White, PowerUps.Instance.HealthRegenAmmountIncrease, new Icon(IconType.ElixirofEternal, content.Load<Texture2D>("CardIcons/drops_64"), 1.7f)));
            PowerUpCards.Add(PowerUpType.HealthRegenRateIncrease, new Card(content.Load<Texture2D>("Cards/Swiftheal_Medallion_Card"), Rarity.Common, Color.White, PowerUps.Instance.HealthRegenRateIncrease, new Icon(IconType.SwifthealMedalion, content.Load<Texture2D>("CardIcons/accessory_84"), 1.7f)));
            PowerUpCards.Add(PowerUpType.Bleed, new Card(content.Load<Texture2D>("Cards/Bloodspiller_Scythe_Card"), Rarity.Rare, Color.White, PowerUps.Instance.Bleed, new Icon(IconType.BloodspillerScythe, content.Load<Texture2D>("CardIcons/scythe"), 1.7f)));
            PowerUpCards.Add(PowerUpType.Overshield, new Card(content.Load<Texture2D>("Cards/Glintweave_Overshield_Card"), Rarity.Common, Color.White, PowerUps.Instance.Overshield, new Icon(IconType.GlintweaveOvershield, content.Load<Texture2D>("CardIcons/shield_15"), 1.7f)));
            PowerUpCards.Add(PowerUpType.LifeSteal, new Card(content.Load<Texture2D>("Cards/Lifedrain_Tempest_Katana_Card"), Rarity.Rare, Color.White, PowerUps.Instance.LifesSteal, new Icon(IconType.LifedrainTempestKatana, content.Load<Texture2D>("CardIcons/weapon_311"), 1.7f)));
            PowerUpCards.Add(PowerUpType.MaxHealthIncrease, new Card(content.Load<Texture2D>("Cards/Draconic_Vitality_Wing_Card"), Rarity.Common, Color.White, PowerUps.Instance.MaxHealthIncrease, new Icon(IconType.DraconicVitalityWing, content.Load<Texture2D>("CardIcons/drops_32"), 1.7f)));
            PowerUpCards.Add(PowerUpType.SpeedIncrease, new Card(content.Load<Texture2D>("Cards/Soaring_Swiftness_Plume_Card"), Rarity.Common, Color.White, PowerUps.Instance.SpeedIncrease, new Icon(IconType.SoaringSwiftnessPlume, content.Load<Texture2D>("CardIcons/drops_25"), 1.7f)));
            PowerUpCards.Add(PowerUpType.BaseDamageIncrease, new Card(content.Load<Texture2D>("Cards/Ravager's_Blade_Card"), Rarity.Common, Color.White, PowerUps.Instance.BaseDamageIncrease, new Icon(IconType.SerratedClaw, content.Load<Texture2D>("CardIcons/weapon_07"), 1.7f)));
            PowerUpCards.Add(PowerUpType.CriticalChanceIncrease, new Card(content.Load<Texture2D>("Cards/Veilstrike_Critblade_Card"), Rarity.Rare, Color.White, PowerUps.Instance.CriticalChanceIncrease, new Icon(IconType.VeilstrikeCritblade, content.Load<Texture2D>("CardIcons/weapon_245"), 1.7f)));
            PowerUpCards.Add(PowerUpType.CriticalDamageIncrease, new Card(content.Load<Texture2D>("Cards/Direstrike_Critblade_Card"), Rarity.Legendary, Color.White, PowerUps.Instance.CriticalDamageIncrease, new Icon(IconType.DirestrikeCritblade, content.Load<Texture2D>("CardIcons/weapon_244"), 1.7f)));
            PowerUpCards.Add(PowerUpType.GoldDropIncrease, new Card(content.Load<Texture2D>("Cards/Rich_Merchant_Ring_Card"), Rarity.Legendary, Color.White, PowerUps.Instance.GoldDropIncrease, new Icon(IconType.RichMerchantRing, content.Load<Texture2D>("CardIcons/ring_168"), 1.7f)));
            PowerUpCards.Add(PowerUpType.LightningStrike, new Card(content.Load<Texture2D>("Cards/Stormcaster_Bow_Card"), Rarity.Legendary, Color.White, PowerUps.Instance.LightningStrike, new Icon(IconType.StormcasterBow, content.Load<Texture2D>("CardIcons/weapon_226"), 1.7f)));
            #endregion

            #region Hashashin
            Texture2D HashashinTexture = content.Load<Texture2D>("Characters/HashashinFullSpritesheet");
            EntitySpriteSheets.Add(EntityName.Hashashin, HashashinTexture);
            EntityTextures.Add(EntityName.Hashashin, new Rectangle(132, 90, 34, 37));

            Dictionary<AnimationType, Icon> HashashinAbilityIcons = new Dictionary<AnimationType, Icon>()
            {
                [AnimationType.Ability1] = new Icon(IconType.HashashinAbility1, HashashinTexture, new Rectangle(988, 597, 50, 50), 1f),
                [AnimationType.Ability2] = new Icon(IconType.HashashinAbility2, HashashinTexture, new Rectangle(5677, 1106, 50, 50), 1f),
                [AnimationType.Ability3] = new Icon(IconType.HashashinAbility3, HashashinTexture, new Rectangle(1300, 1236, 50, 50), 0.9f),
                [AnimationType.Dodge] = new Icon(IconType.HashashinDodge, HashashinTexture, new Rectangle(982, 728, 50, 50), 1f),

                [AnimationType.UltimateAbility1] = new Icon(IconType.HashashinUltimateAbility1, HashashinTexture, new Rectangle(1853, 2748, 52, 50), new Vector2(0.95f, 1f)),
                [AnimationType.UltimateAbility2] = new Icon(IconType.HashashinUltimateAbility2, HashashinTexture, new Rectangle(4495, 2686, 64, 62), new Vector2(0.78f, 0.78f)),
                [AnimationType.UltimateAbility3] = new Icon(IconType.HashashinUltimateAbility3, HashashinTexture, new Rectangle(2154, 2859, 84, 67), new Vector2(0.6f, 0.7f)),
                [AnimationType.UltimateDodge] = new Icon(IconType.HashashinUltimateDodge, HashashinTexture, new Rectangle(1618, 1235, 50, 50), 1f),
            };
            Dictionary<CharacterPortrait, Texture2D> HashashinPortraits = new Dictionary<CharacterPortrait, Texture2D>()
            {
                [CharacterPortrait.HashashinBase] = content.Load<Texture2D>("wind_hashashin"),
            };
            Dictionary<AnimationType, Animation> Hashashin = new Dictionary<AnimationType, Animation>();

            List<FrameHelper> HashashinRun = new List<FrameHelper>();
            HashashinRun.Add(new FrameHelper(new Rectangle(123, 219, 39, 36)));
            HashashinRun.Add(new FrameHelper(new Rectangle(413, 218, 40, 37)));
            HashashinRun.Add(new FrameHelper(new Rectangle(698, 216, 44, 30)));
            HashashinRun.Add(new FrameHelper(new Rectangle(987, 218, 40, 37)));
            HashashinRun.Add(new FrameHelper(new Rectangle(1275, 219, 39, 36)));
            HashashinRun.Add(new FrameHelper(new Rectangle(1567, 218, 38, 37)));
            HashashinRun.Add(new FrameHelper(new Rectangle(1849, 216, 44, 30)));
            HashashinRun.Add(new FrameHelper(new Rectangle(2140, 218, 39, 37)));
            Hashashin.Add(AnimationType.Run, new Animation(HashashinTexture, 0.1f, HashashinRun));

            List<FrameHelper> HashashinBasicAttack = new List<FrameHelper>();
            HashashinBasicAttack.Add(new FrameHelper(new Rectangle(135, 858, 48, 37), new Rectangle(145, 858, 38, 25), canHit));
            HashashinBasicAttack.Add(new FrameHelper(new Rectangle(424, 858, 28, 37), new Rectangle(435, 858, 23, 19), canHit));
            HashashinBasicAttack.Add(new FrameHelper(new Rectangle(691, 858, 58, 37), new Rectangle(690, 859, 61, 30), canHit));
            HashashinBasicAttack.Add(new FrameHelper(new Rectangle(978, 858, 45, 37), new Rectangle(970, 867, 35, 20), canHit));
            HashashinBasicAttack.Add(new FrameHelper(new Rectangle(1273, 858, 40, 37), new Rectangle(1270, 863, 21, 19), canHit));
            HashashinBasicAttack.Add(new FrameHelper(new Rectangle(1562, 856, 39, 39)));
            HashashinBasicAttack.Add(new FrameHelper(new Rectangle(1854, 858, 39, 37)));
            HashashinBasicAttack.Add(new FrameHelper(new Rectangle(2148, 858, 33, 37)));
            Hashashin.Add(AnimationType.BasicAttack, new Animation(HashashinTexture, 0.1f, HashashinBasicAttack));

            List<FrameHelper> HashashinDodge = new List<FrameHelper>();
            HashashinDodge.Add(new FrameHelper(new Rectangle(127, 731, 39, 36)));
            HashashinDodge.Add(new FrameHelper(new Rectangle(412, 737, 40, 30)));
            HashashinDodge.Add(new FrameHelper(new Rectangle(699, 733, 34, 34)));
            HashashinDodge.Add(new FrameHelper(new Rectangle(992, 736, 34, 31)));
            HashashinDodge.Add(new FrameHelper(new Rectangle(1280, 736, 34, 31)));
            HashashinDodge.Add(new FrameHelper(new Rectangle(1572, 730, 26, 37)));
            Hashashin.Add(AnimationType.Dodge, new Animation(HashashinTexture, 0.1f, HashashinDodge));

            List<FrameHelper> HashashinAbility1 = new List<FrameHelper>();
            HashashinAbility1.Add(new FrameHelper(new Rectangle(134, 601, 24, 36)));
            HashashinAbility1.Add(new FrameHelper(new Rectangle(414, 571, 53, 66), new Rectangle(427, 567, 44, 71), new Rectangle(424, 604, 25, 30), canHit));
            HashashinAbility1.Add(new FrameHelper(new Rectangle(702, 578, 70, 59), new Rectangle(731, 576, 49, 65), new Rectangle(712, 603, 27, 31), canHit));
            HashashinAbility1.Add(new FrameHelper(new Rectangle(990, 578, 73, 59), new Rectangle(1017, 574, 48, 66), new Rectangle(1000, 603, 26, 32), canHit));
            HashashinAbility1.Add(new FrameHelper(new Rectangle(1269, 575, 84, 61), new Rectangle(1305, 573, 51, 64), new Rectangle(1280, 599, 32, 37), canHit));
            HashashinAbility1.Add(new FrameHelper(new Rectangle(1560, 571, 84, 67), new Rectangle(1601, 569, 49, 70), new Rectangle(1568, 588, 34, 38), canHit));
            HashashinAbility1.Add(new FrameHelper(new Rectangle(1853, 572, 80, 67), new Rectangle(1895, 569, 43, 72), new Rectangle(1859, 600, 26, 38), canHit));
            Hashashin.Add(AnimationType.Ability1, new Animation(HashashinTexture, 0.1f, HashashinAbility1));


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
            Hashashin.Add(AnimationType.Ability2, new Animation(HashashinTexture, 0.1f, HashashinAbility2));

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
            Hashashin.Add(AnimationType.Ability3, new Animation(HashashinTexture, 0.06f, HashashinAbility3));


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
            Hashashin.Add(AnimationType.UltimateTransform, new Animation(HashashinTexture, 0.1f, HashashinUltimateTransformation));


            List<FrameHelper> HashashinUltimateStand = new List<FrameHelper>();
            HashashinUltimateStand.Add(new FrameHelper(new Rectangle(120, 1860, 59, 56)));
            HashashinUltimateStand.Add(new FrameHelper(new Rectangle(408, 1859, 59, 56)));
            HashashinUltimateStand.Add(new FrameHelper(new Rectangle(696, 1857, 62, 59)));
            HashashinUltimateStand.Add(new FrameHelper(new Rectangle(984, 1857, 62, 58)));
            HashashinUltimateStand.Add(new FrameHelper(new Rectangle(1272, 1859, 62, 57)));
            HashashinUltimateStand.Add(new FrameHelper(new Rectangle(1560, 1861, 63, 54)));
            HashashinUltimateStand.Add(new FrameHelper(new Rectangle(1848, 1858, 63, 58)));
            HashashinUltimateStand.Add(new FrameHelper(new Rectangle(2136, 1860, 62, 55)));
            Hashashin.Add(AnimationType.UltimateStand, new Animation(HashashinTexture, 0.1f, HashashinUltimateStand));


            List<FrameHelper> HashashinUltimateRun = new List<FrameHelper>();
            HashashinUltimateRun.Add(new FrameHelper(new Rectangle(116, 1993, 57, 52)));
            HashashinUltimateRun.Add(new FrameHelper(new Rectangle(403, 1995, 58, 49)));
            HashashinUltimateRun.Add(new FrameHelper(new Rectangle(692, 1994, 57, 50)));
            HashashinUltimateRun.Add(new FrameHelper(new Rectangle(979, 1996, 58, 51)));
            HashashinUltimateRun.Add(new FrameHelper(new Rectangle(1270, 1995, 55, 49)));
            HashashinUltimateRun.Add(new FrameHelper(new Rectangle(1554, 1994, 59, 50)));
            Hashashin.Add(AnimationType.UltimateRun, new Animation(HashashinTexture, 0.1f, HashashinUltimateRun));


            List<FrameHelper> HashashinUltimateDodge = new List<FrameHelper>();
            HashashinUltimateDodge.Add(new FrameHelper(new Rectangle(122, 3014, 41, 53)));
            HashashinUltimateDodge.Add(new FrameHelper(new Rectangle(652, 3014, 48, 56)));
            HashashinUltimateDodge.Add(new FrameHelper(new Rectangle(1243, 3016, 40, 52)));
            HashashinUltimateDodge.Add(new FrameHelper(new Rectangle(1556, 3014, 59, 48)));
            HashashinUltimateDodge.Add(new FrameHelper(new Rectangle(1846, 3014, 55, 54)));
            HashashinUltimateDodge.Add(new FrameHelper(new Rectangle(2122, 3014, 64, 53)));
            Hashashin.Add(AnimationType.UltimateDodge, new Animation(HashashinTexture, 0.1f, HashashinUltimateDodge));


            List<FrameHelper> HashashinUltimateBasicAttack = new List<FrameHelper>();
            HashashinUltimateBasicAttack.Add(new FrameHelper(new Rectangle(139, 2503, 59, 53)));
            HashashinUltimateBasicAttack.Add(new FrameHelper(new Rectangle(372, 2501, 133, 54), new Rectangle(449, 2500, 72, 57), new Rectangle(404, 2505, 43, 50), canHit));
            HashashinUltimateBasicAttack.Add(new FrameHelper(new Rectangle(659, 2500, 203, 56), new Rectangle(770, 2497, 122, 57), new Rectangle(690, 2505, 45, 45), canHit));
            HashashinUltimateBasicAttack.Add(new FrameHelper(new Rectangle(944, 2457, 198, 98), new Rectangle(1059, 2437, 102, 70), new Rectangle(979, 2502, 40, 47), canHit));
            HashashinUltimateBasicAttack.Add(new FrameHelper(new Rectangle(1232, 2473, 145, 83), new Rectangle(1311, 2458, 93, 59), new Rectangle(1264, 2497, 39, 59), canHit));
            HashashinUltimateBasicAttack.Add(new FrameHelper(new Rectangle(1518, 2491, 95, 64)));
            HashashinUltimateBasicAttack.Add(new FrameHelper(new Rectangle(2142, 2500, 49, 55)));
            Hashashin.Add(AnimationType.UltimateBasicAttack, new Animation(HashashinTexture, 0.1f, HashashinUltimateBasicAttack));


            List<FrameHelper> HashashinUltimateAbility1 = new List<FrameHelper>();
            HashashinUltimateAbility1.Add(new FrameHelper(new Rectangle(99, 2374, 52, 54)));
            HashashinUltimateAbility1.Add(new FrameHelper(new Rectangle(383, 2364, 80, 64), new Rectangle(359, 2360, 128, 52), new Rectangle(412, 2370, 30, 58), canHit));
            HashashinUltimateAbility1.Add(new FrameHelper(new Rectangle(671, 2366, 97, 61), new Rectangle(655, 2363, 124, 51), new Rectangle(707, 2374, 30, 51), canHit));
            HashashinUltimateAbility1.Add(new FrameHelper(new Rectangle(956, 2362, 103, 65), new Rectangle(931, 2353, 157, 69), new Rectangle(991, 2372, 30, 55), canHit));
            HashashinUltimateAbility1.Add(new FrameHelper(new Rectangle(1239, 2357, 115, 69), new Rectangle(1230, 2353, 131, 74), new Rectangle(1278, 2367, 30, 58), canHit));
            HashashinUltimateAbility1.Add(new FrameHelper(new Rectangle(1522, 2355, 121, 72), new Rectangle(1514, 2351, 138, 80), new Rectangle(1563, 2367, 43, 54), canHit));
            HashashinUltimateAbility1.Add(new FrameHelper(new Rectangle(1808, 2354, 128, 72), new Rectangle(1803, 2350, 138, 78), new Rectangle(1854, 2372, 36, 52), canHit));
            HashashinUltimateAbility1.Add(new FrameHelper(new Rectangle(2125, 2353, 71, 75)));
            Hashashin.Add(AnimationType.UltimateAbility1, new Animation(HashashinTexture, 0.1f, HashashinUltimateAbility1));


            List<FrameHelper> HashashinUltimateAbility2 = new List<FrameHelper>();
            HashashinUltimateAbility2.Add(new FrameHelper(new Rectangle(3604, 2764, 63, 52)));
            HashashinUltimateAbility2.Add(new FrameHelper(new Rectangle(3897, 2743, 70, 73), new Rectangle(3926, 2735, 48, 88), new Rectangle(3906, 2758, 33, 50), canHit));
            HashashinUltimateAbility2.Add(new FrameHelper(new Rectangle(4158, 2697, 118, 119), new Rectangle(4204, 2692, 90, 133), new Rectangle(4174, 2757, 47, 54), canHit));
            HashashinUltimateAbility2.Add(new FrameHelper(new Rectangle(4445, 2691, 128, 125), new Rectangle(4493, 2681, 95, 144), new Rectangle(4445, 2758, 39, 55), canHit));
            HashashinUltimateAbility2.Add(new FrameHelper(new Rectangle(4738, 2692, 141, 124), new Rectangle(4798, 2684, 91, 139), new Rectangle(4738, 2753, 50, 58), canHit));
            HashashinUltimateAbility2.Add(new FrameHelper(new Rectangle(5015, 2696, 160, 116), new Rectangle(5102, 2687, 84, 131), new Rectangle(5015, 2754, 63, 58), canHit));
            HashashinUltimateAbility2.Add(new FrameHelper(new Rectangle(5304, 2694, 167, 117), new Rectangle(5387, 2689, 97, 125), new Rectangle(5304, 2756, 62, 55), canHit));
            Hashashin.Add(AnimationType.UltimateAbility2, new Animation(HashashinTexture, 0.1f, HashashinUltimateAbility2));


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
            Hashashin.Add(AnimationType.UndoTransform, new Animation(HashashinTexture, 0.1f, HashashinUndoTransform));


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
            Hashashin.Add(AnimationType.UltimateAbility3, new Animation(HashashinTexture, 0.1f, HashashinUltimateAbility3));


            List<FrameHelper> HashashinStand = new List<FrameHelper>();
            HashashinStand.Add(new FrameHelper(new Rectangle(132, 90, 34, 37)));
            HashashinStand.Add(new FrameHelper(new Rectangle(419, 91, 33, 36)));
            HashashinStand.Add(new FrameHelper(new Rectangle(705, 92, 31, 35)));
            HashashinStand.Add(new FrameHelper(new Rectangle(991, 92, 32, 35)));
            HashashinStand.Add(new FrameHelper(new Rectangle(1278, 92, 35, 35)));
            HashashinStand.Add(new FrameHelper(new Rectangle(1568, 92, 33, 35)));
            HashashinStand.Add(new FrameHelper(new Rectangle(1858, 92, 35, 35)));
            HashashinStand.Add(new FrameHelper(new Rectangle(2147, 91, 34, 36)));
            Hashashin.Add(AnimationType.Stand, new Animation(HashashinTexture, 0.1f, HashashinStand));


            Dictionary<AnimationType, AnimationBehaviour> HashashinAbilites = new Dictionary<AnimationType, AnimationBehaviour>()
            {
                [AnimationType.Run] = new Run(AnimationType.Run),
                [AnimationType.Dodge] = new Dodge(AnimationType.Dodge, 5, 2),
                [AnimationType.BasicAttack] = new MeleeAttack(AnimationType.BasicAttack, 1, 0, 0, true, AnimationType.Stand),
                [AnimationType.Ability1] = new MeleeAttack(AnimationType.Ability1, 1.5f, 0, 2, true, AnimationType.Stand),
                [AnimationType.Ability2] = new MeleeAttack(AnimationType.Ability2, 1, 0, 3, true, AnimationType.Stand),
                [AnimationType.Ability3] = new MeleeAttack(AnimationType.Ability3, 2f, 0, 2, false, AnimationType.Stand),
                [AnimationType.UltimateTransform] = new UltimateTransform(AnimationType.UltimateTransform, 1.0f, 0, 0, false),
                [AnimationType.UltimateStand] = new Stand(AnimationType.UltimateStand),
                [AnimationType.UltimateRun] = new Run(AnimationType.UltimateRun),
                [AnimationType.UltimateBasicAttack] = new MeleeAttack(AnimationType.UltimateBasicAttack, 2.0f, 0, 0, true, AnimationType.UltimateStand),
                [AnimationType.UltimateAbility1] = new MeleeAttack(AnimationType.UltimateAbility1, 1.5f, 0, 1, true, AnimationType.UltimateStand),
                [AnimationType.UltimateAbility2] = new MeleeAttack(AnimationType.UltimateAbility2, 2.2f, 0, 3, true, AnimationType.UltimateStand),
                [AnimationType.UltimateAbility3] = new MeleeAttack(AnimationType.UltimateAbility3, 1.7f, 0, 4, true, AnimationType.UltimateStand),
                [AnimationType.UltimateDodge] = new Dodge(AnimationType.UltimateDodge, 6, 1),
                [AnimationType.UndoTransform] = new UndoTransform(AnimationType.UndoTransform),
                [AnimationType.Stand] = new Stand(AnimationType.Stand),
            };

            EntityAnimations.Add(EntityName.Hashashin, Hashashin);
            EntityAnimationBehaviours.Add(EntityName.Hashashin, HashashinAbilites);
            CharacterAbilityIcons.Add(EntityName.Hashashin, HashashinAbilityIcons);
            CharacterPortraits.Add(EntityName.Hashashin, HashashinPortraits);
            Characters.Add(EntityName.Hashashin, new Character(EntityName.Hashashin, 100, 12, 4, 1.3f));
            #endregion

            #region Skeleton
            EntitySpriteSheets.Add(EntityName.Skeleton, content.Load<Texture2D>("Enemies/Skeleton"));
            EntityTextures.Add(EntityName.Skeleton, new Rectangle(17, 70, 15, 21));
            Dictionary<AnimationType, Animation> Skeleton = new Dictionary<AnimationType, Animation>();

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
            Skeleton.Add(AnimationType.Run, new Animation(EntitySpriteSheets[EntityName.Skeleton], 0.1f, SkeletonRun));

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
            Skeleton.Add(AnimationType.BasicAttack, new Animation(EntitySpriteSheets[EntityName.Skeleton], 0.1f, SkeletonBasicAttack));

            List<FrameHelper> SkeletonDeath = new List<FrameHelper>();
            SkeletonDeath.Add(new FrameHelper(new Rectangle(137, 81, 31, 32)));
            SkeletonDeath.Add(new FrameHelper(new Rectangle(265, 83, 37, 30)));
            SkeletonDeath.Add(new FrameHelper(new Rectangle(329, 82, 38, 31)));
            SkeletonDeath.Add(new FrameHelper(new Rectangle(392, 80, 42, 33)));
            SkeletonDeath.Add(new FrameHelper(new Rectangle(457, 79, 42, 34)));
            SkeletonDeath.Add(new FrameHelper(new Rectangle(521, 79, 42, 34)));
            SkeletonDeath.Add(new FrameHelper(new Rectangle(585, 79, 42, 34)));
            SkeletonDeath.Add(new FrameHelper(new Rectangle(649, 80, 42, 33)));
            Skeleton.Add(AnimationType.Death, new Animation(EntitySpriteSheets[EntityName.Skeleton], 0.1f, SkeletonDeath));

            List<FrameHelper> SkeletonSpawn = new List<FrameHelper>();
            SkeletonSpawn.Add(new FrameHelper(new Rectangle(9, 271, 32, 34)));
            SkeletonSpawn.Add(new FrameHelper(new Rectangle(73, 272, 32, 33)));
            SkeletonSpawn.Add(new FrameHelper(new Rectangle(137, 272, 32, 33)));
            Skeleton.Add(AnimationType.Spawn, new Animation(EntitySpriteSheets[EntityName.Skeleton], 0.1f, SkeletonSpawn));

            List<FrameHelper> SkeletonStand = new List<FrameHelper>();
            SkeletonStand.Add(new FrameHelper(new Rectangle(9, 208, 33, 33)));
            SkeletonStand.Add(new FrameHelper(new Rectangle(9, 208, 33, 33)));
            Skeleton.Add(AnimationType.Stand, new Animation(EntitySpriteSheets[EntityName.Skeleton], 0.1f, SkeletonStand));

            Dictionary<AnimationType, AnimationBehaviour> SkeletonAbilites = new Dictionary<AnimationType, AnimationBehaviour>()
            {
                [AnimationType.Run] = new Run(AnimationType.Run),
                [AnimationType.BasicAttack] = new MeleeAttack(AnimationType.BasicAttack, 5, 50, 0, false, AnimationType.Stand),
                [AnimationType.Death] = new Death(AnimationType.Death),
                [AnimationType.Stand] = new Stand(AnimationType.Stand),
            };
            EntityAnimations.Add(EntityName.Skeleton, Skeleton);
            EntityAnimationBehaviours.Add(EntityName.Skeleton, SkeletonAbilites);
            #endregion

            #region Necromancer
            var NecromancerSpritesheet = content.Load<Texture2D>("Enemies/NecromancerFullSpritesheet");
            EntitySpriteSheets.Add(EntityName.Necromancer, NecromancerSpritesheet);
            EntityTextures.Add(EntityName.Necromancer, new Rectangle(38, 17, 28, 49));

            Dictionary<AnimationType, Animation> Necromancer = new Dictionary<AnimationType, Animation>();

            List<FrameHelper> NecromancerBasicAttack = new List<FrameHelper>();
            NecromancerBasicAttack.Add(new FrameHelper(new Rectangle(54, 186, 28, 49)));
            NecromancerBasicAttack.Add(new FrameHelper(new Rectangle(182, 185, 28, 50)));
            NecromancerBasicAttack.Add(new FrameHelper(new Rectangle(310, 181, 28, 54)));
            NecromancerBasicAttack.Add(new FrameHelper(new Rectangle(438, 174, 29, 61)));
            NecromancerBasicAttack.Add(new FrameHelper(new Rectangle(1078, 178, 29, 57)));
            NecromancerBasicAttack.Add(new FrameHelper(new Rectangle(1205, 188, 49, 47), new Rectangle(1232, 194, 33, 42), new Rectangle(1205, 188, 30, 47), canHit));
            NecromancerBasicAttack.Add(new FrameHelper(new Rectangle(1335, 187, 47, 48), new Rectangle(1361, 192, 33, 45), new Rectangle(1335, 187, 28, 48), canHit));
            NecromancerBasicAttack.Add(new FrameHelper(new Rectangle(1462, 186, 51, 49), new Rectangle(1489, 187, 36, 49), new Rectangle(1462, 186, 29, 49), canHit));
            NecromancerBasicAttack.Add(new FrameHelper(new Rectangle(1590, 186, 58, 49), new Rectangle(1614, 186, 42, 49), new Rectangle(1590, 186, 29, 49), canHit));
            NecromancerBasicAttack.Add(new FrameHelper(new Rectangle(1718, 186, 61, 49), new Rectangle(1741, 182, 46, 53), new Rectangle(1718, 186, 30, 49), canHit));
            NecromancerBasicAttack.Add(new FrameHelper(new Rectangle(1846, 185, 63, 50), new Rectangle(1867, 174, 52, 61), new Rectangle(1846, 186, 31, 49), canHit));
            NecromancerBasicAttack.Add(new FrameHelper(new Rectangle(1974, 182, 65, 53), new Rectangle(1994, 172, 54, 63), new Rectangle(1974, 186, 31, 49), canHit));
            NecromancerBasicAttack.Add(new FrameHelper(new Rectangle(2102, 180, 66, 55), new Rectangle(2129, 174, 49, 62), new Rectangle(2102, 186, 28, 49), canHit));
            NecromancerBasicAttack.Add(new FrameHelper(new Rectangle(2203, 179, 65, 56), new Rectangle(2262, 169, 37, 56), new Rectangle(2230, 186, 29, 48), canHit));
            NecromancerBasicAttack.Add(new FrameHelper(new Rectangle(2358, 178, 46, 57), new Rectangle(2358, 186, 29, 49)));
            NecromancerBasicAttack.Add(new FrameHelper(new Rectangle(2486, 186, 29, 49)));
            Necromancer.Add(AnimationType.BasicAttack, new Animation(NecromancerSpritesheet, 0.1f, NecromancerBasicAttack));

            List<FrameHelper> NecromancerSummon = new List<FrameHelper>();
            NecromancerSummon.Add(new FrameHelper(new Rectangle(273, 286, 30, 49)));
            NecromancerSummon.Add(new FrameHelper(new Rectangle(401, 284, 30, 51)));
            NecromancerSummon.Add(new FrameHelper(new Rectangle(529, 278, 30, 57)));
            NecromancerSummon.Add(new FrameHelper(new Rectangle(657, 274, 30, 61)));
            NecromancerSummon.Add(new FrameHelper(new Rectangle(785, 274, 30, 61)));
            NecromancerSummon.Add(new FrameHelper(new Rectangle(1297, 276, 30, 59)));
            NecromancerSummon.Add(new FrameHelper(new Rectangle(1425, 288, 30, 47)));
            NecromancerSummon.Add(new FrameHelper(new Rectangle(1553, 287, 30, 48)));
            NecromancerSummon.Add(new FrameHelper(new Rectangle(1681, 286, 30, 49)));
            NecromancerSummon.Add(new FrameHelper(new Rectangle(1809, 286, 30, 49)));
            Necromancer.Add(AnimationType.Ability1, new Animation(NecromancerSpritesheet, 0.1f, NecromancerSummon));

            List<FrameHelper> NecromancerRun = new List<FrameHelper>();
            NecromancerRun.Add(new FrameHelper(new Rectangle(32, 96, 32, 49)));
            NecromancerRun.Add(new FrameHelper(new Rectangle(128, 96, 31, 49)));
            NecromancerRun.Add(new FrameHelper(new Rectangle(225, 96, 28, 49)));
            NecromancerRun.Add(new FrameHelper(new Rectangle(322, 96, 22, 49)));
            NecromancerRun.Add(new FrameHelper(new Rectangle(420, 96, 18, 49)));
            NecromancerRun.Add(new FrameHelper(new Rectangle(516, 96, 18, 49)));
            NecromancerRun.Add(new FrameHelper(new Rectangle(613, 95, 17, 50)));
            NecromancerRun.Add(new FrameHelper(new Rectangle(708, 94, 20, 51)));
            NecromancerRun.Add(new FrameHelper(new Rectangle(803, 94, 26, 51)));
            NecromancerRun.Add(new FrameHelper(new Rectangle(897, 95, 30, 50)));
            Necromancer.Add(AnimationType.Run, new Animation(NecromancerSpritesheet, 0.1f, NecromancerRun));

            List<FrameHelper> NecromancerDeath = new List<FrameHelper>();
            NecromancerDeath.Add(new FrameHelper(new Rectangle(310, 382, 20, 49)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(406, 380, 20, 51)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(502, 379, 20, 52)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(598, 376, 20, 55)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(694, 375, 22, 56)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(790, 374, 22, 57)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(886, 373, 21, 58)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(981, 376, 25, 55)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(1076, 379, 28, 52)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(1170, 388, 30, 43)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(1265, 387, 29, 44)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(1361, 390, 30, 41)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(1457, 395, 25, 36)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(1553, 395, 25, 36)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(1650, 395, 24, 36)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(1745, 395, 25, 36)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(1846, 395, 20, 36)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(499, 477, 20, 36)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(595, 478, 18, 35)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(691, 480, 16, 33)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(787, 484, 20, 29)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(883, 488, 23, 25)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(979, 507, 19, 6)));
            NecromancerDeath.Add(new FrameHelper(new Rectangle(1075, 505, 19, 8)));
            Necromancer.Add(AnimationType.Death, new Animation(NecromancerSpritesheet, 0.1f, NecromancerDeath));

            List<FrameHelper> NecromancerStand = new List<FrameHelper>();
            NecromancerStand.Add(new FrameHelper(new Rectangle(38, 17, 28, 49)));
            NecromancerStand.Add(new FrameHelper(new Rectangle(134, 17, 28, 49)));
            NecromancerStand.Add(new FrameHelper(new Rectangle(230, 17, 28, 49)));
            NecromancerStand.Add(new FrameHelper(new Rectangle(326, 17, 28, 49)));
            Necromancer.Add(AnimationType.Stand, new Animation(NecromancerSpritesheet, 0.1f, NecromancerStand));

            Dictionary<AnimationType, AnimationBehaviour> NecromancerBehaviours = new Dictionary<AnimationType, AnimationBehaviour>()
            {
                [AnimationType.BasicAttack] = new MeleeAttack(AnimationType.BasicAttack, 10, 60, 0, false, AnimationType.Stand),
                [AnimationType.Ability1] = new NecromancerSummon(AnimationType.Ability1, 0, 600, 20, false),
                [AnimationType.Death] = new Death(AnimationType.Death),
                [AnimationType.Run] = new Run(AnimationType.Run),
                [AnimationType.Stand] = new Stand(AnimationType.Stand),
            };

            EntityAnimationBehaviours.Add(EntityName.Necromancer, NecromancerBehaviours);
            EntityAnimations.Add(EntityName.Necromancer, Necromancer);
            #endregion

            #region BringerOfDeath
            var BringerOfDeathTexture = content.Load<Texture2D>("Enemies/Bringer-of-Death-SpritSheet");
            EntitySpriteSheets.Add(EntityName.BringerOfDeath, BringerOfDeathTexture);
            EntityTextures.Add(EntityName.BringerOfDeath, new Rectangle(86, 38, 40, 54));

            Dictionary<AnimationType, Animation> BringerOfDeath = new Dictionary<AnimationType, Animation>();

            List<FrameHelper> BringerOfDeathRun = new List<FrameHelper>();
            BringerOfDeathRun.Add(new FrameHelper(new Rectangle(215, 133, 56, 52)));
            BringerOfDeathRun.Add(new FrameHelper(new Rectangle(354, 131, 56, 54)));
            BringerOfDeathRun.Add(new FrameHelper(new Rectangle(495, 130, 54, 55)));
            BringerOfDeathRun.Add(new FrameHelper(new Rectangle(637, 131, 48, 54)));
            BringerOfDeathRun.Add(new FrameHelper(new Rectangle(778, 133, 46, 52)));
            BringerOfDeathRun.Add(new FrameHelper(new Rectangle(920, 131, 45, 54)));
            BringerOfDeathRun.Add(new FrameHelper(new Rectangle(1056, 130, 50, 55)));
            BringerOfDeath.Add(AnimationType.Run, new Animation(BringerOfDeathTexture, 0.1f, BringerOfDeathRun));

            List<FrameHelper> BringerOfDeathDeath = new List<FrameHelper>();
            BringerOfDeathDeath.Add(new FrameHelper(new Rectangle(785, 318, 40, 53)));
            BringerOfDeathDeath.Add(new FrameHelper(new Rectangle(922, 317, 45, 54)));
            BringerOfDeathDeath.Add(new FrameHelper(new Rectangle(1061, 317, 48, 54)));
            BringerOfDeathDeath.Add(new FrameHelper(new Rectangle(81, 408, 49, 56)));
            BringerOfDeathDeath.Add(new FrameHelper(new Rectangle(222, 406, 51, 58)));
            BringerOfDeathDeath.Add(new FrameHelper(new Rectangle(365, 404, 50, 60)));
            BringerOfDeathDeath.Add(new FrameHelper(new Rectangle(506, 400, 48, 59)));
            BringerOfDeathDeath.Add(new FrameHelper(new Rectangle(645, 399, 48, 52)));
            BringerOfDeathDeath.Add(new FrameHelper(new Rectangle(788, 397, 40, 49)));
            BringerOfDeathDeath.Add(new FrameHelper(new Rectangle(936, 397, 33, 40)));
            BringerOfDeath.Add(AnimationType.Death, new Animation(BringerOfDeathTexture, 0.1f, BringerOfDeathDeath));

            List<FrameHelper> BringerOfDeathAttack = new List<FrameHelper>();
            BringerOfDeathAttack.Add(new FrameHelper(new Rectangle(84, 224, 44, 54)));
            BringerOfDeathAttack.Add(new FrameHelper(new Rectangle(218, 224, 60, 54)));
            BringerOfDeathAttack.Add(new FrameHelper(new Rectangle(353, 209, 61, 69)));
            BringerOfDeathAttack.Add(new FrameHelper(new Rectangle(495, 209, 62, 69)));
            BringerOfDeathAttack.Add(new FrameHelper(new Rectangle(565, 202, 133, 76), new Rectangle(565, 202, 133, 76), new Rectangle(617, 234, 62, 44), canHit));
            BringerOfDeathAttack.Add(new FrameHelper(new Rectangle(700, 195, 140, 83), new Rectangle(700, 195, 140, 83), new Rectangle(761, 235, 58, 43), canHit));
            BringerOfDeathAttack.Add(new FrameHelper(new Rectangle(984, 193, 126, 85), new Rectangle(984, 193, 126, 85), new Rectangle(1056, 231, 42, 47), canHit));
            BringerOfDeathAttack.Add(new FrameHelper(new Rectangle(6, 289, 112, 82), new Rectangle(6, 289, 112, 82), new Rectangle(77, 321, 41, 50), canHit));
            BringerOfDeathAttack.Add(new FrameHelper(new Rectangle(226, 318, 37, 53)));
            BringerOfDeath.Add(AnimationType.BasicAttack, new Animation(BringerOfDeathTexture, 0.1f, BringerOfDeathAttack));

            List<FrameHelper> BringerOfDeathAbility1 = new List<FrameHelper>();
            BringerOfDeathAbility1.Add(new FrameHelper(new Rectangle(1058, 412, 48, 52)));
            BringerOfDeathAbility1.Add(new FrameHelper(new Rectangle(78, 506, 48, 51)));
            BringerOfDeathAbility1.Add(new FrameHelper(new Rectangle(219, 507, 46, 50)));
            BringerOfDeathAbility1.Add(new FrameHelper(new Rectangle(357, 507, 48, 50)));
            BringerOfDeathAbility1.Add(new FrameHelper(new Rectangle(495, 508, 50, 49)));
            BringerOfDeathAbility1.Add(new FrameHelper(new Rectangle(634, 465, 51, 92)));
            BringerOfDeathAbility1.Add(new FrameHelper(new Rectangle(774, 465, 51, 92)));
            BringerOfDeathAbility1.Add(new FrameHelper(new Rectangle(913, 466, 52, 91)));
            BringerOfDeath.Add(AnimationType.Ability1, new Animation(BringerOfDeathTexture, 0.1f, BringerOfDeathAbility1));

            List<FrameHelper> BringerOfDeathPortalSummon = new List<FrameHelper>();
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(331, 595, 35, 16)));
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(192, 593, 32, 19)));
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(471, 595, 35, 16)));
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(331, 595, 35, 16)));
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(192, 593, 32, 19)));
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(471, 595, 35, 16)));
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(614, 593, 29, 23), new Rectangle(614, 593, 29, 19)));
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(752, 593, 31, 36), new Rectangle(752, 593, 31, 18)));
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(892, 593, 31, 53), new Rectangle(892, 593, 31, 19)));
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(1032, 593, 31, 57), new Rectangle(1032, 593, 31, 19)));
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(51, 686, 33, 57), new Rectangle(51, 686, 33, 19)));
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(190, 686, 34, 57), new Rectangle(190, 686, 34, 16)));
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(331, 688, 35, 55), new Rectangle(331, 688, 35, 16)));
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(470, 688, 36, 55), new Rectangle(470, 688, 36, 17)));
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(609, 686, 34, 57), new Rectangle(609, 686, 34, 20)));
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(754, 686, 29, 18)));
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(892, 686, 31, 19)));
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(754, 686, 29, 18)));
            BringerOfDeathPortalSummon.Add(new FrameHelper(new Rectangle(892, 686, 31, 19)));

            List<FrameHelper> BringerOfDeathStand = new List<FrameHelper>();
            BringerOfDeathStand.Add(new FrameHelper(new Rectangle(86, 38, 40, 54)));
            BringerOfDeathStand.Add(new FrameHelper(new Rectangle(226, 37, 40, 55)));
            BringerOfDeathStand.Add(new FrameHelper(new Rectangle(365, 36, 41, 56)));
            BringerOfDeathStand.Add(new FrameHelper(new Rectangle(504, 36, 41, 56)));
            BringerOfDeath.Add(AnimationType.Stand, new Animation(BringerOfDeathTexture, 0.1f, BringerOfDeathStand));

            Dictionary<AnimationType, AnimationBehaviour> BringerofDeathBehaviours = new Dictionary<AnimationType, AnimationBehaviour>()
            {
                [AnimationType.Run] = new Run(AnimationType.Run),
                [AnimationType.BasicAttack] = new MeleeAttack(AnimationType.BasicAttack, 5, 80, 0, false, AnimationType.Stand),
                [AnimationType.Ability1] = new BringerOfDeathRangedAttack(AnimationType.Ability1, ProjectileType.BringerOfDeathPortalSummon, new Rectangle(913, 466, 52, 91), 0, 5, 500, 5, false),
                [AnimationType.Death] = new Death(AnimationType.Death),
                [AnimationType.Stand] = new Stand(AnimationType.Stand),
            };
            EntityAnimations.Add(EntityName.BringerOfDeath, BringerOfDeath);
            EntityAnimationBehaviours.Add(EntityName.BringerOfDeath, BringerofDeathBehaviours);

            #endregion

            #region Ghost Warrior
            //EntitySpriteSheets.Add(EntityName.GhostWarrior, content.Load<Texture2D>("GhostWarrior"));
            //EntityTextures.Add(EntityName.GhostWarrior, new Rectangle(49, 130, 77, 56));
            //Dictionary<AnimationType, Animation> GhostWarrior = new Dictionary<AnimationType, Animation>();

            //List<FrameHelper> GhostWarriorStand = new List<FrameHelper>();
            //GhostWarriorStand.Add(new FrameHelper(new Rectangle(49, 130, 77, 56)));
            //GhostWarriorStand.Add(new FrameHelper(new Rectangle(179, 129, 77, 57)));
            //GhostWarriorStand.Add(new FrameHelper(new Rectangle(309, 127, 77, 59)));
            //GhostWarriorStand.Add(new FrameHelper(new Rectangle(439, 126, 77, 60)));
            //GhostWarriorStand.Add(new FrameHelper(new Rectangle(569, 129, 77, 57)));
            //GhostWarriorStand.Add(new FrameHelper(new Rectangle(699, 129, 77, 57)));
            //GhostWarriorStand.Add(new FrameHelper(new Rectangle(829, 127, 77, 59)));
            //GhostWarriorStand.Add(new FrameHelper(new Rectangle(959, 129, 77, 57)));
            //GhostWarrior.Add(AnimationType.Stand, new Animation(EntitySpriteSheets[EntityName.GhostWarrior], 0.1f, GhostWarriorStand));

            //List<FrameHelper> GhostWarriorDeath = new List<FrameHelper>();
            //GhostWarriorDeath.Add(new FrameHelper(new Rectangle(47, 339, 64, 58)));
            //GhostWarriorDeath.Add(new FrameHelper(new Rectangle(177, 340, 66, 57)));
            //GhostWarriorDeath.Add(new FrameHelper(new Rectangle(307, 341, 63, 56)));
            //GhostWarriorDeath.Add(new FrameHelper(new Rectangle(437, 337, 63, 60)));
            //GhostWarriorDeath.Add(new FrameHelper(new Rectangle(567, 339, 63, 58)));
            //GhostWarriorDeath.Add(new FrameHelper(new Rectangle(697, 333, 63, 64)));
            //GhostWarriorDeath.Add(new FrameHelper(new Rectangle(0, 0, 0, 0)));
            //GhostWarrior.Add(AnimationType.Death, new Animation(EntitySpriteSheets[EntityName.GhostWarrior], 0.1f, GhostWarriorDeath));


            //List<FrameHelper> GhostWarriorBasicAttack = new List<FrameHelper>();
            //GhostWarriorBasicAttack.Add(new FrameHelper(new Rectangle(49, 39, 77, 56), new Rectangle(49, 39, 36, 56)));
            //GhostWarriorBasicAttack.Add(new FrameHelper(new Rectangle(179, 40, 74, 55), new Rectangle(179, 40, 32, 55)));
            //GhostWarriorBasicAttack.Add(new FrameHelper(new Rectangle(309, 38, 79, 57), new Rectangle(339, 45, 49, 36), new Rectangle(309, 38, 32, 57), canHit));
            //GhostWarriorBasicAttack.Add(new FrameHelper(new Rectangle(416, 7, 78, 88), new Rectangle(412, 3, 57, 85), new Rectangle(439, 44, 30, 51), canHit));
            //GhostWarriorBasicAttack.Add(new FrameHelper(new Rectangle(546, 14, 54, 81), new Rectangle(546, 14, 38, 75), new Rectangle(569, 46, 31, 49), canHit));
            //GhostWarriorBasicAttack.Add(new FrameHelper(new Rectangle(675, 35, 56, 60), new Rectangle(675, 46, 37, 45), new Rectangle(699, 48, 32, 47), canHit));
            //GhostWarriorBasicAttack.Add(new FrameHelper(new Rectangle(811, 3, 96, 92), new Rectangle(833, 3, 74, 92), new Rectangle(829, 58, 29, 36), canHit));
            //GhostWarriorBasicAttack.Add(new FrameHelper(new Rectangle(1088, 55, 72, 40), new Rectangle(1105, 80, 55, 15), new Rectangle(1088, 55, 35, 40), canHit));
            //GhostWarriorBasicAttack.Add(new FrameHelper(new Rectangle(1219, 52, 78, 43)));
            //GhostWarrior.Add(AnimationType.BasicAttack, new Animation(EntitySpriteSheets[EntityName.GhostWarrior], 0.1f, GhostWarriorBasicAttack));


            //List<FrameHelper> GhostWarriorRun = new List<FrameHelper>();
            //GhostWarriorRun.Add(new FrameHelper(new Rectangle(43, 232, 70, 58), new Rectangle(43, 232, 35, 58)));
            //GhostWarriorRun.Add(new FrameHelper(new Rectangle(176, 228, 67, 62), new Rectangle(176, 228, 30, 62)));
            //GhostWarriorRun.Add(new FrameHelper(new Rectangle(307, 227, 66, 63), new Rectangle(307, 227, 29, 63)));
            //GhostWarriorRun.Add(new FrameHelper(new Rectangle(437, 231, 66, 59), new Rectangle(437, 231, 30, 59)));
            //GhostWarriorRun.Add(new FrameHelper(new Rectangle(568, 233, 65, 57), new Rectangle(568, 233, 30, 57)));
            //GhostWarriorRun.Add(new FrameHelper(new Rectangle(700, 229, 63, 61), new Rectangle(700, 229, 26, 61)));
            //GhostWarriorRun.Add(new FrameHelper(new Rectangle(830, 233, 63, 57), new Rectangle(830, 233, 26, 57)));
            //GhostWarriorRun.Add(new FrameHelper(new Rectangle(956, 231, 67, 59), new Rectangle(956, 231, 31, 59)));
            //GhostWarrior.Add(AnimationType.Run, new Animation(EntitySpriteSheets[EntityName.GhostWarrior], 0.1f, GhostWarriorRun));


            //Dictionary<AnimationType, AnimationBehaviour> GhostWarriorAnimations = new Dictionary<AnimationType, AnimationBehaviour>()
            //{
            //    [AnimationType.Run] = new Run(AnimationType.Run),
            //    [AnimationType.BasicAttack] = new MeleeAttack(AnimationType.BasicAttack, 12, 100, 0, false),
            //    [AnimationType.Death] = new Death(AnimationType.Death),
            //    [AnimationType.Stand] = new Stand(AnimationType.Stand),
            //};

            //EntityAnimations.Add(EntityName.GhostWarrior, GhostWarrior);
            //EntityAnimationBehaviours.Add(EntityName.GhostWarrior, GhostWarriorAnimations);
            ////EntityActions.Add(EntityName.GhostWarrior, GhostWarriorActions);
            #endregion

            #region Ghost Warrior 2
            var GhostWarrior2Spritesheet = content.Load<Texture2D>("Enemies/GhostWarrior2");
            EntitySpriteSheets.Add(EntityName.GhostWarrior2, GhostWarrior2Spritesheet);
            EntityTextures.Add(EntityName.GhostWarrior2, new Rectangle(49, 130, 77, 56));

            Dictionary<AnimationType, Animation> GhostWarrior2 = new Dictionary<AnimationType, Animation>();

            List<FrameHelper> GhostWarrior2Run = new List<FrameHelper>();
            GhostWarrior2Run.Add(new FrameHelper(new Rectangle(52, 477, 52, 53)));
            GhostWarrior2Run.Add(new FrameHelper(new Rectangle(213, 480, 51, 51)));
            GhostWarrior2Run.Add(new FrameHelper(new Rectangle(373, 476, 51, 56)));
            GhostWarrior2Run.Add(new FrameHelper(new Rectangle(533, 476, 51, 57)));
            GhostWarrior2Run.Add(new FrameHelper(new Rectangle(692, 479, 52, 54)));
            GhostWarrior2Run.Add(new FrameHelper(new Rectangle(853, 481, 51, 54)));
            GhostWarrior2Run.Add(new FrameHelper(new Rectangle(1013, 475, 51, 58)));
            GhostWarrior2Run.Add(new FrameHelper(new Rectangle(1173, 474, 51, 56)));
            GhostWarrior2.Add(AnimationType.Run, new Animation(GhostWarrior2Spritesheet, 0.1f, GhostWarrior2Run));

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
            GhostWarrior2.Add(AnimationType.BasicAttack, new Animation(GhostWarrior2Spritesheet, 0.1f, GhostWarrior2BasicAttack));

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
            GhostWarrior2.Add(AnimationType.Death, new Animation(GhostWarrior2Spritesheet, 0.1f, GhostWarrior2Death));

            List<FrameHelper> GhostWarrior2Stand = new List<FrameHelper>();
            GhostWarrior2Stand.Add(new FrameHelper(new Rectangle(68, 283, 35, 82)));
            GhostWarrior2Stand.Add(new FrameHelper(new Rectangle(228, 290, 46, 74)));
            GhostWarrior2Stand.Add(new FrameHelper(new Rectangle(388, 289, 38, 75)));
            GhostWarrior2Stand.Add(new FrameHelper(new Rectangle(548, 280, 40, 84)));
            GhostWarrior2Stand.Add(new FrameHelper(new Rectangle(708, 279, 43, 83)));
            GhostWarrior2.Add(AnimationType.Stand, new Animation(GhostWarrior2Spritesheet, 0.1f, GhostWarrior2Stand));

            //Dictionary<AnimationType, EntityAction> GhostWarrior2Actions = new Dictionary<AnimationType, EntityAction>()
            //{
            //    [AnimationType.BasicAttack] = new Ability(AnimationType.BasicAttack, GhostWarrior2BasicAttack, !CanBeCanceled, 0.1f, 0, 90, 20, false),
            //    [AnimationType.Death] = new Death(AnimationType.Death, GhostWarrior2Death, !CanBeCanceled, 0.1f),
            //    [AnimationType.Run] = new Run(AnimationType.Run, GhostWarrior2Run, CanBeCanceled, 0.2f),
            //    [AnimationType.Stand] = new Stand(AnimationType.Stand, GhostWarrior2Stand, CanBeCanceled, 0.1f),
            //};
            Dictionary<AnimationType, AnimationBehaviour> GhostWarrior2Behaviours = new Dictionary<AnimationType, AnimationBehaviour>()
            {
                [AnimationType.Run] = new Run(AnimationType.Run),
                [AnimationType.BasicAttack] = new MeleeAttack(AnimationType.BasicAttack, 10, 70, 0, false, AnimationType.Stand),
                [AnimationType.Death] = new Death(AnimationType.Death),
                [AnimationType.Stand] = new Stand(AnimationType.Stand),
            };

            EntityAnimationBehaviours.Add(EntityName.GhostWarrior2, GhostWarrior2Behaviours);
            EntityAnimations.Add(EntityName.GhostWarrior2, GhostWarrior2);
            //EntityActions.Add(EntityName.GhostWarrior2, GhostWarrior2Actions);
            #endregion

            #region RangedCultist
            Texture2D rangedCultistSprite = content.Load<Texture2D>("Enemies/Cultist_Sheet");
            EntitySpriteSheets.Add(EntityName.RangedCultist, rangedCultistSprite);
            EntityTextures.Add(EntityName.RangedCultist, new Rectangle(59, 47, 23, 37));

            Dictionary<AnimationType, Animation> RangedCultist = new Dictionary<AnimationType, Animation>();

            List<FrameHelper> RangeCultistRun = new List<FrameHelper>();
            RangeCultistRun.Add(new FrameHelper(new Rectangle(59, 47, 23, 37)));
            RangeCultistRun.Add(new FrameHelper(new Rectangle(104, 48, 23, 36)));
            RangeCultistRun.Add(new FrameHelper(new Rectangle(151, 48, 21, 36)));
            RangeCultistRun.Add(new FrameHelper(new Rectangle(194, 47, 23, 37)));
            RangeCultistRun.Add(new FrameHelper(new Rectangle(14, 89, 25, 37)));
            RangeCultistRun.Add(new FrameHelper(new Rectangle(59, 90, 24, 36)));
            RangeCultistRun.Add(new FrameHelper(new Rectangle(104, 90, 24, 36)));
            RangeCultistRun.Add(new FrameHelper(new Rectangle(149, 89, 23, 37)));
            RangedCultist.Add(AnimationType.Run, new Animation(rangedCultistSprite, 0.1f, RangeCultistRun));

            List<FrameHelper> RangedCultistAttack = new List<FrameHelper>();
            RangedCultistAttack.Add(new FrameHelper(new Rectangle(191, 90, 27, 36)));
            RangedCultistAttack.Add(new FrameHelper(new Rectangle(14, 131, 25, 37)));
            RangedCultistAttack.Add(new FrameHelper(new Rectangle(59, 131, 25, 37)));
            RangedCultistAttack.Add(new FrameHelper(new Rectangle(96, 131, 34, 37)));
            RangedCultistAttack.Add(new FrameHelper(new Rectangle(137, 132, 37, 36)));
            RangedCultistAttack.Add(new FrameHelper(new Rectangle(182, 133, 38, 35)));
            RangedCultistAttack.Add(new FrameHelper(new Rectangle(0, 175, 40, 35)));
            RangedCultistAttack.Add(new FrameHelper(new Rectangle(55, 173, 29, 37)));
            RangedCultistAttack.Add(new FrameHelper(new Rectangle(103, 172, 26, 38)));
            RangedCultistAttack.Add(new FrameHelper(new Rectangle(145, 173, 29, 37)));
            RangedCultist.Add(AnimationType.BasicAttack, new Animation(rangedCultistSprite, 0.1f, RangedCultistAttack));

            List<FrameHelper> CultistFireball = new List<FrameHelper>();
            CultistFireball.Add(new FrameHelper(new Rectangle(198, 184, 19, 10)));
            CultistFireball.Add(new FrameHelper(new Rectangle(18, 225, 19, 11)));
            CultistFireball.Add(new FrameHelper(new Rectangle(63, 225, 18, 11)));
            CultistFireball.Add(new FrameHelper(new Rectangle(108, 226, 18, 10)));

            List<FrameHelper> CultistFireBallImpact = new List<FrameHelper>();
            CultistFireBallImpact.Add(new FrameHelper(new Rectangle(18, 263, 20, 15)));
            CultistFireBallImpact.Add(new FrameHelper(new Rectangle(62, 262, 19, 17)));
            CultistFireBallImpact.Add(new FrameHelper(new Rectangle(112, 261, 14, 19)));

            List<FrameHelper> RangedCultistDeath = new List<FrameHelper>();
            RangedCultistDeath.Add(new FrameHelper(new Rectangle(13, 3240, 24, 38)));
            RangedCultistDeath.Add(new FrameHelper(new Rectangle(58, 341, 23, 37)));
            RangedCultistDeath.Add(new FrameHelper(new Rectangle(103, 343, 22, 35)));
            RangedCultistDeath.Add(new FrameHelper(new Rectangle(142, 350, 37, 28)));
            RangedCultistDeath.Add(new FrameHelper(new Rectangle(183, 360, 41, 18)));
            RangedCultistDeath.Add(new FrameHelper(new Rectangle(1, 401, 43, 19)));
            RangedCultistDeath.Add(new FrameHelper(new Rectangle(46, 414, 42, 6)));
            RangedCultistDeath.Add(new FrameHelper(new Rectangle(91, 415, 43, 5)));
            RangedCultist.Add(AnimationType.Death, new Animation(rangedCultistSprite, 0.1f, RangedCultistDeath));

            List<FrameHelper> RangedCultistStand = new List<FrameHelper>();
            RangedCultistStand.Add(new FrameHelper(new Rectangle(8, 6, 31, 36)));
            RangedCultistStand.Add(new FrameHelper(new Rectangle(54, 5, 30, 37)));
            RangedCultistStand.Add(new FrameHelper(new Rectangle(99, 5, 31, 37)));
            RangedCultistStand.Add(new FrameHelper(new Rectangle(144, 6, 31, 36)));
            RangedCultistStand.Add(new FrameHelper(new Rectangle(188, 7, 31, 35)));
            RangedCultist.Add(AnimationType.Stand, new Animation(rangedCultistSprite, 0.1f, RangedCultistStand));

            Dictionary<AnimationType, AnimationBehaviour> RangedCultistBehaviour = new Dictionary<AnimationType, AnimationBehaviour>()
            {
                [AnimationType.Run] = new Run(AnimationType.Run),
                [AnimationType.Death] = new Death(AnimationType.Death),
                [AnimationType.BasicAttack] = new EnemyRangedAttack(AnimationType.BasicAttack, ProjectileType.CultistFireBall, new Vector2(187, 145), new Rectangle(182, 133, 38, 35), 2, 5, 400, 5, false),
                [AnimationType.Stand] = new Stand(AnimationType.Stand),
            };

            EntityAnimations.Add(EntityName.RangedCultist, RangedCultist);
            EntityAnimationBehaviours.Add(EntityName.RangedCultist, RangedCultistBehaviour);
            #endregion

            #region AssassinCultist
            var AssassinCultistSpriteSheet = content.Load<Texture2D>("Enemies/Cultist_Assassin_Sheet");
            EntitySpriteSheets.Add(EntityName.AssassinCultist, AssassinCultistSpriteSheet);
            EntityTextures.Add(EntityName.AssassinCultist, new Rectangle(16, 8, 31, 30));

            Dictionary<AnimationType, Animation> AssassinCultist = new Dictionary<AnimationType, Animation>();

            List<FrameHelper> AssassinCultistRun = new List<FrameHelper>();
            AssassinCultistRun.Add(new FrameHelper(new Rectangle(442, 12, 26, 26)));
            AssassinCultistRun.Add(new FrameHelper(new Rectangle(497, 10, 25, 28)));
            AssassinCultistRun.Add(new FrameHelper(new Rectangle(12, 47, 32, 24)));
            AssassinCultistRun.Add(new FrameHelper(new Rectangle(71, 50, 23, 26)));
            AssassinCultistRun.Add(new FrameHelper(new Rectangle(125, 50, 26, 26)));
            AssassinCultistRun.Add(new FrameHelper(new Rectangle(180, 48, 25, 28)));
            AssassinCultistRun.Add(new FrameHelper(new Rectangle(225, 47, 32, 24)));
            AssassinCultistRun.Add(new FrameHelper(new Rectangle(283, 50, 24, 26)));
            AssassinCultist.Add(AnimationType.Run, new Animation(AssassinCultistSpriteSheet, 0.1f, AssassinCultistRun));

            List<FrameHelper> AssassinCultistBasicAttack = new List<FrameHelper>();
            AssassinCultistBasicAttack.Add(new FrameHelper(new Rectangle(436, 48, 30, 28)));
            AssassinCultistBasicAttack.Add(new FrameHelper(new Rectangle(488, 49, 30, 27)));
            AssassinCultistBasicAttack.Add(new FrameHelper(new Rectangle(15, 86, 29, 28)));
            AssassinCultistBasicAttack.Add(new FrameHelper(new Rectangle(67, 86, 33, 28)));
            AssassinCultistBasicAttack.Add(new FrameHelper(new Rectangle(120, 83, 32, 30)));
            AssassinCultistBasicAttack.Add(new FrameHelper(new Rectangle(176, 82, 30, 30)));
            AssassinCultistBasicAttack.Add(new FrameHelper(new Rectangle(228, 80, 30, 30)));
            AssassinCultistBasicAttack.Add(new FrameHelper(new Rectangle(280, 78, 30, 30)));
            AssassinCultistBasicAttack.Add(new FrameHelper(new Rectangle(318, 81, 45, 33), new Rectangle(310, 79, 52, 37), new Rectangle(327, 85, 35, 29), canHit));
            AssassinCultistBasicAttack.Add(new FrameHelper(new Rectangle(318, 81, 45, 33), new Rectangle(310, 79, 52, 37), new Rectangle(327, 85, 35, 29), canHit));
            AssassinCultistBasicAttack.Add(new FrameHelper(new Rectangle(371, 83, 45, 30), new Rectangle(371, 83, 45, 30), new Rectangle(381, 87, 34, 27), canHit));
            AssassinCultistBasicAttack.Add(new FrameHelper(new Rectangle(371, 83, 45, 30), new Rectangle(371, 83, 45, 30), new Rectangle(381, 87, 34, 27), canHit));
            AssassinCultistBasicAttack.Add(new FrameHelper(new Rectangle(433, 85, 36, 29)));
            AssassinCultistBasicAttack.Add(new FrameHelper(new Rectangle(486, 85, 36, 29)));
            AssassinCultistBasicAttack.Add(new FrameHelper(new Rectangle(15, 125, 30, 27)));
            AssassinCultist.Add(AnimationType.BasicAttack, new Animation(AssassinCultistSpriteSheet, 0.1f, AssassinCultistBasicAttack));

            List<FrameHelper> AssassinCultistAmbushAttack = new List<FrameHelper>();
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(393, 198, 25, 30)));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(447, 196, 22, 33)));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(501, 193, 19, 34)));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(25, 234, 16, 29)));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(76, 242, 18, 19)));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(128, 238, 19, 23)));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(181, 239, 15, 18)));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(233, 238, 21, 20)));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(343, 236, 12, 19)));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(393, 231, 19, 27)));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(443, 228, 26, 35)));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(492, 229, 31, 34)));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(14, 267, 33, 34)));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(67, 267, 33, 34)));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(123, 267, 21, 33), new Rectangle(116, 267, 35, 40), new Rectangle(123, 267, 21, 33), canHit));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(169, 267, 28, 37), new Rectangle(161, 267, 35, 43), new Rectangle(169, 267, 28, 37), canHit));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(225, 281, 31, 23), new Rectangle(217, 284, 31, 23), new Rectangle(225, 281, 31, 23), canHit));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(279, 286, 31, 18)));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(332, 286, 31, 18)));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(385, 281, 31, 23)));
            AssassinCultistAmbushAttack.Add(new FrameHelper(new Rectangle(440, 276, 31, 28)));
            AssassinCultist.Add(AnimationType.Ability1, new Animation(AssassinCultistSpriteSheet, 0.1f, AssassinCultistAmbushAttack));

            List<FrameHelper> AssassinCultistDeath = new List<FrameHelper>();
            AssassinCultistDeath.Add(new FrameHelper(new Rectangle(286, 121, 26, 31)));
            AssassinCultistDeath.Add(new FrameHelper(new Rectangle(339, 121, 26, 31)));
            AssassinCultistDeath.Add(new FrameHelper(new Rectangle(390, 123, 24, 29)));
            AssassinCultistDeath.Add(new FrameHelper(new Rectangle(443, 125, 21, 27)));
            AssassinCultistDeath.Add(new FrameHelper(new Rectangle(495, 128, 25, 24)));
            AssassinCultistDeath.Add(new FrameHelper(new Rectangle(18, 167, 24, 23)));
            AssassinCultistDeath.Add(new FrameHelper(new Rectangle(71, 166, 26, 24)));
            AssassinCultistDeath.Add(new FrameHelper(new Rectangle(125, 166, 26, 24)));
            AssassinCultistDeath.Add(new FrameHelper(new Rectangle(178, 165, 26, 25)));
            AssassinCultist.Add(AnimationType.Death, new Animation(AssassinCultistSpriteSheet, 0.17f, AssassinCultistDeath));

            List<FrameHelper> AssassinCultistStand = new List<FrameHelper>();
            AssassinCultistStand.Add(new FrameHelper(new Rectangle(16, 8, 31, 30)));
            AssassinCultistStand.Add(new FrameHelper(new Rectangle(70, 7, 30, 31)));
            AssassinCultistStand.Add(new FrameHelper(new Rectangle(124, 6, 30, 32)));
            AssassinCultistStand.Add(new FrameHelper(new Rectangle(178, 6, 30, 32)));
            AssassinCultist.Add(AnimationType.Stand, new Animation(AssassinCultistSpriteSheet, 0.1f, AssassinCultistStand));

            Dictionary<AnimationType, AnimationBehaviour> AssassinCultistBehaviours = new Dictionary<AnimationType, AnimationBehaviour>()
            {
                [AnimationType.BasicAttack] = new MeleeAttack(AnimationType.BasicAttack, 20, 90, 0, true, AnimationType.Stand),
                [AnimationType.Ability1] = new AssassinCultistAmbush(AnimationType.Ability1, 20, 200, 10, false),
                [AnimationType.Death] = new Death(AnimationType.Death),
                [AnimationType.Run] = new Run(AnimationType.Run),
                [AnimationType.Stand] = new Stand(AnimationType.Stand),
            };

            EntityAnimationBehaviours.Add(EntityName.AssassinCultist, AssassinCultistBehaviours);
            EntityAnimations.Add(EntityName.AssassinCultist, AssassinCultist);
            #endregion

            #region Projectiles
            Projectiles.Add(ProjectileType.CultistFireBall, new MovingProjectile(ProjectileType.CultistFireBall, 5, rangedCultistSprite, CultistFireball, CultistFireBallImpact, 0.1f, 1f));
            Projectiles.Add(ProjectileType.BringerOfDeathPortalSummon, new StationaryProjectile(ProjectileType.BringerOfDeathPortalSummon, 5, BringerOfDeathTexture, BringerOfDeathPortalSummon, 0.1f, 1.3f));

            Texture2D LightningStrikeTexture = content.Load<Texture2D>("Effects/CLOUD LIGHTNING ATTACK-Sheet");
            List<FrameHelper> LightningStrike = new List<FrameHelper>();
            LightningStrike.Add(new FrameHelper(new Rectangle(170, 19, 33, 14)));
            LightningStrike.Add(new FrameHelper(new Rectangle(271, 15, 89, 14)));
            LightningStrike.Add(new FrameHelper(new Rectangle(403, 18, 84, 14)));
            LightningStrike.Add(new FrameHelper(new Rectangle(522, 17, 98, 16)));
            LightningStrike.Add(new FrameHelper(new Rectangle(653, 17, 96, 198), new Rectangle(653, 17, 92, 16)));
            LightningStrike.Add(new FrameHelper(new Rectangle(787, 18, 84, 196), new Rectangle(787, 18, 84, 16)));
            LightningStrike.Add(new FrameHelper(new Rectangle(906, 17, 98, 198), new Rectangle(906, 17, 98, 17)));
            LightningStrike.Add(new FrameHelper(new Rectangle(1037, 17, 100, 199), new Rectangle(1037, 17, 92, 18)));
            LightningStrike.Add(new FrameHelper(new Rectangle(1169, 18, 90, 195), new Rectangle(1171, 18, 84, 15)));
            LightningStrike.Add(new FrameHelper(new Rectangle(1290, 17, 98, 196), new Rectangle(1290, 17, 98, 17)));
            LightningStrike.Add(new FrameHelper(new Rectangle(1521, 17, 92, 16)));
            LightningStrike.Add(new FrameHelper(new Rectangle(1546, 15, 95, 198), new Rectangle(1546, 15, 95, 22)));
            LightningStrike.Add(new FrameHelper(new Rectangle(1683, 17, 85, 17)));
            LightningStrike.Add(new FrameHelper(new Rectangle(1808, 15, 93, 197), new Rectangle(1808, 15, 93, 24)));
            Projectiles.Add(ProjectileType.LightningStrike, new StationaryProjectile(ProjectileType.LightningStrike, 50, LightningStrikeTexture, LightningStrike, 0.1f, 0.8f));

            #endregion


        }
    }
}
