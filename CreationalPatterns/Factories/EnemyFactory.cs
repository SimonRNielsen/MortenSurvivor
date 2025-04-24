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
        /// <summary>
        /// Opretter en enemy med en tilfældig placering (ikke Gossifer)
        /// </summary>
        /// <returns></returns>
        public override GameObject Create()
        {
            //Enemy type udfra Enum
            int rndType = GameWorld.Instance.Random.Next(0, 4); //Ikke goosifer


            //Samler position og EnemyType til en enemy
            enemyGO = new Enemy((EnemyType)rndType, SetPosition());

            return enemyGO;
        }

        /// <summary>
        /// Opretter en Goosifer med en tilfældig placering
        /// </summary>
        /// <returns></returns>
        public GameObject CreateGoosefer()
        {
            //Samler position og EnemyType til en enemy
            enemyGO = new Enemy(EnemyType.Goosifer, SetPosition());

            return enemyGO;
        }

        /// <summary>
        /// Returnere spawn position
        /// </summary>
        /// <returns></returns>
        public Vector2 SetPosition()
        {
            //Enemies spawner kan spawne fra et tilfældigt hjørne
            int rndPosition = GameWorld.Instance.Random.Next(1, 6);

            //int rndPosition = 4;

            int yPosition = GameWorld.Instance.Random.Next((int)-GameWorld.Instance.Screensize.Y, (int)GameWorld.Instance.Screensize.Y * 2);
            int xPosition = GameWorld.Instance.Random.Next((int)-GameWorld.Instance.Screensize.X, (int)GameWorld.Instance.Screensize.X * 2);


            switch (rndPosition) //Skal ændre spawn position, når jeg ved selve størrelsen på banen
            {
                case 1: //Spawner fra et firepit
                    this.position = Vector2.Zero;
                    break;
                case 2: //Top
                    this.position = new Vector2(xPosition, -GameWorld.Instance.Screensize.Y);
                    break;
                case 3: //Bottom
                    this.position = new Vector2(xPosition, GameWorld.Instance.Screensize.Y * 2);
                    break;
                case 4: //Left
                    this.position = new Vector2(-GameWorld.Instance.Screensize.X, yPosition);
                    break;
                case 5: //Rigth
                    position = new Vector2(GameWorld.Instance.Screensize.X * 2, yPosition);
                    break;
            }

            return position;
        }

        #endregion
    }
}
