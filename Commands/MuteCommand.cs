using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenSurvivor.Commands
{
    class MuteCommand : ICommand
    {
        public void Execute()
        {
            GameWorld.Instance.Mute();
        }
    }
}
