using States.StatesProject.GameObjects;
using System.Drawing;
using System.Linq;

namespace States.StatesProject.States
{
    public class StateFollowing : AbstractFOWState
    {
        public override string Name { get => "Преследование"; }

        protected override void Run()
        {
            Character.mood = GameObject.Mood.Relaxing;

            var visibleGameObjects = (Character as Creation).visibleObjects.Where(x => x is GameObject).ToList();

            Point targetPoint = new Point(
                (visibleGameObjects[0] as GameObject).location.X - Character.size.Width / 2,
                (visibleGameObjects[0] as GameObject).location.Y - Character.size.Height / 2
            );

            if (!Physics2D.PointTargeting(Character.location, targetPoint, Character.speed))
                Character.MoveTo(targetPoint, 1);
            else
            {
                IsActivated = false;
            }
        }
    }
}
