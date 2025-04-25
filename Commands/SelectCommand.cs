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

namespace MortenSurvivor.Commands
{
    public class SelectCommand : ICommand
    {
            Random rnd = new Random();
        public void Execute()
        {
            if (!GameWorld.Instance.GamePaused)
            {
                Menu activeMenu = new Menu();
                Menu mousedOverMenu = new Menu();
                if(GameWorld.Instance.GameMenu != null)
                {
                activeMenu = GameWorld.Instance.GameMenu.Find(x => x.IsActive == true);
                }
                if(activeMenu.relatedButtons != null)
                {
                Menu mousedOverMenu = activeMenu.relatedButtons.Find(x => x.MousedOver == true);
                mousedOverMenu.SelectionEffect();
                }
                
                int randomUpgrade = rnd.Next(4, 6);
                Player.Instance.Upgrade((UpgradeType)randomUpgrade);
            }
        }
    }
}
