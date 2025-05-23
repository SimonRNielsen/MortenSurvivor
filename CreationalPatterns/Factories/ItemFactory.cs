﻿using Microsoft.Xna.Framework;
using System;

namespace MortenSurvivor.CreationalPatterns.Factories
{
    public class ItemFactory : GameObjectFactory
    {
        #region Singelton
        private static ItemFactory instance;

        public static ItemFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ItemFactory();
                }
                return instance;
            }
        }
        #endregion

        #region Fields
        private Item itemGO;


        #endregion

        #region Properties

        #endregion

        #region Constructor
        private ItemFactory()
        {
        }

        #endregion

        #region Method

        /// <summary>
        /// Spawner et tilfældig item i midten af ScreenSize/Players start position
        /// </summary>
        /// <returns></returns>
        public override GameObject Create()
        {
            //Enemy type udfra Enum
            int itemTypeLength = Enum.GetNames(typeof(ItemType)).Length;
            int rndType = GameWorld.Instance.Random.Next(0, itemTypeLength);

            //Samler position og ItemType til en enemy
            itemGO = new Item((ItemType)rndType, GameWorld.Instance.Screensize / 2);

            return itemGO;
        }

        /// <summary>
        /// Spawner et item på spawnPosition
        /// </summary>
        /// <param name="spawnPosition">Den position item skal spawne på</param>
        /// <returns></returns>
        public GameObject Create(Vector2 spawnPosition)
        {
            //Enemy type udfra Enum
            int itemTypeLength = Enum.GetNames(typeof(ItemType)).Length;
            int rndType = GameWorld.Instance.Random.Next(0, itemTypeLength);

            //Samler position og ItemType til en enemy
            itemGO = new Item((ItemType)rndType, spawnPosition);

            return itemGO;
        }

        #endregion
    }
}
