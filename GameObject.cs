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

namespace MortenSurvivor
{
    public abstract class GameObject
    {

        #region Fields

        private bool isAlive = true;
        private float rotation = 0;
        protected float scale = 1f;
        protected float layer = 0.5f;
        protected Enum type;
        private Texture2D sprite;
        private Vector2 position;
        private Vector2 origin;
        protected Color drawColor = Color.White;
        protected SpriteEffects spriteEffect = SpriteEffects.None;

        #endregion
        #region Propertitties


        public bool IsAlive
        {

            get => isAlive;

            set
            {

                isAlive = value;

            }

        }


        public Texture2D Sprite { get => sprite; protected set => sprite = value; }


        public Vector2 Position { get => position; set => position = value; }


        public float Rotation { get => rotation; set => rotation = value; }

        #endregion
        #region Constructor


        public GameObject(Enum type)
        {

            this.type = type;

            try
            {
                sprite = GameWorld.Instance.Sprites[(type as Enum)][0];
                origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        #endregion
        #region Methods


        public virtual void Load()
        {



        }


        public virtual void Update(GameTime gameTime)
        {



        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Sprite, Position, null, drawColor, Rotation, origin, scale, spriteEffect, layer);

        }


        public override string ToString()
        {

            return type.ToString();

        }


        public virtual void CheckCollision(GameObject other) { }


        public virtual void OnCollision(GameObject other) { }

        #endregion

    }
}
