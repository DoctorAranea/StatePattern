using States.StatesProject.States;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace States.StatesProject.GameObjects
{
    public abstract class Creation : GameObject
    {
        public Fraction fraction = Fraction.Null;
        public Fraction[] enemyFractions;
        public Type stateMouseClick;
        public Type stateObjectFound;
        public Type stateEnemyObjectFound;
        public int fieldOfView;
        public List<object> visibleObjects;

        public Creation(StatesControl control, Type currentState) : base(control, currentState)
        {
            size = new Size(50, 50);
            color = Color.Red;
            fieldOfView = 200;
            speed = 3;
        }

        public void MouseClicked()
        {
            SetState(stateMouseClick);
        }

        protected override void Update(object sender, EventArgs e)
        {
            CheckFieldOfView();

            if (CurrentState.IsActivated)
            {
                CurrentState.DoAction();
            }
            else
            {
                SetState(defaultState);
            }
        }

        protected void CheckFieldOfView()
        {
            if (stateMouseClick != null && CurrentState.GetType().Name == stateMouseClick.Name) return;

            Rectangle fieldOfViewRect = new Rectangle(location.X - fieldOfView, location.Y - fieldOfView, fieldOfView * 2, fieldOfView * 2);
            var objects = control.FindGameObjectsPointsByRect(fieldOfViewRect).ToList();
            if (objects.Count > 1)
            {
                objects.Remove(this as object);

                visibleObjects = objects;

                if (visibleObjects.Where(x => x is Creation && enemyFractions.Contains((x as Creation).fraction)).ToArray().Length > 0)
                    FoundEnemyObject();
                else
                    FoundObject();
            }
            else if (CurrentState is AbstractFOWState)
            {
                CurrentState.IsActivated = false;
            }
        }

        protected virtual void FoundEnemyObject()
        {
            if (stateEnemyObjectFound == null) return;

            if (CurrentState.GetType().Name != stateEnemyObjectFound.Name)
                SetState(stateEnemyObjectFound);
        }

        protected virtual void FoundObject()
        {
            if (stateObjectFound == null) return;

            if (CurrentState.GetType().Name != stateObjectFound.Name)
                SetState(stateObjectFound);
        }
    }
}
