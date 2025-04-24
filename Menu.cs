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

    public class Menu
    {

        #region Fields

        private Texture2D sprite;
        private bool isActive = false;
        private bool isButton = false;
        private bool isUpgrade = false;
        private bool hasMouseOver = false;
        private MenuItem type;
        private Vector2 position;
        private Vector2 origin;
        private Color color;
        private float scale = 1f;
        private float rotation = 0f;
        private float layer = 0.9f;
        private Action action;
        private List<Menu> relatedButtons;
        private Menu parent;

        public static Menu[] selectableUpgrades = new Menu[3];

        #endregion
        #region Properties


        public Rectangle CollisionBox
        {

            get
            {

                if (sprite != null)
                    return new Rectangle((int)(position.X - (sprite.Width / 2) * scale), (int)(position.Y - (sprite.Height / 2) * scale), (int)(sprite.Width * scale), (int)(sprite.Height * scale));
                else
                    return new Rectangle();

            }

        }


        public bool IsActive { get => isActive; set => isActive = value; }


        public bool IsButton { get => isButton; }

        #endregion
        #region Constructor


        public Menu(MenuItem type)
        {

            sprite = GameWorld.Instance.Sprites[type][0];
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            this.type = type;

            switch (type)
            {
                case MenuItem.Start:
                    isActive = true;
                    relatedButtons = new List<Menu>();
                    break;
                case MenuItem.Pause:
                    relatedButtons = new List<Menu>();
                    break;
                case MenuItem.Loss:
                    relatedButtons = new List<Menu>();
                    break;
                case MenuItem.Upgrade:
                    relatedButtons = new List<Menu>();
                    isUpgrade = true;
                    break;
            }

        }


        public Menu(MenuItem type, Action action, bool upgrade)
        {

            sprite = GameWorld.Instance.Sprites[type][0];
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            this.type = type;

            switch (type)
            {
                case MenuItem.StackableButton:
                    isButton = true;
                    layer += 0.1f;
                    this.action = action;
                    isUpgrade = upgrade;
                    break;
                case MenuItem.SingleButton:
                    isButton = true;
                    layer += 0.1f;
                    this.action = action;
                    isUpgrade = upgrade;
                    break;
            }

        }

        #endregion
        #region Methods


        public void Update()
        {

            if (isUpgrade && selectableUpgrades[0] == null)
            {
                for (int i = 0; i < selectableUpgrades.Length; i++)
                {

                }
            }

            switch (type)
            {
                default:
                    position = GameWorld.Instance.Camera.Position;
                    break;
            }

        }


        public void Draw(SpriteBatch spriteBatch)
        {

            if (isActive)
                spriteBatch.Draw(sprite, position, null, color, rotation, origin, scale, SpriteEffects.None, layer);

            color = Color.White;

        }


        public void OnCollision()
        {

            if (isButton)
            {
                color = Color.Gray;
            }

        }


        public void SelectionEffect()
        {

            if (isButton)
                action?.Invoke();

            if (isUpgrade)
            {
                GameWorld.Instance.GameMenu.Remove(this);
                selectableUpgrades = new Menu[3];
            }

        }


        public override string ToString()
        {

            return type.ToString();

        }

        /// <summary>
        /// Sørger for at oprette alle relevante menu'er
        /// </summary>
        public static void CreateMenus()
        {



        }


        private void Deactivate()
        {

            isActive = false;

        }

        #endregion

    }

}
