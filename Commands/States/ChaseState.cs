using Microsoft.Xna.Framework;

namespace MortenSurvivor.Commands.States
{


    public class ChaseState : IState<Enemy>
    {

        private readonly Enemy parent;

        /// <summary>
        /// Sætter stadiet for en fjende til at jagte Morten
        /// </summary>
        /// <param name="parent">Fjenden der skal manipuleres med</param>
        public ChaseState(Enemy parent)
        {

            this.parent = parent;

        }

        /// <summary>
        /// Anvendes ikke
        /// </summary>
        /// <param name="parent"></param>
        public void Enter(Enemy parent)
        {
            


        }

        /// <summary>
        /// Bevæger fjenden mod Morten
        /// </summary>
        public void Execute()
        {

            Vector2 direction = Player.Instance.Position - parent.Position;
            direction.Normalize();
            parent.Move(direction);

        }

        /// <summary>
        /// Anvendes ikke
        /// </summary>
        public void Exit()
        {
            


        }

    }
}
