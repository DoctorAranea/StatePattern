using States.StatesProject.States;
using System;
using System.Drawing;
using System.Linq;

namespace States.StatesProject.GameObjects
{
    public class CreationEnemy : Creation
    {
        public CreationEnemy(StatesControl control, Type currentState) : base(control, currentState)
        {
            fraction = Fraction.Green;

            enemyFractions = new Fraction[]
            {
                Fraction.Red
            };

            stateEnemyObjectFound = typeof(StateRunAwayOfEnemy);

            size = new Size(25, 25);
            color = Color.Green;
            fieldOfView = 100;
            speed = 5;
        }
    }
}
