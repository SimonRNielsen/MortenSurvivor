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
    public class Item : GameObject
    {
        #region Fields
        private int xpUp; //xpcrystal
        private int healUp; //roast goode
        private int speed; //boots
        private bool isConfused; //bible
        private bool isFleeing; //rosary
        private bool isSpeed = false;
        private Texture2D sprite;
        private Vector2 position;
        private float speedTimer;

        #endregion

        #region Properties

        #endregion

        #region Constructor

        public Item(ItemType itemType, Vector2 spawnPos) : base(itemType, spawnPos)
        {
            this.type = itemType;
            position = spawnPos;

            if (GameWorld.Instance.Sprites.TryGetValue(itemType, out var sprites))
                Sprite = sprites[0];
            else
                Debug.WriteLine("Kunne ikke sætte sprite for " + ToString());

            if (sprite != null)
                origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);


            //switch (itemType)
            //{
            //    case ItemType.XPCrystal:

            //        break;
            //    case ItemType.DamageBoost:

            //        break;
            //    case ItemType.SpeedBoost:

            //        break;
            //    case ItemType.HealBoost:

            //        break;
            //    case ItemType.ConfuseEnemy:
            //        foreach (Enemy enemy in GameWorld.Instance.GameObjects)
            //        {
            //            enemy.CurrentState = new ConfusedState(enemy,5f);
            //        }



            //        break;
            //    case ItemType.ScareEnemy:

            //        break;
            //    case ItemType.Rosary:

            //        break;
            //    default:
            //        break;
            //}
        }

        #endregion

        #region Method

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {

                switch (this.type)
                {
                    case ItemType.XPCrystal:
                        //Player.Instance.
                        break;

                    case ItemType.SpeedBoost:
                        Player.Instance.AddSpeed(300);
                        //isSpeed = true;
                        //speedTimer += GameWorld.Instance.DeltaTime;
                        //Speedy();
                        break;

                    case ItemType.HealBoost:
                        Player.Instance.CurrentHealth += 3;
                        break;

                    case ItemType.Bible:
                        foreach (GameObject go in GameWorld.Instance.GameObjects)
                        {
                            if (go is Enemy)
                            {
                                (go as Enemy).CurrentState = new ConfusedState((go as Enemy), 5f);
                            }
                        }
                        break;

                    case ItemType.Rosary:
                        foreach (GameObject go in GameWorld.Instance.GameObjects)
                        {
                            if (go is Enemy)
                            {
                                (go as Enemy).CurrentState = new FleeState((go as Enemy), 5f);
                            }
                        }
                        break;

                }


                //Item dør, når Player samler den op
                IsAlive = false;
            }

            //base.OnCollision(other);
        }

        public override void Update(GameTime gameTime)
        {
            if (isSpeed == true)
            {

                
                
            }
            base.Update(gameTime);
        }


        public void Speedy()
        {

            if (speedTimer > 5f)
            {
                Player.Instance.AddSpeed(-300);
                speedTimer = 0;

            }

            isSpeed = false;


        }


        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    if (GameWorld.Instance.Sprites.TryGetValue(ItemType.ConfuseEnemy, out Texture2D[] sprites))
        //    {
        //        Texture2D sprite = sprites[0]; // Der er kun én sprite i array
        //        spriteBatch.Draw(sprite, new Vector2(830, - 508), null, Color.White, 0f, Vector2.Zero, 100f, SpriteEffects.None, 0.6f);


        //    }






        //    base.Draw(spriteBatch);
        //}


        #endregion
    }
}
