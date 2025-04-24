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
        private Texture2D sprite;
        private float layer;
         
        #endregion
        #region Properties
        #endregion
        #region Constructor
        #endregion
        #region Methods
        public Status() 
        {
            GameWorld.Instance.Attach(this);
            this.layer = 1f;
        }

      

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameWorld.Instance.GameFont, $"XP: {xpCounter}", new Vector2(-550, -300), Color.White);
            spriteBatch.DrawString(GameWorld.Instance.GameFont, $"Kills: {enemiesKilled}", new Vector2(-550, -270), Color.White);
            spriteBatch.DrawString(GameWorld.Instance.GameFont, $"Health: {playerHealth}", new Vector2(-550, -240), Color.White);
            spriteBatch.DrawString(GameWorld.Instance.GameFont, $"Upgrades: {upgradeCount}", new Vector2(-550, -210), Color.White);
            spriteBatch.DrawString(GameWorld.Instance.GameFont, $"LvL: {levelUp}", new Vector2(-550, -180), Color.White);

            // Hent og tegn sprite
            if (GameWorld.Instance.Sprites.TryGetValue(StatusType.EnemiesKilled, out Texture2D[] sprites))
            {
                Texture2D sprite = sprites[0]; // Der er kun én sprite i array
                spriteBatch.Draw(sprite, new Vector2(-600, -270), Color.White);
            }

            //spriteBatch.Draw(SpriteLibrary.StatusSprites[StatusType.EnemiesKilled], new Vector2(-600, -270), Color.White);

            //if (statusToSpriteEnum.TryGetValue(StatusType.EnemiesKilled, out Enum enemyKey))
            //{
            //    Texture2D[] sprites = GameWorld.Instance.Sprites[enemyKey];
            //    Texture2D sprite = sprites[0]; // Tag første billede
            //    spriteBatch.Draw(sprite, new Vector2(-600, -270), Color.White);
            //}

            //spriteBatch.Draw(GameWorld.Instance.Sprites, Position, null, drawColor, Rotation, origin, scale, spriteEffect, layer);

            //Texture2D sprite = SpriteLibrary.EnemySprites[enemyType];
            //spriteBatch.Draw(sprite, Position, null, drawColor, Rotation, origin, scale, spriteEffect, layer);


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
