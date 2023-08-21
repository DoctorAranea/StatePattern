using States.StatesProject.States;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace States.StatesProject.GameObjects
{
    public class CreationPlayer : Creation
    {
        public CreationPlayer(StatesControl control, Type currentState) : base(control, currentState)
        {
            fraction = Fraction.Red;
            enemyFractions = new Fraction[]
            {
                Fraction.Green
            };

            stateMouseClick = typeof(StateCursorFollowing);
            stateEnemyObjectFound = typeof(StateKillEnemy);
            stateNotEnoughEnergy = typeof(StateLookingForFood);

            size = new Size(50, 50);
            color = Color.Red;
            fieldOfView = 300;
            speed = 3;
        }
    }
}
