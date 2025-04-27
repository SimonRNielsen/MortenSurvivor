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
        private int kills = 0;
        private int playerHealth;
        private int currentLVL = 1; 
        private int xpToLevelUp = 5;
        private float xpCounter = 0;
        private int upgradeCount = 0; // Will be implemented if there is time
        private float layer;
        private float elapsedTime = 0f;

        public float Layer { get => layer; set => layer = value; }
        public int Kills { get => kills; set => kills = value; }


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

        public void Update(GameTime gameTime)
        {
            if (!GameWorld.Instance.GamePaused)
            {
                elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            playerHealth = Player.Instance.CurrentHealth;

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            #region timer
            int minutes = (int)(elapsedTime / 60);
            int seconds = (int)(elapsedTime % 60);

            string timeText = $"{minutes:D2}:{seconds:D2}";
            spriteBatch.DrawString(GameWorld.Instance.GameFont, $"Tid: {timeText}", new Vector2(GameWorld.Instance.Camera.Position.X, GameWorld.Instance.Camera.Position.Y - 500), Color.White, 0f, Vector2.Zero, 0.15f, SpriteEffects.None, 1f);
            #endregion

            spriteBatch.DrawString(GameWorld.Instance.GameFont, $"LvL: {currentLVL}", new Vector2(GameWorld.Instance.Camera.Position.X - 910, GameWorld.Instance.Camera.Position.Y - 500), Color.White, 0f, Vector2.Zero, 0.15f, SpriteEffects.None, 0.8f);
            spriteBatch.DrawString(GameWorld.Instance.GameFont, $"Kills: {Kills}", new Vector2(GameWorld.Instance.Camera.Position.X - 770, GameWorld.Instance.Camera.Position.Y - 500), Color.White, 0f, Vector2.Zero, 0.15f, SpriteEffects.None, 0.8f); //new Vector2(-550, -270)
            
            //teksten skjules, bruges bare til at se om det virker
            //spriteBatch.DrawString(GameWorld.Instance.GameFont, $"Upgrades: {upgradeCount}", new Vector2(GameWorld.Instance.Camera.Position.X - 900, GameWorld.Instance.Camera.Position.Y - 120), Color.White, 0f, Vector2.Zero, 0.15f, SpriteEffects.None, 0.8f);
            //spriteBatch.DrawString(GameWorld.Instance.GameFont, $"Health: {playerHealth}", new Vector2(GameWorld.Instance.Camera.Position.X - 900, GameWorld.Instance.Camera.Position.Y - 90), Color.White, 0f, Vector2.Zero, 0.15f, SpriteEffects.None, 0.9f);
            //spriteBatch.DrawString(GameWorld.Instance.GameFont, $"XP: {xpCounter}", new Vector2(GameWorld.Instance.Camera.Position.X - 900, GameWorld.Instance.Camera.Position.Y - 50), Color.White, 0f, Vector2.Zero, 0.19f, SpriteEffects.None, 1f); //new Vector2(-550, -300)

            //spriteBatch.DrawString(GameWorld.Instance.GameFont, $"SPEED: {Player.Instance.Speed}", new Vector2(GameWorld.Instance.Camera.Position.X - 900, GameWorld.Instance.Camera.Position.Y - 30), Color.White, 0f, Vector2.Zero, 0.15f, SpriteEffects.None, 1f); //new Vector2(-550, -300)


            // Hent og tegn sprite for enemies killed
            if (GameWorld.Instance.Sprites.TryGetValue(StatusType.EnemiesKilled, out Texture2D[] sprites))
            {
                Texture2D sprite = sprites[0]; // Der er kun én sprite i array
                spriteBatch.Draw(sprite, new Vector2(GameWorld.Instance.Camera.Position.X - 820, GameWorld.Instance.Camera.Position.Y - 508), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
            }


            // Hent og tegn sprite XP bar
            if (GameWorld.Instance.Sprites.TryGetValue(StatusType.BarBottom, out Texture2D[] spritesXP))
            {
                Texture2D spriteXpBar = spritesXP[0]; // Der er kun én i dit array
                spriteBatch.Draw(spriteXpBar, new Vector2(GameWorld.Instance.Camera.Position.X - 910, GameWorld.Instance.Camera.Position.Y - 460), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
            }

            if (GameWorld.Instance.Sprites.TryGetValue(StatusType.BarViolet, out Texture2D[] spritesXP1))
            {
                Texture2D spriteXpBar1 = spritesXP1[0];

                float xpProgress = MathHelper.Clamp((float)xpCounter / xpToLevelUp, 0f, 1f); // Hvor meget fyldt

                int fullHeight = spriteXpBar1.Height;
                int fillHeight = (int)(fullHeight * xpProgress);

                Rectangle sourceRectangle = new Rectangle(
                    0,
                    fullHeight - fillHeight, // starter længere nede
                    spriteXpBar1.Width,
                    fillHeight // højden afhænger af hvor meget XP vi har
                );

                Vector2 xpBarPosition = new Vector2(
                    GameWorld.Instance.Camera.Position.X - 910,
                    (GameWorld.Instance.Camera.Position.Y - 460) + (fullHeight - fillHeight) // vi flytter den op
                );

                spriteBatch.Draw(spriteXpBar1, xpBarPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.81f);
            }


            // Hent og tegn sprite healthbar
            if (GameWorld.Instance.Sprites.TryGetValue(StatusType.HealthBottom, out Texture2D[] sprites2))
            {
                Texture2D healthSprite2 = sprites2[0]; // Der er kun én i dit array
                spriteBatch.Draw(healthSprite2, new Vector2(Player.Instance.Position.X - 60, Player.Instance.Position.Y - 80), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
            }

            //Dynamisk healthbar
            if (GameWorld.Instance.Sprites.TryGetValue(StatusType.HealthTop, out Texture2D[] fill))
            {
                Texture2D fillSprite = fill[0];

                float healthPercent = MathHelper.Clamp(playerHealth / 10f, 0f, 1f); // max HP = 100
                Rectangle sourceRectangle = new Rectangle(0, 0, (int)(fillSprite.Width * healthPercent), fillSprite.Height);
                Vector2 healthBarPosition = new Vector2(Player.Instance.Position.X - 60, Player.Instance.Position.Y - 80);

                spriteBatch.Draw(fillSprite, healthBarPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.81f);
            }


        }


        public void OnNotify(StatusType statusType)
        {
           
            switch (statusType)
            {
                case StatusType.XpUp:
                    xpCounter++;

                    XPBar();
                    //currentLVL++;
                    break;
                //case StatusType.LevelUp:
                //    break;
                case StatusType.HealthUpdate:
                    break;
                case StatusType.EnemiesKilled:
                    Kills++;
                    break;
                default:
                    break;
            }

        }


        public void XPBar()
        {

            if (xpCounter >= xpToLevelUp)
            {
                currentLVL++;
                xpCounter = 0; //nulstiller xpcounter, til næste lvl
                xpToLevelUp += 5;

            }
        }



        #endregion

    }

}
