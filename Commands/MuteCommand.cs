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
