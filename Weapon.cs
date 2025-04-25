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
using System.Runtime.Intrinsics.Arm;

namespace MortenSurvivor
{
    public class Weapon
    {

        #region Fields

        private int damage;
        private WeaponType type;
        private ProjectileType weaponProjectile;
        private Sound weaponSoundEffect;

        #endregion
        #region Properties
        public WeaponType Type { get => type; }
        public ProjectileType WeaponProjectile { get => weaponProjectile; set => weaponProjectile = value; }

        public Sound WeaponSoundEffect { get => weaponSoundEffect; private set => weaponSoundEffect = value; }




        #endregion
        #region Constructor

        public Weapon(WeaponType type)
        {
            this.type = type;

            switch (type)
            {
                case WeaponType.Sling:
                    weaponProjectile = ProjectileType.Eggs;
                    WeaponSoundEffect = Sound.PlayerShoot;
                    break;
                case WeaponType.GeasterSling:
                    weaponProjectile = ProjectileType.GeasterEgg;
                    WeaponSoundEffect = Sound.PlayerShoot;
                    break;
                case WeaponType.PopeStaff:
                    weaponProjectile = ProjectileType.Magic;
                    WeaponSoundEffect = Sound.MagicShoot;
                    break;
                default:
                    weaponProjectile = ProjectileType.Eggs;
                    WeaponSoundEffect = Sound.PlayerShoot;
                    break;
            }

        }


        #endregion
        #region Methods



        #endregion

    }
}
