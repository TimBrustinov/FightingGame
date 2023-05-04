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
    [Flags]
    public enum AnimationType
    {
        Jump,
        LeftRun,
        RightRun,
        Down,
        Fall,
        Dodge,
        Stand,
        Attack,
        Special,

        NeutralAttack = Stand | Attack,
        LeftAttack = LeftRun | Attack,
        RightAttack = RightRun | Attack,
        DownAttack = Down | Attack,

        NeutralSpecial = Stand | Special,
        LeftSpecial = LeftRun | Special,
        RightSpecial = RightRun | Special,
        DownSpecial = Down | Special,
        UpSpecial = Jump | Special,
    }
    public enum CharacterName
    {
        CaptainFalcon,
    }
}
