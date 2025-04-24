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
    public class OrbitState : IState<Projectile>
    {

        private Player player;
        private Projectile parent;
        private float orbitAngle = 0f;
        private float orbitSpeed = MathHelper.TwoPi / 5f; // Full orbit every 5 seconds
        private float orbitRadius;
        private float duration;

        /// <summary>
        /// Applies an orbiting effect on the parent
        /// </summary>
        /// <param name="parent">Projectile to apply effect on</param>
        /// <param name="radius">Radius of the orbiting object from Player</param>
        public OrbitState(Projectile parent, float radius)
        {

            this.parent = parent;
            player = Player.Instance;
            orbitRadius = radius;

        }

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="parent"></param>
        public void Enter(Projectile parent)
        {
            


        }

        /// <summary>
        /// Handles movement and lifetime of the projectile
        /// </summary>
        public void Execute()
        {

            duration += GameWorld.Instance.DeltaTime;

            if (duration >= 5f)
                parent.IsAlive = false;

            // Update angle over time
            orbitAngle += orbitSpeed * GameWorld.Instance.DeltaTime;

            // Keep angle within 0 to 2π for neatness (optional)
            orbitAngle %= MathHelper.TwoPi;

            parent.Position = GetOrbitPosition();

        }

        /// <summary>
        /// Not used
        /// </summary>
        public void Exit()
        {
            


        }

        /// <summary>
        /// Calculates the next position of the projectile
        /// </summary>
        /// <returns>Position to move projectile to</returns>
        private Vector2 GetOrbitPosition()
        {

            float x = player.Position.X + orbitRadius * (float)Math.Cos(orbitAngle);
            float y = player.Position.Y + orbitRadius * (float)Math.Sin(orbitAngle);
            return new Vector2(x, y);

        }

    }
}
