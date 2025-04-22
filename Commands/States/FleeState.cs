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


    public class FleeState : IState<Enemy>
    {

        private readonly Enemy parent;
        private float duration;
        private float timeElapsed = 0f;

        /// <summary>
        /// Sætter fjenden til at flygte fra Morten
        /// </summary>
        /// <param name="parent">Fjenden der skal manipuleres med</param>
        /// <param name="duration">Hvor lang tid fjenden skal flygte</param>
        public FleeState(Enemy parent, float duration)
        {

            this.parent = parent;
            this.duration = duration;
            parent.DrawColor = Color.Green;

        }

        /// <summary>
        /// Anvendes ikke
        /// </summary>
        /// <param name="parent"></param>
        public void Enter(Enemy parent)
        {



        }

        /// <summary>
        /// Bevæger fjenden væk fra Morten i en angiven tidsspan
        /// </summary>
        public void Execute()
        {

            timeElapsed += GameWorld.Instance.DeltaTime;

            if (duration > timeElapsed)
            {
                Vector2 direction = parent.Position - Player.Instance.Position;
                direction.Normalize();
                parent.Move(direction);
            }
            else
                Exit();

        }

        /// <summary>
        /// Returnerer fjenden til dens naturlige tilstand
        /// </summary>
        public void Exit()
        {

            parent.DrawColor = parent.OriginalColor;
            parent.CurrentState = new ChaseState(parent);

        }

    }
}
