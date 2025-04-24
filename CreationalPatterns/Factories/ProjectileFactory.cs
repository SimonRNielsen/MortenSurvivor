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
        private GameObject magicPrototype;
        private GameObject geasterEggPrototype;
        private float speed;
        private int damage;


        #endregion

        #region Properties
        public GameObject Prototype { get => prototype; set => prototype = value; }
        public GameObject MagicPrototype { get => magicPrototype; set => magicPrototype = value; }
        public GameObject GeasterEggPrototype { get => geasterEggPrototype; set => geasterEggPrototype = value; }

        #endregion

        #region Constructor
        public ProjectileFactory()
        {
            //Skal have lavet det, så det passer med det rigtige projektil
            Prototype = ProjectileStat(ProjectileType.Eggs);
            MagicPrototype = ProjectileStat(ProjectileType.Magic);
            GeasterEggPrototype = ProjectileStat(ProjectileType.GeasterEgg);
        }

        #endregion

        #region Method
        /// <summary>
        /// Opretter en klone af en projectil
        /// </summary>
        /// <returns></returns>
        public override GameObject Create()
        {
            Prototype.Position = Player.Instance.Position;
            return (Projectile)Prototype.Clone();
        }
        public GameObject Create(ProjectileType projectileType)
        {
            Prototype.Position = Player.Instance.Position;
            MagicPrototype.Position = Player.Instance.Position;
            GeasterEggPrototype.Position = Player.Instance.Position;
            switch (projectileType)
            {
                case ProjectileType.Eggs:
                    return (Projectile)Prototype.Clone();
                case ProjectileType.GeasterEgg:
                    return (Projectile)GeasterEggPrototype.Clone();
                case ProjectileType.Magic:
                    return (Projectile)MagicPrototype.Clone();
                default:
                    return (Projectile)Prototype.Clone();

            }
        }


        public GameObject ProjectileStat(ProjectileType projectile)
        {
            int rdn = GameWorld.Instance.Random.Next(0, 3);

            switch (projectile)
            {
                case ProjectileType.Eggs:
                    speed = 200f;
                    damage = 1;
                    break;
                case ProjectileType.GeasterEgg:
                    speed = 500f;
                    damage = 3;
                    break;
                case ProjectileType.Magic:
                    speed = 700f;
                    damage = 5;
                    break;
            }

            return new Projectile(projectile, Player.Instance.Position, speed, damage);
        }

        #endregion
    }
}
