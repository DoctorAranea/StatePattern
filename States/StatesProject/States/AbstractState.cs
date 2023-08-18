using States.StatesProject.GameObjects;

namespace States.StatesProject.States
{
    public abstract class AbstractState
    {
        public bool IsActivated { get; set; }
        public GameObject Character { get; set; }

        private bool IsInitialized = false;

        public virtual void Init() { }

        public void DoAction()
        {
            if (!IsInitialized)
            {
                Init();
                IsInitialized = true;
            }
            Run();
        }

        protected abstract void Run();
    }
}
