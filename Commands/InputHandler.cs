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
        private Dictionary<Keys, ICommand> keybindsUpdate = new Dictionary<Keys, ICommand>();
        private Dictionary<Keys, ICommand> keyBindsButtonDown = new Dictionary<Keys, ICommand>();
        private Dictionary<Keys, ICommand> keyBindsOncePerCountdown = new Dictionary<Keys, ICommand>();
        private Dictionary<MouseKeys, ICommand> mouseKeybindsUpdate = new Dictionary<MouseKeys, ICommand>();
        private Dictionary<MouseKeys, ICommand> mouseKeyBindsButtonDown = new Dictionary<MouseKeys, ICommand>();
        private Dictionary<MouseKeys, ICommand> mouseKeybindsOncePerCoundown = new Dictionary<MouseKeys, ICommand>();
        private KeyboardState previousKeyState;
        private List<MouseKeys> previousPressedMouseKeys;

        private float timeElapsed;
        private float countdown = 1;

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

        public Rectangle MouseCollisionBox
        {
            get
            {
                Vector2 mousePosition = Mouse.GetState().Position.ToVector2();
                return new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 1, 1);
            }
        }

        public void AddUpdateCommand(Keys inputKey, ICommand command)
        {
            keybindsUpdate.Add(inputKey, command);
        }
        public void AddUpdateCommand(MouseKeys inputButton, ICommand command)
        {
            mouseKeybindsUpdate.Add(inputButton, command);
        }

        public void AddButtonDownCommand(Keys inputKey, ICommand command)
        {
            keyBindsButtonDown.Add(inputKey, command);
        }

        public void AddButtonDownCommand(MouseKeys inputKey, ICommand command)
        {
            mouseKeyBindsButtonDown.Add(inputKey, command);
        }


        public void AddOncePerCountdownCommand(Keys inputKey, ICommand command)
        {
            keyBindsOncePerCountdown.Add(inputKey, command);
        }
        public void AddOncePerCountdownCommand(MouseKeys inputKey, ICommand command)
        {
            mouseKeybindsOncePerCoundown.Add(inputKey, command);
        }

        /// <summary>
        /// Mehtod that returns a list of the MouseKeys (enum) of the mouse buttons currently pressed
        /// </summary>
        /// <param name="mouseState">the current mousestate</param>
        /// <returns></returns>
        public List<MouseKeys> GetPressedMouseKeys(MouseState mouseState)
        {
            List<MouseKeys> pressedKeys = new List<MouseKeys>();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                pressedKeys.Add(MouseKeys.LeftButton);
            }
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                pressedKeys.Add(MouseKeys.RightButton);
            }
            if (mouseState.MiddleButton == ButtonState.Pressed)
            {
                pressedKeys.Add(MouseKeys.MiddleButton);
            }
            if (mouseState.XButton1 == ButtonState.Pressed)
            {
                pressedKeys.Add(MouseKeys.xButton1);
            }
            if (mouseState.XButton2 == ButtonState.Pressed)
            {
                pressedKeys.Add(MouseKeys.xButton2);
            }

            return pressedKeys;
        }


        public void Execute()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            List<MouseKeys> pressedMouseKeys = GetPressedMouseKeys(mouseState);
            timeElapsed += GameWorld.Instance.DeltaTime;
            foreach (var pressedKey in keyboardState.GetPressedKeys())
            {
                if (keybindsUpdate.TryGetValue(pressedKey, out ICommand command))
                {
                    command.Execute();
                }
                if (!previousKeyState.IsKeyDown(pressedKey) && keyboardState.IsKeyDown(pressedKey))
                {
                    if (keyBindsButtonDown.TryGetValue(pressedKey, out ICommand commandButtonDown))
                    {
                        commandButtonDown.Execute();
                    }
                }
                if (timeElapsed > countdown)
                {
                    if (keyBindsOncePerCountdown.TryGetValue(pressedKey, out ICommand commandCountdown))
                    {
                        commandCountdown.Execute();
                        timeElapsed = 0;
                    }

                }

            }
            previousKeyState = keyboardState;

            foreach (var mouseKey in pressedMouseKeys)
            {
                if (mouseKeybindsUpdate.TryGetValue(mouseKey, out ICommand command))
                {
                    command.Execute();
                }

                if (timeElapsed > countdown)
                {
                    if (mouseKeybindsOncePerCoundown.TryGetValue(mouseKey, out ICommand commandCountdown))
                    {
                        commandCountdown.Execute();
                        timeElapsed = 0;
                    }

                }
                if (!previousPressedMouseKeys.Contains(mouseKey) && pressedMouseKeys.Contains(mouseKey))
                {
                    if (mouseKeyBindsButtonDown.TryGetValue(mouseKey, out ICommand commandButtonDown))
                    {
                        commandButtonDown.Execute();
                    }
                }
            }
            previousPressedMouseKeys = pressedMouseKeys;
        }

    }
}
