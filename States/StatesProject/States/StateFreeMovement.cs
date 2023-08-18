using States.StatesProject.GameObjects;
using System;
using System.Drawing;
using System.Threading;

namespace States.StatesProject.States
{
    public class StateFreeMovement : AbstractState
    {
        private Point targetPoint;

        public override void Init()
        {
            var distance = 50;
            targetPoint = StatesControl.Filter(Character.size, new Point(
                StatesControl.Rand.Next(Character.location.X - distance, Character.location.X + distance),
                StatesControl.Rand.Next(Character.location.Y - distance, Character.location.Y + distance)
            ));
        }

        protected override void Run()
        {
            Character.mood = GameObject.Mood.Relaxing;

            if (!Physics2D.PointTargeting(Character.location, targetPoint, Character.speed))
                Character.MoveTo(targetPoint);
            else
            {
                Character.SetState(typeof(StateRest));
            }
        }
    }
}
