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
    public class Status : IObserver
    {
        #region Fields
        private int xpCounter = 0;
        private int enemiesKilled = 0;
        private int playerHealth = 0;
        private int levelUp = 1;
        private int upgradeCount = 0; // Will be implemented if there is time
         
        #endregion
        #region Properties
        #endregion
        #region Constructor
        #endregion
        #region Methods
        public Status() 
        {
            GameWorld.Instance.Attach(this);
           
        }

      

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameWorld.GameFont, $"XP: {xpCounter}", new Vector2(-550, -300), Color.White);
            spriteBatch.DrawString(GameWorld.GameFont, $"Kills: {enemiesKilled}", new Vector2(-550, -270), Color.White);
            spriteBatch.DrawString(GameWorld.GameFont, $"Health: {playerHealth}", new Vector2(-550, -240), Color.White);
            spriteBatch.DrawString(GameWorld.GameFont, $"Upgrades: {upgradeCount}", new Vector2(-550, -210), Color.White);
            spriteBatch.DrawString(GameWorld.GameFont, $"LvL: {levelUp}", new Vector2(-550, -180), Color.White);
            //spriteBatch.Draw(, Position, null, drawColor, Rotation, origin, scale, spriteEffect, layer);
        }

        public void OnNotify(StatusType statusType)
        {

            switch (statusType)
            {
                case StatusType.XpUp:
                    XPBar();
                    break;
                case StatusType.LevelUp:
                    levelUp++;
                    break;
                case StatusType.HealthUpdate:
                    HealthBar();
                    break;
                case StatusType.EnemiesKilled:
                    
                    enemiesKilled += 1000;
                    break;
                default:
                    break;
            }

        }

        public void HealthBar()
        { 
            
        }

        public void XPBar()
        {   
            
        }



        #endregion

    } 

}
