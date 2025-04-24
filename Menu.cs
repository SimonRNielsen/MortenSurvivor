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

    public class Menu
    {

        private Texture2D sprite;
        private bool active = false;
        private MenuItem type;
        private Vector2 position;
        private Vector2 origin;
        private float scale = 1f;
        private float rotation = 0f;
        private float layer = 0.7f;


        public Menu(MenuItem type)
        {

            sprite = GameWorld.Instance.Sprites[type][0];
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            this.type = type;

        }


        public void Update()
        {

            switch (type)
            {
                default:
                    position = GameWorld.Instance.Camera.Position;
                    break;
            }

        }


        public void Draw(SpriteBatch spriteBatch)
        {

            if (active)
                spriteBatch.Draw(sprite, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, layer);

        }


        public override string ToString()
        {
            return type.ToString();
        }

    }

}
