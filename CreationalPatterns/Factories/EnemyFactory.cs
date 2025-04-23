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
    public class EnemyFactory : GameObjectFactory
    {
        #region Fields
        private Enemy enemyGO;
        private Vector2 position;

        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Method
        public override GameObject Create()
        {
            //Enemies spawner kan spawne fra et tilfældigt hjørne
            int rndPosition = GameWorld.Instance.Random.Next(1, 5); //4 hjørner

            switch (rndPosition) //Skal ændre spawn position, når jeg ved selve størrelsen på banen
            {
                case 1:
                    this.position = Vector2.Zero;
                    break;
                case 2:
                    this.position = new Vector2(0, GameWorld.Instance.Screensize.Y);
                    break;
                case 3:
                    this.position = new Vector2(GameWorld.Instance.Screensize.X, 0);
                    break;
                case 4:
                    this.position = new Vector2(GameWorld.Instance.Screensize.X, GameWorld.Instance.Screensize.Y);
                    break;
            }

            //Enemy type udfra Enum
            int rndType = GameWorld.Instance.Random.Next(0, 5); //4 hjørner;


            //Samler position og EnemyType til en enemy
            enemyGO = new Enemy((EnemyType)rndType, position);

            return enemyGO;
        }

        #endregion
    }
}
