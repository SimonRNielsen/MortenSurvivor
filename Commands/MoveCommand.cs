using Microsoft.Xna.Framework;

namespace MortenSurvivor.Commands
{
    public class MoveCommand : ICommand
    {
        private Player player;
        private Vector2 direction;

        public MoveCommand(Player player, Vector2 direction)
        {
            this.player = player;
            this.direction = direction;
        }

        public void Execute()
        {
            if (!GameWorld.Instance.GamePaused)
            {
                player.Move(direction);
            }
        }
    }
}
