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
    public class MoveCommand : ICommand
    {
        private Player player;
        private Vector2 direction;

        public MoveCommand(Player player, Vector2 direction)
        {
            this.player = player;
            this.direction = direction;
        }

        public void Execute()
        {
            if (!GameWorld.Instance.GamePaused)
            {
                player.Move(direction);
            }
        }
    }
}
