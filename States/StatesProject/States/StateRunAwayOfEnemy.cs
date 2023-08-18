using States.StatesProject.GameObjects;
using System.Drawing;
using System.Linq;

namespace States.StatesProject.States
{
    public class StateRunAwayOfEnemy : AbstractFOWState
    {
        protected override void Run()
        {
            var visibleGameObjects = (Character as Creation).visibleObjects.Where(x => x is GameObject).ToList();
            var enemy = visibleGameObjects.FirstOrDefault(x => (Character as Creation).enemyFractions.Contains((x as Creation).fraction));
            if (enemy == null) return;

            Character.mood = GameObject.Mood.Fear;
            var enemyObj = enemy as GameObject;

            Point targetPoint = new Point(
                enemyObj.location.X - Character.size.Width / 2,
                enemyObj.location.Y - Character.size.Height / 2
            );

            if (!Physics2D.PointTargeting(Character.location, targetPoint, Character.speed))
                Character.MoveOut(targetPoint, 2);
            else
            {
                IsActivated = false;
            }
        }
    }
}
