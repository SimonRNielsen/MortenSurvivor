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
    public abstract class Character : GameObject
    {

        #region Fields

        protected float speed;
        protected float elapsedTime;
        protected float fps = 6;
        protected int health = 1;
        protected int currentHealth;
        protected int currentIndex;
        protected Vector2 velocity;
        protected Texture2D[] sprites;

        #endregion
        #region Properties

        /// <summary>
        /// Håndterer automatisk at objektet bliver fjernet efter at nå 0 eller under i liv
        /// </summary>
        public int CurrentHealth
        {
            get => currentHealth;
            set
            {

                currentHealth = value;

                if (currentHealth <= 0)
                    IsAlive = false;

            }
        }


        public float Speed { get => speed; }


        public Vector2 Velocity { get => velocity; }

        #endregion
        #region Constructor

        /// <summary>
        /// Sætter automatisk sprites
        /// </summary>
        /// <param name="type">Bruges til at angive hvilke sprites der skal vises for objektet</param>
        /// <param name="spawnPos">Angiver startposition for objektet</param>
        /// <exception cref="Exception">Giver fejlmeddelelse hvis det ikke lykkedes at finde sprites</exception>
        protected Character(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {

            try
            {
                sprites = GameWorld.Instance.Sprites[type];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        #endregion
        #region Methods

        /// <summary>
        /// Står for at nulstille/klargøre objektets primære parametre
        /// </summary>
        public override void Load()
        {

            CurrentHealth = health;

            base.Load();

        }

        /// <summary>
        /// Står for animeringsprocessen og generel opdaterings-logik
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {

            Animate();

            base.Update(gameTime);

        }

        /// <summary>
        /// Håndterer hvilken sprite i sprites der skal illustreres
        /// </summary>
        protected virtual void Animate()
        {

            //Restart the animation
            if (currentIndex >= sprites.Length - 1)
            {
                currentIndex = 0;
            }

            //Adding the time which has passed since the last update
            elapsedTime += GameWorld.Instance.DeltaTime;

            currentIndex = (int)(elapsedTime * fps % sprites.Length);

        }

        /// <summary>
        /// Håndterer visning af sprite(s)
        /// </summary>
        /// <param name="spriteBatch">Game-logic</param>
        public override void Draw(SpriteBatch spriteBatch)
        {

            if (sprites != null)
                spriteBatch.Draw(sprites[currentIndex], Position, null, drawColor, Rotation, origin, scale, spriteEffect, layer);
            else if (Sprite != null)
                spriteBatch.Draw(Sprite, Position, null, drawColor, Rotation, origin, scale, spriteEffect, layer);

        }

        #endregion

    }
}
