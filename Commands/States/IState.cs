namespace MortenSurvivor.Commands.States
{
    public interface IState<T>
    {

        public void Enter(T parent);


        public void Execute();


        public void Exit();

    }
}
