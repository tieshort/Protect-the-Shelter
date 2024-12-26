using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommandInput
{
    
}

// Номер игрока
public enum PlayerIndex
{
    Any = -1,
    First = 0,
    Second = 1,
}

// Список команд в игре
public enum ControlCommand
{
    Forward,
    Backward,
    Left,
    Right,

    ActionOne,
    ActionTwo,
    SpecialAction,
    Interaction,

    AdditionActionOne,
    AdditionActionTwo,

    NextChoice,
    PreviousChoice,

    ScrollingUp,
    ScrollingDown,
    ScrollingLeft,
    ScrollingRight,

    OtherUp,
    OtherDown,
    OtherLeft,
    OtherRight
}
