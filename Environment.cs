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
using SharpDX.Direct3D9;

namespace MortenSurvivor
{
    public class Environment : GameObject
    {

        #region Fields
        private EnvironmentTile tileType;

        protected float speed;
        protected float elapsedTime;
        protected float fps = 7;
        //protected int health = 1;
        protected int currentHealth;
        protected int currentIndex;
        protected Vector2 velocity;
        protected Texture2D[] sprites;

        #endregion
        #region Properties



        #endregion
        #region Constructor


        public Environment(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {

            sprites = GameWorld.Instance.Sprites[type];

            layer = 0f;
           
            tileType = (EnvironmentTile)type;

        }

        #endregion
        #region Methods

        public void Tile()
        {
            switch (tileType)
            {
                case EnvironmentTile.TopLeft:
                    break;
                case EnvironmentTile.Top:
                    break;
                case EnvironmentTile.TopRight:
                    break;
                case EnvironmentTile.Left:
                    break;
                case EnvironmentTile.Center:
                    break;
                case EnvironmentTile.Right:
                    break;
                case EnvironmentTile.BottomLeft:
                    break;
                case EnvironmentTile.Bottom:
                    break;
                case EnvironmentTile.BottomRight:
                    break;
                case EnvironmentTile.AvSurface:
                    layer = 0.91f;
                    scale = 0.6f;
                    break;
                case EnvironmentTile.Room:
                    break;
                case EnvironmentTile.Firepit:
                    layer = 0.1f;
                    scale = 0.5f;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Håndterer hvilken sprite i sprites der skal illustreres
        /// </summary>
        protected virtual void Animate()
        {

            //Adding the time which has passed since the last update
            elapsedTime += GameWorld.Instance.DeltaTime;

            currentIndex = (int)(elapsedTime * fps % sprites.Length);

        }

        /// <summary>
        /// Håndterer visning af sprite(s)
        /// </summary>
        /// <param name="spriteBatch">Game-logic</param>
        public override void Draw(SpriteBatch spriteBatch)
        {

            if (sprites != null)
                spriteBatch.Draw(sprites[currentIndex], Position, null, drawColor, Rotation, origin, scale, spriteEffect, layer);
            else if (Sprite != null)
                spriteBatch.Draw(Sprite, Position, null, drawColor, Rotation, origin, scale, spriteEffect, layer);

            velocity = Vector2.Zero;

        }

        public override void Update(GameTime gameTime)
        {
            
                Animate();

            base.Update(gameTime);
        }

        #endregion

    }
}
