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

namespace MortenSurvivor.CreationalPatterns.Factories
{
    public class ProjectileFactory : GameObjectFactory
    {
        #region Singleton
        private static ProjectileFactory instance;

        public static ProjectileFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProjectileFactory();
                }
                return instance;
            }
        }
        #endregion

        #region Fields
        private GameObject prototype;
        private float speed;
        private int damage;


        #endregion

        #region Properties

        #endregion

        #region Constructor
        public ProjectileFactory()
        {
            //Skal have lavet det, så det passer med det rigtige projektil
            prototype = ProjectileStat();
        }

        #endregion

        #region Method
        /// <summary>
        /// Opretter en klone af en projectil
        /// </summary>
        /// <returns></returns>
        public override GameObject Create()
        {
            return (Projectile)prototype.Clone();
        }


        public GameObject ProjectileStat()
        {
            int rdn = GameWorld.Instance.Random.Next(0,3);

            switch (rdn)
            {
                case 0:
                    speed = 200f;
                    damage = 2;
                    break;
                case 1:
                    speed = 500f;
                    damage = 7;
                    break;
                case 2:
                    speed = 700f;
                    damage = 10;
                    break;
            }

            return new Projectile((ProjectileType)rdn, Player.Instance.Position, speed, damage);
        }

        #endregion
    }
}
