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
    public class Enemy : Character
    {

        #region Fields

        private IState<Enemy> currentState;
        private int damage;
        private float damageTimer;
        private float damageGracePeriod;

        #endregion
        #region Properties


        public int Damage { get => damage; }


        public float DamageTimer { get => damageTimer; set => damageTimer = value; }


        public float DamageGracePeriod { get => damageGracePeriod; }

        #endregion
        #region Constructor


        public Enemy(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {

            switch (type) //Sprites bliver sat i Character
            {
                case EnemyType.Slow:
                    
                    break;
                case EnemyType.SlowChampion:

                    break;
                case EnemyType.Fast:

                    break;
                case EnemyType.FastChampion:

                    break;
                case EnemyType.Goosifer:

                    break;
                default:
                    break;
            }

        }

        #endregion
        #region Methods


        public override void Update(GameTime gameTime)
        {

            if (currentState != null)
                currentState.Execute();

            base.Update(gameTime); //Skal blive for at animationen kører

        }


        public void Move(Vector2 velocity)
        {



        }


        public override void OnCollision(GameObject other)
        {

            base.OnCollision(other);

        }

        #endregion

    }
}
