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

namespace MortenSurvivor
{
    public class Environment : GameObject
    {

        #region Fields
        private EnvironmentTile tileType;

        #endregion
        #region Properties



        #endregion
        #region Constructor


        public Environment(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            layer = 0f;
            tileType = (EnvironmentTile)type;

        }

        #endregion
        #region Methods

     


        #endregion

    }
}
