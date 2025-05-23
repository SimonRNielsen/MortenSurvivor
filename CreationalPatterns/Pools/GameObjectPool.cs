﻿using MortenSurvivor.CreationalPatterns.Factories;
using System.Collections.Generic;

namespace MortenSurvivor.CreationalPatterns.Pools
{
    public abstract class GameObjectPool
    {
        #region Field
        private List<GameObject> active = new List<GameObject>();
        private Stack<GameObject> inactive = new Stack<GameObject>();

        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Method
        public GameObject GetObject()
        {
            GameObject gameObject;

            //Hvis poolen er tom, opretter dden en ny. Hvis den ikke er tom, skal den poppe i stacken
            if (inactive.Count == 0)
            {
                gameObject = Create();
            }
            else
            {
                gameObject = inactive.Pop();

                //Resetter enemies position til yderkanten af banen
                if (gameObject is Enemy)
                {
                    gameObject.Position = new EnemyFactory().SetPosition();
                }
            }


            active.Add(gameObject);
            return gameObject;
        }

        public void ReleaseObject(GameObject gameObject)
        {
            if (gameObject is Enemy)
            {
                gameObject = new EnemyFactory().Create();
            }

            //Fjerner fra active og tilføjer den til inactive
            active.Remove(gameObject);
            inactive.Push(gameObject);

            CleanUp(gameObject);

            //Fjener fra GameWorld
            //GameWorld.Instance.Destroy(gameObject);
        }

        protected abstract GameObject Create();

        protected abstract void CleanUp(GameObject gameObject);
        #endregion
    }
}
