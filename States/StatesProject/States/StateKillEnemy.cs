using States.StatesProject.GameObjects;
using System.Drawing;
using System.Linq;

namespace States.StatesProject.States
{
    public class StateKillEnemy : AbstractFOWState
    {
        protected override void Run()
        {
            var visibleGameObjects = (Character as Creation).visibleObjects.Where(x => x is GameObject).ToList();
            var enemies = visibleGameObjects.Where(x => (Character as Creation).enemyFractions.Contains((x as Creation).fraction)).ToArray();
            if (enemies == null) return;
            var enemy = enemies[Physics2D.FindTheNearestPoint(Character.Center, enemies.Select(x => (x as GameObject).location).ToArray()).Item1] as GameObject;

            Character.mood = GameObject.Mood.Agression;
            var enemyObj = enemy;

            Point targetPoint = new Point(
                enemyObj.location.X - Character.size.Width / 2,
                enemyObj.location.Y - Character.size.Height / 2
            );

            if (!Physics2D.PointTargeting(Character.location, targetPoint, Character.speed))
                Character.MoveTo(targetPoint, 1);
            else
            {
                IsActivated = false;
            }

            Character.KillEnemy();
        }
    }
}
