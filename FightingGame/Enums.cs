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
        RunUp,
        RunDown,
        Dodge,
        Stand,
        Attack,

        SideAttack,
        UpAttack,
        DownAttack,
    }
    public enum CharacterName
    {
        Swordsman,
    }
    public enum EnemyName
    {
        Skeleton,
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
