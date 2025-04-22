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


    public class MoveState : IState<Projectile>
    {

        private Vector2 direction;
        private float rotation;
        private readonly Projectile parent;

        /// <summary>
        /// Sætter stadiet for et projektil til at flyve fra startposition (Helst Morten) imod musens position
        /// </summary>
        /// <param name="parent">Det projektil der skal manipuleres med</param>
        public MoveState(Projectile parent)
        {

            this.parent = parent;

            Vector2 mousePos = Mouse.GetState().Position.ToVector2(); //Finder musens position og omdanner til en Vector2
            Matrix inverseTransform = Matrix.Invert(GameWorld.Instance.Camera.GetTransformation()); //Danner en invers-matrice til at modvirke kameraets zoom effekt
            mousePos = Vector2.Transform(mousePos, inverseTransform); //Omdanner muse-positionen til den reelle position

            direction = mousePos - parent.Position;
            direction.Normalize();

            switch(direction.X)
            {
                case < 0:
                    rotation = -0.1f;
                    break;
                default:
                    rotation = 0.1f;
                    break;
            }

        }

        /// <summary>
        /// Anvendes ikke
        /// </summary>
        /// <param name="parent"></param>
        public void Enter(Projectile parent)
        {
            


        }

        /// <summary>
        /// Står for rotation og bevægelse af projektilet
        /// </summary>
        public void Execute()
        {

            parent.Rotation += rotation;
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
