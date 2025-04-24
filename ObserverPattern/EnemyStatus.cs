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

namespace MortenSurvivor.ObserverPattern
{
    class EnemyStatus : IObserver
    {
        //Enum StatusType.EnemiesKilled

        #region Fields
        private int xpCounter = 0;
        private int enemiesKilled = 0;
        private int playerHealth = 0;
        private int upgradeCount = 0; // Will be implemented if there is time
        private int statusUI;
        #endregion
        #region Properties
        #endregion
        #region Constructor
        #endregion
        #region Methods
        public EnemyStatus()
        {
            GameWorld.Instance.Attach(this);

        }



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameWorld.GameFont, $"XP: {xpCounter}", new Vector2(-550, -300), Color.White);
            spriteBatch.DrawString(GameWorld.GameFont, $"Kills: {enemiesKilled}", new Vector2(-550, -270), Color.White);
            spriteBatch.DrawString(GameWorld.GameFont, $"Health: {playerHealth}", new Vector2(-550, -240), Color.White);
            spriteBatch.DrawString(GameWorld.GameFont, $"Upgrades: {upgradeCount}", new Vector2(-550, -210), Color.White);
        }

        public void OnNotify(StatusType status)
        {

            /*StatusType.EnemiesKilled = enemiesKilled; *///brug enemypool? factory?
            //xpCounter += 10; //brug xpcrystals værdi fra items
            //playerHealth = 10; //brug playerhealth fra player
            upgradeCount++; // brug sprite fra items?
        }

        #endregion
    }
}
