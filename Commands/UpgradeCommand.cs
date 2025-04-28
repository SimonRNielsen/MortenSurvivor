namespace MortenSurvivor.Commands
{
    public class UpgradeCommand : ICommand
    {

        public void Execute()
        {
            GameWorld.Instance.ActivateMenu(MenuItem.Upgrade);
            //GameWorld.Instance.Notify(StatusType.Upgrade);
        }
    }
}
