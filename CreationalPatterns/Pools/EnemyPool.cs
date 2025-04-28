using MortenSurvivor.CreationalPatterns.Factories;

namespace MortenSurvivor.CreationalPatterns.Pools
{
    public class EnemyPool : GameObjectPool
    {
        #region Singelton
        private static EnemyPool instance;

        public static EnemyPool Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnemyPool();
                }
                return instance;
            }
        }
        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Method
        /// <summary>
        /// Opretter et GameObject (Enemy) via EnemyFactory
        /// </summary>
        /// <returns></returns>
        protected override GameObject Create()
        {
            GameObject gameObject = new EnemyFactory().Create();
            return gameObject;
        }

        /// <summary>
        /// Opretter et GameObject (Enemy - Gossifer) via EnemyFactory
        /// </summary>
        /// <returns></returns>
        public GameObject CreateGoosifer()
        {
            GameObject gameObject = new EnemyFactory().CreateGoosefer();
            return gameObject;
        }

        protected override void CleanUp(GameObject gameObject)
        {

        }

        #endregion
    }
}
