namespace MortenSurvivor.ObserverPattern
{
    public interface IObserver
    {
        public void OnNotify(StatusType status);
    }
}
