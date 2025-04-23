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
    public class Player : Character
    {

        #region Fields & SingleTon

        #region SingleTon

        private static Player instance;
        public static Player Instance
        {
            get
            {
                if (instance == null)
                    instance = new Player(PlayerType.UndercoverMortenWalk, GameWorld.Instance.Screensize / 2);

                return instance;
            }
        }

        #endregion
        private Weapon weapon;

        #endregion
        #region Properties



        #endregion
        #region Constructor


        private Player(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {

            velocity = Vector2.One; //Til at bevare animation indtil anden form implementeres
            layer = 1;
        }

        #endregion
        #region Methods


        public void Move(Vector2 spawnPos)
        {



        }


        public void Shoot()
        {



        }


        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime); //Skal blive for at animationen kører

        }


        public override void OnCollision(GameObject other)
        {

            base.OnCollision(other);

        }

        #endregion

    }
}
