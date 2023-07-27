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
        UltimateStand,
        UltimateRun,
        UltimateDodge,
        UltimateBasicAttack,
        UltimateAbility1,
        UltimateAbility2,
        UltimateAbility3,
    }
    public enum EntityName
    {
        Hashashin,
        GhostWarrior,
        Skeleton,
    }
    public enum EnemyName
    {
        Skeleton,
        Mage,
        DeathMage,
    }
    public enum CharacterDirection
    {
        Left,
        Right,
    }
    public enum SideHit
    {
        Left,
        Right,
        Top,
        Bottom,
        None,
    }
}
