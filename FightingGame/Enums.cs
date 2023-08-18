using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public enum Screenum
    {
        StartMenuScreen,
        CharacterSelectionScreen,
        GameScreen,
        CardSelectionScreen,
    }
    public enum ClickResult
    {
        LeftClicked,
        RightClicked,
        Nothing,
        Released,
        Hovering,
    }
    public enum Texture
    {
        PlayGame,
        QuitGame,
        GameScreenBackground,
        StageTile,
        DungeonUIBackground
    }
   
    [Flags] public enum AnimationType
    {
        None,
        Spawn,
        Run,
        Dodge,
        Stand,
        Attack,
        Death,

        BasicAttack,
        Ability1,
        Ability2,
        Ability3,

        UltimateTransform,
        UndoTransform,
        UltimateStand,
        UltimateRun,
        UltimateDodge,
        UltimateBasicAttack,
        UltimateAbility1,
        UltimateAbility2,
        UltimateAbility3,

        ProjectileFlight,
        ProjectileHit,

    }
    public enum EntityName
    {
        //Characters
        Hashashin,

        //Enemies
        Skeleton,
        Necromancer,
        BringerOfDeath,

        RangedCultist,
        BigCultist,
        AssassinCultist,

        Enforcer,
        Prototype,

        Rogue,
        Executioner,

        //Bosses
        GhostWarrior,
        GhostWarrior2,

        EvilWizard,

        NightBorneWarrior,

        Troll,
        ArmoredExecutioner,

        Demon,

    }
    public enum CharacterPortrait
    {
        HashashinElemental,
        HashashinBase,
    }
    public enum SideHit
    {
        Left,
        Right,
        Top,
        Bottom,
        None,
    }

    public enum ProjectileType
    {
        CultistFireBall,
        BringerOfDeathPortalSummon,
    }

    public enum PowerUpType
    {
        HealthRegenAmmountIncrease,
        HealthRegenRateIncrease,
        MaxHealthIncrease,
        MaxStaminaIncrease,
        DamageIncrease,
        Bleed,
        LifeSteal,
        Overshield,
    }
    public enum IconType
    {
        HashashinAbility1,
        HashashinAbility2,
        HashashinAbility3,
        HashashinDodge,
        HashashinUltimateAbility1,
        HashashinUltimateAbility2,
        HashashinUltimateAbility3,
        HashashinUltimateDodge,

    }
    public enum CardRarity
    {
        Common,
        Rare, 
        Legendary,
    }
}
