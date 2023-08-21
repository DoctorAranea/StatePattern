using States.StatesProject.States;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace States.StatesProject.GameObjects
{
    public abstract class GameObject
    {
        public enum Fraction
        {
            Null,
            Red,
            Green
        }

        public enum Mood
        {
            Relaxing,
            Agression,
            Fear
        }

        public Mood mood;
        public Size size = new Size(25, 25);
        public Point location = new Point(0, 0);
        public Point direction = new Point(0, 0);
        public Color color = Color.Pink;
        public int speed = 1;

        protected StatesControl control;
        protected Timer timer;
        protected AbstractState defaultState;

        protected GameObject(StatesControl control, Type defaultState)
        {
            this.control = control;
            this.defaultState = (AbstractState)Activator.CreateInstance(defaultState);
            SetState(this.defaultState);

            timer = new Timer();
            timer.Interval = 1;
            timer.Tick += Update;
            timer.Start();
        }

        public AbstractState CurrentState { get; protected set; }
        public Point Center 
        { 
            get
            {
                return new Point()
                {
                    X = location.X + size.Width / 2,
                    Y = location.Y + size.Height / 2
                };
            }
        }

        protected virtual void Update(object sender, EventArgs e)
        {
            if (CurrentState.IsActivated)
            {
                CurrentState.DoAction();
            }
            else
            {
                SetState(defaultState);
            }
        }

        public void SetState(AbstractState newState)
        {
            if (newState == null) return;

            if (CurrentState != null)
                CurrentState.IsActivated = false;
            var state = Activator.CreateInstance(newState.GetType());
            CurrentState = (AbstractState)state;
            CurrentState.Character = this;
            CurrentState.IsActivated = true;

            if (this is CreationPlayer)
                control.UpdateStatusLabel();
        }

        public void SetState(Type newStateType)
        {
            if (newStateType == null) return;

            if (CurrentState != null)
                CurrentState.IsActivated = false;
            var state = Activator.CreateInstance(newStateType);
            CurrentState = (AbstractState)state;
            CurrentState.Character = this;
            CurrentState.IsActivated = true;

            if (this is CreationPlayer)
                control.UpdateStatusLabel();
        }

        public virtual void Draw(Graphics g)
        {
            SolidBrush brush = new SolidBrush(color);
            g.FillEllipse(brush, new Rectangle(location, size));
        }

        public void MoveTo(Point targetPoint, int multiplier = 1)
        {
            Point vector = new Point(targetPoint.X - location.X, targetPoint.Y - location.Y);
            PointF normalizedVector = Physics2D.NormalizePoint(new PointF(vector.X, vector.Y));
            normalizedVector = Physics2D.RemoveUnderOnes(normalizedVector);
            location.X += (int)Math.Round(normalizedVector.X) * speed * multiplier;
            location.Y += (int)Math.Round(normalizedVector.Y) * speed * multiplier;
            direction = Physics2D.SetVector(direction, new Point()
            {
                X = (int)Math.Round(normalizedVector.X),
                Y = (int)Math.Round(normalizedVector.Y)
            });

            Point filteredPoint = StatesControl.Filter(size, location);
            location = filteredPoint;
        }

        public void MoveOut(Point targetPoint, int multiplier = 1)
        {
            Point vector = new Point(targetPoint.X - location.X, targetPoint.Y - location.Y);
            PointF normalizedVector = Physics2D.NormalizePoint(new PointF(vector.X, vector.Y));
            normalizedVector = Physics2D.RemoveUnderOnes(normalizedVector);
            location.X += (int)Math.Round(normalizedVector.X * -1) * speed * multiplier;
            location.Y += (int)Math.Round(normalizedVector.Y * -1) * speed * multiplier;
            direction = Physics2D.SetVector(direction, new Point()
            {
                X = (int)Math.Round(normalizedVector.X * -1),
                Y = (int)Math.Round(normalizedVector.Y * -1)
            });

            Point filteredPoint = StatesControl.Filter(size, location);
            location = filteredPoint;
        }

        public void Destroy()
        {
            timer.Stop();
        }
    }
}
