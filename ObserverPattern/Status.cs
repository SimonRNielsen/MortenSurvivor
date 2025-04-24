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
        private float elapsedTime = 0f;

        public Texture2D Sprite { get => sprite; set => sprite = value; }
        public float Layer { get => layer; set => layer = value; }

        #endregion
        #region Properties
        #endregion
        #region Constructor
        #endregion
        #region Methods
        public Status() 
        {
            this.Layer = 1f;
            GameWorld.Instance.Attach(this);

        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        

        public void Draw(SpriteBatch spriteBatch)
        {
            //timer
            int minutes = (int)(elapsedTime / 60);
            int seconds = (int)(elapsedTime % 60);

            string timeText = $"{minutes:D2}:{seconds:D2}";
            spriteBatch.DrawString(GameWorld.Instance.GameFont, $"Tid: {timeText}", new Vector2(GameWorld.Instance.Camera.Position.X, GameWorld.Instance.Camera.Position.Y - 500), Color.White, 0f, Vector2.Zero, 0.15f, SpriteEffects.None, 1f);


            spriteBatch.DrawString(GameWorld.Instance.GameFont, $"LvL: {levelUp}", new Vector2(GameWorld.Instance.Camera.Position.X - 910, GameWorld.Instance.Camera.Position.Y - 500), Color.White, 0f, Vector2.Zero, 0.15f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(GameWorld.Instance.GameFont, $"Kills: {enemiesKilled}", new Vector2(GameWorld.Instance.Camera.Position.X - 780, GameWorld.Instance.Camera.Position.Y - 500), Color.White, 0f, Vector2.Zero, 0.15f, SpriteEffects.None, 1f); //new Vector2(-550, -270)
            
            //teksten skjules, bruges bare til at se om det virker
            spriteBatch.DrawString(GameWorld.Instance.GameFont, $"Upgrades: {upgradeCount}", new Vector2(GameWorld.Instance.Camera.Position.X - 900, GameWorld.Instance.Camera.Position.Y - 120), Color.White, 0f, Vector2.Zero, 0.15f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(GameWorld.Instance.GameFont, $"Health: {playerHealth}", new Vector2(GameWorld.Instance.Camera.Position.X - 900, GameWorld.Instance.Camera.Position.Y - 90), Color.White, 0f, Vector2.Zero, 0.15f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(GameWorld.Instance.GameFont, $"XP: {xpCounter}", new Vector2(GameWorld.Instance.Camera.Position.X - 900, GameWorld.Instance.Camera.Position.Y - 30), Color.White, 0f, Vector2.Zero, 0.15f, SpriteEffects.None, 1f); //new Vector2(-550, -300)

            // Hent og tegn sprite for enemies killed
            if (GameWorld.Instance.Sprites.TryGetValue(StatusType.EnemiesKilled, out Texture2D[] sprites))
            {
                Texture2D sprite = sprites[0]; // Der er kun én sprite i array
                spriteBatch.Draw(sprite, new Vector2(GameWorld.Instance.Camera.Position.X - 830, GameWorld.Instance.Camera.Position.Y - 508), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }

            // Hent og tegn sprite 
            if (GameWorld.Instance.Sprites.TryGetValue(StatusType.HealthUpdate, out Texture2D[] spritesHealth))
            {
                Texture2D sprite = sprites[0]; // Der er kun én i dit array
                spriteBatch.Draw(sprite, new Vector2(-600, -270), Color.White);
            }

            // Hent og tegn sprite
            if (GameWorld.Instance.Sprites.TryGetValue(StatusType.XpUp, out Texture2D[] spritesXP))
            {
                Texture2D sprite = sprites[0]; // Der er kun én i dit array
                spriteBatch.Draw(sprite, new Vector2(-600, -270), Color.White);
            }


            
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
