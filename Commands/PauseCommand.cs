namespace MortenSurvivor.Commands
{
    public class PauseCommand : ICommand
    {
        public void Execute()
        {
            if(!GameWorld.Instance.GamePaused)
            {
            GameWorld.Instance.ActivateMenu(MenuItem.Pause);
            }
        }
    }
}
