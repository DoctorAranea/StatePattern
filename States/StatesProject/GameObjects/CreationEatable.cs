using States.StatesProject.States;
using System;
using System.Drawing;
using System.Linq;

namespace States.StatesProject.GameObjects
{
    public class CreationEatable : Creation
    {
        public CreationEatable(StatesControl control, Type currentState) : base(control, currentState)
        {
            fraction = Fraction.Green;

            enemyFractions = new Fraction[]
            {
                Fraction.Red
            };

            stateEnemyObjectFound = typeof(StateRunAwayOfEnemy);

            var randSize = StatesControl.Rand.Next(15, 30);
            size = new Size(randSize, randSize);
            color = Color.FromArgb(StatesControl.Rand.Next(60), StatesControl.Rand.Next(75, 150), StatesControl.Rand.Next(25));
            fieldOfView = 100;
            speed = 5;
        }
    }
}
