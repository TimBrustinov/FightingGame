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
    }
   
    [Flags] public enum AnimationType
    {
        None,
        Jump,
        Run,
        Down,
        Fall,
        Dodge,
        Stand,
        Attack,
        Special,

        NeutralAttack,
        DirectionalAttack,
        DownAttack,
        UpAttack,

        NeutralSpecial = Stand | Special,
        DirectionalSpecial = Run | Special,
        DownSpecial = Down | Special,
        UpSpecial = Jump | Special,

    }
    public enum CharacterName
    {
        CaptainFalcon,
        Zombie,
    }
    public enum EnemyName
    {
        Zombie,
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
