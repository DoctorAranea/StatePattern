using States.StatesProject.GameObjects;
using States.StatesProject.States;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace States.StatesProject
{
    public class StatesControl : Control
    {
        private PictureBox behaviour;
        private List<GameObject> gameObjects;
        private CreationPlayer player;
        private Timer timer;
        private Timer spawnTimer;

        public StatesControl()
        {
            gameObjects = new List<GameObject>();

            player = new CreationPlayer(this, typeof(StateFreeMovement));
            SpawnObject(player, new Point(FieldSize.Width / 2, FieldSize.Height / 2));
            
            for (int i = 0; i < 1; i++)
            {
                var enemy = new CreationEnemy(this, typeof(StateFreeMovement));
                SpawnObject(enemy, new Point(Rand.Next(0, FieldSize.Width), Rand.Next(0, FieldSize.Height)));
            }

            this.Resize += StatesControl_Resize;
            this.Dock = DockStyle.Fill;

            behaviour = new PictureBox();
            behaviour.Parent = this;
            behaviour.Dock = DockStyle.Fill;
            behaviour.Paint += Behaviour_Paint;
            behaviour.MouseClick += Behaviour_MouseClick;
            behaviour.MouseMove += Behaviour_MouseMove;

            timer = new Timer();
            timer.Interval = 1;
            timer.Tick += Timer_Tick;
            timer.Start();

            spawnTimer = new Timer();
            spawnTimer.Interval = 3000;
            spawnTimer.Tick += SpawnTimer_Tick;
            spawnTimer.Start();
        }

        private void SpawnTimer_Tick(object sender, EventArgs e)
        {
            var enemy = new CreationEnemy(this, typeof(StateFreeMovement));
            SpawnObject(enemy, new Point(Rand.Next(0, FieldSize.Width), Rand.Next(0, FieldSize.Height)));
        }

        private void StatesControl_Resize(object sender, EventArgs e)
        {
            FieldSize = this.Size;
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].location = Filter(gameObjects[i].size, gameObjects[i].location);
            }
        }

        public static Random Rand { get; } = new Random();
        public static Size FieldSize { get; private set; } = new Size(500, 500);
        public static Point MouseCursor { get; private set; }

        private void Behaviour_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.LightGray);

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Draw(g);
                if (gameObjects[i].direction == new Point()) continue;
                Point gameObjectCenter = new Point()
                {
                    X = gameObjects[i].location.X + gameObjects[i].size.Width / 2,
                    Y = gameObjects[i].location.Y + gameObjects[i].size.Height / 2
                };
                //g.DrawLine(new Pen(Color.Black), gameObjectCenter, new Point()
                //{
                //    X = gameObjectCenter.X + gameObjects[i].direction.X * gameObjects[i].size.Width / 5,
                //    Y = gameObjectCenter.Y + gameObjects[i].direction.Y * gameObjects[i].size.Height / 5
                //});
                int eyeDiameter = gameObjects[i].size.Width / 2;
                g.FillEllipse(new SolidBrush(Color.White), new Rectangle()
                {
                    X = gameObjectCenter.X + gameObjects[i].direction.X * gameObjects[i].size.Width / 5 - eyeDiameter / 2,
                    Y = gameObjectCenter.Y + gameObjects[i].direction.Y * gameObjects[i].size.Height / 5 - eyeDiameter / 2,
                    Width = eyeDiameter,
                    Height = eyeDiameter
                });
                eyeDiameter /= 2;
                g.FillEllipse(new SolidBrush(Color.Black), new Rectangle()
                {
                    X = gameObjectCenter.X + gameObjects[i].direction.X * gameObjects[i].size.Width / 3 - eyeDiameter / 2,
                    Y = gameObjectCenter.Y + gameObjects[i].direction.Y * gameObjects[i].size.Height / 3 - eyeDiameter / 2,
                    Width = eyeDiameter,
                    Height = eyeDiameter
                });
            }
        }

        public void SpawnObject(GameObject gameObject, Point location)
        {
            gameObject.location = Filter(gameObject.size, location);
            gameObjects.Add(gameObject);
        }

        public void DestroyObject(GameObject gameObject)
        {
            gameObject.Destroy();
            gameObjects.Remove(gameObject);
        }

        public static Point Filter(Size size, Point location)
        {
            Point endLocation = new Point();

            if (location.X < 0)
            {
                endLocation.X = 0;
            }
            else if (location.X + size.Width >= FieldSize.Width)
            {
                endLocation.X = FieldSize.Width - size.Width;
            }
            else
                endLocation.X = location.X;

            if (location.Y < 0)
            {
                endLocation.Y = 0;
            }
            else if (location.Y + size.Height >= FieldSize.Height)
            {
                endLocation.Y = FieldSize.Height - size.Height;
            }
            else
                endLocation.Y = location.Y;

            return endLocation;
        }
        
        public object[] FindGameObjectsPointsByRect(Rectangle rect)
        {
            var objects = gameObjects.Where(x => rect.Contains(new Point(x.location.X + x.size.Width / 2, x.location.Y + x.size.Height / 2))).ToList<object>();
            return objects.ToArray();
        }
        
        private void Behaviour_MouseMove(object sender, MouseEventArgs e)
        {
            MouseCursor = e.Location;
        }

        private void Behaviour_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    player.MouseClicked();
                    break;
                case MouseButtons.Right:
                    var enemy = new CreationEnemy(this, typeof(StateFreeMovement));
                    SpawnObject(enemy, new Point(e.Location.X, e.Location.Y));
                    break;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            behaviour.Invalidate();
        }
    }
}
