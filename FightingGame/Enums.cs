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
    public enum AnimationType
    {
        NeutralAttack,
        SideAttack,
        DownAttack,
        UpAttack,
        NeutralSpecial,
        SideSpecial,
        DownSpecial,
        UpSpecial,
        Jump,
        Run,
        Dodge,
        Stand,
    }
    public enum CharacterName
    {
        CaptainFalcon,
    }
}
