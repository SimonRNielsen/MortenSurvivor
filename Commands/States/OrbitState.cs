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


        public OrbitState(Projectile parent, float radius)
        {

            this.parent = parent;
            player = Player.Instance;
            orbitRadius = radius;

        }


        public void Enter(Projectile parent)
        {
            


        }


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


        public void Exit()
        {
            


        }


        private Vector2 GetOrbitPosition()
        {

            float x = player.Position.X + orbitRadius * (float)Math.Cos(orbitAngle);
            float y = player.Position.Y + orbitRadius * (float)Math.Sin(orbitAngle);
            Vector2 rotation = new Vector2(x, y);
            return rotation;

        }

    }
}
