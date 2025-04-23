using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using MortenSurvivor.Commands;
using MortenSurvivor.Commands.States;
using MortenSurvivor.CreationalPatterns.Factories;
using MortenSurvivor.CreationalPatterns.Pools;
using MortenSurvivor.ObserverPattern;

namespace MortenSurvivor.Commands
{
    public class InputHandler
    {
        private static InputHandler instance;
        private Dictionary<Keys, ICommand> keybinds = new Dictionary<Keys, ICommand>();

        public static InputHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputHandler();
                }
                return instance;
            }
        }

        private InputHandler()
        {

        }


        public void AddCommand(Keys inputKey, ICommand command)
        {
            keybinds.Add(inputKey, command);
        }

        public void Execute()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            foreach (var pressedKey in keyboardState.GetPressedKeys())
            {
                if(keybinds.TryGetValue(pressedKey, out ICommand command))
                {
                    command.Execute();
                }

            }
        }

    }
}
