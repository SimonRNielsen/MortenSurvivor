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

        protected Color drawColor = Color.White;
        protected SpriteEffects spriteEffect = SpriteEffects.None;
        protected Enum type;
        protected float scale = 1f;
        protected float layer = 0.5f;
        private float rotation = 0f;
        private bool isAlive = true;
        private Texture2D sprite;
        private Vector2 position;
        private Vector2 origin;

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


        public virtual Rectangle CollisionBox
        {

            get
            {

                if (sprite != null && (this is Player || this is Enemy || this is Projectile || this is Item))
                    return new Rectangle((int)(Position.X - (Sprite.Width / 2) * scale), (int)(Position.Y - (Sprite.Height / 2) * scale), (int)(Sprite.Width * scale), (int)(Sprite.Height * scale));
                else
                    return new Rectangle();

            }

        }

        #endregion
        #region Constructor


        public GameObject(Enum type, Vector2 spawnPos)
        {

            this.type = type;
            position = spawnPos;

            try
            {
                Sprite = GameWorld.Instance.Sprites[(type as Enum)][0];
                origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        #endregion
        #region Methods


        public virtual void Load() { }


        public virtual void Update(GameTime gameTime) { }


        public virtual void Draw(SpriteBatch spriteBatch)
        {

            if (sprite != null)
                spriteBatch.Draw(Sprite, Position, null, drawColor, Rotation, origin, scale, spriteEffect, layer);

        }


        public override string ToString()
        {

            return type.ToString();

        }


        public virtual void OnCollision(GameObject other) { }

        #endregion

    }
}
