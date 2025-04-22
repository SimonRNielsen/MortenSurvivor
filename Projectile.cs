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
    public class Projectile : GameObject
    {

        #region Fields

        private Vector2 velocity;
        private float speed;
        private int damage;
        private HashSet<GameObject> collidedWith = new HashSet<GameObject>();
        private IState<Projectile> currentState;

        #endregion
        #region Properties

        
        public float Speed { get => speed; set => speed = value; }


        public int Damage { get => damage; set => damage = value; }


        public Vector2 Velocity { get => velocity; }

        #endregion
        #region Constuctor


        public Projectile(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
        }

        #endregion
        #region Methods


        public override void Load()
        {

            collidedWith.Clear();

            currentState = new MoveState();

            base.Load();

        }


        public override void OnCollision(GameObject other)
        {

            base.OnCollision(other);

        }


        public override void Update(GameTime gameTime)
        {

            if (currentState != null)
                currentState.Execute();

            base.Update(gameTime);

        }

        #endregion

    }
}
