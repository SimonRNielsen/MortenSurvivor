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

        private int xpUp; //xpcrystal
        private int healUp; //roast goode
        private int speed; //boots
        private bool isConfused; //bible
        private bool isFleeing; //rosary
        private bool isPickedUp = false;
        private Texture2D sprite;
        private Vector2 position;



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


            switch (itemType)
            {
                case ItemType.XPCrystal:

                    break;
                case ItemType.DamageBoost:
                    
                    break;
                case ItemType.SpeedBoost:
                    
                    break;
                case ItemType.HealBoost:
                    
                    break;
                case ItemType.ConfuseEnemy:
                    
                    break;
                case ItemType.ScareEnemy:
                    
                    break;
                case ItemType.Rosary:
                    
                    break;
                default:
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (GameWorld.Instance.Sprites.TryGetValue(ItemType.ConfuseEnemy, out Texture2D[] sprites))
            {
                Texture2D sprite = sprites[0]; // Der er kun én sprite i array
                spriteBatch.Draw(sprite, new Vector2(830, - 508), null, Color.White, 0f, Vector2.Zero, 100f, SpriteEffects.None, 0.6f);


            }

            

            base.Draw(spriteBatch);
        }

        public void ItemPickup()
        {
            isPickedUp = true;
        }



    }
}
