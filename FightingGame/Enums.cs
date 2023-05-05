﻿using System;
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
        Jump,
        Run,
        Down,
        Fall,
        Dodge,
        Stand,
        Attack,
        Special,

        NeutralAttack = Stand | Attack,
        DirectionalAttack = Run | Attack,
        DownAttack = Down | Attack,
        UpAttack = Jump | Attack,

        NeutralSpecial = Stand | Special,
        DirectionalSpecial = Run | Special,
        DownSpecial = Down | Special,
        UpSpecial = Jump | Special,

    }
    public enum CharacterName
    {
        CaptainFalcon,
    }
    public enum CharacterDirection
    {
        Left,
        Right,
    }
}
