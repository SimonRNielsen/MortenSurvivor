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

        //private Vector2 velocity;
        private float speed = 300f;
        private int damage;
        private HashSet<GameObject> collidedWith;
        private IState<Projectile> currentState;
        private readonly Vector2 screenHalfs;

        #endregion
        #region Properties


        public float Speed { get => speed; }
        public int Damage { get => damage; set => damage = value; }


        //public int Damage { get => damage; set => damage = value; }


        //public Vector2 Velocity { get => velocity; }

        #endregion
        #region Constuctor

        /// <summary>
        /// Opretter et projektil
        /// </summary>
        /// <param name="type">Hvilket sprite projektilet skal have</param>
        /// <param name="spawnPos">Hvor projektilet skal starte fra</param>
        /// <param name="speed">Hvor hurtigt projektilet skal flyve</param>
        /// <param name="damage">Hvor meget skade projektilet skal gøre</param>
        public Projectile(Enum type, Vector2 spawnPos, float speed, int damage) : base(type, spawnPos)
        {

            this.damage = damage;
            this.speed = speed;
            screenHalfs = GameWorld.Instance.Screensize / 2 / GameWorld.Instance.Camera.Zoom;

        }

        #endregion
        #region Methods

        /// <summary>
        /// Kan bruges til at nulstille projektilet
        /// </summary>
        public override void Load()
        {

            collidedWith = new HashSet<GameObject>();

            switch (type)
            {
                case ProjectileType.Magic:
                    currentState = new OrbitState(this, 125f);
                    break;
                default:
                    currentState = new MoveState(this);
                    break;
            }


            base.Load();

        }

        /// <summary>
        /// Sørger for kollisions-effekten, og at den kun opstår én gang pr. fjende
        /// </summary>
        /// <param name="other"></param>
        public override void OnCollision(GameObject other)
        {

            base.OnCollision(other);

            IsAlive = false;

            if (collidedWith.Contains(other))
                return;
            else
            {
                (other as Enemy).CurrentHealth -= damage;
                collidedWith.Add(other);
            }

        }

        /// <summary>
        /// Bevæger projektilet på dens vektor, og fjerner det fra update-listen hvis den kommer udenfor skærmen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {

            if (Position.X + Sprite.Width < GameWorld.Instance.Camera.Position.X - screenHalfs.X ||     //Udenfor venstre side
                Position.X - Sprite.Width > GameWorld.Instance.Camera.Position.X + screenHalfs.X ||     //Udenfor højre side
                Position.Y + Sprite.Height < GameWorld.Instance.Camera.Position.Y - screenHalfs.Y ||    //Udenfor toppen
                Position.Y - Sprite.Height > GameWorld.Instance.Camera.Position.Y + screenHalfs.Y)      //Udenfor bunden
                IsAlive = false;

            if (currentState != null)
                currentState.Execute();

            base.Update(gameTime);

        }

        /// <summary>
        /// Bevæger projektilet imod vektor'en
        /// </summary>
        /// <param name="direction">Den retning objektet skal have</param>
        public void Move(Vector2 direction)
        {

            Position += (direction * GameWorld.Instance.DeltaTime * speed);

        }

        #endregion

    }
}
