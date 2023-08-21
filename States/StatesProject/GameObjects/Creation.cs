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
        public Type stateNotEnoughEnergy;
        public List<object> visibleObjects;
        public int fieldOfView;
        public float energy = 100;

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

            if (CurrentState.GetType().Name == defaultState.GetType().Name && energy < 50)
            {
                if (stateNotEnoughEnergy != null)
                    SetState(stateNotEnoughEnergy);
            }

            if (CurrentState.IsActivated)
            {
                CurrentState.DoAction();
            }
            else
            {
                SetState(defaultState);
            }

            AddEnergy(-.05f);
        }

        public void AddEnergy(float value)
        {
            if (energy + value > 100)
                energy = 100;
            else if (energy + value < 0)
                energy = 0;
            else
                energy += value;
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
            if (energy > 75) return;

            if (CurrentState.GetType().Name != stateEnemyObjectFound.Name)
                SetState(stateEnemyObjectFound);
        }

        protected virtual void FoundObject()
        {
            if (stateObjectFound == null) return;

            if (CurrentState.GetType().Name != stateObjectFound.Name)
                SetState(stateObjectFound);
        }

        public void KillEnemy()
        {
            Rectangle body = new Rectangle(location, size);
            var objects = control.FindGameObjectsPointsByRect(body);
            foreach (var obj in objects.Where(x => x is CreationEatable))
            {
                control.DestroyObject(obj as GameObject);
                AddEnergy((obj as CreationEatable).size.Width / 2);
            }
        }
    }
}
