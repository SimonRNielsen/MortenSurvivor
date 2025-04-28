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
    public abstract class Character : GameObject
    {

        #region Fields

        protected float speed;
        protected float elapsedTime;
        protected float fps = 10;
        protected int health = 1;
        protected int currentHealth;
        protected int currentIndex;
        protected Vector2 velocity;
        protected Texture2D[] sprites;
        protected List<RectangleData> rectangles;

        #endregion
        #region Properties

        /// <summary>
        /// Håndterer automatisk at objektet bliver fjernet efter at nå 0 eller under i liv
        /// </summary>
        public int CurrentHealth
        {
            get => currentHealth;
            set
            {

                if (value > health)
                    value = health;

                currentHealth = value;


                if (currentHealth <= 0)
                {
                    IsAlive = false;
                    if (this is Enemy)
                    {
                        GameWorld.Instance.Sounds[Sound.EnemyHonk].Play();
                        //Notifies Status about when an enemy is killed
                        GameWorld.Instance.Notify(StatusType.EnemiesKilled);
                        GameWorld.Instance.Notify(StatusType.XpUp); //fjernes

                    }

                    if (this is Player)
                    {
                        GameWorld.Instance.ActivateMenu(MenuItem.Loss);
                        GameWorld.Instance.Notify(StatusType.PlayerDead);
                    }

                }

                if (currentHealth != value)
                {
                    currentHealth = value;
                    GameWorld.Instance.Notify(StatusType.HealthUpdate);
                }

            }

        }



        public float Speed { get => speed; }


        public List<RectangleData> Rectangles { get => rectangles; }


        public Vector2 Velocity { get => velocity; }

        #endregion
        #region Constructor

        /// <summary>
        /// Sætter automatisk sprites
        /// </summary>
        /// <param name="type">Bruges til at angive hvilke sprites der skal vises for objektet</param>
        /// <param name="spawnPos">Angiver startposition for objektet</param>
        protected Character(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {

            if (GameWorld.Instance.Sprites.TryGetValue(type, out sprites)) { }
            else
                Debug.WriteLine("Kunne ikke sætte sprite for " + ToString());

        }

        #endregion
        #region Methods

        /// <summary>
        /// Står for at nulstille/klargøre objektets primære parametre
        /// </summary>
        public override void Load()
        {

            CurrentHealth = health;

            rectangles = CreateRectangles();

            base.Load();

        }

        /// <summary>
        /// Står for animeringsprocessen og generel opdaterings-logik
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {

            if (velocity != Vector2.Zero)
                Animate();

            UpdatePixelCollider();

            base.Update(gameTime);

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


        private List<RectangleData> CreateRectangles()
        {

            List<RectangleData> rectangleList = new List<RectangleData>();
            List<Color[]> lines = new List<Color[]>();

            for (int i = 0; i < Sprite.Height; i++)
            {

                Color[] colors = new Color[Sprite.Width];
                Sprite.GetData(0, new Rectangle(0, i,
                    Sprite.Width, 1), colors, 0,
                    Sprite.Width);
                lines.Add(colors);

            }

            for (int y = 0; y < lines.Count; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {

                    if (lines[y][x].A != 0)
                        if ((x == 0) ||
                            (x == lines[y].Length) ||
                            (x > 0 && lines[y][x - 1].A == 0) ||
                            (x < lines[y].Length - 1 && lines[y][x + 1].A == 0) ||
                            (y == 0) || (y > 0 && lines[y - 1][x].A == 0) ||
                            (y < lines.Count - 1 && lines[y + 1][x].A == 0))
                        {

                            RectangleData rd = new RectangleData(x, y);

                            rectangleList.Add(rd);

                        }

                }
            }

            return rectangleList;

        }


        private void UpdatePixelCollider()
        {

            foreach (RectangleData rectangleData in Rectangles)
                rectangleData.UpdatePosition(this, Sprite.Width, Sprite.Height);

        }

        #endregion

    }
}
