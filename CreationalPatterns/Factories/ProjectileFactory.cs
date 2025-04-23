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
        #region Fields
        private Projectile projectileGO;
        private Vector2 position;
        private float speed = 200f;
        private int damage = 10;

        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Method
        public override GameObject Create()
        {
            this.position = Player.Instance.Position;

            //Skal have lavet det, så det passer med det rigtige projektil
            projectileGO = new Projectile(ProjectileType.Eggs, position, speed, damage);

            return projectileGO;

        }

        #endregion
    }
}
