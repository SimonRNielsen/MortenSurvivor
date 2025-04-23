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
    public class Weapon
    {

        #region Fields

        private int damage;

        #endregion
        #region Properties

        public int Damage { get => damage; set => damage = value; }



        #endregion
        #region Constructor

        public Weapon(WeaponType type)
        {
            switch (type)
            {
                case WeaponType.Sling:
                    damage = 1;
                    break;
                default:
                    damage = 1;
                    break;
            }


        }

        #endregion
        #region Methods



        #endregion

    }
}
