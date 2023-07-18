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
        Run,
        Dodge,
        Stand,
        Attack,
        Death,

        BasicAttack,
        Ability1,
        Ability2,
        Ability3,
    }
    public enum EntityName
    {
        Swordsman,
        Hashashin,
        Samurai,
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
