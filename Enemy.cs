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
    public class Enemy : Character
    {
        #region Fields

        private IState<Enemy> currentState;
        private IState<Enemy> originalState;
        private int damage;
        private float damageTimer;
        private float damageGracePeriod = 2f;
        private Color originalColor;

        #endregion

        #region Properties

        public int Damage { get => damage; }


        public float DamageTimer { get => damageTimer; set => damageTimer = value; }


        public float DamageGracePeriod { get => damageGracePeriod; }

        /// <summary>
        /// Bruges til at ændre CurrentState fra ConfusedState og FleeState
        /// </summary>
        public IState<Enemy> CurrentState { set => currentState = value; }

        /// <summary>
        /// Gemmer den originale ChaseState for optimisering
        /// </summary>
        public IState<Enemy> OriginalState { get => originalState; }

        /// <summary>
        /// Bruges til at ændre farven for visuel representation af effekter
        /// </summary>
        public Color DrawColor { get => drawColor; set => drawColor = value; }

        /// <summary>
        /// Returnerer originalColor
        /// </summary>
        public Color OriginalColor { get => originalColor; }

        #endregion

        #region Constructor

        /// <summary>
        /// Laver en instans af en fjende og ændrer på attributter ud fra hvilken type det er. Sprites sættes i base-konstruktørerne (Sprites i Character, Sprite i GameObject)
        /// </summary>
        /// <param name="type">Enum af tyoeb EnemyType</param>
        /// <param name="spawnPos">Startposition for fjenden</param>
        public Enemy(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {

            originalState = new ChaseState(this);
            currentState = originalState;

            Types(type);

            drawColor = originalColor;
        }

        #endregion
        #region Methods

        public void Types(Enum type)
        {
            damage = 1;
            
            switch (type)
            {
                case EnemyType.Slow:
                    speed = 125f;
                    originalColor = Color.White;
                    health = 1;
                    break;
                case EnemyType.SlowChampion:
                    speed = 125f;
                    originalColor = Color.SlateGray;
                    health = 2;
                    break;
                case EnemyType.Fast:
                    speed = 200f;
                    originalColor = Color.White;
                    health = 1;
                    break;
                case EnemyType.FastChampion:
                    speed = 200f;
                    originalColor = Color.SlateGray;
                    health = 2;
                    break;
                case EnemyType.Goosifer:
                    speed = 165f;
                    originalColor = Color.White;
                    health = 8;
                    damage = 3;
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Kører State's "Execute" metode og base.Update der står for animation
        /// </summary>
        /// <param name="gameTime">Game Logic</param>
        public override void Update(GameTime gameTime)
        {

            if (currentState != null)
                currentState.Execute();

            //damageTimer til OnCollision
            damageTimer += GameWorld.Instance.DeltaTime;
            
            base.Update(gameTime);

        }

        /// <summary>
        /// Står for at bevæge fjenden alt efter hvilken Vector den modtager, håndterer også SpriteEffects ud fra det
        /// </summary>
        /// <param name="direction">Retningen fjenden skal bevæge sig</param>
        public void Move(Vector2 direction)
        {

            velocity = direction; //Fortæller draw om fjenden har bevæget sig

            if (Vector2.Distance(Position, Player.Instance.Position) < 10)
                velocity = Vector2.Zero; //Fortæller draw at den ikke skal animere fjenden
            else
            {
                Position += (direction * GameWorld.Instance.DeltaTime * speed);
                switch (direction.X)
                {
                    case > 0:
                        spriteEffect = SpriteEffects.FlipHorizontally;
                        break;
                    default:
                        spriteEffect = SpriteEffects.None;
                        break;
                }
            }

        }


        public override void Load()
        {

            base.Load();

        }


        public override void OnCollision(GameObject other)
        {

            if (damageTimer > damageGracePeriod)
            {
                //Player tager sakde
                Player.Instance.CurrentHealth = Player.Instance.CurrentHealth - damage;

                Debug.WriteLine(Player.Instance.CurrentHealth);

                //Player tager skade sounds
                GameWorld.Instance.Sounds[Sound.PlayerTakeDamage].Play();

                //Enemy tager en skade, når de angriber Player
                CurrentHealth--;

                damageTimer = 0;
            }

            base.OnCollision(other);

        }

        #endregion

    }
}
