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
    public class RectangleData
    {

        #region Fields

        private Rectangle rectangle;
        private int x;
        private int y;

        #endregion
        #region Properties


        public Rectangle Rectangle { get =>  rectangle; set => rectangle = value; }


        public int X { get => x; set => x = value; }


        public int Y { get => y; set => y = value; }

        #endregion
        #region Constructor


        public RectangleData(int x, int y)
        {

            rectangle = new Rectangle();
            this.x = x;
            this.y = y;

        }

        #endregion
        #region Methods


        public void UpdatePosition(GameObject gameObject, int width, int height)
        {

            Rectangle = new Rectangle((int)gameObject.Position.X + X - width / 2, (int)gameObject.Position.Y + Y - height / 2, 1, 1);

        }

        #endregion

    }
}
