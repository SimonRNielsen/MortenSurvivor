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

        public List<Menu> relatedButtons;
        //public Menu[] selectableUpgrades;
        private Texture2D sprite;
        private Action action;
        private Menu parent;
        private MenuItem type;
        private UpgradeType upgradeType;
        private Vector2 position;
        private Vector2 origin;
        private Color color = Color.Yellow;
        private string text;
        private float scale = 1f;
        private float rotation = 0f;
        private float layer = 0.9f;
        private bool isActive = false;
        private bool isButton = false;
        private bool isUpgrade = false;
        private bool mousedOver = false;

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


        public bool IsUpgrade { get => isUpgrade; }


        public bool MousedOver { get => mousedOver; }


        public MenuItem Type { get => type; }


        public UpgradeType UpgradeType { get => upgradeType; }


        public Vector2 Position { set => position = value; }

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
                    relatedButtons.Add(new Menu(this, MenuItem.SingleButton, "Start", true));
                    break;
                case MenuItem.Pause:
                    relatedButtons = new List<Menu>();
                    relatedButtons.Add(new Menu(this, MenuItem.SingleButton, "Continue", false));
                    break;
                case MenuItem.Win:
                case MenuItem.Loss:
                    relatedButtons = new List<Menu>();
                    relatedButtons.Add(new Menu(this, MenuItem.SingleButton, "Restart", false));
                    relatedButtons.Add(new Menu(this, MenuItem.SingleButton, "Exit", false));
                    break;
                case MenuItem.Upgrade:
                    relatedButtons = new List<Menu>();
                    isUpgrade = true;
                    break;
            }

        }


        public Menu(Menu parent, MenuItem type, string buttonText, bool activate)
        {

            sprite = GameWorld.Instance.Sprites[type][0];
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            this.parent = parent;
            this.type = type;
            text = buttonText;
            isActive = activate;

            switch (type)
            {
                case MenuItem.StackableButton:
                case MenuItem.SingleButton:
                    isButton = true;
                    layer += 0.01f;
                    break;
            }

            switch (text)
            {
                case "Exit":
                    action = () => GameWorld.Instance.Exit();
                    break;
                case "Continue":
                case "Start":
                    action = () => GameWorld.Instance.Pause();
                    break;
                case "Restart":
                    action = () => GameWorld.Instance.Restart();
                    break;
                default:
                    break;
            }

        }


        public Menu(Menu parent, MenuItem type, UpgradeType upgrade)
        {

            sprite = GameWorld.Instance.Sprites[type][0];
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            this.parent = parent;
            this.type = type;
            upgradeType = upgrade;
            text = "Select";

            switch (type)
            {
                case MenuItem.StackableButton:
                case MenuItem.SingleButton:
                    isButton = true;
                    layer += 0.01f;
                    break;
            }

            action = () => Player.Instance.Upgrade(upgradeType);

        }

        #endregion
        #region Methods


        public void Update()
        {

            if (isButton)
                mousedOver = false;
            else
                position = GameWorld.Instance.Camera.Position;

            if (relatedButtons != null)
                foreach (Menu menu in relatedButtons)
                {
                    
                    if (menu.CollisionBox.Intersects(InputHandler.Instance.MouseCollisionBox))
                        menu.OnMouseOver();

                    menu.Update();

                }

            if (isUpgrade && relatedButtons.Count == 0)
            {

                var ints = new List<int>();

                while (ints.Count < 3)
                {

                    int i = GameWorld.Instance.Random.Next(0, 5);

                    if (ints.Contains(i))
                        continue;
                    else
                        ints.Add(i);

                }

                foreach (int i in ints)
                    relatedButtons.Add(new Menu(this, MenuItem.SingleButton, (UpgradeType)i));

            }

        }


        public void Draw(SpriteBatch spriteBatch)
        {

            if (isActive)
            {

                spriteBatch.Draw(sprite, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, layer);

                if (!string.IsNullOrEmpty(text))
                    spriteBatch.DrawString(GameWorld.Instance.GameFont, text, origin, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer + 0.02f);

            }

            if (isUpgrade)
            {

                spriteBatch.Draw(GameWorld.Instance.Sprites[relatedButtons[0].UpgradeType][0], new Vector2(position.X - 500, position.Y - 300), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer + 0.02f);
                spriteBatch.Draw(GameWorld.Instance.Sprites[relatedButtons[1].UpgradeType][0], new Vector2(position.X, position.Y - 300), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer + 0.02f);
                spriteBatch.Draw(GameWorld.Instance.Sprites[relatedButtons[2].UpgradeType][0], new Vector2(position.X + 500, position.Y - 300), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer + 0.02f);
                string firstChoice = SetString(relatedButtons[0].UpgradeType);
                string secondChoice = SetString(relatedButtons[1].UpgradeType);
                string thirdChoice = SetString(relatedButtons[2].UpgradeType);
                spriteBatch.DrawString(GameWorld.Instance.GameFont, firstChoice, new Vector2(position.X - 500, position.Y - 200), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer + 0.02f);
                spriteBatch.DrawString(GameWorld.Instance.GameFont, secondChoice, new Vector2(position.X, position.Y - 200), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer + 0.02f);
                spriteBatch.DrawString(GameWorld.Instance.GameFont, thirdChoice, new Vector2(position.X + 500, position.Y - 200), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer + 0.02f);

            }

            if (relatedButtons != null)
            {

                switch (relatedButtons.Count)
                {
                    case 1:
                        relatedButtons[0].Position = new Vector2(position.X, position.Y + 350);
                        break;
                    case 2:
                        relatedButtons[0].Position = new Vector2(position.X - 300, position.Y + 350);
                        relatedButtons[1].Position = new Vector2(position.X + 300, position.Y + 350);
                        break;
                    case 3:
                        relatedButtons[0].Position = new Vector2(position.X - 500, position.Y + 350);
                        relatedButtons[1].Position = new Vector2(position.X, position.Y + 350);
                        relatedButtons[2].Position = new Vector2(position.X + 500, position.Y + 350);
                        break;
                }

                foreach (Menu button in relatedButtons)
                    button.Draw(spriteBatch);

            }

            if (isButton)
                color = Color.Yellow;

        }


        public void OnMouseOver()
        {

            color = Color.Red;

            mousedOver = true;

        }


        public void SelectionEffect()
        {

            if (isButton)
                action?.Invoke();

            if (parent.IsUpgrade)
                parent.relatedButtons.Clear();

            Deactivate();

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

            GameWorld.Instance.GameMenu.Add(new Menu(MenuItem.Start));
            GameWorld.Instance.GameMenu.Add(new Menu(MenuItem.Win));
            GameWorld.Instance.GameMenu.Add(new Menu(MenuItem.Loss));
            GameWorld.Instance.GameMenu.Add(new Menu(MenuItem.Pause));
            GameWorld.Instance.GameMenu.Add(new Menu(MenuItem.Upgrade));

        }


        private void Deactivate()
        {

            isActive = false;
            foreach (Menu menu in parent.relatedButtons)
                menu.IsActive = false;

        }


        public void Activate()
        {

            isActive = true;
            foreach (Menu menu in parent.relatedButtons)
                menu.IsActive = true;

        }


        private string SetString(UpgradeType upgrade)
        {

            string text;

            switch (upgrade)
            {
                default:
                    text = upgrade.ToString();
                    break;
            }

            return text;

        }

        #endregion

    }

}
