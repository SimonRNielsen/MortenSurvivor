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

namespace MortenSurvivor.Commands.States
{


    public class ChaseState : IState<Enemy>
    {

        private readonly Enemy parent;

        /// <summary>
        /// Sætter stadiet for en fjende til at jagte Morten
        /// </summary>
        /// <param name="parent">Fjenden der skal manipuleres med</param>
        public ChaseState(Enemy parent)
        {

            this.parent = parent;

        }

        /// <summary>
        /// Anvendes ikke
        /// </summary>
        /// <param name="parent"></param>
        public void Enter(Enemy parent)
        {
            


        }

        /// <summary>
        /// Bevæger fjenden mod Morten
        /// </summary>
        public void Execute()
        {

            Vector2 direction = Player.Instance.Position - parent.Position;
            direction.Normalize();
            parent.Move(direction);

        }

        /// <summary>
        /// Anvendes ikke
        /// </summary>
        public void Exit()
        {
            


        }

    }
}
