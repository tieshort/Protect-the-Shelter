using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ControlSetup
{
    // Устройства игроков
    public static PlayerSetup[] Players = new PlayerSetup[]
    {
        new(),
        new(),
    };

    // Назначить клавишу newKey для раскладки с номером keyboard на команду command
    public static void BindKeyboardKey(KeyboardIndex keyboard, ControlCommand command, KeyCode newKey)
    {
        // Если клавишу нельзя назначить или если клавиатура не указана явно, выходим
        if (ForbiddenBindings.Contains(newKey) || keyboard == KeyboardIndex.Any) return;

        // Если для команды не назначена клавиша, добавляем для нее заготовку и вызываем функцию с начала
        int kIndex = (int)keyboard;
        if (!keyboardLayout[kIndex].ContainsKey(command))
        {
            keyboardLayout[kIndex].Add(command, KeyCode.None);
            BindKeyboardKey(keyboard, command, newKey);
            return;
        }

        // Здесь оказываемя если есть команда
        if(newKey != KeyCode.None)
        {
            RemoveOtherBindings(newKey);
        }
        // Назначаем новую
        keyboardLayout[kIndex][command] = newKey;
    }

    // Проверяем каждую раскладку и убираем клавишу, если она привязана к чему-то
    public static void RemoveOtherBindings(KeyCode key)
    {
        foreach (var kbLayout in keyboardLayout)
        {
            foreach (var keyCommand in kbLayout.Where(kvp => kvp.Value == key).ToList())
            {
                kbLayout[keyCommand.Key] = KeyCode.None;
                
            }
        }
    }

    public static void ResetBindings(KeyboardIndex keyboard = KeyboardIndex.Any)
    {
        for(var i = 0; i < keyboardLayout.Length; i++)
        {
            if (keyboard == KeyboardIndex.Any || i == (int)keyboard)
            {
                keyboardLayout[i] = new Dictionary<ControlCommand, KeyCode>(defaultKeyboardLayout[i]);
            }
        }
    }

    // Назначения клавиш для игроков
    private static Dictionary<ControlCommand, KeyCode>[] keyboardLayout = new Dictionary<ControlCommand, KeyCode>[]
    {
        new() {
            { ControlCommand.Forward, KeyCode.W },
            { ControlCommand.Backward, KeyCode.S },
            { ControlCommand.Left, KeyCode.A },
            { ControlCommand.Right, KeyCode.D },
            { ControlCommand.ActionOne, KeyCode.Space },
            { ControlCommand.ActionTwo, KeyCode.T },
            { ControlCommand.SpecialAction, KeyCode.Tab },
            { ControlCommand.Interaction, KeyCode.E },
        },
        new() {
            { ControlCommand.Forward, KeyCode.UpArrow },
            { ControlCommand.Backward, KeyCode.DownArrow },
            { ControlCommand.Left, KeyCode.LeftArrow },
            { ControlCommand.Right, KeyCode.RightArrow },
            { ControlCommand.ActionOne, KeyCode.RightShift },
            { ControlCommand.ActionTwo, KeyCode.RightControl },
            { ControlCommand.SpecialAction, KeyCode.Slash },
            { ControlCommand.Interaction, KeyCode.RightAlt },

        }
    };

    private static Dictionary<ControlCommand, KeyCode>[] defaultKeyboardLayout = new Dictionary<ControlCommand, KeyCode>[]
    {
        new() {
            { ControlCommand.Forward, KeyCode.W },
            { ControlCommand.Backward, KeyCode.S },
            { ControlCommand.Left, KeyCode.A },
            { ControlCommand.Right, KeyCode.D },
            { ControlCommand.ActionOne, KeyCode.Space },
            { ControlCommand.ActionTwo, KeyCode.T },
            { ControlCommand.SpecialAction, KeyCode.Tab },
            { ControlCommand.Interaction, KeyCode.E },
        },
        new() {
            { ControlCommand.Forward, KeyCode.UpArrow },
            { ControlCommand.Backward, KeyCode.DownArrow },
            { ControlCommand.Left, KeyCode.LeftArrow },
            { ControlCommand.Right, KeyCode.RightArrow },
            { ControlCommand.ActionOne, KeyCode.RightShift },
            { ControlCommand.ActionTwo, KeyCode.RightControl },
            { ControlCommand.SpecialAction, KeyCode.Slash },
            { ControlCommand.Interaction, KeyCode.RightAlt },

        }
    };

    // Запрещенные для назначения клавиши
    public static KeyCode[] ForbiddenBindings = new KeyCode[]
    {
        KeyCode.Escape,
        KeyCode.KeypadEnter,
        KeyCode.LeftWindows,
        KeyCode.RightWindows,
        KeyCode.F1,
        KeyCode.F2,
        KeyCode.F3,
        KeyCode.F4,
        KeyCode.F5,
        KeyCode.F6,
        KeyCode.F7,
        KeyCode.F8,
        KeyCode.F9,
        KeyCode.F10,
        KeyCode.F11,
        KeyCode.F12,
        KeyCode.F13,
        KeyCode.F14,
        KeyCode.F15,
    };

    // 
    public class KeyboardData
    {
        public KeyboardIndex Index = KeyboardIndex.Any;

        public KeyCode GetBindTo(ControlCommand command)
        {
            int kIndex = (int)Index;

            if (Index == KeyboardIndex.Any)
            {
                kIndex = 0;
            }

            if (keyboardLayout[kIndex].ContainsKey(command))
            {
                return keyboardLayout[kIndex][command];
            }
            else 
            {
                return KeyCode.None;
            }
        }
    }

    public class PlayerSetup
    {
        public ControlDevice Device { get; private set; }
        public KeyboardData Keyboard = new();

        public void SetAnyDevice()
        {
            Device = ControlDevice.Any;
            Keyboard.Index = KeyboardIndex.Any;
        }

        public void SetKeyboard(KeyboardIndex index)
        {
            Device = ControlDevice.Keyboard;
            Keyboard.Index = index;
        }

        public void ResetDevice()
        {
            Device = ControlDevice.None;
            Keyboard.Index = KeyboardIndex.Any;
        }
    }
}

public enum ControlDevice
{
    None,
    Any,
    Keyboard,
    Gamepad
}

public enum KeyboardIndex
{
    Any = -1,
    First = 0,
    Second = 1
}
