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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace MortenSurvivor.Commands.States
{


    public class ConfusedState : IState<Enemy>
    {

        private readonly Enemy parent;
        private float duration;
        private float switchTimer = 0f;
        private float timeElapsed = 0f;
        private Vector2 randomDirection;

        /// <summary>
        /// Sætter fjenden til at gå i en tilfældig retning
        /// </summary>
        /// <param name="parent">Fjenden der skal manipuleres med</param>
        /// <param name="duration">Varigheden af tilstanden</param>
        public ConfusedState(Enemy parent, float duration)
        {

            this.parent = parent;
            this.duration = duration;
            parent.DrawColor = Color.Aquamarine;
            RandomDirection();

        }

        /// <summary>
        /// Anvendes ikke
        /// </summary>
        /// <param name="parent"></param>
        public void Enter(Enemy parent)
        {



        }

        /// <summary>
        /// Bevæger fjenden i en tilfældig retning, der skiftes hvert sekund i et angivet tidsspan
        /// </summary>
        public void Execute()
        {

            timeElapsed += GameWorld.Instance.DeltaTime;
            switchTimer += GameWorld.Instance.DeltaTime;

            if (timeElapsed >= duration)
                Exit();
            else if (switchTimer >= 1f)
            {
                switchTimer = 0f;
                RandomDirection();
            }

            parent.Move(randomDirection);

        }

        /// <summary>
        /// Returnerer fjenden til dens normale stadie
        /// </summary>
        public void Exit()
        {

            parent.DrawColor = parent.OriginalColor;
            parent.CurrentState = parent.OriginalState;

        }

        /// <summary>
        /// Giver fjenden en tilfældig retning
        /// </summary>
        private void RandomDirection()
        {

            randomDirection = new Vector2((float)(GameWorld.Instance.Random.NextDouble() * 2.0 - 1.0), (float)(GameWorld.Instance.Random.NextDouble() * 2.0 - 1.0));
            randomDirection.Normalize();

        }

    }
}
