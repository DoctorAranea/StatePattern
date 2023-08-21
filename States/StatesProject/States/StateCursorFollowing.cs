using States.StatesProject.GameObjects;
using System.Drawing;

namespace States.StatesProject.States
{
    public class StateCursorFollowing : AbstractState
    {
        Point? targetPoint = null;

        public override string Name { get => "Следование за курсором"; }

        protected override void Run()
        {
            Character.mood = GameObject.Mood.Relaxing;

            if (targetPoint == null)
                targetPoint = new Point(StatesControl.MouseCursor.X - Character.size.Width / 2, StatesControl.MouseCursor.Y - Character.size.Height / 2);

            if (!Physics2D.PointTargeting(Character.location, (Point)targetPoint, Character.speed))
                Character.MoveTo((Point)targetPoint);
            else
            {
                IsActivated = false;
            }
        }
    }
}
