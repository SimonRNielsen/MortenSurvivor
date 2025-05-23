﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MortenSurvivor.Commands;
using MortenSurvivor.CreationalPatterns.Factories;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MortenSurvivor
{
    public class Player : Character
    {

        #region Fields & SingleTon

        #region SingleTon

        private static Player instance;
        public static Player Instance
        {
            get
            {
                if (instance == null)
                    instance = new Player(PlayerType.UndercoverMortenWalk, GameWorld.Instance.Screensize / 2);

                return instance;
            }
        }


        #endregion
        private Weapon weapon;
        private List<Weapon> weapons = new List<Weapon>();
        private float walkTimer;
        private SoundEffect currentWalkSound;
        //private float xpCounter;
        private float originalSpeed = 200f;

        private float speedTimer;
        private bool speedBool = false;
        #endregion
        #region Properties
        //public float XpCounter { get => xpCounter; set => xpCounter = value; }



        #endregion
        #region Constructor


        private Player(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            this.fps = 15;
            velocity = Vector2.One; //Til at bevare animation indtil anden form implementeres
            this.speed = originalSpeed;
            weapon = new Weapon(WeaponType.Sling);
            weapons.Add(weapon);
            layer = 0.7f;

            health = 10; //Sæt tilbage til 10
            
        }

        #endregion
        #region Methods

        public override void Load()
        {
            weapons.Clear();
            weapons.Add(weapon);
            ProjectileFactory.Instance.Prototype = (Projectile)ProjectileFactory.Instance.ProjectileStat(ProjectileType.Eggs);
            ProjectileFactory.Instance.MagicPrototype = (Projectile)ProjectileFactory.Instance.ProjectileStat(ProjectileType.Magic);
            ProjectileFactory.Instance.GeasterEggPrototype = (Projectile)ProjectileFactory.Instance.ProjectileStat(ProjectileType.GeasterEgg);
            speed = originalSpeed;
            InputHandler.Instance.Countdown = 1f;
            base.Load();
        }

        public void Move(Vector2 velocity)
        {
            this.velocity = velocity;

            if (velocity.Y == 0)
                switch (velocity.X)
                {
                    case < 0:
                        spriteEffect = SpriteEffects.FlipHorizontally;
                        break;
                    default:
                        spriteEffect = SpriteEffects.None;
                        break;
                }

            switch (velocity)
            {
                case (1, 0) when Position.X >= 3800:
                case (-1, 0) when Position.X <= -1860:
                case (0, 1) when Position.Y >= 2110:
                case (0, -1) when Position.Y <= -1000:
                    velocity = Vector2.Zero;
                    break;
                default:
                    break;
            }

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
            Position += velocity * speed * GameWorld.Instance.DeltaTime;
            PlayWalkSound();
        }


        public void Shoot()
        {
            foreach (Weapon weapon in weapons)
            {
                GameWorld.Instance.SpawnObject(ProjectileFactory.Instance.Create(weapon.WeaponProjectile));
                GameWorld.Instance.Sounds[weapon.WeaponSoundEffect].Play();
            }

        }


        public override void Update(GameTime gameTime)
        {

            GameWorld.Instance.Camera.Position = Position;
            walkTimer += GameWorld.Instance.DeltaTime;

            //Når AddSpeed bliver kaldt skal speedTimer starte, og efter 3 sekunder bliver speed og speedTimer nulstillet igen 
            if (speedBool == true)
            {
                speedTimer += GameWorld.Instance.DeltaTime;

                if (speedTimer > 3)
                {
                    AddSpeed(-300);
                    speedBool = false;
                    speedTimer = 0;
                }
            }

            base.Update(gameTime); //Skal blive for at animationen kører

        }


        public override void OnCollision(GameObject other)
        {

            base.OnCollision(other);

        }

        /// <summary>
        /// Upgrades the player, and its weapon, with the chosen upgrade
        /// </summary>
        /// <param name="upgradeType">The chosen upgrade for the player</param>
        public void Upgrade(UpgradeType upgradeType)
        {
            switch (upgradeType)
            {
                case UpgradeType.Mitre:
                    this.speed += 50f;
                    Debug.WriteLine("Speed increased by 50");
                    break;
                case UpgradeType.PopeStaff:
                    if (!weapons.Contains(weapons.Find(x => x.Type == WeaponType.PopeStaff)))
                    {
                        weapons.Add(new Weapon(WeaponType.PopeStaff));
                        Debug.WriteLine("Popestaff added");
                    }
                    else
                    {
                        ProjectileFactory.Instance.MagicPrototype.Damage += 1;
                        Debug.WriteLine("PopeStaff damage increased by 1!");
                    }
                    break;
                case UpgradeType.GeasterEgg:
                    if (!weapons.Contains(weapons.Find(x => x.Type == WeaponType.GeasterSling)))
                    {
                        weapons.Add(new Weapon(WeaponType.GeasterSling));
                        Debug.WriteLine("GeasterSling added");
                    }
                    else
                    {
                        ProjectileFactory.Instance.GeasterEggPrototype.Damage += 1;
                        Debug.WriteLine("GeasterEgg damage increased by 1!");
                    }
                    break;
                case UpgradeType.HolyWater:
                    if (InputHandler.Instance.Countdown > 0.3f)
                    {
                        InputHandler.Instance.Countdown -= 0.1f;
                    }
                    break;
                case UpgradeType.GeesusBlood:
                    currentHealth = health;
                    break;

            }

        }

        public void PlayWalkSound()
        {
            if (walkTimer > 0.4f)
            {
                walkTimer = 0;
                if (currentWalkSound == GameWorld.Instance.Sounds[Sound.PlayerWalk2])
                {
                    GameWorld.Instance.Sounds[Sound.PlayerWalk1].Play();
                    currentWalkSound = GameWorld.Instance.Sounds[Sound.PlayerWalk1];
                }
                else
                {
                    GameWorld.Instance.Sounds[Sound.PlayerWalk2].Play();
                    currentWalkSound = GameWorld.Instance.Sounds[Sound.PlayerWalk2];
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
#if DEBUG
            //spriteBatch.DrawString(GameWorld.Instance.GameFont, $"X:{Position.X}\nY:{Position.Y}", GameWorld.Instance.Camera.Position - (GameWorld.Instance.Screensize / 8), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
#endif
        }

        /// <summary>
        /// Øger speed med speedboost
        /// </summary>
        /// <param name="speedboost"></param>
        public void AddSpeed(int speedboost)
        {
            //Starter speedTimer i Update metoden til at tælle op
            if(!speedBool || speedboost<0)
            {
            speedBool = true;
            this.speed += speedboost;
            }
            else
            {
                speedTimer = 0;
            }

        }

        #endregion

    }
}
