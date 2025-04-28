using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenSurvivor.Commands
{
    public class UpgradeCommand : ICommand
    {

        public void Execute()
        {
            GameWorld.Instance.ActivateMenu(MenuItem.Upgrade);
            GameWorld.Instance.Notify(StatusType.Upgrade);
        }
    }
}
