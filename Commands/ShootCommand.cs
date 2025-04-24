using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenSurvivor.Commands
{
    public class ShootCommand : ICommand
    {
        private Player player;

        public ShootCommand(Player player)
        {
            this.player = player;
        }
        public void Execute()
        {
            if (!GameWorld.Instance.GamePaused)
            {
                player.Shoot();
            }
        }
    }
}
