using System;
using System.Diagnostics;

namespace MortenSurvivor.Commands
{
    public class SelectCommand : ICommand
    {
        Random rnd = new Random();
        public void Execute()
        {
            if (GameWorld.Instance.GamePaused)
            {
                Menu activeMenu;
                if (GameWorld.Instance.GameMenu != null)
                {
                    activeMenu = GameWorld.Instance.GameMenu.Find(x => x.IsActive == true);
                    if (activeMenu != null)
                    {
                        Menu mousedOverMenu = activeMenu.relatedButtons.Find(x => x.MousedOver == true);
                        if (mousedOverMenu != null)
                        {
                            mousedOverMenu.SelectionEffect();
                        }
                        else
                        {
                            Debug.WriteLine("No button found...");
                        }
                    }
                }
            }
        }
    }
}
