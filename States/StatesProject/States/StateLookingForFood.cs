using States.StatesProject.GameObjects;
using System;
using System.Drawing;
using System.Threading;

namespace States.StatesProject.States
{
    public class StateLookingForFood : AbstractState
    {
        public override string Name { get => "Поиск еды"; }

        private Point targetPoint;

        public override void Init()
        {
            targetPoint = StatesControl.Filter(Character.size, new Point(
                StatesControl.Rand.Next(StatesControl.FieldSize.Width),
                StatesControl.Rand.Next(StatesControl.FieldSize.Height)
            ));
        }

        protected override void Run()
        {
            Character.mood = GameObject.Mood.Fear;

            if (!Physics2D.PointTargeting(Character.location, targetPoint, Character.speed))
                Character.MoveTo(targetPoint);
            else
                Init();
        }
    }
}
