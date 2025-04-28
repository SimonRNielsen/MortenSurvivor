namespace MortenSurvivor.Commands
{
    public class ExitCommand : ICommand
    {
        public void Execute()
        {
            GameWorld.Instance.Exit();
        }
    }
}
