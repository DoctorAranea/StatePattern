using System;
using System.Threading;

namespace States.StatesProject.States
{
    public class StateRest : AbstractState
    {
        public override string Name { get => "Отдых"; }

        private bool isWaiting;

        protected override void Run()
        {
            if (isWaiting) return;

            isWaiting = true;
            new Thread(() =>
            {
                int time = StatesControl.Rand.Next(0, 5);
                DateTime last = DateTime.Now;
                while (IsActivated)
                {
                    if ((DateTime.Now.ToLocalTime() - last).TotalSeconds >= time)
                    {
                        break;
                    }
                    Thread.Sleep(1);
                }
                IsActivated = false;
            }).Start();

        }
    }
}
